﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.TestData.ClrResources;

public class Home : JsonObject
    , IGetLinks
    , ISetLinks
{
    public string Message { get; set; }
    public Links Links { get; set; }
}