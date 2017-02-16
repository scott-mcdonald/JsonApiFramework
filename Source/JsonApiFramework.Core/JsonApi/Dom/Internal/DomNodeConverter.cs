// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Properties;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
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
        #region Methods
        protected static IDomArray CreateDomArray(DomReadJsonContext domReadJsonContext, JArray jArray)
        {
            Contract.Requires(domReadJsonContext != null);

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
                                    var domValue = (DomNode)CreateDomValue(domReadJsonContext, jValue);
                                    var domItem = new DomItem(index++, domValue);
                                    return domItem;
                                }

                            case JTokenType.Object:
                                {
                                    var jObject = (JObject)jToken;
                                    var domObject = (DomNode)CreateDomObject(domReadJsonContext, jObject);
                                    var domItem = new DomItem(index++, domObject);
                                    return domItem;
                                }

                            default:
                                {
                                    var json = jToken.ToString();
                                    var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                    var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidArrayItemJsonDetail.FormatWith(json);
                                    throw new JsonApiException(title, detail);
                                }
                        }

                    });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        protected static IDomDocument CreateDomDocument(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var apiDocumentType = jObject.GetApiDocumentType();
            domReadJsonContext.SetApiDocumentType(apiDocumentType);

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomDocumentPropertyConverterDictionary);
            var domDocument = new DomDocument(apiDocumentType, domProperties);
            return domDocument;
        }

        protected static IDomError CreateDomError(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomErrorPropertyConverterDictionary);
            var domError = new DomError(domProperties);
            return domError;
        }

        protected static IDomJsonApi CreateDomJsonApi(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomJsonApiPropertyConverterDictionary);
            var domJsonApi = new DomJsonApi(domProperties);
            return domJsonApi;
        }

        protected static IDomLink CreateDomLink(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomLinkPropertyConverterDictionary);
            var domLink = new DomLink(domProperties);
            return domLink;
        }

        protected static IDomLink CreateDomLink(DomReadJsonContext domReadJsonContext, JValue jValue)
        {
            Contract.Requires(domReadJsonContext != null);

            var hRef = (string)jValue;
            var domValue = CreateDomValue(domReadJsonContext, hRef);
            var domProperty = new DomProperty(PropertyType.HRef, Keywords.HRef, (DomNode)domValue);
            var domLink = new DomLink(domProperty);
            return domLink;
        }

        protected static IDomLinks CreateDomLinks(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

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
                        case JTokenType.String:
                            {
                                var apiPropertyJValue = (JValue)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(domReadJsonContext, apiPropertyJValue);
                                var domProperty = new DomProperty(PropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                        case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(domReadJsonContext, apiPropertyJObject);
                                var domProperty = new DomProperty(PropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }
                    }

                    var json = jObject.ToString();
                    var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                    var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidPropertyJsonDetail.FormatWith(apiPropertyName, json);
                    throw new JsonApiException(title, detail);
                });

            var domLinks = new DomLinks(domProperties);
            return domLinks;
        }

        protected static IDomObject CreateDomObject(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

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
                                var domPropertyValue = (DomNode)CreateDomValue(domReadJsonContext, apiPropertyJValue);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomObject(domReadJsonContext, apiPropertyJObject);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Array:
                            {
                                var apiPropertyJArray = (JArray)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomArray(domReadJsonContext, apiPropertyJArray);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }
                        }

                        var json = jObject.ToString();
                        var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                        var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidPropertyJsonDetail.FormatWith(apiPropertyName, json);
                        throw new JsonApiException(title, detail);
                    });

            var domObject = new DomObject(domProperties);
            return domObject;
        }

        protected static IDomRelationship CreateDomRelationship(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var apiRelationshipType = jObject.GetApiRelationshipType();
            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomRelationshipPropertyConverterDictionary);
            var domRelationship = new DomRelationship(apiRelationshipType, domProperties);
            return domRelationship;
        }

        protected static IDomRelationships CreateDomRelationships(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

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
                        case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomRelationship(domReadJsonContext, apiPropertyJObject);
                                var domProperty = new DomProperty(PropertyType.Relationship, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }
                    }

                    var json = jObject.ToString();
                    var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                    var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidPropertyJsonDetail.FormatWith(apiPropertyName, json);
                    throw new JsonApiException(title, detail);
                });

            var domRelationships = new DomRelationships(domProperties);
            return domRelationships;
        }

        protected static IDomResource CreateDomResource(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomResourcePropertyConverterDictionary);
            var domResource = new DomResource(domProperties);
            return domResource;
        }

        protected static IDomResourceIdentifier CreateDomResourceIdentifier(DomReadJsonContext domReadJsonContext, JObject jObject)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(domReadJsonContext, jObject, JPropertyToDomResourceIdentifierPropertyConverterDictionary);
            var domResourceIdentifier = new DomResourceIdentifier(domProperties);
            return domResourceIdentifier;
        }

        protected static IDomValue CreateDomValue(DomReadJsonContext domReadJsonContext, JValue jValue)
        {
            Contract.Requires(domReadJsonContext != null);

            if (jValue == null)
                return null;

            var clrUnderlyingValue = jValue.Value;
            var clrUnderlyingValueType = clrUnderlyingValue.GetType();

            Func<JValue, IDomValue> jValueToDomValueConverter;
            if (JValueToDomValueConverterDictionary.TryGetValue(clrUnderlyingValueType, out jValueToDomValueConverter))
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
        private static IDomArray CreateDomErrorArray(DomReadJsonContext domReadJsonContext, JArray jArray)
        {
            Contract.Requires(domReadJsonContext != null);

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
                                var domObject = (DomNode)CreateDomError(domReadJsonContext, jObject);
                                var domItem = new DomItem(index++, domObject);
                                return domItem;
                            }

                        default:
                            {
                                var json = jToken.ToString();
                                var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidArrayItemJsonDetail.FormatWith(json);
                                throw new JsonApiException(title, detail);
                            }
                    }
                });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IEnumerable<DomProperty> CreateDomProperties(DomReadJsonContext domReadJsonContext, JObject jObject, IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> jPropertyToDomPropertyConverterDictionary)
        {
            Contract.Requires(domReadJsonContext != null);
            Contract.Requires(jObject != null);
            Contract.Requires(jPropertyToDomPropertyConverterDictionary != null);

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                {
                    var apiPropertyName = jProperty.GetApiPropertyName();

                    Func<DomReadJsonContext, JProperty, DomProperty> jPropertyToDomPropertyConverter;
                    if (jPropertyToDomPropertyConverterDictionary.TryGetValue(apiPropertyName, out jPropertyToDomPropertyConverter))
                    {
                        var domProperty = jPropertyToDomPropertyConverter(domReadJsonContext, jProperty);
                        if (domProperty != null)
                            return domProperty;
                    }

                    var json = jObject.ToString();
                    var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                    var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidPropertyJsonDetail.FormatWith(apiPropertyName, json);
                    throw new JsonApiException(title, detail);
                });

            return domProperties;
        }

        private static IDomArray CreateDomResourceArray(DomReadJsonContext domReadJsonContext, JArray jArray)
        {
            Contract.Requires(domReadJsonContext != null);

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
                                var domObject = (DomNode)CreateDomResource(domReadJsonContext, jObject);
                                var domItem = new DomItem(index++, domObject);
                                return domItem;
                            }

                        default:
                            {
                                var json = jToken.ToString();
                                var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidArrayItemJsonDetail.FormatWith(json);
                                throw new JsonApiException(title, detail);
                            }
                    }
                });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IDomArray CreateDomResourceIdentifierArray(DomReadJsonContext domReadJsonContext, JArray jArray)
        {
            Contract.Requires(domReadJsonContext != null);

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
                                var domObject = (DomNode)CreateDomResourceIdentifier(domReadJsonContext, jObject);
                                var domItem = new DomItem(index++, domObject);
                                return domItem;
                            }

                        default:
                            {
                                var json = jToken.ToString();
                                var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidArrayItemJsonDetail.FormatWith(json);
                                throw new JsonApiException(title, detail);
                            }
                    }
                });

            var domArray = new DomArray(domItems);
            return domArray;
        }

        private static IDomValue CreateDomValue<TValue>(DomReadJsonContext domReadJsonContext, TValue clrValue)
        {
            Contract.Requires(domReadJsonContext != null);

            return new DomValue<TValue>(clrValue);
        }

        private static DomProperty JPropertyToDomStringPropertyConverter(DomReadJsonContext domReadJsonContext, JProperty jProperty, PropertyType apiPropertyType, string apiPropertyName)
        {
            Contract.Requires(domReadJsonContext != null);
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
                        var domPropertyValue = (DomNode)CreateDomValue(domReadJsonContext, apiPropertyJValue);
                        var domProperty = new DomProperty(apiPropertyType, apiPropertyName, domPropertyValue);
                        return domProperty;
                    }
            }

            return null;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region JPropertyToDomPropertyConverters
        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> AttributesJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Attributes, Keywords.Attributes);
                        return domProperty;
                    }

                    case JTokenType.Object:
                    {
                        var apiPropertyJObject = (JObject)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomObject(domReadJsonContext, apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Attributes, Keywords.Attributes, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> CodeJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Code, Keywords.Code);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> DetailJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Detail, Keywords.Detail);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> DocumentDataJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiDocumentType = domReadJsonContext.ApiDocumentType;
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data);
                            return domProperty;
                        }

                    case JTokenType.Object:
                        {
                            var apiPropertyJObject = (JObject)apiPropertyJToken;
                            switch (apiDocumentType)
                            {
                                case DocumentType.ResourceDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResource(domReadJsonContext, apiPropertyJObject);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                case DocumentType.ResourceIdentifierDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResourceIdentifier(domReadJsonContext, apiPropertyJObject);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                default:
                                    {
                                        var json = apiPropertyJToken.ToString();
                                        var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(json);
                                        throw new JsonApiException(title, detail);
                                    }
                            }
                        }

                    case JTokenType.Array:
                        {
                            var apiPropertyJArray = (JArray)apiPropertyJToken;
                            switch (apiDocumentType)
                            {
                                case DocumentType.EmptyDocument:
                                    {
                                        var domPropertyValue = new DomArray();
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                case DocumentType.ResourceCollectionDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResourceArray(domReadJsonContext, apiPropertyJArray);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                case DocumentType.ResourceIdentifierCollectionDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResourceIdentifierArray(domReadJsonContext, apiPropertyJArray);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                default:
                                    {
                                        var json = apiPropertyJToken.ToString();
                                        var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(json);
                                        throw new JsonApiException(title, detail);
                                    }
                            }
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> ErrorsJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Errors, Keywords.Errors, new DomArray());
                            return domProperty;
                        }

                    case JTokenType.Array:
                        {
                            var apiPropertyJArray = (JArray)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomErrorArray(domReadJsonContext, apiPropertyJArray);
                            var domProperty = new DomProperty(PropertyType.Errors, Keywords.Errors, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> HRefJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.HRef, Keywords.HRef);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> IdJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Id, Keywords.Id);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> IncludedDataJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Included, Keywords.Included, new DomArray());
                            return domProperty;
                        }

                    case JTokenType.Array:
                        {
                            var apiPropertyJArray = (JArray)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomResourceArray(domReadJsonContext, apiPropertyJArray);
                            var domProperty = new DomProperty(PropertyType.Included, Keywords.Included, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> JsonApiJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.JsonApi, Keywords.JsonApi);
                        return domProperty;
                    }

                    case JTokenType.Object:
                    {
                        var apiPropertyJObject = (JObject)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomJsonApi(domReadJsonContext, apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.JsonApi, Keywords.JsonApi, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> LinksJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Links, Keywords.Links);
                        return domProperty;
                    }

                    case JTokenType.Object:
                    {
                        var apiPropertyJObject = (JObject)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomLinks(domReadJsonContext, apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Links, Keywords.Links, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> MetaJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Meta, Keywords.Meta);
                        return domProperty;
                    }

                    case JTokenType.Object:
                    {
                        var apiPropertyJObject = (JObject)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomObject(domReadJsonContext, apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Meta, Keywords.Meta, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> RelationshipDataJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data);
                            return domProperty;
                        }

                    case JTokenType.Object:
                        {
                            var apiPropertyJObject = (JObject)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomResourceIdentifier(domReadJsonContext, apiPropertyJObject);
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                            return domProperty;
                        }

                    case JTokenType.Array:
                        {
                            var apiPropertyJArray = (JArray)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomResourceIdentifierArray(domReadJsonContext, apiPropertyJArray);
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> RelationshipsJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Relationships, Keywords.Relationships);
                            return domProperty;
                        }

                    case JTokenType.Object:
                        {
                            var apiPropertyJObject = (JObject)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomRelationships(domReadJsonContext, apiPropertyJObject);
                            var domProperty = new DomProperty(PropertyType.Relationships, Keywords.Relationships, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> SourceJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) =>
            {
                Contract.Requires(domReadJsonContext != null);
                Contract.Requires(jProperty != null);

                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                        {
                            var domProperty = new DomProperty(PropertyType.Source, Keywords.Source);
                            return domProperty;
                        }

                    case JTokenType.Object:
                        {
                            var apiPropertyJObject = (JObject)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomObject(domReadJsonContext, apiPropertyJObject);
                            var domProperty = new DomProperty(PropertyType.Source, Keywords.Source, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> StatusJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Status, Keywords.Status);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> TitleJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Title, Keywords.Title);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> TypeJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Type, Keywords.Type);

        private static readonly Func<DomReadJsonContext, JProperty, DomProperty> VersionJPropertyToDomPropertyConverter = (domReadJsonContext, jProperty) => JPropertyToDomStringPropertyConverter(domReadJsonContext, jProperty, PropertyType.Version, Keywords.Version);
        #endregion

        #region JPropertyToDomDocumentPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomDocumentPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
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
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomErrorPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
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
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomJsonApiPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
            {
                { Keywords.Version, VersionJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomLinkPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomLinkPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
            {
                { Keywords.HRef, HRefJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomRelationshipPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomRelationshipPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
            {
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Data, RelationshipDataJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomResourcePropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomResourcePropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
            {
                { Keywords.Type, TypeJPropertyToDomPropertyConverter },
                { Keywords.Id, IdJPropertyToDomPropertyConverter },
                { Keywords.Attributes, AttributesJPropertyToDomPropertyConverter },
                { Keywords.Relationships, RelationshipsJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomResourceIdentifierPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>> JPropertyToDomResourceIdentifierPropertyConverterDictionary = new Dictionary<string, Func<DomReadJsonContext, JProperty, DomProperty>>
            {
                { Keywords.Type, TypeJPropertyToDomPropertyConverter },
                { Keywords.Id, IdJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
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