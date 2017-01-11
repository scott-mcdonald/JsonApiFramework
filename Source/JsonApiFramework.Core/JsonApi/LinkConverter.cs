// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>Link</c> objects.
    /// </summary>
    public class LinkConverter : Converter<Link>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinkConverter()
            : base(default(IServiceModel))
        { }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Link ReadTypedString(string hRef)
        {
            var link = new Link
            {
                HRef = hRef
            };
            return link;
        }

        protected override Link ReadTypedObject(JObject linkJObject, JsonSerializer serializer)
        {
            Contract.Requires(linkJObject != null);
            Contract.Requires(serializer != null);

            var link = new Link();

            ReadHRef(linkJObject, serializer, link);
            ReadMeta(linkJObject, serializer, link);

            return link;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, Link link)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(link != null);

            // Serialize the Link object as a string for the special case
            // were Meta is null, HRef is not null, and the current serializer
            // settings are set to ignore null values.
            var isMetaNull = link.Meta == null;
            var isHRefNotNull = link.HRef != null;
            var isNullValueHandlingIgnore = serializer.NullValueHandling == NullValueHandling.Ignore;
            if (isMetaNull && isHRefNotNull && isNullValueHandlingIgnore)
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

            var hRef = link.HRef;
            WriteString(writer, serializer, Keywords.HRef, hRef);
        }
        #endregion
    }
}