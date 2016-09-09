// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleDocuments
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region JsonApi Documents
        public static readonly Document OrderResourceDocumentWithAllIncludedResources =
            new ResourceDocument
                {
                    Links = new Links
                        {
                            {Keywords.Self, "http://api.example.com/orders/1"}
                        },
                    Data = new Resource
                        {
                            Type = ClrSampleData.OrderType,
                            Id = "1",
                            Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                            Relationships = new Relationships
                                {
                                    {
                                        ClrSampleData.OrderToOrderItemsRel, new ToManyRelationship
                                            {
                                                Links = new Links
                                                    {
                                                        {
                                                            Keywords.Self,
                                                            "http://api.example.com/orders/1/relationships/line-items"
                                                        },
                                                        {
                                                            Keywords.Related,
                                                            "http://api.example.com/orders/1/line-items"
                                                        }
                                                    },
                                                Data = new List<ResourceIdentifier>
                                                    {
                                                        new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                                        new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")
                                                    }
                                            }
                                    },
                                    {
                                        ClrSampleData.OrderToPaymentsRel, new ToManyRelationship
                                            {
                                                Links = new Links
                                                    {
                                                        {
                                                            Keywords.Self,
                                                            "http://api.example.com/orders/1/relationships/payments"
                                                        },
                                                        {
                                                            Keywords.Related,
                                                            "http://api.example.com/orders/1/payments"
                                                        }
                                                    },
                                                Data = new List<ResourceIdentifier>
                                                    {
                                                        new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                                        new ResourceIdentifier(ClrSampleData.PaymentType, "102")
                                                    }
                                            }
                                    },
                                    {
                                        ClrSampleData.OrderToStoreRel, new ToOneRelationship
                                            {
                                                Links = new Links
                                                    {
                                                        {
                                                            Keywords.Self,
                                                            "http://api.example.com/orders/1/relationships/store"
                                                        },
                                                        {
                                                            Keywords.Related, "http://api.example.com/orders/1/store"
                                                        }
                                                    },
                                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                                            }
                                    },
                                },
                            Links = new Links
                                {
                                    {Keywords.Self, "http://api.example.com/orders/1"},
                                },
                        },
                    Included = new List<Resource>
                        {
                            new Resource
                                {
                                    Type = ClrSampleData.OrderItemType,
                                    Id = "1001",
                                    Attributes = new ApiObject(
                                        ApiProperty.Create("product-name", "Widget A"),
                                        ApiProperty.Create("quantity", 2),
                                        ApiProperty.Create("unit-price", 25.0m)),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.OrderItemToOrderRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/orders/1/line-items/1001/relationships/order"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/orders/1/line-items/1001/order"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                    }
                                            },
                                            {
                                                ClrSampleData.OrderItemToProductRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/orders/1/line-items/1001/relationships/product"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/orders/1/line-items/1001/product"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.ProductType, "501")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/orders/1/line-items/1001"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.OrderItemType,
                                    Id = "1002",
                                    Attributes = new ApiObject(
                                        ApiProperty.Create("product-name", "Widget B"),
                                        ApiProperty.Create("quantity", 1),
                                        ApiProperty.Create("unit-price", 50.0m)),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.OrderItemToOrderRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/orders/1/line-items/1002/relationships/order"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/orders/1/line-items/1002/order"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                    }
                                            },
                                            {
                                                ClrSampleData.OrderItemToProductRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/orders/1/line-items/1002/relationships/product"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/orders/1/line-items/1002/product"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.ProductType, "502")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/orders/1/line-items/1002"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.PaymentType,
                                    Id = "101",
                                    Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.PaymentToOrderRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/payments/101/relationships/order"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/payments/101/order"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/payments/101"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.PaymentType,
                                    Id = "102",
                                    Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.PaymentToOrderRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/payments/102/relationships/order"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/payments/102/order"
                                                                }
                                                            },
                                                        Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/payments/102"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.StoreType,
                                    Id = "50",
                                    Attributes = new ApiObject(
                                        ApiProperty.Create("store-name", "Store 50"),
                                        ApiProperty.Create("address", "1234 Main Street"),
                                        ApiProperty.Create("city", "Boynton Beach"),
                                        ApiProperty.Create("state", "FL"),
                                        ApiProperty.Create("zip-code", "33472")),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.StoreToStoreConfigurationRel, new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/stores/50/relationships/configuration"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/stores/50/configuration"
                                                                }
                                                            },
                                                        Data =
                                                            new ResourceIdentifier(
                                                                ClrSampleData.StoreConfigurationType,
                                                                "50-Configuration")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/stores/50"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.ProductType,
                                    Id = "501",
                                    Attributes = new ApiObject(
                                        ApiProperty.Create("name", "Widget A"),
                                        ApiProperty.Create("unit-price", 25.0m)),
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/products/501"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.ProductType,
                                    Id = "502",
                                    Attributes = new ApiObject(
                                        ApiProperty.Create("name", "Widget B"),
                                        ApiProperty.Create("unit-price", 50.0m)),
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/products/502"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.StoreConfigurationType,
                                    Id = "50-Configuration",
                                    Attributes = new ApiObject(
                                            ApiProperty.Create("is-live", SampleStoreConfigurations.StoreConfiguration.IsLive),
                                            ApiProperty.Create("mailing-address", new ApiObject(
                                                ApiProperty.Create("address", SampleStoreConfigurations.StoreConfiguration.MailingAddress.Address),
                                                ApiProperty.Create("city", SampleStoreConfigurations.StoreConfiguration.MailingAddress.City),
                                                ApiProperty.Create("state", SampleStoreConfigurations.StoreConfiguration.MailingAddress.State),
                                                ApiProperty.Create("zip-code", SampleStoreConfigurations.StoreConfiguration.MailingAddress.ZipCode))),
                                            ApiProperty.Create("phone-numbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers
                                                .Select(x =>
                                                    {
                                                        var apiObject = new ApiObject(
                                                            ApiProperty.Create("area-code", x.AreaCode),
                                                            ApiProperty.Create("number", x.Number));
                                                        return apiObject;
                                                    })
                                                .ToArray())),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.StoreToStoreConfigurationToPosSystemRel,
                                                new ToOneRelationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/stores/50/configuration/relationships/pos"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/stores/50/configuration/pos"
                                                                }
                                                            },
                                                        Data =
                                                            new ResourceIdentifier(ClrSampleData.PosSystemType,
                                                                "RadiantRest")
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/stores/50/configuration"},
                                        },
                                },
                            new Resource
                                {
                                    Type = ClrSampleData.PosSystemType,
                                    Id = "RadiantRest",
                                    Attributes = new ApiObject(ApiProperty.Create("pos-system-name", "Radiant REST-Based Api")),
                                    Relationships = new Relationships
                                        {
                                            {
                                                ClrSampleData.PosSystemToStoreConfigurationsRelPathSegment,
                                                new Relationship
                                                    {
                                                        Links = new Links
                                                            {
                                                                {
                                                                    Keywords.Self,
                                                                    "http://api.example.com/pos-systems/RadiantRest/relationships/store-configurations"
                                                                },
                                                                {
                                                                    Keywords.Related,
                                                                    "http://api.example.com/pos-systems/RadiantRest/store-configurations"
                                                                }
                                                            }
                                                    }
                                            },
                                        },
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/pos-systems/RadiantRest"},
                                        },
                                },
                        }
                };
        #endregion
    }
}