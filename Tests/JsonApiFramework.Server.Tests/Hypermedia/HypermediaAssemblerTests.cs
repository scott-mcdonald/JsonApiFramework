// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Server.Tests.Hypermedia
{
    public class HypermediaAssemblerTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaAssemblerTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateDocumentLinksTestData))]
        public void TestHypermediaAssemblerCreateDocumentLinks(string name, IHypermediaAssemblerTest test)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            test.Arrange();

            // Act
            test.Act();
            test.OutputTest(this);

            // Assert
            test.AssertTest();
        }

        [Theory]
        [MemberData(nameof(CreateResourceLinksTestData))]
        public void TestHypermediaAssemblerCreateResourceLinks(string name, IHypermediaAssemblerTest test)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            test.Arrange();

            // Act
            test.Act();
            test.OutputTest(this);

            // Assert
            test.AssertTest();
        }

        [Theory]
        [MemberData(nameof(CreateResourceRelationshipsTestData))]
        public void TestHypermediaAssemblerCreateResourceRelationships(string name, IHypermediaAssemblerTest test)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            test.Arrange();

            // Act
            test.Act();
            test.OutputTest(this);

            // Assert
            test.AssertTest();
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Constants
        private const string DocumentLinkFormatString = "Document [rel={0} href={1}]";
        private const string ResourceLinkFormatString = "Resource [rel={0} href={1}]";

        private static readonly ILinkContext UpLinkContext   = new LinkContext(Keywords.Up);
        private static readonly ILinkContext SelfLinkContext = new LinkContext(Keywords.Self);

        private static readonly ILinkContext UpLinkContextWithMeta   = new LinkContext(Keywords.Up,   ApiSampleData.LinkMeta);
        private static readonly ILinkContext SelfLinkContextWithMeta = new LinkContext(Keywords.Self, ApiSampleData.LinkMeta);
        #endregion

        #region Test Data
        public static readonly IUrlBuilderConfiguration UrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.example.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration UrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.example.com"
        };

        public static readonly IUrlBuilderConfiguration HomeUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.home.com"
        };

        public static readonly IUrlBuilderConfiguration OrdersUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.orders.com"
        };

        public static readonly IUrlBuilderConfiguration OrderItemsUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.order-items.com"
        };

        public static readonly IUrlBuilderConfiguration PaymentsUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.payments.com"
        };

        public static readonly IUrlBuilderConfiguration PosSystemsUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.pos-systems.com"
        };

        public static readonly IUrlBuilderConfiguration StoresUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.stores.com"
        };

        public static readonly IUrlBuilderConfiguration StoreConfigurationsUrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.store-configurations.com"
        };

        public static readonly IDictionary<Type, IUrlBuilderConfiguration> UrlBuilderConfigurationsPerResourceTypeWithoutRootPathSegments
            = new Dictionary<Type, IUrlBuilderConfiguration>
            {
                {typeof(Home), HomeUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(Order), OrdersUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(OrderItem), OrderItemsUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(Payment), PaymentsUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(PosSystem), PosSystemsUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(Store), StoresUrlBuilderConfigurationWithoutRootPathSegments},
                {typeof(StoreConfiguration), StoreConfigurationsUrlBuilderConfigurationWithoutRootPathSegments},
            };

        public static readonly IUrlBuilderConfiguration HomeUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.home.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration OrdersUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.orders.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration OrderItemsUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.order-items.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration PaymentsUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.payments.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration PosSystemsUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.pos-systems.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration StoresUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.stores.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IUrlBuilderConfiguration StoreConfigurationsUrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
        {
            Scheme = "http",
            Host   = "api.store-configurations.com",
            RootPathSegments = new[]
            {
                "api",
                "v2"
            }
        };

        public static readonly IDictionary<Type, IUrlBuilderConfiguration> UrlBuilderConfigurationsPerResourceTypeWithRootPathSegments
            = new Dictionary<Type, IUrlBuilderConfiguration>
            {
                {typeof(Home), HomeUrlBuilderConfigurationWithRootPathSegments},
                {typeof(Order), OrdersUrlBuilderConfigurationWithRootPathSegments},
                {typeof(OrderItem), OrderItemsUrlBuilderConfigurationWithRootPathSegments},
                {typeof(Payment), PaymentsUrlBuilderConfigurationWithRootPathSegments},
                {typeof(PosSystem), PosSystemsUrlBuilderConfigurationWithRootPathSegments},
                {typeof(Store), StoresUrlBuilderConfigurationWithRootPathSegments},
                {typeof(StoreConfiguration), StoreConfigurationsUrlBuilderConfigurationWithRootPathSegments},
            };

        public static readonly IHypermediaContext OrderBasedHypermediaContextWithoutRootPathSegments = new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments, null);
        public static readonly IHypermediaContext OrderBasedHypermediaContextWithRootPathSegments    = new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithRootPathSegments,    null);

        public static readonly IHypermediaContext OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments =
            new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments, UrlBuilderConfigurationsPerResourceTypeWithoutRootPathSegments);

        public static readonly IHypermediaContext OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments =
            new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments, UrlBuilderConfigurationsPerResourceTypeWithRootPathSegments);

        #region CreateDocumentLinksTestData
        public static readonly IEnumerable<object[]> CreateDocumentLinksTestData = new[]
        {
            new object[]
            {
                "WithoutRootPathSegmentsAndResourceCollectionPath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/payments"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourceCollectionPathAndMeta",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.example.com",
                            Meta = ApiSampleData.LinkMeta
                        }),
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.example.com/payments",
                            Meta = ApiSampleData.LinkMeta
                        }),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/payments/101",
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/payments"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/payments/101"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndMeta",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/payments/101",
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.example.com/payments",
                            Meta = ApiSampleData.LinkMeta
                        }),
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.example.com/payments/101",
                            Meta = ApiSampleData.LinkMeta
                        }),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathCanonical",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/payments/101/order",
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/payments/101"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/payments/101/order"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchical",
                new CreateDocumentLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/stores/50/configuration",
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/stores/50"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/stores/50/configuration"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                new CreateDocumentLinksTest<PosSystem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/stores/50/configuration/pos",
                    SamplePosSystems.PosSystem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/stores/50/configuration"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/stores/50/configuration/pos"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/orders/1/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/orders/1/payments"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourceCollectionPath",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/orders",
                    new[]
                    {
                        SampleOrders.Order1,
                        SampleOrders.Order2
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePath",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/orders/1",
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2/orders"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders/1"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders/1/line-items"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/orders/1/line-items/1001",
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2/orders/1/line-items"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders/1/line-items/1001"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndNonResourcePathAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndNonResourcePathAndResourcePathAndNonResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.example.com/en-us/order-items"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePath",
                new CreateDocumentLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    "http://api.example.com/en-us/order-items/1001",
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.example.com/en-us/order-items/1001"),
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourceCollectionPath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.payments.com/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.home.com"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.payments.com/payments"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourceCollectionPathAndMeta",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.payments.com/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.home.com",
                            Meta = ApiSampleData.LinkMeta
                        }),
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.payments.com/payments",
                            Meta = ApiSampleData.LinkMeta
                        }),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.payments.com/payments/101",
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.payments.com/payments"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.payments.com/payments/101"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndMeta",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.payments.com/payments/101",
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.payments.com/payments",
                            Meta = ApiSampleData.LinkMeta
                        }),
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.payments.com/payments/101",
                            Meta = ApiSampleData.LinkMeta
                        }),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathCanonical",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.payments.com/payments/101/order",
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.payments.com/payments/101"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.payments.com/payments/101/order"),
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchical",
                new CreateDocumentLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.stores.com/stores/50/configuration",
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.stores.com/stores/50"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.stores.com/stores/50/configuration"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                new CreateDocumentLinksTest<PosSystem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.store-configurations.com/stores/50/configuration/pos",
                    SamplePosSystems.PosSystem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.store-configurations.com/stores/50/configuration"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.store-configurations.com/stores/50/configuration/pos"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.orders.com/orders/1/payments",
                    new[]
                    {
                        SamplePayments.Payment101,
                        SamplePayments.Payment102
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.orders.com/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/orders/1/payments"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourceCollectionPath",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.orders.com/api/v2/orders",
                    new[]
                    {
                        SampleOrders.Order1,
                        SampleOrders.Order2
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.home.com/api/v2"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/orders"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePath",
                new CreateDocumentLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.orders.com/api/v2/orders/1",
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.orders.com/api/v2/orders"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/orders/1"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.orders.com/api/v2/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.orders.com/api/v2/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/orders/1/line-items"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.order-items.com/api/v2/orders/1/line-items/1001",
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.order-items.com/api/v2/orders/1/line-items"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.order-items.com/api/v2/orders/1/line-items/1001"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndNonResourcePathAndResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1/line-items"),
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndNonResourcePathAndResourcePathAndNonResourcePathAndToManyResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(UpLinkContext,   "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4"),
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourceCollectionPath",
                new CreateDocumentLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.order-items.com/orders/1/line-items",
                    new[]
                    {
                        SampleOrderItems.OrderItem1001,
                        SampleOrderItems.OrderItem1002
                    },
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.order-items.com/en-us/order-items"),
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePath",
                new CreateDocumentLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    "http://api.order-items.com/en-us/order-items/1001",
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.order-items.com/en-us/order-items/1001"),
                    })
            },
        };
        #endregion

        #region CreateResourceLinksTestData
        public static readonly IEnumerable<object[]> CreateResourceLinksTestData = new[]
        {
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePath",
                new CreateResourceLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/payments/101")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndMeta",
                new CreateResourceLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.example.com/payments/101",
                            Meta = ApiSampleData.LinkMeta
                        })
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathCanonical",
                new CreateResourceLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/orders/1")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchical",
                new CreateResourceLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/stores/50/configuration")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                new CreateResourceLinksTest<PosSystem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(PosSystem), ClrSampleData.PosSystemCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePosSystems.PosSystem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/pos-systems/RadiantWcf")
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePath",
                new CreateResourceLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders/1")
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePath",
                new CreateResourceLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/api/v2/orders/1/line-items/1001")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePath",
                new CreateResourceLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.example.com/en-us/order-items/1001")
                    })
            },


            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePath",
                new CreateResourceLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.payments.com/payments/101")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndMeta",
                new CreateResourceLinksTest<Payment>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePayments.Payment,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContextWithMeta, new Link
                        {
                            HRef = "http://api.payments.com/payments/101",
                            Meta = ApiSampleData.LinkMeta
                        })
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathCanonical",
                new CreateResourceLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/orders/1")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchical",
                new CreateResourceLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.store-configurations.com/stores/50/configuration")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                new CreateResourceLinksTest<PosSystem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(PosSystem), ClrSampleData.PosSystemCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SamplePosSystems.PosSystem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.pos-systems.com/pos-systems/RadiantWcf")
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsAndSingletonPath",
                new CreateResourceLinksTest<Search>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(
                        new IHypermediaPath[]
                        {
                            new SingletonHypermediaPath(typeof(Search), ClrSampleData.SearchCollectionPathSegment)
                        }, ResourcePathMode.IgnoreApiId),
                    new Search(),
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/search")
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsAndSingletonPathWithEmptyPathSegment",
                new CreateResourceLinksTest<Home>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(
                        new IHypermediaPath[]
                        {
                            new SingletonHypermediaPath(typeof(Home), ClrSampleData.HomeCollectionPathSegment)
                        }, ResourcePathMode.IgnoreApiId),
                    new Home(),
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com")
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsAndSingletonPathAndToOneResourcePathHierarchical",
                new CreateResourceLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(
                        new IHypermediaPath[]
                        {
                            new SingletonHypermediaPath(typeof(Search), ClrSampleData.SearchCollectionPathSegment),
                            new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                        }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/search/configuration")
                    })
            },

            new object[]
            {
                "WithoutRootPathSegmentsAndSingletonPathWithEmptyPathSegmentAndToOneResourcePathHierarchical",
                new CreateResourceLinksTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(
                        new IHypermediaPath[]
                        {
                            new SingletonHypermediaPath(typeof(Home), ClrSampleData.HomeCollectionPathSegment),
                            new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                        }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.example.com/configuration")
                    })
            },


            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePath",
                new CreateResourceLinksTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.orders.com/api/v2/orders/1")
                    })
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePath",
                new CreateResourceLinksTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(SelfLinkContext, "http://api.order-items.com/api/v2/orders/1/line-items/1001")
                    })
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePath",
                new CreateResourceLinksTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<ILinkContext, Link>(new LinkContext("en-us-canonical"), "http://api.order-items.com/en-us/order-items/1001")
                    })
            },
        };
        #endregion

        #region CreateResourceRelationshipsTestData
        public static readonly IEnumerable<object[]> CreateResourceRelationshipsTestData = new[]
        {
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndLinks",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/line-items")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/payments")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/store")}
                                }
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndData",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, null, new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}),
                            new ToManyRelationship
                            {
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, null, new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}),
                            new ToManyRelationship
                            {
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, null, new ResourceIdentifier(ClrSampleData.StoreType, "50")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathLinksAndData",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/line-items")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/payments")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.StoreType, "50")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/store")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndLinksAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/line-items")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/payments")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/store")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}, ApiSampleData.RelationshipMeta),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/line-items")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}, ApiSampleData.RelationshipMeta),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/payments")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.StoreType, "50"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.example.com/orders/1/store")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50"),
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },


            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePathAndLinks",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/order")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/product")}
                                }
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePathAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToOrderRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToProductRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePathAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, null, new ResourceIdentifier(ClrSampleData.OrderType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, null, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePathAndLinksAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.OrderType, "1")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/order")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsAndResourcePathAndToManyResourcePathAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.OrderType, "1"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/order")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                                         ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.example.com/api/v2/orders/1/line-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                    }
                )
            },


            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalAndLinks",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.example.com/stores/50/configuration/pos")}
                                }
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalAndMeta",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalAndData",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, null, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalAndLinksAndData",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.example.com/stores/50/configuration/pos")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndResourcePathAndToOneResourcePathHierarchicalAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest"),
                                                         ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.example.com/stores/50/configuration/pos")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest"),
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },


            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePathAndLinks",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext("en-us-product", new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/en-us/order-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.example.com/en-us/order-items/1001/product")}
                                }
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePathAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext("en-us-product", null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePathAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext("en-us-product", null, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsAndDerivedHypermediaAssemblerAndResourcePathAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext("en-us-product", new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com/en-us/order-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.example.com/en-us/order-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },


            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndLinks",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/line-items")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/payments")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/store")}
                                }
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndData",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, null, new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}),
                            new ToManyRelationship
                            {
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, null, new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}),
                            new ToManyRelationship
                            {
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, null, new ResourceIdentifier(ClrSampleData.StoreType, "50")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathLinksAndData",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/line-items")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/payments")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.StoreType, "50")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/store")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndLinksAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/line-items")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/payments")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/store")}
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<Order>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new[] {new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment)}, ResourcePathMode.IncludeApiId),
                    SampleOrders.Order,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToOrderItemsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"), new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")}, ApiSampleData.RelationshipMeta),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/line-items")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/line-items")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002"),
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToManyRelationshipContext(ClrSampleData.OrderToPaymentsRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)},
                                                          new[] {new ResourceIdentifier(ClrSampleData.PaymentType, "101"), new ResourceIdentifier(ClrSampleData.PaymentType, "102")}, ApiSampleData.RelationshipMeta),
                            new ToManyRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/payments")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/payments")}
                                },
                                Data = new List<ResourceIdentifier>
                                {
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102"),
                                },
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderToStoreRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.StoreType, "50"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.orders.com/orders/1/relationships/store")},
                                    {Keywords.Related, new Link("http://api.orders.com/orders/1/store")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.StoreType, "50"),
                                Meta = ApiSampleData.RelationshipMeta,
                            }),
                    }
                )
            },


            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePathAndLinks",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/order")}
                                }
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/product")}
                                }
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePathAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToOrderRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.OrderItemToProductRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePathAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, null, new ResourceIdentifier(ClrSampleData.OrderType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, null, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePathAndLinksAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.OrderType, "1")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/order")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithRootPathSegmentsPerResourceTypeAndResourcePathAndToManyResourcePathAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                        new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToOrderRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.OrderType, "1"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/order")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/order")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.OrderType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.OrderItemToProductRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                                         ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.order-items.com/api/v2/orders/1/line-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            }),
                    }
                )
            },


            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalAndLinks",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.store-configurations.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.store-configurations.com/stores/50/configuration/pos")}
                                }
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalAndMeta",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalAndData",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, null, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalAndLinksAndData",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.store-configurations.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.store-configurations.com/stores/50/configuration/pos")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndResourcePathAndToOneResourcePathHierarchicalAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<StoreConfiguration>(
                    new HypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                        new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment)
                    }, ResourcePathMode.IgnoreApiId),
                    SampleStoreConfigurations.StoreConfiguration,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest"),
                                                         ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.store-configurations.com/stores/50/configuration/relationships/pos")},
                                    {Keywords.Related, new Link("http://api.store-configurations.com/stores/50/configuration/pos")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest"),
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },


            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePathAndLinks",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext("en-us-product", new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}),
                            new Relationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/en-us/order-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.order-items.com/en-us/order-items/1001/product")}
                                }
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePathAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new RelationshipContext("en-us-product", null, ApiSampleData.RelationshipMeta),
                            new Relationship
                            {
                                Meta = ApiSampleData.RelationshipMeta,
                            })
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePathAndData",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext("en-us-product", null, new ResourceIdentifier(ClrSampleData.ProductType, "1")),
                            new ToOneRelationship
                            {
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1")
                            }),
                    }
                )
            },
            new object[]
            {
                "WithoutRootPathSegmentsPerResourceTypeAndDerivedHypermediaAssemblerAndResourcePathAndLinksAndDataAndMeta",
                new CreateResourceRelationshipsTest<OrderItem>(
                    new TestHypermediaAssembler(),
                    OrderBasedHypermediaContextPerResourceTypeWithoutRootPathSegments,
                    new ResourcePathContext(new IHypermediaPath[]
                    {
                        new ResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderItemCollectionPathSegment)
                    }, ResourcePathMode.IncludeApiId),
                    SampleOrderItems.OrderItem,
                    new[]
                    {
                        new Tuple<IRelationshipContext, Relationship>(
                            new ToOneRelationshipContext("en-us-product", new[] {new LinkContext(Keywords.Self), new LinkContext(Keywords.Related)}, new ResourceIdentifier(ClrSampleData.ProductType, "1"), ApiSampleData.RelationshipMeta),
                            new ToOneRelationship
                            {
                                Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.order-items.com/en-us/order-items/1001/relationships/product")},
                                    {Keywords.Related, new Link("http://api.order-items.com/en-us/order-items/1001/product")}
                                },
                                Data = new ResourceIdentifier(ClrSampleData.ProductType, "1"),
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    }
                )
            },
        };
        #endregion
        #endregion

        #region Test Types
        public class TestHypermediaAssembler : HypermediaAssembler<OrderItem>
        {
            #region HypermediaAssembler<TResource> Overrides
            protected override Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, IEnumerable<OrderItem> orderItemCollection, ILinkContext linkContext)
            {
                var rel  = linkContext.Rel;
                var meta = linkContext.Meta;
                switch (rel)
                {
                    case "en-us-canonical":
                    {
                        var hRef = hypermediaContext.CreateUrlBuilder<OrderItem>()
                                                    .Path("en-us")
                                                    .Path(ClrSampleData.OrderItemCollectionPathSegment)
                                                    .Build();
                        var link = new Link
                        {
                            HRef = hRef,
                            Meta = meta
                        };
                        return link;
                    }
                }

                return base.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, orderItemCollection, linkContext);
            }

            protected override Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, OrderItem orderItem, ILinkContext linkContext)
            {
                var rel  = linkContext.Rel;
                var meta = linkContext.Meta;
                switch (rel)
                {
                    case "en-us-canonical":
                    {
                        var orderItemId = orderItem.OrderItemId;
                        var hRef = hypermediaContext.CreateUrlBuilder<OrderItem>()
                                                    .Path("en-us")
                                                    .Path(ClrSampleData.OrderItemCollectionPathSegment)
                                                    .Path(orderItemId)
                                                    .Build();
                        var link = new Link
                        {
                            HRef = hRef,
                            Meta = meta
                        };
                        return link;
                    }
                }

                return base.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, orderItem, linkContext);
            }

            protected override Link CreateResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, OrderItem orderItem, ILinkContext linkContext)
            {
                Contract.Requires(hypermediaContext != null);
                Contract.Requires(resourcePathContext != null);
                Contract.Requires(orderItem != null);
                Contract.Requires(linkContext != null);

                var rel  = linkContext.Rel;
                var meta = linkContext.Meta;
                switch (rel)
                {
                    case "en-us-canonical":
                    {
                        var orderItemId = orderItem.OrderItemId;
                        var hRef = hypermediaContext.CreateUrlBuilder<OrderItem>()
                                                    .Path("en-us")
                                                    .Path(ClrSampleData.OrderItemCollectionPathSegment)
                                                    .Path(orderItemId)
                                                    .Build();
                        var link = new Link
                        {
                            HRef = hRef,
                            Meta = meta
                        };
                        return link;
                    }
                }

                return base.CreateResourceLink(hypermediaContext, resourcePathContext, orderItem, linkContext);
            }

            protected override Relationship CreateResourceRelationship(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, OrderItem orderItem, IRelationshipContext relationshipContext)
            {
                Contract.Requires(hypermediaContext != null);
                Contract.Requires(orderItem != null);
                Contract.Requires(relationshipContext != null);

                var rel = relationshipContext.Rel;
                switch (rel)
                {
                    case "en-us-product":
                    {
                        var orderItemId = orderItem.OrderItemId;

                        var links = default(Links);

                        var selfLinkContext = relationshipContext.LinkContexts.SingleOrDefault(x => x.Rel == Keywords.Self);
                        if (selfLinkContext != null)
                        {
                            var selfHRef = hypermediaContext.CreateUrlBuilder<OrderItem>()
                                                            .Path("en-us")
                                                            .Path(ClrSampleData.OrderItemCollectionPathSegment)
                                                            .Path(orderItemId)
                                                            .Path(Keywords.Relationships)
                                                            .Path(ClrSampleData.OrderItemToProductRel)
                                                            .Build();
                            var selfLink = new Link
                            {
                                HRef = selfHRef,
                                Meta = selfLinkContext.Meta
                            };

                            links = new Links
                            {
                                {Keywords.Self, selfLink}
                            };
                        }

                        var relatedLinkContext = relationshipContext.LinkContexts.SingleOrDefault(x => x.Rel == Keywords.Related);
                        if (relatedLinkContext != null)
                        {
                            var relatedHRef = hypermediaContext.CreateUrlBuilder<OrderItem>()
                                                               .Path("en-us")
                                                               .Path(ClrSampleData.OrderItemCollectionPathSegment)
                                                               .Path(orderItemId)
                                                               .Path(ClrSampleData.OrderItemToProductRel)
                                                               .Build();
                            var relatedLink = new Link
                            {
                                HRef = relatedHRef,
                                Meta = relatedLinkContext.Meta
                            };

                            links = links ?? new Links();
                            links.Add(Keywords.Related, relatedLink);
                        }

                        var relationshipType = relationshipContext.GetRelationshipType();
                        switch (relationshipType)
                        {
                            case RelationshipType.Relationship:
                            {
                                var relationship = new Relationship
                                {
                                    Links = links,
                                    Meta  = relationshipContext.Meta
                                };
                                return relationship;
                            }

                            case RelationshipType.ToOneRelationship:
                            {
                                var data = relationshipContext.GetToOneResourceLinkage();
                                var relationship = new ToOneRelationship
                                {
                                    Links = links,
                                    Data  = data,
                                    Meta  = relationshipContext.Meta
                                };
                                return relationship;
                            }

                            case RelationshipType.ToManyRelationship:
                            {
                                var data = relationshipContext.GetToManyResourceLinkage()
                                                              .ToList();
                                var relationship = new ToManyRelationship
                                {
                                    Links = links,
                                    Data  = data,
                                    Meta  = relationshipContext.Meta
                                };
                                return relationship;
                            }

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

                return base.CreateResourceRelationship(hypermediaContext, resourcePathContext, orderItem, relationshipContext);
            }
            #endregion
        }

        public interface IHypermediaAssemblerTest
        {
            void Arrange();
            void Act();
            void OutputTest(HypermediaAssemblerTests parent);
            void AssertTest();
        }

        public class CreateDocumentLinksTest<TResource> : IHypermediaAssemblerTest
            where TResource : class
        {
            #region Constructors
            public CreateDocumentLinksTest(
                IHypermediaAssembler                     hypermediaAssembler,
                IHypermediaContext                       hypermediaContext,
                string                                   currentRequestUri,
                TResource                                clrResource,
                IReadOnlyList<Tuple<ILinkContext, Link>> expectedDocumentLinkContextAndLinkTuples)
            {
                this.HypermediaAssembler                      = hypermediaAssembler;
                this.HypermediaContext                        = hypermediaContext;
                this.CurrentRequestUri                        = currentRequestUri;
                this.DocumentType                             = DocumentType.ResourceDocument;
                this.ClrResource                              = clrResource;
                this.ExpectedDocumentLinkContextAndLinkTuples = expectedDocumentLinkContextAndLinkTuples;
            }

            public CreateDocumentLinksTest(
                IHypermediaAssembler                     hypermediaAssembler,
                IHypermediaContext                       hypermediaContext,
                string                                   currentRequestUri,
                IReadOnlyList<TResource>                 clrResourceCollection,
                IReadOnlyList<Tuple<ILinkContext, Link>> expectedDocumentLinkContextAndLinkTuples)
            {
                this.HypermediaAssembler                      = hypermediaAssembler;
                this.HypermediaContext                        = hypermediaContext;
                this.CurrentRequestUri                        = currentRequestUri;
                this.DocumentType                             = DocumentType.ResourceCollectionDocument;
                this.ClrResourceCollection                    = clrResourceCollection;
                this.ExpectedDocumentLinkContextAndLinkTuples = expectedDocumentLinkContextAndLinkTuples;
            }
            #endregion

            #region IHypermediaAssemblerTest Implementation
            public void Arrange()
            {
                this.ClrResourceType = typeof(TResource);

                var documentPathContext = new DocumentPathContext(this.HypermediaContext, this.CurrentRequestUri);
                this.DocumentPathContext = documentPathContext;
            }

            public void Act()
            {
                var hypermediaContext   = this.HypermediaContext;
                var documentPathContext = this.DocumentPathContext;

                var apiDocumentType = this.DocumentType;
                switch (apiDocumentType)
                {
                    case DocumentType.ResourceDocument:
                    {
                        var actualDocumentLinkContextAndLinkTuples = this.ExpectedDocumentLinkContextAndLinkTuples
                                                                         .Select(x =>
                                                                         {
                                                                             var linkContext     = x.Item1;
                                                                             var clrResourceType = this.ClrResourceType;
                                                                             var clrResource     = this.ClrResource;
                                                                             var link            = this.HypermediaAssembler.CreateDocumentLink(hypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResource, linkContext);
                                                                             return new Tuple<ILinkContext, Link>(linkContext, link);
                                                                         })
                                                                         .ToList();
                        this.ActualDocumentLinkContextAndLinkTuples = actualDocumentLinkContextAndLinkTuples;
                    }
                        break;

                    case DocumentType.ResourceCollectionDocument:
                    {
                        var actualDocumentLinkContextAndLinkTuples = this.ExpectedDocumentLinkContextAndLinkTuples
                                                                         .Select(x =>
                                                                         {
                                                                             var linkContext           = x.Item1;
                                                                             var clrResourceType       = this.ClrResourceType;
                                                                             var clrResourceCollection = this.ClrResourceCollection;
                                                                             var link =
                                                                                 this.HypermediaAssembler.CreateDocumentLink(hypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResourceCollection, linkContext);
                                                                             return new Tuple<ILinkContext, Link>(linkContext, link);
                                                                         })
                                                                         .ToList();
                        this.ActualDocumentLinkContextAndLinkTuples = actualDocumentLinkContextAndLinkTuples;
                    }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public void OutputTest(HypermediaAssemblerTests parent)
            {
                // Expected Links
                parent.Output.WriteLine("Expected Document Links");

                var expectedDocumentLinkContextAndLinkTuples = this.ExpectedDocumentLinkContextAndLinkTuples;
                OutputLinks(parent, DocumentLinkFormatString, expectedDocumentLinkContextAndLinkTuples);

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Actual Links
                parent.Output.WriteLine("Actual Document Links");

                var actualDocumentLinks = this.ActualDocumentLinkContextAndLinkTuples;
                OutputLinks(parent, DocumentLinkFormatString, actualDocumentLinks);
            }

            public void AssertTest()
            {
                var expectedDocumentLinks = this.ExpectedDocumentLinkContextAndLinkTuples
                                                .Select(x => x.Item2)
                                                .ToList();
                var actualDocumentLinks = this.ActualDocumentLinkContextAndLinkTuples
                                              .Select(x => x.Item2)
                                              .ToList();
                LinkAssert.Equal(expectedDocumentLinks, actualDocumentLinks);
            }
            #endregion

            #region User Supplied Properties
            private IHypermediaAssembler                     HypermediaAssembler                      { get; set; }
            private IHypermediaContext                       HypermediaContext                        { get; set; }
            private string                                   CurrentRequestUri                        { get; set; }
            private DocumentType                             DocumentType                             { get; set; }
            private TResource                                ClrResource                              { get; set; }
            private IReadOnlyList<TResource>                 ClrResourceCollection                    { get; set; }
            private IReadOnlyList<Tuple<ILinkContext, Link>> ExpectedDocumentLinkContextAndLinkTuples { get; set; }
            #endregion

            #region Calculated Properties
            private Type                                     ClrResourceType                        { get; set; }
            private IDocumentPathContext                     DocumentPathContext                    { get; set; }
            private IReadOnlyList<Tuple<ILinkContext, Link>> ActualDocumentLinkContextAndLinkTuples { get; set; }
            #endregion

            #region Methods
            private static void OutputLinks(HypermediaAssemblerTests parent, string linkFormatString, IEnumerable<Tuple<ILinkContext, Link>> linkContextAndLinkTuples)
            {
                foreach (var linkContextAndLinkTuple in linkContextAndLinkTuples)
                {
                    var rel  = linkContextAndLinkTuple.Item1.Rel;
                    var link = linkContextAndLinkTuple.Item2;
                    parent.Output.WriteLine(linkFormatString, rel, link);
                }
            }

            private static void OutputLinks(HypermediaAssemblerTests parent, string linkFormatString, IEnumerable<Tuple<ILinkContext, IReadOnlyList<Link>>> linkContextAndLinkCollectionTuples)
            {
                foreach (var linkContextAndLinkTuple in linkContextAndLinkCollectionTuples)
                {
                    var rel            = linkContextAndLinkTuple.Item1.Rel;
                    var linkCollection = linkContextAndLinkTuple.Item2;
                    foreach (var link in linkCollection)
                    {
                        parent.Output.WriteLine(linkFormatString, rel, link);
                    }
                }
            }
            #endregion
        }

        public class CreateResourceLinksTest<TResource> : IHypermediaAssemblerTest
            where TResource : class
        {
            #region Constructors
            public CreateResourceLinksTest(
                IHypermediaAssembler                     hypermediaAssembler,
                IHypermediaContext                       hypermediaContext,
                IResourcePathContext                     resourcePathContext,
                TResource                                clrResource,
                IReadOnlyList<Tuple<ILinkContext, Link>> expectedResourceLinkContextAndLinkTuples)
            {
                this.HypermediaAssembler                      = hypermediaAssembler;
                this.HypermediaContext                        = hypermediaContext;
                this.ResourcePathContext                      = resourcePathContext;
                this.ClrResource                              = clrResource;
                this.ExpectedResourceLinkContextAndLinkTuples = expectedResourceLinkContextAndLinkTuples;
            }
            #endregion

            #region IHypermediaAssemblerTest Implementation
            public void Arrange()
            {
                this.ClrResourceType = typeof(TResource);
            }

            public void Act()
            {
                var hypermediaContext   = this.HypermediaContext;
                var resourcePathContext = this.ResourcePathContext;

                var actualResourceLinkContextAndLinkTuples = this.ExpectedResourceLinkContextAndLinkTuples
                                                                 .Select(x =>
                                                                 {
                                                                     var linkContext     = x.Item1;
                                                                     var clrResourceType = this.ClrResourceType;
                                                                     var clrResource     = this.ClrResource;
                                                                     var link            = this.HypermediaAssembler.CreateResourceLink(hypermediaContext, resourcePathContext, clrResourceType, clrResource, linkContext);
                                                                     return new Tuple<ILinkContext, Link>(linkContext, link);
                                                                 })
                                                                 .ToList();
                this.ActualResourceLinkContextAndLinkTuples = actualResourceLinkContextAndLinkTuples;
            }

            public void OutputTest(HypermediaAssemblerTests parent)
            {
                // Expected Links
                parent.Output.WriteLine("Expected Resource Links");
                var expectedResourceLinkContextAndLinkTuples = this.ExpectedResourceLinkContextAndLinkTuples;
                OutputLinks(parent, ResourceLinkFormatString, expectedResourceLinkContextAndLinkTuples);

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Actual Links
                parent.Output.WriteLine("Actual Resource Links");
                var actualResourceLinkContextAndLinkTuples = this.ActualResourceLinkContextAndLinkTuples;
                OutputLinks(parent, ResourceLinkFormatString, actualResourceLinkContextAndLinkTuples);
            }

            private static void OutputLinks(HypermediaAssemblerTests parent, string linkFormatString, IEnumerable<Tuple<ILinkContext, Link>> linkContextAndLinkTuples)
            {
                foreach (var linkContextAndLinkTuple in linkContextAndLinkTuples)
                {
                    var rel  = linkContextAndLinkTuple.Item1.Rel;
                    var link = linkContextAndLinkTuple.Item2;
                    parent.Output.WriteLine(linkFormatString, rel, link);
                }
            }

            private static void OutputLinks(HypermediaAssemblerTests parent, string linkFormatString, IEnumerable<Tuple<ILinkContext, IReadOnlyList<Link>>> linkContextAndLinkCollectionTuples)
            {
                foreach (var linkContextAndLinkTuple in linkContextAndLinkCollectionTuples)
                {
                    var rel            = linkContextAndLinkTuple.Item1.Rel;
                    var linkCollection = linkContextAndLinkTuple.Item2;
                    foreach (var link in linkCollection)
                    {
                        parent.Output.WriteLine(linkFormatString, rel, link);
                    }
                }
            }

            public void AssertTest()
            {
                var expectedResourceLinks = this.ExpectedResourceLinkContextAndLinkTuples
                                                .Select(x => x.Item2)
                                                .ToList();
                var actualResourceLinks = this.ActualResourceLinkContextAndLinkTuples
                                              .Select(x => x.Item2)
                                              .ToList();
                LinkAssert.Equal(expectedResourceLinks, actualResourceLinks);
            }
            #endregion

            #region User Supplied Properties
            private IHypermediaAssembler                     HypermediaAssembler                      { get; set; }
            private IHypermediaContext                       HypermediaContext                        { get; set; }
            private IResourcePathContext                     ResourcePathContext                      { get; set; }
            private TResource                                ClrResource                              { get; set; }
            private IReadOnlyList<Tuple<ILinkContext, Link>> ExpectedResourceLinkContextAndLinkTuples { get; set; }
            #endregion

            #region Calculated Properties
            private Type                                     ClrResourceType                        { get; set; }
            private IReadOnlyList<Tuple<ILinkContext, Link>> ActualResourceLinkContextAndLinkTuples { get; set; }
            #endregion
        }

        public class CreateResourceRelationshipsTest<TResource> : IHypermediaAssemblerTest
        {
            #region Constructors
            public CreateResourceRelationshipsTest(
                IHypermediaAssembler                                     hypermediaAssembler,
                IHypermediaContext                                       hypermediaContext,
                IResourcePathContext                                     resourcePathContext,
                TResource                                                resource,
                IReadOnlyList<Tuple<IRelationshipContext, Relationship>> expectedResourceRelationshipContextAndRelationshipTuples)
            {
                this.HypermediaAssembler                                      = hypermediaAssembler;
                this.HypermediaContext                                        = hypermediaContext;
                this.ResourcePathContext                                      = resourcePathContext;
                this.ClrResource                                              = resource;
                this.ExpectedResourceRelationshipContextAndRelationshipTuples = expectedResourceRelationshipContextAndRelationshipTuples;
            }
            #endregion

            #region IHypermediaAssemblerTest Implementation
            public void Arrange()
            {
                this.ClrResourceType = typeof(TResource);
            }

            public void Act()
            {
                var hypermediaContext   = this.HypermediaContext;
                var resourcePathContext = this.ResourcePathContext;

                var actualResourceRelationshipContextAndRelationshipTuples =
                    this.ExpectedResourceRelationshipContextAndRelationshipTuples
                        .Select(x =>
                        {
                            var relationshipContext = x.Item1;
                            var clrResourceType     = this.ClrResourceType;
                            var clrResource         = this.ClrResource;
                            var relationship        = this.HypermediaAssembler.CreateResourceRelationship(hypermediaContext, resourcePathContext, clrResourceType, clrResource, relationshipContext);
                            return new Tuple<IRelationshipContext, Relationship>(relationshipContext, relationship);
                        })
                        .ToList();
                this.ActualResourceRelationshipContextAndRelationshipTuples = actualResourceRelationshipContextAndRelationshipTuples;
            }

            public void OutputTest(HypermediaAssemblerTests parent)
            {
                // Expected Relationships
                parent.Output.WriteLine("Expected Relationships");

                var expectedResourceRelationshipContextAndRelationshipTuples = this.ExpectedResourceRelationshipContextAndRelationshipTuples;
                OutputRelationships(parent, expectedResourceRelationshipContextAndRelationshipTuples);

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Actual Relationships
                parent.Output.WriteLine("Actual Relationships");

                var actualResourceRelationshipContextAndRelationshipTuples = this.ActualResourceRelationshipContextAndRelationshipTuples;
                OutputRelationships(parent, actualResourceRelationshipContextAndRelationshipTuples);
            }

            private static void OutputRelationships(HypermediaAssemblerTests parent, IEnumerable<Tuple<IRelationshipContext, Relationship>> relationshipContextAndRelationshipTuples)
            {
                foreach (var relationshipContextAndRelationshipTuple in relationshipContextAndRelationshipTuples)
                {
                    var relationshipContext = relationshipContextAndRelationshipTuple.Item1;
                    var rel                 = relationshipContext.Rel;

                    var relationship         = relationshipContextAndRelationshipTuple.Item2;
                    var relationshipType     = relationship.GetType();
                    var relationshipTypeName = relationshipType.Name;

                    parent.Output.WriteLine("{0} [rel={1}]", relationshipTypeName, rel);

                    if (relationship.HasLinks())
                    {
                        foreach (var link in relationship.Links)
                        {
                            parent.Output.WriteLine("  Link: rel={0, -8} href={1}", link.Key, link.Value);
                        }
                    }

                    if (relationship.IsToOneRelationship())
                    {
                        var toOneResourceLinkage = relationship.GetToOneResourceLinkage();
                        parent.Output.WriteLine("  Data: {0}", toOneResourceLinkage);
                    }
                    else if (relationship.IsToManyRelationship())
                    {
                        var toManyResourceLinkage = relationship.GetToManyResourceLinkage();
                        foreach (var resourceLinkage in toManyResourceLinkage)
                        {
                            parent.Output.WriteLine("  Data: {0}", resourceLinkage);
                        }
                    }
                }
            }

            public void AssertTest()
            {
                var expectedResourceRelationships = this.ExpectedResourceRelationshipContextAndRelationshipTuples
                                                        .Select(x => x.Item2)
                                                        .ToList();
                var actualResourceRelationships = this.ActualResourceRelationshipContextAndRelationshipTuples
                                                      .Select(x => x.Item2)
                                                      .ToList();
                RelationshipAssert.Equal(expectedResourceRelationships, actualResourceRelationships);
            }
            #endregion

            #region User Supplied Properties
            private IHypermediaAssembler                                     HypermediaAssembler                                      { get; set; }
            private IHypermediaContext                                       HypermediaContext                                        { get; set; }
            private IResourcePathContext                                     ResourcePathContext                                      { get; set; }
            private TResource                                                ClrResource                                              { get; set; }
            private IReadOnlyList<Tuple<IRelationshipContext, Relationship>> ExpectedResourceRelationshipContextAndRelationshipTuples { get; set; }
            #endregion

            #region Calculated Properties
            private Type                                                     ClrResourceType                                        { get; set; }
            private IReadOnlyList<Tuple<IRelationshipContext, Relationship>> ActualResourceRelationshipContextAndRelationshipTuples { get; set; }
            #endregion
        }
        #endregion
    }
}