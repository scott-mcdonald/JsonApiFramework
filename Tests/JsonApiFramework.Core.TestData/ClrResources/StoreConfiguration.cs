// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.TestData.ClrResources.ComplexTypes;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StoreConfiguration : JsonObject, IResource
    {
        [JsonProperty] public string StoreConfigurationId { get; set; }
        [JsonProperty] public bool IsLive { get; set; }
        [JsonProperty] public MailingAddress MailingAddress { get; set; }
        [JsonProperty] public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}