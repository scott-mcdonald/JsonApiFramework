// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class LinksConverter : Converter<Links>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override Links DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrLinks = ReadLinksObject(jObject, serializer);
            return clrLinks;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Links clrLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrLinks != null);

            WriteLinksObject(writer, serializer, clrLinks);
        }
        #endregion
    }
}