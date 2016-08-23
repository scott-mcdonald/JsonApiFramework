// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AuditAttributeItem : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        [JsonProperty("date")] public DateTimeOffset Date { get; set; }
        [JsonProperty("by")] public string By { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}