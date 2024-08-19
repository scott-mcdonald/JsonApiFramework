// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json.Serialization;
using JsonApiFramework.Json;

namespace JsonApiFramework.TestData.ApiResources;
public class DocumentMeta : JsonObject
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [JsonPropertyName("is-public")] public bool IsPublic { get; set; }
    [JsonPropertyName("version")] public decimal Version { get; set; }
    [JsonPropertyName("copyright")] public string Copyright { get; set; }
    [JsonPropertyName("authors")] public string[] Authors { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}