// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Blog : JsonObject
        , IGetLinks
        , IGetMeta
        , IGetRelationships
        , ISetLinks
        , ISetMeta
        , ISetRelationships
    {
        [JsonProperty] public string Id { get; set; }
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public Relationships Relationships { get; set; }
        [JsonProperty] public Links Links { get; set; }
        [JsonProperty] public Meta Meta { get; set; }
    }
}