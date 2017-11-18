// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Internal
{
    /// <summary>Extension methods specific to json:api for the JSON.NET JObject class.</summary>
    internal static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static Error ReadErrorObject(this JObject jObject, JsonSerializer serializer)
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

        public static ErrorSource ReadErrorSourceObject(this JObject jObject, JsonSerializer serializer)
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

        public static Link ReadLinkObject(this JObject jObject, JsonSerializer serializer)
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

        public static Links ReadLinksObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var dictionary = jObject.Properties()
                                    .ToDictionary(x => x.Name, y => y.ReadLinkPropertyValue(serializer));
            var clrLinks = new Links(dictionary);
            return clrLinks;
        }

        public static Meta ReadMetaObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrMeta = new ReadMeta(jObject);
            return clrMeta;
        }

        public static Relationship ReadRelationshipObject(this JObject jObject, JsonSerializer serializer)
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
                    var data = context.ToOneResourceLinkage;
                    var clrRelationship = new ToOneRelationship(links, data, meta);
                    return clrRelationship;
                }

                case RelationshipType.ToManyRelationship:
                {
                    var data = context.ToManyResourceLinkage;
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

        public static Relationships ReadRelationshipsObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var dictionary = jObject.Properties()
                                    .ToDictionary(x => x.Name, y => y.ReadRelationshipPropertyValue(serializer));
            var clrRelationships = new Relationships(dictionary);
            return clrRelationships;
        }

        public static ResourceIdentifier ReadResourceIdentifierObject(this JObject jObject, JsonSerializer serializer)
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
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ErrorContext>> ErrorContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ErrorContext>>
            {
                {Keywords.Id, (jProperty, serializer, context) => { var id = jProperty.ReadStringPropertyValue(); context.Id = id;} },
                {Keywords.Links, (jProperty, serializer, context) => { var links = jProperty.ReadLinksPropertyValue(serializer); context.Links = links;} },
                {Keywords.Status, (jProperty, serializer, context) => { var status = jProperty.ReadStatusPropertyValue(serializer); context.Status = status;} },
                {Keywords.Code, (jProperty, serializer, context) => { var code = jProperty.ReadStringPropertyValue(); context.Code = code;} },
                {Keywords.Title, (jProperty, serializer, context) => { var title = jProperty.ReadStringPropertyValue(); context.Title = title;} },
                {Keywords.Detail, (jProperty, serializer, context) => { var detail = jProperty.ReadStringPropertyValue(); context.Detail = detail;} },
                {Keywords.Source, (jProperty, serializer, context) => { var source = jProperty.ReadErrorSourcePropertyValue(serializer); context.Source = source;} },
                {Keywords.Meta, (jProperty, serializer, context) => { var meta = jProperty.ReadMetaPropertyValue(serializer); context.Meta = meta;} }
            };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ErrorSourceContext>> ErrorSourceContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ErrorSourceContext>>
            {
                {Keywords.Parameter, (jProperty, serializer, context) => { var parameter = jProperty.ReadStringPropertyValue(); context.Parameter = parameter;} },
                {Keywords.Pointer, (jProperty, serializer, context) => { var pointer = jProperty.ReadStringPropertyValue(); context.Pointer = pointer;} }
            };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, LinkContext>> LinkContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, LinkContext>>
            {
                {Keywords.HRef, (jProperty, serializer, context) => { var hRef = jProperty.ReadStringPropertyValue(); context.HRef = hRef;} },
                {Keywords.Meta, (jProperty, serializer, context) => { var meta = jProperty.ReadMetaPropertyValue(serializer); context.Meta = meta;} }
            };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, RelationshipContext>> RelationshipContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, RelationshipContext>>
            {
                {Keywords.Links, (jProperty, serializer, context) => { var links = jProperty.ReadLinksPropertyValue(serializer); context.Links = links;} },
                // ReSharper disable once InlineOutVariableDeclaration
                {Keywords.Data, (jProperty, serializer, context) =>
                    {
                        var jToken = jProperty.Value;
                        var jTokenType = jToken.Type;
                        switch (jTokenType)
                        {
                            case JTokenType.Null:
                            {
                                context.Type = RelationshipType.ToOneRelationship;
                            }
                            break;

                            case JTokenType.Object:
                            {
                                context.Type = RelationshipType.ToOneRelationship;

                                var jObject = (JObject)jToken;
                                var toOneResourceLinkage = jObject.ReadResourceIdentifierObject(serializer);
                                context.ToOneResourceLinkage = toOneResourceLinkage;
                            }
                            break;

                            case JTokenType.Array:
                            {
                                context.Type = RelationshipType.ToManyRelationship;

                                var jArray = (JArray)jToken;
                                var toManyResourceLinkage = jArray.ReadResourceIdentifierArray(serializer);
                                context.ToManyResourceLinkage = toManyResourceLinkage;
                            }
                            break;

                            default:
                            {
                                context.Type = RelationshipType.Relationship;
                            }
                            break;
                        }
                    }
                },
                {Keywords.Meta, (jProperty, serializer, context) => { var meta = jProperty.ReadMetaPropertyValue(serializer); context.Meta = meta;} }
            };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ResourceIdentifierContext>> ResourceIdentifierContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ResourceIdentifierContext>>
            {
                {Keywords.Type, (jProperty, serializer, context) => { var type = jProperty.ReadStringPropertyValue(); context.Type = type;} },
                {Keywords.Id, (jProperty, serializer, context) => { var id = jProperty.ReadStringPropertyValue(); context.Id = id;} },
                {Keywords.Meta, (jProperty, serializer, context) => { var meta = jProperty.ReadMetaPropertyValue(serializer); context.Meta = meta;} }
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

        private class RelationshipContext
        {
            public RelationshipType Type { get; set; }
            public Links Links { get; set; }
            public ResourceIdentifier ToOneResourceLinkage { get; set; }
            public IEnumerable<ResourceIdentifier> ToManyResourceLinkage { get; set; }
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
}
