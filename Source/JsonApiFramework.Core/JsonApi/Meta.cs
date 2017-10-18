// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts an immutable json:api meta object.</summary>
    [JsonConverter(typeof(MetaConverter))]
    public abstract class Meta : JsonObject
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static Meta Create<T>(T clrObject)
        {
            var writeMeta = new WriteMeta<T>(clrObject);
            return writeMeta;
        }
        #endregion

        #region Meta Overrides
        public abstract T GetObject<T>();
        #endregion

        // PROTECTED/INTERNAL METHODS ///////////////////////////////////////
        #region Meta Overrides
        protected internal abstract void WriteJson(JsonWriter writer, JsonSerializer serializer);
        #endregion
    }
}