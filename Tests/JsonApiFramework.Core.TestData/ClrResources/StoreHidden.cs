// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

namespace JsonApiFramework.TestData.ClrResources;

public class StoreHidden : JsonObject
{
    public long StoreHiddenId { get; set; }
    public string StoreHiddenName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Hidden { get; set; }
}