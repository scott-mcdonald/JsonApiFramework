// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Metadata;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Metadata
{
    public class ServiceModelTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModelTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(JsonSerializationTestData))]
        public void TestJsonSerialize(JsonObjectSerializationUnitTestFactory jsonSerializationTest)
        {
            var data = jsonSerializationTest.Data;
            var factory = jsonSerializationTest.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(JsonSerializationTestData))]
        public void TestJsonDeserialize(JsonObjectSerializationUnitTestFactory jsonSerializationTest)
        {
            var data = jsonSerializationTest.Data;
            var factory = jsonSerializationTest.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(CreateComplexObjectTestData))]
        public void TestCreateComplexObject(IUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(CreateResourceObjectTestData))]
        public void TestCreateResourceObject(IUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(TryGetComplexTypeWithClrTypeTestData))]
        public void TestTryGetComplexTypeWithClrType(IUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(TryGetResourceTypeWithApiTypeTestData))]
        public void TestTryGetResourceTypeWithApiType(IUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(TryGetResourceTypeWithClrTypeTestData))]
        public void TestTryGetResourceTypeWithClrType(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IComplexType MailingAddressComplexType = new ComplexType<MailingAddress>(
            Factory.CreateAttributesInfo(
                Factory<MailingAddress>.CreateAttributeInfo("street-address", x => x.StreetAddress),
                Factory<MailingAddress>.CreateAttributeInfo("city", x => x.City),
                Factory<MailingAddress>.CreateAttributeInfo("state", x => x.State),
                Factory<MailingAddress>.CreateAttributeInfo("zip-code", x => x.ZipCode)));

        private static readonly IComplexType PhoneNumberComplexType = new ComplexType<PhoneNumber>(
            Factory.CreateAttributesInfo(
                Factory<PhoneNumber>.CreateAttributeInfo("area-code", x => x.AreaCode),
                Factory<PhoneNumber>.CreateAttributeInfo("number", x => x.Number)));

        private static readonly IResourceType ArticleResourceType = new ResourceType<Article>(
            Factory<Article>.CreateResourceIdentityInfo("articles", x => x.ArticleId),

            Factory.CreateAttributesInfo(
                Factory<Article>.CreateAttributeInfo("title", x => x.Title),
                Factory<Article>.CreateAttributeInfo("body", x => x.Body)),

            Factory<Article>.CreateRelationshipsInfo(x => x.Relationships,
                Factory<Article>.CreateRelationshipInfo("author", x => x.Author),
                Factory<Article>.CreateRelationshipInfo("comments", x => x.Comments)),

            Factory<Article>.CreateLinksInfo(x => x.Links));

        private static readonly IResourceType PersonResourceType = new ResourceType<Person>(
            Factory<Person>.CreateResourceIdentityInfo("people", x => x.PersonId),

            Factory.CreateAttributesInfo(
                Factory<Person>.CreateAttributeInfo("first-name", x => x.FirstName),
                Factory<Person>.CreateAttributeInfo("last-name", x => x.LastName),
                Factory<Person>.CreateAttributeInfo("mailing-address", x => x.MailingAddress),
                Factory<Person>.CreateAttributeInfo("phone-numbers", x => x.PhoneNumbers)),

            Factory.CreateRelationshipsInfo(),

            Factory.CreateLinksInfo());

        private static readonly IServiceModel TestServiceModel = new ServiceModel(
            "Test",
            new[]
            {
                MailingAddressComplexType,
                PhoneNumberComplexType
            },
            new[]
            {
                ArticleResourceType,
                PersonResourceType
            });

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
        {
            Converters = new JsonConverter[] { new StringEnumConverter() },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
        {
            Converters = new JsonConverter[] { new StringEnumConverter() },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include
        };

        public static readonly IEnumerable<object[]> JsonSerializationTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<IServiceModel>(x),
                    x => new JsonObjectDeserializeUnitTest<IServiceModel>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithTestServiceModelAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        TestServiceModel,
@"{
  ""Name"": ""Test"",
  ""ComplexTypes"": [
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests"",
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""street-address"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""StreetAddress"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""city"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""City"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""state"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""State"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""zip-code"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""ZipCode"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      }
    },
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests"",
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""area-code"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""AreaCode"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""number"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Number"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      }
    }
  ],
  ""ResourceTypes"": [
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Article, JsonApiFramework.Core.Tests"",
      ""ResourceIdentityInfo"": {
        ""ApiType"": ""articles"",
        ""ClrIdPropertyBinding"": {
          ""ClrPropertyName"": ""ArticleId"",
          ""ClrPropertyType"": ""System.Guid, mscorlib""
        }
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""title"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Title"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""body"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Body"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""ClrRelationshipsPropertyBinding"": {
          ""ClrPropertyName"": ""Relationships"",
          ""ClrPropertyType"": ""JsonApiFramework.JsonApi.Relationships, JsonApiFramework.Core""
        },
        ""RelationshipInfoCollection"": [
          {
            ""ApiRel"": ""author"",
            ""ApiCardinality"": ""ToOne"",
            ""ClrRelatedResourceType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests"",
            ""ClrRelatedResourcePropertyBinding"": {
              ""ClrPropertyName"": ""Author"",
              ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests""
            }
          },
          {
            ""ApiRel"": ""comments"",
            ""ApiCardinality"": ""ToMany"",
            ""ClrRelatedResourceType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests"",
            ""ClrRelatedResourcePropertyBinding"": {
              ""ClrPropertyName"": ""Comments"",
              ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests]], mscorlib""
            }
          }
        ]
      },
      ""LinksInfo"": {
        ""ClrLinksPropertyBinding"": {
          ""ClrPropertyName"": ""Links"",
          ""ClrPropertyType"": ""JsonApiFramework.JsonApi.Links, JsonApiFramework.Core""
        }
      }
    },
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests"",
      ""ResourceIdentityInfo"": {
        ""ApiType"": ""people"",
        ""ClrIdPropertyBinding"": {
          ""ClrPropertyName"": ""PersonId"",
          ""ClrPropertyType"": ""System.Int32, mscorlib""
        }
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""first-name"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""FirstName"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""last-name"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""LastName"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""mailing-address"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""MailingAddress"",
              ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests""
            }
          },
          {
            ""ApiAttributeName"": ""phone-numbers"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""PhoneNumbers"",
              ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests]], mscorlib""
            }
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""RelationshipInfoCollection"": []
      },
      ""LinksInfo"": {}
    }
  ]
}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<IServiceModel>(x),
                    x => new JsonObjectDeserializeUnitTest<IServiceModel>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithTestServiceModelAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        TestServiceModel,
@"{
  ""Name"": ""Test"",
  ""ComplexTypes"": [
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests"",
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""street-address"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""StreetAddress"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""city"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""City"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""state"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""State"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""zip-code"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""ZipCode"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      }
    },
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests"",
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""area-code"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""AreaCode"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""number"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Number"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      }
    }
  ],
  ""ResourceTypes"": [
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Article, JsonApiFramework.Core.Tests"",
      ""ResourceIdentityInfo"": {
        ""ApiType"": ""articles"",
        ""ClrIdPropertyBinding"": {
          ""ClrPropertyName"": ""ArticleId"",
          ""ClrPropertyType"": ""System.Guid, mscorlib""
        }
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""title"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Title"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""body"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""Body"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""ClrRelationshipsPropertyBinding"": {
          ""ClrPropertyName"": ""Relationships"",
          ""ClrPropertyType"": ""JsonApiFramework.JsonApi.Relationships, JsonApiFramework.Core""
        },
        ""RelationshipInfoCollection"": [
          {
            ""ApiRel"": ""author"",
            ""ApiCardinality"": ""ToOne"",
            ""ClrRelatedResourceType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests"",
            ""ClrRelatedResourcePropertyBinding"": {
              ""ClrPropertyName"": ""Author"",
              ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests""
            }
          },
          {
            ""ApiRel"": ""comments"",
            ""ApiCardinality"": ""ToMany"",
            ""ClrRelatedResourceType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests"",
            ""ClrRelatedResourcePropertyBinding"": {
              ""ClrPropertyName"": ""Comments"",
              ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests]], mscorlib""
            }
          }
        ]
      },
      ""LinksInfo"": {
        ""ClrLinksPropertyBinding"": {
          ""ClrPropertyName"": ""Links"",
          ""ClrPropertyType"": ""JsonApiFramework.JsonApi.Links, JsonApiFramework.Core""
        }
      }
    },
    {
      ""ClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests"",
      ""ResourceIdentityInfo"": {
        ""ApiType"": ""people"",
        ""ClrIdPropertyBinding"": {
          ""ClrPropertyName"": ""PersonId"",
          ""ClrPropertyType"": ""System.Int32, mscorlib""
        }
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""first-name"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""FirstName"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""last-name"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""LastName"",
              ""ClrPropertyType"": ""System.String, mscorlib""
            }
          },
          {
            ""ApiAttributeName"": ""mailing-address"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""MailingAddress"",
              ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests""
            }
          },
          {
            ""ApiAttributeName"": ""phone-numbers"",
            ""ClrPropertyBinding"": {
              ""ClrPropertyName"": ""PhoneNumbers"",
              ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests]], mscorlib""
            }
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""ClrRelationshipsPropertyBinding"": null,
        ""RelationshipInfoCollection"": []
      },
      ""LinksInfo"": {
        ""ClrLinksPropertyBinding"": null
      }
    }
  ]
}"))
            },
        };

        public static readonly IEnumerable<object[]> CreateComplexObjectTestData = new[]
        {
            new object[] { new CreateObjectUnitTest<MailingAddress>("WithKnownComplexType", s => s.CreateClrComplexObject<MailingAddress>(), TestServiceModel, new MailingAddress()) },
            new object[] { new CreateObjectUnitTest<Point>("WithUnknownComplexType", s => s.CreateClrComplexObject<Point>(), TestServiceModel, default(Point)) },
        };

        public static readonly IEnumerable<object[]> CreateResourceObjectTestData = new[]
        {
            new object[] { new CreateObjectUnitTest<Article>("WithKnownResourceType", s => s.CreateClrResourceObject<Article>(), TestServiceModel, new Article()) },
            new object[] { new CreateObjectUnitTest<Comment>("WithUnknownResourceType", s => s.CreateClrResourceObject<Comment>(), TestServiceModel, default(Comment)) },
        };

        public static readonly IEnumerable<object[]> TryGetComplexTypeWithClrTypeTestData = new[]
        {
            new object[] { new TryGetComplexTypeWithClrTypeUnitTest("WithKnownComplexType", TestServiceModel, typeof(MailingAddress), MailingAddressComplexType) },
            new object[] { new TryGetComplexTypeWithClrTypeUnitTest("WithUnknownComplexType", TestServiceModel, typeof(Comment), null) },
            new object[] { new TryGetComplexTypeWithClrTypeUnitTest("WithNullComplexType", TestServiceModel, null, null) },
        };

        public static readonly IEnumerable<object[]> TryGetResourceTypeWithApiTypeTestData = new[]
        {
            new object[] { new TryGetResourceTypeWithApiTypeUnitTest("WithKnownResourceType", TestServiceModel, "articles", ArticleResourceType) },
            new object[] { new TryGetResourceTypeWithApiTypeUnitTest("WithUnknownResourceType", TestServiceModel, "comments", null) },
            new object[] { new TryGetResourceTypeWithApiTypeUnitTest("WithNullResourceType", TestServiceModel, null, null) },
        };

        public static readonly IEnumerable<object[]> TryGetResourceTypeWithClrTypeTestData = new[]
        {
            new object[] { new TryGetResourceTypeWithClrTypeUnitTest("WithKnownResourceType", TestServiceModel, typeof(Article), ArticleResourceType) },
            new object[] { new TryGetResourceTypeWithClrTypeUnitTest("WithUnknownResourceType", TestServiceModel, typeof(Comment), null) },
            new object[] { new TryGetResourceTypeWithClrTypeUnitTest("WithNullResourceType", TestServiceModel, null, null) },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region ComplexType Types
        private class MailingAddress
        {
            // ReSharper disable UnusedMember.Local
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class PhoneNumber
        {
            // ReSharper disable UnusedMember.Local
            public string AreaCode { get; set; }
            public string Number { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Point
        {
            // ReSharper disable UnusedMember.Local
            public int X { get; set; }
            public int Y { get; set; }
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region ResourceType Types
        private class Article
        {
            // ReSharper disable UnusedMember.Local
            public Guid ArticleId { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public Person Author { get; set; }
            public List<Comment> Comments { get; set; }

            public Relationships Relationships { get; set; }
            public Links Links { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Person
        {
            // ReSharper disable UnusedMember.Local
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public MailingAddress MailingAddress { get; set; }
            public List<PhoneNumber> PhoneNumbers { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Comment
        {
            // ReSharper disable UnusedMember.Local
            public int CommentId { get; set; }
            public string Text { get; set; }
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region Unit Tests
        private class CreateObjectUnitTest<T> : UnitTest
        {
            #region Constructors
            public CreateObjectUnitTest(string name, Func<IServiceModel, T> factoryMethod, IServiceModel serviceModel, T clrExpectedObject)
                : base(name)
            {
                this.FactoryMethod = factoryMethod;
                this.ServiceModel = serviceModel;
                this.ClrExpectedObject = clrExpectedObject;
                this.ExpectedSuccess = clrExpectedObject != null;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                var serviceModelDescription = this.ServiceModel.ToString();
                this.WriteLine("ServiceModel: {0}", serviceModelDescription);

                var clrTypeName = typeof(T).Name;
                this.WriteLine("CLR Type    : {0}", clrTypeName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success   : {0}", this.ExpectedSuccess);
                this.WriteLine();
            }

            protected override void Act()
            {
                try
                {
                    var clrActualObject = this.FactoryMethod(this.ServiceModel);

                    this.ClrActualObject = clrActualObject;
                    this.ActualSuccess = this.ClrActualObject != null;
                }
                catch (MetadataException metadataException)
                {
                    this.ClrActualObject = default(T);
                    this.ActualSuccess = false;
                }

                this.WriteLine("Actual");
                this.WriteLine("  Success   : {0}", this.ActualSuccess);
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);
                this.ClrActualObject.ShouldBeEquivalentTo(this.ClrExpectedObject);
            }
            #endregion

            #region User Supplied Properties
            private Func<IServiceModel, T> FactoryMethod { get; }
            private IServiceModel ServiceModel { get; }
            private T ClrExpectedObject { get; }
            #endregion

            #region Calculated Properties
            private bool ExpectedSuccess { get; }
            private bool ActualSuccess { get; set; }
            private T ClrActualObject { get; set; }
            #endregion
        }

        private class TryGetComplexTypeWithClrTypeUnitTest : UnitTest
        {
            #region Constructors
            public TryGetComplexTypeWithClrTypeUnitTest(string name, IServiceModel serviceModel, Type clrComplexType, IComplexType expected)
                : base(name)
            {
                this.ServiceModel = serviceModel;
                this.ClrComplexType = clrComplexType;
                this.Expected = expected;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.Expected != null;

                var serviceModelDescription = this.ServiceModel.ToString();
                this.WriteLine("ServiceModel   : {0}", serviceModelDescription);

                var clrComplexTypeName = this.ClrComplexType != null ? this.ClrComplexType.Name : "null";
                this.WriteLine("CLR ComplexType: {0}", clrComplexTypeName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success: {0}", this.ExpectedSuccess);
                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ServiceModel.TryGetComplexType(this.ClrComplexType, out var actual);

                this.ActualSuccess = actualSuccess;
                this.Actual = actual;

                this.WriteLine("Actual");
                this.WriteLine("  Success: {0}", this.ActualSuccess);
                this.WriteLine();
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; }
            private Type ClrComplexType { get; }
            private IComplexType Expected { get; }
            #endregion

            #region Calculated Properties
            private bool ExpectedSuccess { get; set; }
            private bool ActualSuccess { get; set; }
            private IComplexType Actual { get; set; }
            #endregion
        }

        private class TryGetResourceTypeWithApiTypeUnitTest : UnitTest
        {
            #region Constructors
            public TryGetResourceTypeWithApiTypeUnitTest(string name, IServiceModel serviceModel, string apiResourceType, IResourceType expected)
                : base(name)
            {
                this.ServiceModel = serviceModel;
                this.ApiResourceType = apiResourceType;
                this.Expected = expected;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.Expected != null;

                var serviceModelDescription = this.ServiceModel.ToString();
                this.WriteLine("ServiceModel    : {0}", serviceModelDescription);

                var apiResourceTypeName = !String.IsNullOrWhiteSpace(this.ApiResourceType) ? this.ApiResourceType : "null";
                this.WriteLine("API ResourceType: {0}", apiResourceTypeName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success: {0}", this.ExpectedSuccess);
                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ServiceModel.TryGetResourceType(this.ApiResourceType, out var actual);

                this.ActualSuccess = actualSuccess;
                this.Actual = actual;

                this.WriteLine("Actual");
                this.WriteLine("  Success: {0}", this.ActualSuccess);
                this.WriteLine();
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; }
            private string ApiResourceType { get; }
            private IResourceType Expected { get; }
            #endregion

            #region Calculated Properties
            private bool ExpectedSuccess { get; set; }
            private bool ActualSuccess { get; set; }
            private IResourceType Actual { get; set; }
            #endregion
        }

        private class TryGetResourceTypeWithClrTypeUnitTest : UnitTest
        {
            #region Constructors
            public TryGetResourceTypeWithClrTypeUnitTest(string name, IServiceModel serviceModel, Type clrResourceType, IResourceType expected)
                : base(name)
            {
                this.ServiceModel = serviceModel;
                this.ClrResourceType = clrResourceType;
                this.Expected = expected;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.Expected != null;

                var serviceModelDescription = this.ServiceModel.ToString();
                this.WriteLine("ServiceModel    : {0}", serviceModelDescription);

                var clrResourceTypeName = this.ClrResourceType != null ? this.ClrResourceType.Name : "null";
                this.WriteLine("CLR ResourceType: {0}", clrResourceTypeName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success: {0}", this.ExpectedSuccess);
                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ServiceModel.TryGetResourceType(this.ClrResourceType, out var actual);

                this.ActualSuccess = actualSuccess;
                this.Actual = actual;

                this.WriteLine("Actual");
                this.WriteLine("  Success: {0}", this.ActualSuccess);
                this.WriteLine();
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; }
            private Type ClrResourceType { get; }
            private IResourceType Expected { get; }
            #endregion

            #region Calculated Properties
            private bool ExpectedSuccess { get; set; }
            private bool ActualSuccess { get; set; }
            private IResourceType Actual { get; set; }
            #endregion
        }
        #endregion
    }
}
