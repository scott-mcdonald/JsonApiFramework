// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json.Serialization;
using JsonApiFramework.Json;

namespace JsonApiFramework.TestData.ApiResources;

public class AuditAttributes : JsonObject
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // JSON Object Type
    [JsonPropertyName("created")] public AuditAttributeItem Created { get; set; }

    // JSON Object Type
    [JsonPropertyName("modified")] public AuditAttributeItem Modified { get; set; }

    // JSON Array Type
    [JsonPropertyName("modified-history")] public AuditAttributeItem[] ModifiedHistory { get; set; }

    // JSON Object Type
    [JsonPropertyName("deleted")] public AuditAttributeItem Deleted { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}