// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

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
        protected static IDomArray CreateDomArray(JArray jArray)
        {
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
                                    var domValue = (DomNode)CreateDomValue(jValue);
                                    var domItem = new DomItem(index++, domValue);
                                    return domItem;
                                }

                            case JTokenType.Object:
                                {
                                    var jObject = (JObject)jToken;
                                    var domObject = (DomNode)CreateDomObject(jObject);
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

        protected static IDomArray CreateDomResourceArray(JArray jArray)
        {
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
                                var domObject = (DomNode)CreateDomResource(jObject);
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

        protected static IDomArray CreateDomResourceIdentifierArray(JArray jArray)
        {
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
                                var domObject = (DomNode)CreateDomResourceIdentifier(jObject);
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

        protected static IDomDocument CreateDomDocument(JObject jObject)
        {
            if (jObject == null)
                return null;

            var apiDocumentType = jObject.GetApiDocumentType();
            var deserializationContext = new DeserializationContext
                {
                    ApiDocumentType = apiDocumentType
                };

            var domProperties = CreateDomProperties(jObject, deserializationContext, JPropertyToDomDocumentPropertyConverterDictionary);
            var domDocument = new DomDocument(apiDocumentType, domProperties);
            return domDocument;
        }

        protected static IDomJsonApi CreateDomJsonApi(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, null, JPropertyToDomJsonApiPropertyConverterDictionary);
            var domJsonApi = new DomJsonApi(domProperties);
            return domJsonApi;
        }

        protected static IDomLink CreateDomLink(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, null, JPropertyToDomLinkPropertyConverterDictionary);
            var domLink = new DomLink(domProperties);
            return domLink;
        }

        protected static IDomLink CreateDomLink(JValue jValue)
        {
            var hRef = (string)jValue;
            var domValue = CreateDomValue(hRef);
            var domProperty = new DomProperty(PropertyType.HRef, Keywords.HRef, (DomNode)domValue);
            var domLink = new DomLink(domProperty);
            return domLink;
        }

        protected static IDomLinks CreateDomLinks(JObject jObject)
        {
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
                                var domPropertyValue = (DomNode)CreateDomLink(apiPropertyJValue);
                                var domProperty = new DomProperty(PropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                        case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(apiPropertyJObject);
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

        protected static IDomObject CreateDomObject(JObject jObject)
        {
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
                                var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomObject(apiPropertyJObject);
                                var domProperty = new DomProperty(apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                            case JTokenType.Array:
                            {
                                var apiPropertyJArray = (JArray)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomArray(apiPropertyJArray);
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

        protected static IDomRelationship CreateDomRelationship(JObject jObject)
        {
            if (jObject == null)
                return null;

            var apiRelationshipType = jObject.GetApiRelationshipType();
            var domProperties = CreateDomProperties(jObject, null, JPropertyToDomRelationshipPropertyConverterDictionary);
            var domRelationship = new DomRelationship(apiRelationshipType, domProperties);
            return domRelationship;
        }

        protected static IDomRelationships CreateDomRelationships(JObject jObject)
        {
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
                                var domPropertyValue = (DomNode)CreateDomRelationship(apiPropertyJObject);
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

        protected static IDomResource CreateDomResource(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, null, JPropertyToDomResourcePropertyConverterDictionary);
            var domResource = new DomResource(domProperties);
            return domResource;
        }

        protected static IDomResourceIdentifier CreateDomResourceIdentifier(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, null, JPropertyToDomResourceIdentifierPropertyConverterDictionary);
            var domResourceIdentifier = new DomResourceIdentifier(domProperties);
            return domResourceIdentifier;
        }

        protected static IDomValue CreateDomValue(JValue jValue)
        {
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
        private static IEnumerable<DomProperty> CreateDomProperties(JObject jObject, DeserializationContext deserializationContext, IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> jPropertyToDomPropertyConverterDictionary)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(jPropertyToDomPropertyConverterDictionary != null);

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                {
                    var apiPropertyName = jProperty.GetApiPropertyName();

                    Func<JProperty, DeserializationContext, DomProperty> jPropertyToDomPropertyConverter;
                    if (jPropertyToDomPropertyConverterDictionary.TryGetValue(apiPropertyName, out jPropertyToDomPropertyConverter))
                    {
                        var domProperty = jPropertyToDomPropertyConverter(jProperty, deserializationContext);
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

        private static IDomValue CreateDomValue<TValue>(TValue clrValue)
        { return new DomValue<TValue>(clrValue); }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class DeserializationContext
        {
            #region Properties
            public DocumentType ApiDocumentType { get; set; }
            #endregion
        };
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region JPropertyToDomPropertyConverters
        private static readonly Func<JProperty, DeserializationContext, DomProperty> AttributesJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomObject(apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Attributes, Keywords.Attributes, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> DocumentDataJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
                var apiDocumentType = deserializationContext.ApiDocumentType;
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
                                        var domPropertyValue = (DomNode)CreateDomResource(apiPropertyJObject);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                case DocumentType.ResourceIdentifierDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResourceIdentifier(apiPropertyJObject);
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
                                        var domPropertyValue = (DomNode)CreateDomResourceArray(apiPropertyJArray);
                                        var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                                        return domProperty;
                                    }

                                case DocumentType.ResourceIdentifierCollectionDocument:
                                    {
                                        var domPropertyValue = (DomNode)CreateDomResourceIdentifierArray(apiPropertyJArray);
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

        private static readonly Func<JProperty, DeserializationContext, DomProperty> ErrorsJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
                {
                    var apiPropertyJToken = jProperty.Value;
                    var apiPropertyJTokenType = apiPropertyJToken.Type;
                    switch (apiPropertyJTokenType)
                    {
                        case JTokenType.Null:
                        {
                            throw new NotImplementedException();
                        }

                        case JTokenType.Array:
                        {
                            throw new NotImplementedException();
                        }
                    }

                    return null;
                };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> HRefJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                if (apiPropertyJTokenType != JTokenType.String)
                    return null;

                var apiPropertyJValue = (JValue)apiPropertyJToken;
                var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                var domProperty = new DomProperty(PropertyType.HRef, Keywords.HRef, domPropertyValue);
                return domProperty;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> IdJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Id, Keywords.Id);
                        return domProperty;
                    }

                    case JTokenType.String:
                    {
                        var apiPropertyJValue = (JValue)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                        var domProperty = new DomProperty(PropertyType.Id, Keywords.Id, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> IncludedDataJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
                {
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
                                var domPropertyValue = (DomNode)CreateDomResourceArray(apiPropertyJArray);
                                var domProperty = new DomProperty(PropertyType.Included, Keywords.Included, domPropertyValue);
                                return domProperty;
                            }
                    }

                    return null;
                };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> JsonApiJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
                {
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
                            var domPropertyValue = (DomNode)CreateDomJsonApi(apiPropertyJObject);
                            var domProperty = new DomProperty(PropertyType.JsonApi, Keywords.JsonApi, domPropertyValue);
                            return domProperty;
                        }
                    }

                    return null;
                };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> LinksJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomLinks(apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Links, Keywords.Links, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> MetaJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomObject(apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Meta, Keywords.Meta, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> RelationshipDataJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
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
                            var domPropertyValue = (DomNode)CreateDomResourceIdentifier(apiPropertyJObject);
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                            return domProperty;
                        }

                    case JTokenType.Array:
                        {
                            var apiPropertyJArray = (JArray)apiPropertyJToken;
                            var domPropertyValue = (DomNode)CreateDomResourceIdentifierArray(apiPropertyJArray);
                            var domProperty = new DomProperty(PropertyType.Data, Keywords.Data, domPropertyValue);
                            return domProperty;
                        }
                }

                return null;
            };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> RelationshipsJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
        {
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
                        var domPropertyValue = (DomNode)CreateDomRelationships(apiPropertyJObject);
                        var domProperty = new DomProperty(PropertyType.Relationships, Keywords.Relationships, domPropertyValue);
                        return domProperty;
                    }
            }

            return null;
        };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> TypeJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
        {
            var apiPropertyJToken = jProperty.Value;
            var apiPropertyJTokenType = apiPropertyJToken.Type;
            switch (apiPropertyJTokenType)
            {
                case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Type, Keywords.Type);
                        return domProperty;
                    }

                case JTokenType.String:
                    {
                        var apiPropertyJValue = (JValue)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                        var domProperty = new DomProperty(PropertyType.Type, Keywords.Type, domPropertyValue);
                        return domProperty;
                    }
            }

            return null;
        };

        private static readonly Func<JProperty, DeserializationContext, DomProperty> VersionJPropertyToDomPropertyConverter = (jProperty, deserializationContext) =>
            {
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(PropertyType.Version, Keywords.Version);
                        return domProperty;
                    }

                    case JTokenType.String:
                    {
                        var apiPropertyJValue = (JValue)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                        var domProperty = new DomProperty(PropertyType.Version, Keywords.Version, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };
        #endregion

        #region JPropertyToDomDocumentPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomDocumentPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
            {
                { Keywords.JsonApi, JsonApiJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Data, DocumentDataJPropertyToDomPropertyConverter },
                { Keywords.Included, IncludedDataJPropertyToDomPropertyConverter },
                { Keywords.Errors, ErrorsJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomJsonApiPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomJsonApiPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
            {
                { Keywords.Version, VersionJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomLinkPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomLinkPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
            {
                { Keywords.HRef, HRefJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomRelationshipPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomRelationshipPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
            {
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
                { Keywords.Data, RelationshipDataJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomResourcePropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomResourcePropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
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
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DeserializationContext, DomProperty>> JPropertyToDomResourceIdentifierPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DeserializationContext, DomProperty>>
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