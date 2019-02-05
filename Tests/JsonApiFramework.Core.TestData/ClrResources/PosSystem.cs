// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PosSystem : JsonObject
    {
        [JsonProperty] public string PosSystemId { get; set; }
        [JsonProperty] public string PosSystemName { get; set; }
        [JsonProperty] public DateTime? EndOfLifeDate { get; set; }
    }
}