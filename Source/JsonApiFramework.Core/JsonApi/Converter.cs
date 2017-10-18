// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Reflection;

using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public abstract class Converter : JsonConverter
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Read Methods
        protected static Error ReadErrorObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ErrorContext();
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();
                if (!ErrorContextInitializerDictionary.TryGetValue(apiPropertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var id = context.Id;
            var links = context.Links;
            var status = context.Status;
            var code = context.Code;
            var title = context.Title;
            var detail = context.Detail;
            var source = context.Source;
            var meta = context.Meta;

            var clrError = new Error(id, links, status, code, title, detail, source, meta);
            return clrError;
        }

        protected static ErrorSource ReadErrorSourceObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ErrorSourceContext();
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();
                if (!ErrorSourceContextInitializerDictionary.TryGetValue(apiPropertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var parameter = context.Parameter;
            var pointer = context.Pointer;
            var clrErrorSource = new ErrorSource(parameter, pointer);
            return clrErrorSource;
        }

        protected static Link ReadLinkObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new LinkContext();
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();
                if (!LinkContextInitializerDictionary.TryGetValue(apiPropertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var hRef = context.HRef;
            var meta = context.Meta;
            var clrLink = new Link(hRef, meta);
            return clrLink;
        }

        protected static Links ReadLinksObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var dictionary = jObject.Properties()
                                    .ToDictionary(x => x.Name, y => ReadLinkProperty(y, serializer));
            var clrLinks = new Links(dictionary);
            return clrLinks;
        }

        protected static Meta ReadMetaObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrMeta = new ReadMeta(jObject);
            return clrMeta;
        }

        protected static Relationship ReadRelationshipObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new RelationshipContext();
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();
                if (!RelationshipContextInitializerDictionary.TryGetValue(apiPropertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var links = context.Links;
            var meta = context.Meta;

            var type = context.Type;
            switch (type)
            {
                case RelationshipType.ToOneRelationship:
                {
                    var data = context.DataContext.ToOneResourceLinkage;
                    var clrRelationship = new ToOneRelationship(links, data, meta);
                    return clrRelationship;
                }

                case RelationshipType.ToManyRelationship:
                {
                    var data = context.DataContext.ToManyResourceLinkage;
                    var clrRelationship = new ToManyRelationship(links, data, meta);
                    return clrRelationship;
                }

                default:
                {
                    var clrRelationship = new Relationship(links, meta);
                    return clrRelationship;
                }
            }
        }

        protected static Relationships ReadRelationshipsObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var dictionary = jObject.Properties()
                                    .ToDictionary(x => x.Name, y => ReadRelationshipProperty(y, serializer));
            var clrRelationships = new Relationships(dictionary);
            return clrRelationships;
        }

        protected static ResourceIdentifier ReadResourceIdentifierObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ResourceIdentifierContext();
            foreach (var jProperty in jObject.Properties())
            {
                var apiPropertyName = jProperty.GetApiPropertyName();
                if (!ResourceIdentifierContextInitializerDictionary.TryGetValue(apiPropertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var type = context.Type;
            var id = context.Id;
            var meta = context.Meta;
            var clrResourceIdentifier = new ResourceIdentifier(type, id, meta);
            return clrResourceIdentifier;
        }

        protected static IEnumerable<ResourceIdentifier> ReadResourceIdentifierArray(JArray jArray, JsonSerializer serializer)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);

            var clrResourceIdentifierCollection = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var clrResourceIdentifier = ReadResourceIdentifierObject(jObject, serializer);
                    return clrResourceIdentifier;
                });
            return clrResourceIdentifierCollection;
        }
        #endregion

        #region Write Methods
        protected static void WriteErrorObject(JsonWriter writer, JsonSerializer serializer, Error clrError)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrError != null);

            writer.WriteStartObject();

            WriteIdProperty(writer, serializer, clrError.Id);
            WriteLinksProperty(writer, serializer, clrError.Links);
            WriteStatusProperty(writer, serializer, clrError.Status);
            WriteCodeProperty(writer, serializer, clrError.Code);
            WriteTitleProperty(writer, serializer, clrError.Title);
            WriteDetailProperty(writer, serializer, clrError.Detail);
            WriteErrorSourceProperty(writer, serializer, clrError.Source);
            WriteMetaProperty(writer, serializer, clrError.Meta);

            writer.WriteEndObject();
        }

        protected static void WriteErrorSourceObject(JsonWriter writer, JsonSerializer serializer, ErrorSource clrErrorSource)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrErrorSource != null);

            writer.WriteStartObject();

            WriteParameterProperty(writer, serializer, clrErrorSource.Parameter);
            WritePointerProperty(writer, serializer, clrErrorSource.Pointer);

            writer.WriteEndObject();
        }

        protected static void WriteLinkObject(JsonWriter writer, JsonSerializer serializer, Link clrLink)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLink != null);

            var nullValueHandling = serializer.NullValueHandling;
            if (nullValueHandling == NullValueHandling.Ignore)
            {
                // If link is represented by the HRef only, then serialize the Link object as a JSON string value instead of a JSON object.
                if (clrLink.Meta == null && String.IsNullOrWhiteSpace(clrLink.HRef) == false)
                {
                    // Serialize Link object as a JSON string value.
                    var hRef = clrLink.HRef;
                    writer.WriteValue(hRef);
                    return;
                }
            }

            // Serialize Link object as a JSON object.
            writer.WriteStartObject();

            WriteHRefProperty(writer, serializer, clrLink.HRef);
            WriteMetaProperty(writer, serializer, clrLink.Meta);

            writer.WriteEndObject();
        }

        protected static void WriteLinksObject(JsonWriter writer, JsonSerializer serializer, Links clrLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLinks != null);

            writer.WriteStartObject();

            foreach (var clrLinkPair in clrLinks)
            {
                var rel = clrLinkPair.Key;
                var clrLink = clrLinkPair.Value;

                WriteLinkProperty(writer, serializer, rel, clrLink);
            }

            writer.WriteEndObject();
        }

        protected static void WriteMetaObject(JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrMeta != null);

            clrMeta.WriteJson(writer, serializer);
        }

        protected static void WriteRelationshipObject(JsonWriter writer, JsonSerializer serializer, Relationship clrRelationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrRelationship != null);

            writer.WriteStartObject();

            WriteLinksProperty(writer, serializer, clrRelationship.Links);
            var relationshipType = clrRelationship.GetRelationshipType();
            switch (relationshipType)
            {
                case RelationshipType.ToOneRelationship:
                {
                    var clrData = clrRelationship.GetToOneResourceLinkage();
                    WriteDataProperty(writer, serializer, clrData);
                }
                    break;

                case RelationshipType.ToManyRelationship:
                {
                    var clrDataCollection = clrRelationship.GetToManyResourceLinkage();
                    WriteDataProperty(writer, serializer, clrDataCollection);
                }
                    break;
            }
            WriteMetaProperty(writer, serializer, clrRelationship.Meta);

            writer.WriteEndObject();
        }

        protected static void WriteRelationshipsObject(JsonWriter writer, JsonSerializer serializer, Relationships clrRelationships)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrRelationships != null);

            writer.WriteStartObject();

            foreach (var clrRelationshipPair in clrRelationships)
            {
                var rel = clrRelationshipPair.Key;
                var clrRelationship = clrRelationshipPair.Value;

                WriteRelationshipProperty(writer, serializer, rel, clrRelationship);
            }

            writer.WriteEndObject();
        }

        protected static void WriteResourceIdentifierObject(JsonWriter writer, JsonSerializer serializer, ResourceIdentifier clrResourceIdentifier)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrResourceIdentifier != null);

            writer.WriteStartObject();

            WriteTypeProperty(writer, serializer, clrResourceIdentifier.Type);
            WriteIdProperty(writer, serializer, clrResourceIdentifier.Id);
            WriteMetaProperty(writer, serializer, clrResourceIdentifier.Meta);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        private static ErrorSource ReadErrorSourceProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(ErrorSource);

            var jObject = (JObject)jToken;
            var clrErrorSource = ReadErrorSourceObject(jObject, serializer);
            return clrErrorSource;
        }

        private static Link ReadLinkProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            switch (jTokenType)
            {
                case JTokenType.Object:
                {
                    var jObject = (JObject)jToken;
                    return ReadLinkObject(jObject, serializer);
                }

                case JTokenType.String:
                {
                    var jValue = (JValue)jToken;
                    var hRef = (string)jValue;
                    return new Link(hRef);
                }

                default:
                {
                    return default(Link);
                }
            }
        }

        private static Links ReadLinksProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Links);

            var jObject = (JObject)jToken;
            var clrLinks = ReadLinksObject(jObject, serializer);
            return clrLinks;
        }

        private static Meta ReadMetaProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Meta);

            var jObject = (JObject)jToken;
            var clrMeta = ReadMetaObject(jObject, serializer);
            return clrMeta;
        }

        private static Relationship ReadRelationshipProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Relationship);

            var jObject = (JObject)jToken;
            var clrRelationship = ReadRelationshipObject(jObject, serializer);
            return clrRelationship;
        }

        private static RelationshipType ReadRelationshipDataProperty(JProperty jProperty, JsonSerializer serializer, out RelationshipDataContext dataContext)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            switch (jTokenType)
            {
                case JTokenType.Null:
                {
                    dataContext = new RelationshipDataContext();
                    return RelationshipType.ToOneRelationship;
                }

                case JTokenType.Object:
                {
                    var jObject = (JObject)jToken;
                    var toOneResourceLinkage = ReadResourceIdentifierObject(jObject, serializer);
                    dataContext = new RelationshipDataContext
                    {
                        ToOneResourceLinkage = toOneResourceLinkage
                    };
                    return RelationshipType.ToOneRelationship;
                }

                case JTokenType.Array:
                {
                    var jArray = (JArray)jToken;
                    var toManyResourceLinkage = ReadResourceIdentifierArray(jArray, serializer);
                    dataContext = new RelationshipDataContext
                    {
                        ToManyResourceLinkage = toManyResourceLinkage
                    };
                    return RelationshipType.ToManyRelationship;
                }

                default:
                {
                    dataContext = null;
                    return RelationshipType.Relationship;
                }
            }
        }

        private static HttpStatusCode? ReadStatusValue(JValue jValue, JsonSerializer serializer)
        {
            Contract.Requires(jValue != null);
            Contract.Requires(serializer != null);

            var statusAsString = (string)jValue;
            if (String.IsNullOrWhiteSpace(statusAsString))
                return default(HttpStatusCode?);

            if (!Int32.TryParse(statusAsString, out var statusAsInteger))
                return default(HttpStatusCode?);

            var status = (HttpStatusCode)statusAsInteger;
            return status;
        }

        private static HttpStatusCode? ReadStatusProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(HttpStatusCode?);

            var jValue = (JValue)jToken;
            var status = ReadStatusValue(jValue, serializer);
            return status;
        }

        private static string ReadStringProperty(JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(string);

            var jValue = (JValue)jToken;
            var stringValue = (string)jValue;
            return stringValue;
        }
        #endregion

        #region Write Methods
        private static void WriteCodeProperty(JsonWriter writer, JsonSerializer serializer, string code)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Code, code);
        }

        private static void WriteDataProperty(JsonWriter writer, JsonSerializer serializer, ResourceIdentifier clrData)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrData == null)
            {
                // Write 'null' directory ignoring the 'NullValueHandling' mode of the serializer.
                // Always write data as null to capture the "intent" that the related to-one resource is empty.
                writer.WritePropertyName(Keywords.Data);
                writer.WriteToken(JsonToken.Null);
                return;
            }

            writer.WritePropertyName(Keywords.Data);
            WriteResourceIdentifierObject(writer, serializer, clrData);
        }

        private static void WriteDataProperty(JsonWriter writer, JsonSerializer serializer, IEnumerable<ResourceIdentifier> clrDataCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WritePropertyName(Keywords.Data);
            WriteResourceIdentifierArray(writer, serializer, clrDataCollection);
        }

        private static void WriteDetailProperty(JsonWriter writer, JsonSerializer serializer, string detail)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Detail, detail);
        }

        private static void WriteErrorSourceProperty(JsonWriter writer, JsonSerializer serializer, ErrorSource clrErrorSource)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrErrorSource == null)
            {
                WriteNullProperty(writer, serializer, Keywords.Source);
                return;
            }

            writer.WritePropertyName(Keywords.Source);
            WriteErrorSourceObject(writer, serializer, clrErrorSource);
        }

        private static void WriteHRefProperty(JsonWriter writer, JsonSerializer serializer, string hRef)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.HRef, hRef);
        }

        private static void WriteIdProperty(JsonWriter writer, JsonSerializer serializer, string id)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Id, id);
        }

        private static void WriteLinkProperty(JsonWriter writer, JsonSerializer serializer, string rel, Link clrLink)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (clrLink == null)
            {
                WriteNullProperty(writer, serializer, rel);
                return;
            }

            writer.WritePropertyName(rel);
            WriteLinkObject(writer, serializer, clrLink);
        }

        private static void WriteLinksProperty(JsonWriter writer, JsonSerializer serializer, Links clrLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrLinks == null)
            {
                WriteNullProperty(writer, serializer, Keywords.Links);
                return;
            }

            writer.WritePropertyName(Keywords.Links);
            WriteLinksObject(writer, serializer, clrLinks);
        }

        private static void WriteMetaProperty(JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrMeta == null)
            {
                WriteNullProperty(writer, serializer, Keywords.Meta);
                return;
            }

            writer.WritePropertyName(Keywords.Meta);
            WriteMetaObject(writer, serializer, clrMeta);
        }

        private static void WriteNullProperty(JsonWriter writer, JsonSerializer serializer, string propertyName)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            switch (serializer.NullValueHandling)
            {
                case NullValueHandling.Include:
                writer.WritePropertyName(propertyName);
                writer.WriteToken(JsonToken.Null);
                return;

                case NullValueHandling.Ignore:
                // Ignore a null property.
                return;

                default:
                throw new ArgumentOutOfRangeException();
            }
        }

        private static void WriteParameterProperty(JsonWriter writer, JsonSerializer serializer, string parameter)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Parameter, parameter);
        }

        private static void WritePointerProperty(JsonWriter writer, JsonSerializer serializer, string pointer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Pointer, pointer);
        }

        private static void WriteRelationshipProperty(JsonWriter writer, JsonSerializer serializer, string rel, Relationship clrRelationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (clrRelationship == null)
            {
                WriteNullProperty(writer, serializer, rel);
                return;
            }

            writer.WritePropertyName(rel);
            WriteRelationshipObject(writer, serializer, clrRelationship);
        }

        private static void WriteResourceIdentifierArray(JsonWriter writer, JsonSerializer serializer, IEnumerable<ResourceIdentifier> clrResourceIdentifierCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrResourceIdentifierCollection != null);

            writer.WriteStartArray();

            foreach (var clrResourceIdentifier in clrResourceIdentifierCollection)
            {
                WriteResourceIdentifierObject(writer, serializer, clrResourceIdentifier);
            }

            writer.WriteEndArray();
        }

        private static void WriteStatusProperty(JsonWriter writer, JsonSerializer serializer, HttpStatusCode? status)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (status.HasValue == false)
            {
                WriteNullProperty(writer, serializer, Keywords.Status);
                return;
            }

            writer.WritePropertyName(Keywords.Status);
            WriteStatusValue(writer, serializer, status.Value);
        }

        private static void WriteStatusValue(JsonWriter writer, JsonSerializer serializer, HttpStatusCode status)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var statusAsInteger = (int)status;
            var statusAsString = Convert.ToString(statusAsInteger);
            writer.WriteValue(statusAsString);
        }

        private static void WriteStringProperty(JsonWriter writer, JsonSerializer serializer, string propertyName, string propertyValue)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (propertyValue == null)
            {
                WriteNullProperty(writer, serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteValue(propertyValue);
        }

        private static void WriteTitleProperty(JsonWriter writer, JsonSerializer serializer, string title)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Title, title);
        }

        private static void WriteTypeProperty(JsonWriter writer, JsonSerializer serializer, string type)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            WriteStringProperty(writer, serializer, Keywords.Type, type);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ErrorContext>> ErrorContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ErrorContext>>
        {
            {Keywords.Id, (jProperty, serializer, context) => { var id = ReadStringProperty(jProperty, serializer); context.Id = id;} },
            {Keywords.Links, (jProperty, serializer, context) => { var links = ReadLinksProperty(jProperty, serializer); context.Links = links;} },
            {Keywords.Status, (jProperty, serializer, context) => { var status = ReadStatusProperty(jProperty, serializer); context.Status = status;} },
            {Keywords.Code, (jProperty, serializer, context) => { var code = ReadStringProperty(jProperty, serializer); context.Code = code;} },
            {Keywords.Title, (jProperty, serializer, context) => { var title = ReadStringProperty(jProperty, serializer); context.Title = title;} },
            {Keywords.Detail, (jProperty, serializer, context) => { var detail = ReadStringProperty(jProperty, serializer); context.Detail = detail;} },
            {Keywords.Source, (jProperty, serializer, context) => { var source = ReadErrorSourceProperty(jProperty, serializer); context.Source = source;} },
            {Keywords.Meta, (jProperty, serializer, context) => { var meta = ReadMetaProperty(jProperty, serializer); context.Meta = meta;} }
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ErrorSourceContext>> ErrorSourceContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ErrorSourceContext>>
        {
            {Keywords.Parameter, (jProperty, serializer, context) => { var parameter = ReadStringProperty(jProperty, serializer); context.Parameter = parameter;} },
            {Keywords.Pointer, (jProperty, serializer, context) => { var pointer = ReadStringProperty(jProperty, serializer); context.Pointer = pointer;} }
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, LinkContext>> LinkContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, LinkContext>>
        {
            {Keywords.HRef, (jProperty, serializer, context) => { var hRef = ReadStringProperty(jProperty, serializer); context.HRef = hRef;} },
            {Keywords.Meta, (jProperty, serializer, context) => { var meta = ReadMetaProperty(jProperty, serializer); context.Meta = meta;} }
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, RelationshipContext>> RelationshipContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, RelationshipContext>>
        {
            {Keywords.Links, (jProperty, serializer, context) => { var links = ReadLinksProperty(jProperty, serializer); context.Links = links;} },
            {Keywords.Data, (jProperty, serializer, context) => { RelationshipDataContext dataContext; var type = ReadRelationshipDataProperty(jProperty, serializer, out dataContext); context.Type = type; context.DataContext = dataContext;} },
            {Keywords.Meta, (jProperty, serializer, context) => { var meta = ReadMetaProperty(jProperty, serializer); context.Meta = meta;} }
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ResourceIdentifierContext>> ResourceIdentifierContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ResourceIdentifierContext>>
        {
            {Keywords.Type, (jProperty, serializer, context) => { var type = ReadStringProperty(jProperty, serializer); context.Type = type;} },
            {Keywords.Id, (jProperty, serializer, context) => { var id = ReadStringProperty(jProperty, serializer); context.Id = id;} },
            {Keywords.Meta, (jProperty, serializer, context) => { var meta = ReadMetaProperty(jProperty, serializer); context.Meta = meta;} }
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class ErrorContext
        {
            public string Id { get; set; }
            public Links Links { get; set; }
            public HttpStatusCode? Status { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Detail { get; set; }
            public ErrorSource Source { get; set; }
            public Meta Meta { get; set; }
        }

        private class ErrorSourceContext
        {
            public string Parameter { get; set; }
            public string Pointer { get; set; }
        }

        private class LinkContext
        {
            public string HRef { get; set; }
            public Meta Meta { get; set; }
        }

        private class RelationshipDataContext
        {
            public ResourceIdentifier ToOneResourceLinkage { get; set; }
            public IEnumerable<ResourceIdentifier> ToManyResourceLinkage { get; set; }
        }

        private class RelationshipContext
        {
            public RelationshipType Type { get; set; }
            public Links Links { get; set; }
            public RelationshipDataContext DataContext { get; set; }
            public Meta Meta { get; set; }
        }

        private class ResourceIdentifierContext
        {
            public string Type { get; set; }
            public string Id { get; set; }
            public Meta Meta { get; set; }
        }
        #endregion
    }

    public abstract class Converter<T> : Converter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var objectTypeInfo = objectType.GetTypeInfo();
            var canConvert = TypeInfo.IsAssignableFrom(objectTypeInfo);
            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Contract.Requires(reader != null);
            Contract.Requires(objectType != null);
            Contract.Requires(serializer != null);

            switch (reader.TokenType)
            {
                case JsonToken.None:
                case JsonToken.Null:
                return null;

                case JsonToken.String:
                {
                    var stringValue = (string)reader.Value;
                    var clrObject = this.DeserializeJsonString(stringValue);
                    return clrObject;
                }

                case JsonToken.StartObject:
                {
                    var jObject = JObject.Load(reader);
                    var clrObject = this.DeserializeJsonObject(jObject, serializer);
                    return clrObject;
                }

                default:
                throw new ArgumentOutOfRangeException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var clrObject = (T)value;
            this.SerializeClrObject(writer, serializer, clrObject);
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected virtual T DeserializeJsonString(string stringValue)
        { throw new NotImplementedException(); }

        protected abstract T DeserializeJsonObject(JObject jObject, JsonSerializer serializer);

        protected abstract void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, T clrObject);
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();
        #endregion
    }
}