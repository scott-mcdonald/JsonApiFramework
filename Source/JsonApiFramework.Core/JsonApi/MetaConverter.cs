// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>Meta</c> objects.
    /// </summary>
    public class MetaConverter : Converter<Meta>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Meta ReadObject(JObject metaJObject, JsonSerializer serializer)
        {
            Contract.Requires(metaJObject != null);
            Contract.Requires(serializer != null);

            var meta = (Meta)metaJObject;
            return meta;
        }

        protected override void WriteObject(JsonWriter writer, JsonSerializer serializer, Meta meta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(meta != null);

            var metaJObject = (JObject)meta;
            metaJObject.WriteTo(writer);
        }
        #endregion
    }
}