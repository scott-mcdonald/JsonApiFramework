// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for the family of <c>Relationship</c> objects.
    /// </summary>
    public class RelationshipConverter : Converter<Relationship>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Relationship ReadObject(JObject relationshipJObject, JsonSerializer serializer)
        {
            Contract.Requires(relationshipJObject != null);
            Contract.Requires(serializer != null);

            var relationship = CreateRelationshipAndReadData(relationshipJObject, serializer);
            ReadMeta(relationshipJObject, serializer, relationship);
            ReadLinks(relationshipJObject, serializer, relationship);

            return relationship;
        }

        protected override void WriteObject(JsonWriter writer, JsonSerializer serializer, Relationship relationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(relationship != null);

            writer.WriteStartObject();

            WriteLinks(writer, serializer, relationship);
            WriteData(writer, serializer, relationship);
            WriteMeta(writer, serializer, relationship);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        private static Relationship CreateRelationshipAndReadData(JToken relationshipJToken, JsonSerializer serializer)
        {
            Contract.Requires(relationshipJToken != null);
            Contract.Requires(serializer != null);

            // Data
            Relationship relationship;

            // Analyze data to determine the concrete type of Relationship
            // object to create:
            // 1. If "data" is not present, then create a "Relationship" object.
            // 2. If "data" is present, then
            //    2.1 If "data" is an object, then create a "ToOneRelationship" object.
            //    2.2 If "data" is an array, then create a "ToManyRelationship" object.
            var dataJToken = relationshipJToken.SelectToken(Keywords.Data);
            if (dataJToken == null)
            {
                relationship = new Relationship();
            }
            else
            {
                var dataJTokenType = dataJToken.Type;
                switch (dataJTokenType)
                {
                    case JTokenType.None:
                    case JTokenType.Null:
                        {
                            relationship = new ToOneRelationship
                                {
                                    Data = null
                                };
                        }
                        break;

                    case JTokenType.Object:
                        {
                            var data = dataJToken.ToObject<ResourceIdentifier>(serializer);
                            relationship = new ToOneRelationship
                                {
                                    Data = data
                                };
                        }
                        break;

                    case JTokenType.Array:
                        {
                            var data = dataJToken.Select(x => x.ToObject<ResourceIdentifier>(serializer))
                                                 .ToList();
                            relationship = new ToManyRelationship
                                {
                                    Data = data
                                };
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return relationship;
        }
        #endregion

        #region Write Methods
        private static void WriteData(JsonWriter writer, JsonSerializer serializer, Relationship relationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(relationship != null);

            var relationshipType = relationship.GetRelationshipType();
            switch (relationshipType)
            {
                case RelationshipType.Relationship:
                    {
                        // NOOP
                    }
                    break;

                case RelationshipType.ToOneRelationship:
                    {
                        writer.WritePropertyName(Keywords.Data);

                        var toOneResourceLinkage = relationship.GetToOneResourceLinkage();
                        if (toOneResourceLinkage != null)
                        {
                            var dataJToken = JToken.FromObject(toOneResourceLinkage, serializer);
                            var dataJObject = (JObject)dataJToken;

                            dataJObject.WriteTo(writer);
                        }
                        else
                        {
                            writer.WriteValue(NullData);
                        }
                    }
                    break;

                case RelationshipType.ToManyRelationship:
                    {
                        writer.WritePropertyName(Keywords.Data);

                        var toManyResourceLinkage = relationship.GetToManyResourceLinkage() ?? EmptyDataArray;

                        var dataJToken = JToken.FromObject(toManyResourceLinkage, serializer);
                        var dataJArray = (JArray)dataJToken;

                        dataJArray.WriteTo(writer);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly object NullData = default(object);
        private static readonly ResourceIdentifier[] EmptyDataArray = Enumerable.Empty<ResourceIdentifier>().ToArray();

        private static readonly TypeInfo ToOneRelationshipTypeInfo = typeof(ToOneRelationship).GetTypeInfo();
        private static readonly TypeInfo ToManyRelationshipTypeInfo = typeof(ToManyRelationship).GetTypeInfo();
        #endregion
    }
}