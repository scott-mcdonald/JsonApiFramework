// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal interface IDomWriteable
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings);
        #endregion
    }
}