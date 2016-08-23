// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DocumentMeta : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        [JsonProperty("is-public")] public bool IsPublic { get; set; }
        [JsonProperty("version")] public decimal Version { get; set; }
        [JsonProperty("copyright")] public string Copyright { get; set; }
        [JsonProperty("authors")] public string[] Authors { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}