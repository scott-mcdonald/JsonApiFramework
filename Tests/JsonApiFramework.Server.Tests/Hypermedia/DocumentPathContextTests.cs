// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.Server.TestAsserts.Hypermedia;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Server.Tests.Hypermedia
{
    public class DocumentPathContextTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentPathContextTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("CreateWithUrlAndHypermediaContextTestData")]
        public void TestDocumentPathContextCreateWithUrlAndHypermediaContext(string name, IDocumentPathContextTest test)
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

        public static readonly IHypermediaContext HypermediaContextWithoutRootPathSegments = new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments, null);

        public static readonly IHypermediaContext HypermediaContextWithRootPathSegments = new HypermediaContext(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithRootPathSegments, null);

        public static readonly IEnumerable<object[]> CreateWithUrlAndHypermediaContextTestData = new[]
                                                                                                 {
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/payments",
                                                                                                                                                                      new[] {typeof(Payment)}, new IHypermediaPath[]
                                                                                                                                                                                               {
                                                                                                                                                                                                   new ResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment)
                                                                                                                                                                                               })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourcePath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/payments/101",
                                                                                                                                                                      new[] {typeof(Payment)}, new IHypermediaPath[]
                                                                                                                                                                                               {
                                                                                                                                                                                                   new ResourceHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment, "101")
                                                                                                                                                                                               })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourcePathAndToOneResourcePathCanonical",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/payments/101/order",
                                                                                                                                                                      new[] {typeof(Payment), typeof(Order)}, new IHypermediaPath[]
                                                                                                                                                                                                              {
                                                                                                                                                                                                                  new ResourceHypermediaPath(typeof(Payment), ClrSampleData.PaymentCollectionPathSegment, "101"),
                                                                                                                                                                                                                  new ToOneResourceHypermediaPath(typeof(Order), ClrSampleData.PaymentToOrderRelPathSegment),
                                                                                                                                                                                                              })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourcePathAndToOneResourcePathHierarchical",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/stores/50/configuration",
                                                                                                                                                                      new[] {typeof(Store), typeof(StoreConfiguration)}, new IHypermediaPath[]
                                                                                                                                                                                                                         {
                                                                                                                                                                                                                             new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                                                                                                                                                                                                                             new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                                                                                                                                                                                                                         })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/stores/50/configuration/pos",
                                                                                                                                                                      new[] {typeof(Store), typeof(StoreConfiguration), typeof(PosSystem)}, new IHypermediaPath[]
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                new ResourceHypermediaPath(typeof(Store), ClrSampleData.StoreCollectionPathSegment, "50"),
                                                                                                                                                                                                                                                new ToOneResourceHypermediaPath(typeof(StoreConfiguration), ClrSampleData.StoreToStoreConfigurationRelPathSegment),
                                                                                                                                                                                                                                                new ToOneResourceHypermediaPath(typeof(PosSystem),          ClrSampleData.StoreToStoreConfigurationToPosSystemRelPathSegment),
                                                                                                                                                                                                                                            })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfResourcePathAndToManyResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithoutRootPathSegments,
                                                                                                                                                                      "http://api.example.com/orders/1/payments",
                                                                                                                                                                      new[] {typeof(Order), typeof(Payment)}, new IHypermediaPath[]
                                                                                                                                                                                                              {
                                                                                                                                                                                                                  new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                                                  new ToManyResourceCollectionHypermediaPath(typeof(Payment), ClrSampleData.OrderToPaymentsRelPathSegment),
                                                                                                                                                                                                              })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/orders",
                                                                                                                                                                      new[] {typeof(Order)}, new IHypermediaPath[]
                                                                                                                                                                                             {
                                                                                                                                                                                                 new ResourceCollectionHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment),
                                                                                                                                                                                             })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndResourcePath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/orders/1",
                                                                                                                                                                      new[] {typeof(Order)}, new IHypermediaPath[]
                                                                                                                                                                                             {
                                                                                                                                                                                                 new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                             })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndResourcePathAndToManyResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/orders/1/line-items",
                                                                                                                                                                      new[] {typeof(Order), typeof(OrderItem)}, new IHypermediaPath[]
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                                                    new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment),
                                                                                                                                                                                                                })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndResourcePathAndToManyResourcePath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/orders/1/line-items/1001",
                                                                                                                                                                      new[] {typeof(Order), typeof(OrderItem)}, new IHypermediaPath[]
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                                                    new ToManyResourceHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment, "1001"),
                                                                                                                                                                                                                })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndNonResourcePathAndResourcePathAndToManyResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items",
                                                                                                                                                                      new[] {typeof(Order), typeof(OrderItem)}, new IHypermediaPath[]
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    new NonResourceHypermediaPath(new[] {"nrp-1", "nrp-2"}),
                                                                                                                                                                                                                    new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                                                    new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment),
                                                                                                                                                                                                                })
                                                                                                     },
                                                                                                     new object[]
                                                                                                     {
                                                                                                         "WithUrlComposedOfRootPathSegmentsAndNonResourcePathAndResourcePathAndNonResourcePathAndToManyResourceCollectionPath",
                                                                                                         new DocumentPathContextCreateWithUrlAndHypermediaContextTest(HypermediaContextWithRootPathSegments,
                                                                                                                                                                      "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items",
                                                                                                                                                                      new[] {typeof(Order), typeof(OrderItem)}, new IHypermediaPath[]
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    new NonResourceHypermediaPath(new[] {"nrp-1", "nrp-2"}),
                                                                                                                                                                                                                    new ResourceHypermediaPath(typeof(Order), ClrSampleData.OrderCollectionPathSegment, "1"),
                                                                                                                                                                                                                    new NonResourceHypermediaPath(new[] {"nrp-3", "nrp-4"}),
                                                                                                                                                                                                                    new ToManyResourceCollectionHypermediaPath(typeof(OrderItem), ClrSampleData.OrderToOrderItemsRelPathSegment),
                                                                                                                                                                                                                })
                                                                                                     }
                                                                                                 };
        #endregion

        #region Test Types
        public interface IDocumentPathContextTest
        {
            void Arrange();
            void Act();
            void OutputTest(DocumentPathContextTests parent);
            void AssertTest();
        }

        public class DocumentPathContextCreateWithUrlAndHypermediaContextTest : IDocumentPathContextTest
        {
            #region Constructors
            public DocumentPathContextCreateWithUrlAndHypermediaContextTest(IHypermediaContext hypermediaContext, string urlString, IEnumerable<Type> expectedClrResourceTypes, IEnumerable<IHypermediaPath> expectedDocumentSelfPath)
            {
                this.HypermediaContext        = hypermediaContext;
                this.UrlString                = urlString;
                this.ExpectedClrResourceTypes = expectedClrResourceTypes;
                this.ExpectedDocumentSelfPath = expectedDocumentSelfPath;
            }
            #endregion

            #region IHypermediaContextTest Implementation
            public void Arrange()
            {
            }

            public void Act()
            {
                var documentPathContext = new DocumentPathContext(this.HypermediaContext, this.UrlString);
                this.DocumentPathContext = documentPathContext;

                this.ActualClrResourceTypes = documentPathContext.ClrResourceTypes;
                this.ActualDocumentSelfPath = documentPathContext.DocumentSelfPath;
            }

            public void OutputTest(DocumentPathContextTests parent)
            {
                // URL
                parent.Output.WriteLine(this.UrlString);
                parent.Output.WriteLine(String.Empty);

                // Expected CLR Resource Types
                parent.Output.WriteLine("Expected CLR Resource Types");
                foreach (var clrResourceTypeTextLine in this.ExpectedClrResourceTypes.Select(clrResourceType => "    {0, -40}".FormatWith(clrResourceType.Name)))
                {
                    parent.Output.WriteLine(clrResourceTypeTextLine);
                }

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Expected Paths
                parent.Output.WriteLine("Expected Document Self Path");
                OutputHypermediaPathCollection(parent, this.ExpectedDocumentSelfPath);

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Expected CLR Resource Types
                parent.Output.WriteLine("Actual CLR Resource Types");
                foreach (var clrResourceTypeTextLine in this.ActualClrResourceTypes.Select(clrResourceType => "    {0, -40}".FormatWith(clrResourceType.Name)))
                {
                    parent.Output.WriteLine(clrResourceTypeTextLine);
                }

                // Blank Line
                parent.Output.WriteLine(String.Empty);

                // Actual Paths
                parent.Output.WriteLine("Actual Document Self Path");
                OutputHypermediaPathCollection(parent, this.ActualDocumentSelfPath);
            }

            private static void OutputHypermediaPathCollection(DocumentPathContextTests parent, IEnumerable<IHypermediaPath> hypermediaPathCollection)
            {
                if (hypermediaPathCollection == null)
                    return;

                foreach (var hypermediaPath in hypermediaPathCollection)
                {
                    var hypermediaPathType     = hypermediaPath.GetType();
                    var hypermediaPathTypeName = hypermediaPathType.Name;

                    var clrResourceTypeName = hypermediaPath.HasClrResourceType() ? hypermediaPath.GetClrResourceType().Name : "null";
                    var path = hypermediaPath.PathSegments.Any()
                        ? hypermediaPath.PathSegments.Aggregate((current, next) => current + "/" + next)
                        : String.Empty;

                    var resourceTypePathTextLine = "    {0, -40} {1, -24} {2, -24}".FormatWith(hypermediaPathTypeName, clrResourceTypeName, path);
                    parent.Output.WriteLine(resourceTypePathTextLine);
                }
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedClrResourceTypes, this.ActualClrResourceTypes);
                HypermediaPathAssert.Equal(this.ExpectedDocumentSelfPath, this.ActualDocumentSelfPath);
            }
            #endregion

            #region User Supplied Properties
            private IHypermediaContext           HypermediaContext        { get; set; }
            private string                       UrlString                { get; set; }
            private IEnumerable<Type>            ExpectedClrResourceTypes { get; set; }
            private IEnumerable<IHypermediaPath> ExpectedDocumentSelfPath { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentPathContext         DocumentPathContext    { get; set; }
            private IEnumerable<Type>            ActualClrResourceTypes { get; set; }
            private IEnumerable<IHypermediaPath> ActualDocumentSelfPath { get; set; }
            #endregion
        }
        #endregion
    }
}