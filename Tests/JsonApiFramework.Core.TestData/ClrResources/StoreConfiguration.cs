// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources;

public class StoreConfiguration : JsonObject
{
    public string StoreConfigurationId { get; set; }
    public bool IsLive { get; set; }
    public MailingAddress MailingAddress { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
}