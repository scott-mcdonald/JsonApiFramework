// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.TestData.ClrResources;

public class Article : JsonObject
    , IGetLinks
    , IGetMeta
    , IGetRelationships
    , ISetLinks
    , ISetMeta
    , ISetRelationships
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Relationships Relationships { get; set; }
    public Links Links { get; set; }
    public Meta Meta { get; set; }
}