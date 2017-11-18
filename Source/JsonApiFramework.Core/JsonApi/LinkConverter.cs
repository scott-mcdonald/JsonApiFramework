﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class LinkConverter : JsonConverter<Link>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override Link DeserializeJsonString(string hRef)
        {
            var clrLink = new Link(hRef);
            return clrLink;
        }

        protected override Link DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrLink = jObject.ReadLinkObject(serializer);
            return clrLink;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Link clrLink)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLink != null);

            writer.WriteLinkObject(serializer, clrLink);
        }
        #endregion
    }
}