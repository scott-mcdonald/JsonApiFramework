// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

namespace JsonApiFramework.TestData.ClrResources;

public class PosSystem : JsonObject
{
    public string PosSystemId { get; set; }
    public string PosSystemName { get; set; }
    public DateTime? EndOfLifeDate { get; set; }
}