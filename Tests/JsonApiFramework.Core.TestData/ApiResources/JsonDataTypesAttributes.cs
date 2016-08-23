// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonDataTypesAttributes : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // JSON String Type
        [JsonProperty("title")] public string Title { get; set; }

        // JSON Boolean Type
        [JsonProperty("is-online")] public bool IsOnline { get; set; }

        // JSON Number Type
        [JsonProperty("version")] public decimal Version { get; set; }
        [JsonProperty("line-count")] public int LineCount { get; set; }

        // JSON Array Type
        [JsonProperty("tags")] public string[] Tags { get; set; }

        // JSON Object Type
        [JsonProperty("audit")] public AuditAttributes Audit { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}