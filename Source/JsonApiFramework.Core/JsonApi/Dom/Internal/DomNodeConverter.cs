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

        protected static IDomDocument CreateDomDocument(JObject jObject)
        {
            if (jObject == null)
                return null;

            var apiDocumentType = jObject.GetApiDocumentType();
            var domProperties = CreateDomProperties(jObject, JPropertyToDomDocumentPropertyConverterDictionary);
            var domDocument = new DomDocument(apiDocumentType, domProperties);
            return domDocument;
        }

        protected static IDomJsonApiVersion CreateDomJsonApiVersion(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, JPropertyToDomJsonApiVersionPropertyConverterDictionary);
            var domJsonApiVersion = new DomJsonApiVersion(domProperties);
            return domJsonApiVersion;
        }

        protected static IDomLink CreateDomLink(JObject jObject)
        {
            if (jObject == null)
                return null;

            var domProperties = CreateDomProperties(jObject, JPropertyToDomLinkPropertyConverterDictionary);
            var domLink = new DomLink(domProperties);
            return domLink;
        }

        protected static IDomLink CreateDomLink(JValue jValue)
        {
            var hRef = (string)jValue;
            var domValue = CreateDomValue(hRef);
            var domProperty = new DomProperty(ApiPropertyType.HRef, Keywords.HRef, (DomNode)domValue);
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
                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName, domPropertyValue);
                                return domProperty;
                            }

                        case JTokenType.Object:
                            {
                                var apiPropertyJObject = (JObject)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomLink(apiPropertyJObject);
                                var domProperty = new DomProperty(ApiPropertyType.Link, apiPropertyName, domPropertyValue);
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

                            case JTokenType.Array:
                            {
                                var apiPropertyJArray = (JArray)apiPropertyJToken;
                                var domPropertyValue = (DomNode)CreateDomArray(apiPropertyJArray);
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
                        }

                        var json = jObject.ToString();
                        var title = CoreErrorStrings.JsonApiDeserializationErrorTitle;
                        var detail = CoreErrorStrings.JsonApiDeserializationErrorInvalidPropertyJsonDetail.FormatWith(apiPropertyName, json);
                        throw new JsonApiException(title, detail);
                    });

            var domObject = new DomObject(domProperties);
            return domObject;
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
        private static IEnumerable<DomProperty> CreateDomProperties(JObject jObject, IReadOnlyDictionary<string, Func<JProperty, DomProperty>> jPropertyToDomPropertyConverterDictionary)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(jPropertyToDomPropertyConverterDictionary != null);

            var domProperties = jObject
                .Properties()
                .Select(jProperty =>
                {
                    var apiPropertyName = jProperty.GetApiPropertyName();

                    Func<JProperty, DomProperty> jPropertyToDomPropertyConverter;
                    if (jPropertyToDomPropertyConverterDictionary.TryGetValue(apiPropertyName, out jPropertyToDomPropertyConverter))
                    {
                        var domProperty = jPropertyToDomPropertyConverter(jProperty);
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

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region JPropertyToDomDocumentPropertyConverters
        private static readonly Func<JProperty, DomProperty> HRefJPropertyToDomPropertyConverter = (jProperty) =>
            {
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                if (apiPropertyJTokenType != JTokenType.String)
                    return null;

                var apiPropertyJValue = (JValue)apiPropertyJToken;
                var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                var domProperty = new DomProperty(ApiPropertyType.HRef, Keywords.HRef, domPropertyValue);
                return domProperty;
            };

        private static readonly Func<JProperty, DomProperty> LinksJPropertyToDomPropertyConverter = (jProperty) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomLinks(apiPropertyJObject);
                        var domProperty = new DomProperty(ApiPropertyType.Links, Keywords.Links, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DomProperty> JsonApiVersionJPropertyToDomPropertyConverter = (jProperty) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomJsonApiVersion(apiPropertyJObject);
                        var domProperty = new DomProperty(ApiPropertyType.JsonApi, Keywords.JsonApi, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DomProperty> MetaJPropertyToDomPropertyConverter = (jProperty) =>
            {
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
                        var domPropertyValue = (DomNode)CreateDomObject(apiPropertyJObject);
                        var domProperty = new DomProperty(ApiPropertyType.Meta, Keywords.Meta, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };

        private static readonly Func<JProperty, DomProperty> VersionJPropertyToDomPropertyConverter = (jProperty) =>
            {
                var apiPropertyJToken = jProperty.Value;
                var apiPropertyJTokenType = apiPropertyJToken.Type;
                switch (apiPropertyJTokenType)
                {
                    case JTokenType.Null:
                    {
                        var domProperty = new DomProperty(ApiPropertyType.Version, Keywords.Version);
                        return domProperty;
                    }

                    case JTokenType.String:
                    {
                        var apiPropertyJValue = (JValue)apiPropertyJToken;
                        var domPropertyValue = (DomNode)CreateDomValue(apiPropertyJValue);
                        var domProperty = new DomProperty(ApiPropertyType.Version, Keywords.Version, domPropertyValue);
                        return domProperty;
                    }
                }

                return null;
            };
        #endregion

        #region JPropertyToDomDocumentPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DomProperty>> JPropertyToDomDocumentPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DomProperty>>
            {
                { Keywords.JsonApi, JsonApiVersionJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
                { Keywords.Links, LinksJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomJsonApiVersionPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DomProperty>> JPropertyToDomJsonApiVersionPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DomProperty>>
            {
                { Keywords.Version, VersionJPropertyToDomPropertyConverter },
                { Keywords.Meta, MetaJPropertyToDomPropertyConverter },
            };
        #endregion

        #region JPropertyToDomLinkPropertyConverterDictionary
        private static readonly IReadOnlyDictionary<string, Func<JProperty, DomProperty>> JPropertyToDomLinkPropertyConverterDictionary = new Dictionary<string, Func<JProperty, DomProperty>>
            {
                { Keywords.HRef, HRefJPropertyToDomPropertyConverter },
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