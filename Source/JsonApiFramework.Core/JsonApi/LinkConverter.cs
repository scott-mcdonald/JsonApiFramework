// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>Link</c> objects.
    /// </summary>
    public class LinkConverter : Converter<Link>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Link ReadString(string hRef)
        {
            var link = new Link
                {
                    HRef = hRef
                };
            return link;
        }

        protected override Link ReadObject(JObject relationshipJObject, JsonSerializer serializer)
        {
            Contract.Requires(relationshipJObject != null);
            Contract.Requires(serializer != null);

            var link = new Link();

            ReadHRef(relationshipJObject, serializer, link);
            ReadMeta(relationshipJObject, serializer, link);

            return link;
        }

        protected override void WriteObject(JsonWriter writer, JsonSerializer serializer, Link link)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(link != null);

            // If HRef only, then serialize the Link object as a string whose
            // value is the HRef value.
            if (link.Meta == null && String.IsNullOrWhiteSpace(link.HRef) == false)
            {
                var hRef = link.HRef;
                writer.WriteValue(hRef);
                return;
            }

            // Serialize the Link object as a JSON object.
            writer.WriteStartObject();

            WriteHRef(writer, serializer, link);
            WriteMeta(writer, serializer, link);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        // ReSharper disable once UnusedParameter.Local
        private static void ReadHRef(JToken linkJToken, JsonSerializer serializer, Link link)
        {
            Contract.Requires(linkJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(link != null);

            var hRef = ReadString(linkJToken, Keywords.HRef);
            link.HRef = hRef;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteHRef(JsonWriter writer, JsonSerializer serializer, Link link)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(link != null);

            WriteString(writer, Keywords.HRef, link.HRef);
        }
        #endregion
    }
}