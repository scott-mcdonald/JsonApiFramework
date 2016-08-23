// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ApiResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PersonAttributes : JsonObject
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        [JsonProperty(ApiSampleData.PersonFirstNamePropertyName)] public string FirstName { get; set; }
        [JsonProperty(ApiSampleData.PersonLastNamePropertyName)] public string LastName { get; set; }
        [JsonProperty(ApiSampleData.PersonTwitterPropertyName)] public string Twitter { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}