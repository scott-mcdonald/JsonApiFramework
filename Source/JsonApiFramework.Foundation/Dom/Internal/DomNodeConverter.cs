// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Dom.Internal;
using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.Dom.Internal
{
    /// <summary>
    /// Base class for all JSON.Net converters for derived DomNode objects.
    /// </summary>
    internal abstract class DomNodeConverter : JsonConverter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override void WriteJson(JsonWriter jsonWriter, object value, JsonSerializer jsonSerializer)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(value != null);
            Contract.Requires(jsonSerializer != null);

            if (value == null)
                return;

            var domJsonSerializerSettings = this.DomJsonSerializerSettings;

            var domWriteable = (IDomWriteable)value;
            domWriteable.WriteJson(jsonWriter, jsonSerializer, domJsonSerializerSettings);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomNodeConverter(DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(domJsonSerializerSettings != null);

            this.DomJsonSerializerSettings = domJsonSerializerSettings;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DomJsonSerializerSettings DomJsonSerializerSettings { get; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Factory Methods
        protected static IDomArray CreateDomArray(DomDeserializationContext domDeserializationContext, JArray jArray)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jArray == null)
                return null;

            var index = 0;
            var domItems = jArray
                .Select(jToken =>
                {
                    var jTokenType = jToken.Type;
                    switch (jTokenType)
                    {
                        case JTokenType.Null:
                        {
                            var domItem = new DomItem(index++);
                            return domItem;
                        }

                        case JTokenType.Boolean:
                        case JTokenType.Bytes:
                        case JTokenType.Date:
                        case JTokenType.Float:
                        case JTokenType.Integer:
                        case JTokenType.String:
                        {
                            var jValue = (JValue)jToken;
                            var domValue = (DomNode)CreateDomValue(domDeserializationContext, jValue);
                            var domItem = new DomItem(index++, domValue);
                            return domItem;
                        }

                        case JTokenType.Object:
                        {
                            var jObject = (JObject)jToken;
                            var domObject = (DomNode)CreateDomObject(domDeserializationContext, jObject);
                            var domItem = new DomItem(index++, domObject);
                            return domItem;
                        }

                        default:
                        {
                            var jsonPointer = jToken.GetJsonPointer();
                            domDeserializationContext.AddJsonArrayItemError(jsonPointer);

                            var domItem = new DomItem(index++);
                            return domItem;
                        }
                    }
                });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        protected static IDomDocument CreateDomDocument(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var apiDocumentType = jObject.GetApiDocumentType(domDeserializationContext);
            domDeserializationContext.SetApiDocumentType(apiDocumentType);

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomDocumentPropertyConverterDictionary, Keywords.Document);
            var domDocument = new DomDocument(apiDocumentType, domProperties);
            return domDocument;
        }

        protected static IDomError CreateDomError(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomErrorPropertyConverterDictionary, Keywords.Error);
            var domError = new DomError(domProperties);
            return domError;
        }

        protected static IDomJsonApi CreateDomJsonApi(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomJsonApiPropertyConverterDictionary, Keywords.JsonApi);
            var domJsonApi = new DomJsonApi(domProperties);
            return domJsonApi;
        }

        protected static IDomLink CreateDomLink(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomLinkPropertyConverterDictionary, Keywords.Link);
            var domLink = new DomLink(domProperties);
            return domLink;
        }

        protected static IDomLink CreateDomLink(DomDeserializationContext domDeserializationContext, JValue jValue)
        {
            Contract.Requires(domDeserializationContext != null);

            var hRef = (string)jValue;
            var domValue = CreateDomValue(domDeserializationContext, hRef);
            var domProperty = new DomProperty(ApiPropertyType.HRef, Keywords.HRef, (DomNode)domValue);
            var domLink = new DomLink(domProperty);
            return domLink;
        }

        protected static IDomLinks CreateDomLinks(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                    {
                        var apiPropertyName = jProperty.Name;
                        var apiPropertyJToken = jProperty.Value;
                        var apiPropertyJTokenType = apiPropertyJToken.Type;

                        switch (apiPropertyJTokenType)
                        {
                            case JTokenType.Null:
                            {
                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName);
                                return domProperty;
                            }

                            case JTokenType.String:
                            {
                                var apiPropertyJValue = (JValue)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(domDeserializationContext, apiPropertyJValue);
                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(domDeserializationContext, apiPropertyJObject);
                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            default:
                            {
                                var jsonPointer = apiPropertyJToken.GetJsonPointer();
                                domDeserializationContext.AddJsonApiLinkError(jsonPointer);

                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName);
                                return domProperty;
                            }
                        }
                    });

            var domLinks = new DomLinks(domProperties);
            return domLinks;
        }

        protected static IDomObject CreateDomObject(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                    {
                        var apiPropertyName = jProperty.Name;
                        var apiPropertyJToken = jProperty.Value;
                        var apiPropertyJTokenType = apiPropertyJToken.Type;

                        switch (apiPropertyJTokenType)
                        {
                            case JTokenType.Null:
                            {
                                var domProperty = new DomProperty(apiPropertyName);
                                return domProperty;
                            }

                            case JTokenType.Boolean:
                            case JTokenType.Bytes:
                            case JTokenType.Date:
                            case JTokenType.Float:
                            case JTokenType.Integer:
                            case JTokenType.String:
                            {
                                var apiPropertyJValue = (JValue)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomValue(domDeserializationContext, apiPropertyJValue);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomObject(domDeserializationContext, apiPropertyJObject);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Array:
                            {
                                var apiPropertyJArray = (JArray)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomArray(domDeserializationContext, apiPropertyJArray);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            default:
                            {
                                var jsonPointer = apiPropertyJToken.GetJsonPointer();
                                domDeserializationContext.AddJsonObjectMemberError(apiPropertyName, jsonPointer);

                                var domProperty = new DomProperty(apiPropertyName);
                                return domProperty;
                            }
                        }
                    });

            var domObject = new DomObject(domProperties);
            return domObject;
        }

        protected static IDomRelationship CreateDomRelationship(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var apiRelationshipType = jObject.GetApiRelationshipType();
            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomRelationshipPropertyConverterDictionary, Keywords.Relationship);
            var domRelationship = new DomRelationship(apiRelationshipType, domProperties);
            return domRelationship;
        }

        protected static IDomRelationships CreateDomRelationships(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                {
                    var apiPropertyName = jProperty.Name;
                    var apiPropertyJToken = jProperty.Value;
                    var apiPropertyJTokenType = apiPropertyJToken.Type;

                    switch (apiPropertyJTokenType)
                    {
                        case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName);
                            return domProperty;
                        }

                        case JTokenType.Object:
                        {
                            var apiPropertyJObject = (JObject)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomRelationship(domDeserializationContext, apiPropertyJObject);
                            var domProperty = new DomProperty(ApiPropertyType.Relationship, apiPropertyName, domPropertyValue);
                            return domProperty;
                        }

                        default:
                        {
                            var jsonPointer = apiPropertyJToken.GetJsonPointer();
                            domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Relationship);

                            var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName);
                            return domProperty;
                        }
                    }
                });

            var domRelationships = new DomRelationships(domProperties);
            return domRelationships;
        }

        protected static IDomData CreateDomData(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomDataPropertyConverterDictionary, Keywords.Data);
            var domData = new DomData(domProperties);
            return domData;
        }

        protected static IDomResourceIdentifier CreateDomResourceIdentifier(DomDeserializationContext domDeserializationContext, JObject jObject)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domDeserializationContext, jObject, JPropertyToDomResourceIdentifierPropertyConverterDictionary, Keywords.ResourceIdentifier);
            var domResourceIdentifier = new DomResourceIdentifier(domProperties);
            return domResourceIdentifier;
        }

        protected static IDomValue CreateDomValue(DomDeserializationContext domDeserializationContext, JValue jValue)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jValue == null)
                return null;

            var clrUnderlyingValue = jValue.Value;
            var clrUnderlyingValueType = clrUnderlyingValue.GetType();

            if (JValueToDomValueConverterDictionary.TryGetValue(clrUnderlyingValueType, out var jValueToDomValueConverter))
            {
                var domValue = jValueToDomValueConverter(jValue);
                if (domValue != null)
                    return domValue;
            }

            var message = "No JValueToDomValueConverter found for JValue underlying type [name={0}], add to static initialization of class {1}.".FormatWith(clrUnderlyingValueType, typeof(DomNodeConverter).Name);
            throw new InvalidOperationException(message);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IDomArray CreateDomDataArray(DomDeserializationContext domDeserializationContext, JArray jArray)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jArray == null)
                return null;

            var index = 0;
            var domItems = jArray
                .Select(jToken =>
                    {
                        var jTokenType = jToken.Type;
                        switch (jTokenType)
                        {
                            case JTokenType.Object:
                            {
                                var jObject = (JObject)jToken;
                                var domObject = (DomNode)CreateDomData(domDeserializationContext, jObject);
                                var domItem = new DomItem(index++, domObject);
                                return domItem;
                            }

                            default:
                            {
                                var jsonPointer = jToken.GetJsonPointer();
                                domDeserializationContext.AddJsonApiArrayItemError(jsonPointer);

                                var domItem = new DomItem(index++);
                                return domItem;
                            }
                        }
                    });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IDomArray CreateDomErrorArray(DomDeserializationContext domDeserializationContext, JArray jArray)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jArray == null)
                return null;

            var index = 0;
            var domItems = jArray
                .Select(jToken =>
                    {
                        var jTokenType = jToken.Type;
                        switch (jTokenType)
                        {
                            case JTokenType.Object:
                            {
                                var jObject = (JObject)jToken;
                                var domObject = (DomNode)CreateDomError(domDeserializationContext, jObject);
                                var domItem = new DomItem(index++, domObject);
                                return domItem;
                            }

                            default:
                            {
                                var jsonPointer = jToken.GetJsonPointer();
                                domDeserializationContext.AddJsonApiArrayItemError(jsonPointer);

                                var domItem = new DomItem(index++);
                                return domItem;
                            }
                        }
                    });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IEnumerable<DomProperty> CreateDomProperties(DomDeserializationContext domDeserializationContext, JObject jObject, IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> jPropertyToDomPropertyConverterDictionary, string apiObjectType)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jObject != null);
            Contract.Requires(jPropertyToDomPropertyConverterDictionary != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var jPropertyCount = jObject.Properties().Count();
            var domProperties = new List<DomProperty>(jPropertyCount);
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();

                if (jPropertyToDomPropertyConverterDictionary.TryGetValue(apiPropertyName, out var jPropertyToDomPropertyConverter))
                {
                    // Known property
                    var domProperty = jPropertyToDomPropertyConverter(domDeserializationContext, jProperty);
                    if (domProperty != null)
                    {
                        domProperties.Add(domProperty);
                    }
                    continue;
                }

                // Unknown property
                var jsonPointer = jProperty.GetJsonPointer();
                domDeserializationContext.AddJsonApiObjectMemberError(jsonPointer, apiPropertyName, apiObjectType);
            }

            return domProperties;
        }

        private static IDomArray CreateDomResourceIdentifierArray(DomDeserializationContext domDeserializationContext, JArray jArray)
        {
            Contract.Requires(domDeserializationContext != null);

            if (jArray == null)
                return null;

            var index = 0;
            var domItems = jArray
                .Select(jToken =>
                {
                    var jTokenType = jToken.Type;
                    switch (jTokenType)
                    {
                        case JTokenType.Object:
                        {
                            var jObject = (JObject)jToken;
                            var domObject = (DomNode)CreateDomResourceIdentifier(domDeserializationContext, jObject);
                            var domItem = new DomItem(index++, domObject);
                            return domItem;
                        }

                        default:
                        {
                            var jsonPointer = jToken.GetJsonPointer();
                            domDeserializationContext.AddJsonApiArrayItemError(jsonPointer);

                            var domItem = new DomItem(index++);
                            return domItem;
                        }
                    }
                });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IDomValue CreateDomValue<TValue>(DomDeserializationContext domDeserializationContext, TValue clrValue)
        {
            Contract.Requires(domDeserializationContext != null);

            return new DomValue<TValue>(clrValue);
        }

        private static DomProperty JPropertyToDomStringPropertyConverter(DomDeserializationContext domDeserializationContext, JProperty jProperty, ApiPropertyType apiPropertyType, string apiPropertyName)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(apiPropertyType, apiPropertyName);
                    return domProperty;
                }

                case JTokenType.String:
                {
                    var apiPropertyJValue = (JValue)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomValue(domDeserializationContext, apiPropertyJValue);
                    var domProperty = new DomProperty(apiPropertyType, apiPropertyName, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiStringError(jsonPointer, apiPropertyName);

                    var domProperty = new DomProperty(apiPropertyType, apiPropertyName);
                    return domProperty;
                }
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region JPropertyToDomPropertyConverters
        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> AttributesJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Attributes, Keywords.Attributes);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomObject(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Attributes, Keywords.Attributes, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Attributes);

                    var domProperty = new DomProperty(ApiPropertyType.Attributes, Keywords.Attributes);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> CodeJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Code, Keywords.Code);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> DetailJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Detail, Keywords.Detail);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> DocumentDataJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomData(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data, domPropertyValue);
                    return domProperty;
                }

                case JTokenType.Array:
                {
                    var apiPropertyJArray = (JArray)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomDataArray(domDeserializationContext, apiPropertyJArray);
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiDataError(jsonPointer);

                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> ErrorsJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Errors, Keywords.Errors, new DomArray());
                    return domProperty;
                }

                case JTokenType.Array:
                {
                    var apiPropertyJArray = (JArray)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomErrorArray(domDeserializationContext, apiPropertyJArray);
                    var domProperty = new DomProperty(ApiPropertyType.Errors, Keywords.Errors, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiErrorsError(jsonPointer);

                    var domProperty = new DomProperty(ApiPropertyType.Errors, Keywords.Errors, new DomArray());
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> HRefJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.HRef, Keywords.HRef);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> IdJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Id, Keywords.Id);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> IncludedDataJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Included, Keywords.Included, new DomArray());
                    return domProperty;
                }

                case JTokenType.Array:
                {
                    var apiPropertyJArray = (JArray)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomDataArray(domDeserializationContext, apiPropertyJArray);
                    var domProperty = new DomProperty(ApiPropertyType.Included, Keywords.Included, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiIncludedError(jsonPointer);

                    var domProperty = new DomProperty(ApiPropertyType.Errors, Keywords.Errors, new DomArray());
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> JsonApiJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.JsonApi, Keywords.JsonApi);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomJsonApi(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.JsonApi, Keywords.JsonApi, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.JsonApi);

                    var domProperty = new DomProperty(ApiPropertyType.Errors, Keywords.Errors, new DomArray());
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> LinksJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Links, Keywords.Links);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomLinks(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Links, Keywords.Links, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Links);

                    var domProperty = new DomProperty(ApiPropertyType.Links, Keywords.Links);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> MetaJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Meta, Keywords.Meta);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomObject(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Meta, Keywords.Meta, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Meta);

                    var domProperty = new DomProperty(ApiPropertyType.Meta, Keywords.Meta);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> RelationshipDataJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomResourceIdentifier(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data, domPropertyValue);
                    return domProperty;
                }

                case JTokenType.Array:
                {
                    var apiPropertyJArray = (JArray)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomResourceIdentifierArray(domDeserializationContext, apiPropertyJArray);
                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiDataError(jsonPointer);

                    var domProperty = new DomProperty(ApiPropertyType.Data, Keywords.Data);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> RelationshipsJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Relationships, Keywords.Relationships);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomRelationships(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Relationships, Keywords.Relationships, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Relationships);

                    var domProperty = new DomProperty(ApiPropertyType.Relationships, Keywords.Relationships);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> SourceJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) =>
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(jProperty != null);

            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                {
                    var domProperty = new DomProperty(ApiPropertyType.Source, Keywords.Source);
                    return domProperty;
                }

                case JTokenType.Object:
                {
                    var apiPropertyJObject = (JObject)apiPropertyJToken;
                    var domPropertyValue = (DomNode)CreateDomObject(domDeserializationContext, apiPropertyJObject);
                    var domProperty = new DomProperty(ApiPropertyType.Source, Keywords.Source, domPropertyValue);
                    return domProperty;
                }

                default:
                {
                    var jsonPointer = jProperty.GetJsonPointer();
                    domDeserializationContext.AddJsonApiObjectError(jsonPointer, Keywords.Source);

                    var domProperty = new DomProperty(ApiPropertyType.Source, Keywords.Source);
                    return domProperty;
                }
            }
        };

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> StatusJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Status, Keywords.Status);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> TitleJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Title, Keywords.Title);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> TypeJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Type, Keywords.Type);

        private static readonly Func<DomDeserializationContext, JProperty, DomProperty> VersionJPropertyToDomPropertyConverter = (domDeserializationContext, jProperty) => JPropertyToDomStringPropertyConverter(domDeserializationContext, jProperty, ApiPropertyType.Version, Keywords.Version);
        #endregion

        #region JPropertyToDomDataPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomDataPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.Type, TypeJPropertyToDomPropertyConverter },
                { Keywords.Id, IdJPropertyToDomPropertyConverter },
                { Keywords.Attributes, AttributesJPropertyToDomPropertyConverter },
                { Keywords.Relationships, RelationshipsJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomDocumentPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomDocumentPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.JsonApi, JsonApiJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Data, DocumentDataJPropertyToDomPropertyConverter },
                { Keywords.Included, IncludedDataJPropertyToDomPropertyConverter },
                { Keywords.Errors, ErrorsJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomErrorPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomErrorPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.Id, IdJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Status, StatusJPropertyToDomPropertyConverter },
                { Keywords.Code, CodeJPropertyToDomPropertyConverter },
                { Keywords.Title, TitleJPropertyToDomPropertyConverter },
                { Keywords.Detail, DetailJPropertyToDomPropertyConverter },
                { Keywords.Source, SourceJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomJsonApiPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomJsonApiPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.Version, VersionJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomLinkPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomLinkPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.HRef, HRefJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomRelationshipPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomRelationshipPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
            {
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Data, RelationshipDataJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomResourceIdentifierPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>> JPropertyToDomResourceIdentifierPropertyConverterDictionary = new Dictionary<string, Func<DomDeserializationContext, JProperty, DomProperty>>
        {
            {Keywords.Type, TypeJPropertyToDomPropertyConverter},
            {Keywords.Id, IdJPropertyToDomPropertyConverter},
            {Keywords.Meta, MetaJPropertyToDomPropertyConverter},
        };
        #endregion

        #region JValueToDomValueConverterDictionary
        private static readonly IReadOnlyDictionary<Type, Func<JValue, IDomValue>> JValueToDomValueConverterDictionary = new Dictionary<Type, Func<JValue, IDomValue>>
        {
            {
                typeof(bool), (jValue) =>
                {
                    var clrValue = (bool)jValue.Value;
                    var domValue = new DomValue<bool>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(DateTime), (jValue) =>
                {
                    var clrValue = (DateTime)jValue.Value;
                    var domValue = new DomValue<DateTime>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(DateTimeOffset), (jValue) =>
                {
                    var clrValue = (DateTimeOffset)jValue.Value;
                    var domValue = new DomValue<DateTimeOffset>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(decimal), (jValue) =>
                {
                    var clrValue = (decimal)jValue.Value;
                    var domValue = new DomValue<decimal>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(double), (jValue) =>
                {
                    var clrValue = (double)jValue.Value;
                    var domValue = new DomValue<double>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(long), (jValue) =>
                {
                    var clrValue = (long)jValue.Value;
                    var domValue = new DomValue<long>(clrValue);
                    return domValue;
                }
            },

            {
                typeof(string), (jValue) =>
                {
                    var clrValue = (string)jValue.Value;
                    var domValue = new DomValue<string>(clrValue);
                    return domValue;
                }
            },
        };
        #endregion
    }
}