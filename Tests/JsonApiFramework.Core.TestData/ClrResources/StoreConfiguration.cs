// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PhoneNumber : JsonObject
    {
        [JsonProperty("area-code")] public string AreaCode { get; set; }
        [JsonProperty("number")] public string Number { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MailingAddress : JsonObject
    {
        [JsonProperty("address")] public string Address { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("state")] public string State { get; set; }
        [JsonProperty("zip-code")] public string ZipCode { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class StoreConfigurationAttributes : JsonObject
    {
        [JsonProperty("is-live")] public bool IsLive { get; set; }
        [JsonProperty("mailing-address")] public MailingAddress MailingAddress { get; set; }
        [JsonProperty("phone-numbers")] public List<PhoneNumber> PhoneNumbers { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class StoreConfiguration : JsonObject, IResource
    {
        [JsonProperty] public string StoreConfigurationId { get; set; }
        [JsonProperty] public bool IsLive { get; set; }
        [JsonProperty] public MailingAddress MailingAddress { get; set; }
        [JsonProperty] public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}