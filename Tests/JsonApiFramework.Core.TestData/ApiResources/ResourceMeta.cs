// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceMeta : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        [JsonProperty("version")] public string Version { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}