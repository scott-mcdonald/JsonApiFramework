// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AuditAttributes : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // JSON Object Type
        [JsonProperty("created")] public AuditAttributeItem Created { get; set; }

        // JSON Object Type
        [JsonProperty("modified")] public AuditAttributeItem Modified { get; set; }

        // JSON Array Type
        [JsonProperty("modified-history")] public AuditAttributeItem[] ModifiedHistory { get; set; }

        // JSON Object Type
        [JsonProperty("deleted")] public AuditAttributeItem Deleted { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}