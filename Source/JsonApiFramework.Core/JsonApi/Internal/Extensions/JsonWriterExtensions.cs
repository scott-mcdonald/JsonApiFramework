// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;

using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Internal
{
    /// <summary>Extension methods for the JSON.NET JsonWriter class.</summary>
    internal static class JsonWriterExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Write Array Methods
        public static void WriteResourceIdentifierArray(this JsonWriter writer, JsonSerializer serializer, IEnumerable<ResourceIdentifier> clrResourceIdentifierCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrResourceIdentifierCollection != null);

            writer.WriteStartArray();

            foreach (var clrResourceIdentifier in clrResourceIdentifierCollection)
            {
                writer.WriteResourceIdentifierObject(serializer, clrResourceIdentifier);
            }

            writer.WriteEndArray();
        }
        #endregion

        #region Write Object Methods
        public static void WriteErrorObject(this JsonWriter writer, JsonSerializer serializer, Error clrError)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrError != null);

            writer.WriteStartObject();

            writer.WriteIdProperty(serializer, clrError.Id);
            writer.WriteLinksProperty(serializer, clrError.Links);
            writer.WriteStatusProperty(serializer, clrError.Status);
            writer.WriteCodeProperty(serializer, clrError.Code);
            writer.WriteTitleProperty(serializer, clrError.Title);
            writer.WriteDetailProperty(serializer, clrError.Detail);
            writer.WriteErrorSourceProperty(serializer, clrError.Source);
            writer.WriteMetaProperty(serializer, clrError.Meta);

            writer.WriteEndObject();
        }

        public static void WriteErrorSourceObject(this JsonWriter writer, JsonSerializer serializer, ErrorSource clrErrorSource)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrErrorSource != null);

            writer.WriteStartObject();

            writer.WriteParameterProperty(serializer, clrErrorSource.Parameter);
            writer.WritePointerProperty(serializer, clrErrorSource.Pointer);

            writer.WriteEndObject();
        }

        public static void WriteLinkObject(this JsonWriter writer, JsonSerializer serializer, Link clrLink)
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

            writer.WriteHRefProperty(serializer, clrLink.HRef);
            writer.WriteMetaProperty(serializer, clrLink.Meta);

            writer.WriteEndObject();
        }

        public static void WriteLinksObject(this JsonWriter writer, JsonSerializer serializer, Links clrLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLinks != null);

            writer.WriteStartObject();

            foreach (var clrLinkPair in clrLinks)
            {
                var rel = clrLinkPair.Key;
                var clrLink = clrLinkPair.Value;

                writer.WriteLinkProperty(serializer, rel, clrLink);
            }

            writer.WriteEndObject();
        }

        public static void WriteMetaObject(this JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrMeta != null);

            clrMeta.WriteJson(writer, serializer);
        }

        public static void WriteRelationshipObject(this JsonWriter writer, JsonSerializer serializer, Relationship clrRelationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrRelationship != null);

            writer.WriteStartObject();

            writer.WriteLinksProperty(serializer, clrRelationship.Links);
            var relationshipType = clrRelationship.GetRelationshipType();
            switch (relationshipType)
            {
                case RelationshipType.ToOneRelationship:
                {
                    var clrData = clrRelationship.GetToOneResourceLinkage();
                    writer.WriteDataProperty(serializer, clrData);
                }
                break;

                case RelationshipType.ToManyRelationship:
                {
                    var clrDataCollection = clrRelationship.GetToManyResourceLinkage();
                    writer.WriteDataProperty(serializer, clrDataCollection);
                }
                break;
            }
            writer.WriteMetaProperty(serializer, clrRelationship.Meta);

            writer.WriteEndObject();
        }

        public static void WriteRelationshipsObject(this JsonWriter writer, JsonSerializer serializer, Relationships clrRelationships)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrRelationships != null);

            writer.WriteStartObject();

            foreach (var clrRelationshipPair in clrRelationships)
            {
                var rel = clrRelationshipPair.Key;
                var clrRelationship = clrRelationshipPair.Value;

                writer.WriteRelationshipProperty(serializer, rel, clrRelationship);
            }

            writer.WriteEndObject();
        }

        public static void WriteResourceIdentifierObject(this JsonWriter writer, JsonSerializer serializer, ResourceIdentifier clrResourceIdentifier)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrResourceIdentifier != null);

            writer.WriteStartObject();

            writer.WriteTypeProperty(serializer, clrResourceIdentifier.Type);
            writer.WriteIdProperty(serializer, clrResourceIdentifier.Id);
            writer.WriteMetaProperty(serializer, clrResourceIdentifier.Meta);

            writer.WriteEndObject();
        }
        #endregion

        #region Write Property Methods
        public static void WriteCodeProperty(this JsonWriter writer, JsonSerializer serializer, string code)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Code, code);
        }

        public static void WriteDataProperty(this JsonWriter writer, JsonSerializer serializer, ResourceIdentifier clrData)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrData == null)
            {
                // Write 'null' direct ignoring the 'NullValueHandling' mode of the serializer.
                // Always write data as 'null' to capture the INTENT that the related to-one resource is empty.
                writer.WritePropertyName(Keywords.Data);
                writer.WriteToken(JsonToken.Null);
                return;
            }

            writer.WritePropertyName(Keywords.Data);
            writer.WriteResourceIdentifierObject(serializer, clrData);
        }

        public static void WriteDataProperty(this JsonWriter writer, JsonSerializer serializer, IEnumerable<ResourceIdentifier> clrDataCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WritePropertyName(Keywords.Data);
            writer.WriteResourceIdentifierArray(serializer, clrDataCollection);
        }

        public static void WriteDetailProperty(this JsonWriter writer, JsonSerializer serializer, string detail)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Detail, detail);
        }

        public static void WriteErrorSourceProperty(this JsonWriter writer, JsonSerializer serializer, ErrorSource clrErrorSource)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrErrorSource == null)
            {
                writer.WriteNullProperty(serializer, Keywords.Source);
                return;
            }

            writer.WritePropertyName(Keywords.Source);
            writer.WriteErrorSourceObject(serializer, clrErrorSource);
        }

        public static void WriteHRefProperty(this JsonWriter writer, JsonSerializer serializer, string hRef)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.HRef, hRef);
        }

        public static void WriteIdProperty(this JsonWriter writer, JsonSerializer serializer, string id)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Id, id);
        }

        public static void WriteLinkProperty(this JsonWriter writer, JsonSerializer serializer, string rel, Link link)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (link == null)
            {
                writer.WriteNullProperty(serializer, rel);
                return;
            }

            writer.WritePropertyName(rel);
            writer.WriteLinkObject(serializer, link);
        }

        public static void WriteLinksProperty(this JsonWriter writer, JsonSerializer serializer, Links clrLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrLinks == null)
            {
                writer.WriteNullProperty(serializer, Keywords.Links);
                return;
            }

            writer.WritePropertyName(Keywords.Links);
            writer.WriteLinksObject(serializer, clrLinks);
        }

        public static void WriteMetaProperty(this JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (clrMeta == null)
            {
                writer.WriteNullProperty(serializer, Keywords.Meta);
                return;
            }

            writer.WritePropertyName(Keywords.Meta);
            writer.WriteMetaObject(serializer, clrMeta);
        }

        public static void WriteParameterProperty(this JsonWriter writer, JsonSerializer serializer, string parameter)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Parameter, parameter);
        }

        public static void WritePointerProperty(this JsonWriter writer, JsonSerializer serializer, string pointer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Pointer, pointer);
        }

        public static void WriteRelationshipProperty(this JsonWriter writer, JsonSerializer serializer, string rel, Relationship relationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (relationship == null)
            {
                writer.WriteNullProperty(serializer, rel);
                return;
            }

            writer.WritePropertyName(rel);
            writer.WriteRelationshipObject(serializer, relationship);
        }

        public static void WriteStatusProperty(this JsonWriter writer, JsonSerializer serializer, HttpStatusCode? status)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (status.HasValue == false)
            {
                writer.WriteNullProperty(serializer, Keywords.Status);
                return;
            }

            writer.WritePropertyName(Keywords.Status);
            writer.WriteStatusValue(serializer, status.Value);
        }

        public static void WriteTitleProperty(this JsonWriter writer, JsonSerializer serializer, string title)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Title, title);
        }

        public static void WriteTypeProperty(this JsonWriter writer, JsonSerializer serializer, string type)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            writer.WriteStringProperty(serializer, Keywords.Type, type);
        }
        #endregion

        #region Write Value Methods
        public static void WriteStatusValue(this JsonWriter writer, JsonSerializer serializer, HttpStatusCode status)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var statusAsInteger = (int)status;
            var statusAsString = Convert.ToString(statusAsInteger);
            writer.WriteValue(statusAsString);
        }
        #endregion
    }
}
