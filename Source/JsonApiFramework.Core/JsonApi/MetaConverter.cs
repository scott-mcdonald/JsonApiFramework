// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class MetaConverter : Converter<Meta>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override Meta DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrMeta = ReadMetaObject(jObject, serializer);
            return clrMeta;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Meta clrMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrMeta != null);

            WriteMetaObject(writer, serializer, clrMeta);
        }
        #endregion
    }
}