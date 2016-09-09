// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleStoreConfigurations
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly StoreConfiguration StoreConfiguration = new StoreConfiguration
            {
                StoreConfigurationId = "50-Configuration",
                IsLive = true,
                MailingAddress = new MailingAddress
                    {
                        Address = "1234 Main Street",
                        City = "Naples",
                        State = "FL",
                        ZipCode = "33935"
                    },
                PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                            {
                                AreaCode = "111",
                                Number = "222-3333"
                            },
                        new PhoneNumber
                            {
                                AreaCode = "555",
                                Number = "777-8888"
                            },
                    }
            };

        public static readonly StoreConfiguration Store50Configuration = new StoreConfiguration
            {
                StoreConfigurationId = "50-Configuration",
                IsLive = true,
                MailingAddress = new MailingAddress
                    {
                        Address = "1234 Main Street",
                        City = "Naples",
                        State = "FL",
                        ZipCode = "33935"
                    },
                PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                            {
                                AreaCode = "111",
                                Number = "222-3333"
                            },
                        new PhoneNumber
                            {
                                AreaCode = "555",
                                Number = "777-8888"
                            },
                    }
            };

        public static readonly StoreConfiguration Store51Configuration = new StoreConfiguration
            {
                StoreConfigurationId = "51-Configuration",
                IsLive = false,
                MailingAddress = new MailingAddress
                    {
                        Address = "9999 1st Avenue",
                        City = "Los Angeles",
                        State = "CA",
                        ZipCode = "90210"
                    },
                PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                            {
                                AreaCode = "444",
                                Number = "111-2222"
                            },
                        new PhoneNumber
                            {
                                AreaCode = "888",
                                Number = "444-5555"
                            },
                    }
            };
        #endregion
    }
}