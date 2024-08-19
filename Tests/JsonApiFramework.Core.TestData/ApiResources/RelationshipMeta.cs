// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json.Serialization;
using JsonApiFramework.Json;

namespace JsonApiFramework.TestData.ApiResources;

public class RelationshipMeta : JsonObject
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [JsonPropertyName("cascade-delete")] public bool CascadeDelete { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}