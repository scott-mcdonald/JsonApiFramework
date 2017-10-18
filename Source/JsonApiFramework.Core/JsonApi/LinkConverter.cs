// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class LinkConverter : Converter<Link>
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

            var clrLink = ReadLinkObject(jObject, serializer);
            return clrLink;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Link clrLink)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLink != null);

            WriteLinkObject(writer, serializer, clrLink);
        }
        #endregion
    }
}