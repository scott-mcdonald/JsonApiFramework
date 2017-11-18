// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class MetaConverter : JsonConverter<Meta>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override Meta DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrMeta = jObject.ReadMetaObject(serializer);
            return clrMeta;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrMeta != null);

            writer.WriteMetaObject(serializer, clrMeta);
        }
        #endregion
    }
}