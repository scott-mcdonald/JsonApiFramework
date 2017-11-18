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
            new AttributesInfo(
                new[]
                {
                    new AttributeInfo(
                        "street-address",
                        nameof(MailingAddress.StreetAddress),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<MailingAddress, string>(nameof(MailingAddress.StreetAddress)),
                            new PropertySetter<MailingAddress, string>(nameof(MailingAddress.StreetAddress)))),

                    new AttributeInfo(
                        "city",
                        nameof(MailingAddress.City),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<MailingAddress, string>(nameof(MailingAddress.City)),
                            new PropertySetter<MailingAddress, string>(nameof(MailingAddress.City)))),

                    new AttributeInfo(
                        "state",
                        nameof(MailingAddress.State),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<MailingAddress, string>(nameof(MailingAddress.State)),
                            new PropertySetter<MailingAddress, string>(nameof(MailingAddress.State)))),

                    new AttributeInfo(
                        "zip-code",
                        nameof(MailingAddress.ZipCode),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<MailingAddress, string>(nameof(MailingAddress.ZipCode)),
                            new PropertySetter<MailingAddress, string>(nameof(MailingAddress.ZipCode)))),
                })
        );

        private static readonly IResourceType ArticleResourceType = new ResourceType<Article>(
            new ResourceIdentityInfo(
                "articles",
                nameof(Article.ArticleId),
                typeof(int),
                new ClrPropertyBinding(
                    new PropertyGetter<Article, int>(nameof(Article.ArticleId)),
                    new PropertySetter<Article, int>(nameof(Article.ArticleId)))),

            new AttributesInfo(
                new[]
                {
                    new AttributeInfo(
                        "title",
                        nameof(Article.Title),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<Article, string>(nameof(Article.Title)),
                            new PropertySetter<Article, string>(nameof(Article.Title)))),

                    new AttributeInfo(
                        "author",
                        nameof(Article.Author),
                        typeof(string),
                        new ClrPropertyBinding(
                            new PropertyGetter<Article, string>(nameof(Article.Author)),
                            new PropertySetter<Article, string>(nameof(Article.Author)))),

                    new AttributeInfo(
                        "author-mailing-address",
                        nameof(Article.AuthorMailingAddress),
                        typeof(MailingAddress),
                        new ClrPropertyBinding(
                            new PropertyGetter<Article, MailingAddress>(nameof(Article.AuthorMailingAddress)),
                            new PropertySetter<Article, MailingAddress>(nameof(Article.AuthorMailingAddress)))),

                    new AttributeInfo(
                        "author-phone-numbers",
                        nameof(Article.AuthorPhoneNumbers),
                        typeof(List<PhoneNumber>),
                        new ClrPropertyBinding(
                            new PropertyGetter<Article, List<PhoneNumber>>(nameof(Article.AuthorPhoneNumbers)),
                            new PropertySetter<Article, List<PhoneNumber>>(nameof(Article.AuthorPhoneNumbers)))),
                }),
            new RelationshipsInfo(
                nameof(Article.Relationships),
                new ClrPropertyBinding(
                    new PropertyGetter<Article, Relationships>(nameof(Article.Relationships)),
                    new PropertySetter<Article, Relationships>(nameof(Article.Relationships))),
                new []
                {
                    new RelationshipInfo("author", RelationshipCardinality.ToOne, typeof(Person)),
                    new RelationshipInfo("comments", RelationshipCardinality.ToMany, typeof(Comment)),
                }),
            new LinksInfo(
                nameof(Article.Links),
                new ClrPropertyBinding(
                    new PropertyGetter<Article, Links>(nameof(Article.Links)),
                    new PropertySetter<Article, Links>(nameof(Article.Links)))));

        private static readonly IServiceModel TestServiceModel = new ServiceModel("Test", new[] { MailingAddressComplexType }, new[] { ArticleResourceType });

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
            ""ClrPropertyName"": ""StreetAddress"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""city"",
            ""ClrPropertyName"": ""City"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""state"",
            ""ClrPropertyName"": ""State"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""zip-code"",
            ""ClrPropertyName"": ""ZipCode"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
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
        ""ClrIdPropertyName"": ""ArticleId"",
        ""ClrIdPropertyType"": ""System.Int32, mscorlib""
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""title"",
            ""ClrPropertyName"": ""Title"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""author"",
            ""ClrPropertyName"": ""Author"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""author-mailing-address"",
            ""ClrPropertyName"": ""AuthorMailingAddress"",
            ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests"",
            ""IsComplexType"": true,
            ""IsCollection"": false
          },
          {
            ""ApiAttributeName"": ""author-phone-numbers"",
            ""ClrPropertyName"": ""AuthorPhoneNumbers"",
            ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests]], mscorlib"",
            ""IsComplexType"": true,
            ""IsCollection"": true,
            ""ClrCollectionItemType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests"",
            ""IsCollectionItemComplexType"": true
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""ClrPropertyName"": ""Relationships"",
        ""RelationshipInfoCollection"": [
          {
            ""Rel"": ""author"",
            ""ToCardinality"": ""ToOne"",
            ""ToClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests""
          },
          {
            ""Rel"": ""comments"",
            ""ToCardinality"": ""ToMany"",
            ""ToClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests""
          }
        ]
      },
      ""LinksInfo"": {
        ""ClrPropertyName"": ""Links""
      }
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
            ""ClrPropertyName"": ""StreetAddress"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""city"",
            ""ClrPropertyName"": ""City"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""state"",
            ""ClrPropertyName"": ""State"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""zip-code"",
            ""ClrPropertyName"": ""ZipCode"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
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
        ""ClrIdPropertyName"": ""ArticleId"",
        ""ClrIdPropertyType"": ""System.Int32, mscorlib""
      },
      ""AttributesInfo"": {
        ""AttributeInfoCollection"": [
          {
            ""ApiAttributeName"": ""title"",
            ""ClrPropertyName"": ""Title"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""author"",
            ""ClrPropertyName"": ""Author"",
            ""ClrPropertyType"": ""System.String, mscorlib"",
            ""IsComplexType"": false,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""author-mailing-address"",
            ""ClrPropertyName"": ""AuthorMailingAddress"",
            ""ClrPropertyType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+MailingAddress, JsonApiFramework.Core.Tests"",
            ""IsComplexType"": true,
            ""IsCollection"": false,
            ""ClrCollectionItemType"": null,
            ""IsCollectionItemComplexType"": null
          },
          {
            ""ApiAttributeName"": ""author-phone-numbers"",
            ""ClrPropertyName"": ""AuthorPhoneNumbers"",
            ""ClrPropertyType"": ""System.Collections.Generic.List`1[[JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests]], mscorlib"",
            ""IsComplexType"": true,
            ""IsCollection"": true,
            ""ClrCollectionItemType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+PhoneNumber, JsonApiFramework.Core.Tests"",
            ""IsCollectionItemComplexType"": true
          }
        ]
      },
      ""RelationshipsInfo"": {
        ""ClrPropertyName"": ""Relationships"",
        ""RelationshipInfoCollection"": [
          {
            ""Rel"": ""author"",
            ""ToCardinality"": ""ToOne"",
            ""ToClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Person, JsonApiFramework.Core.Tests""
          },
          {
            ""Rel"": ""comments"",
            ""ToCardinality"": ""ToMany"",
            ""ToClrType"": ""JsonApiFramework.Tests.Metadata.ServiceModelTests+Comment, JsonApiFramework.Core.Tests""
          }
        ]
      },
      ""LinksInfo"": {
        ""ClrPropertyName"": ""Links""
      }
    }
  ]
}"))
            },
        };

        public static readonly IEnumerable<object[]> TryGetComplexTypeWithClrTypeTestData = new[]
        {
            new object[] { new TryGetComplexTypeWithClrTypeUnitTest("WithKnownComplexType", TestServiceModel, typeof(MailingAddress), MailingAddressComplexType) },
            new object[] { new TryGetComplexTypeWithClrTypeUnitTest("WithUnknownComplexType", TestServiceModel, typeof(PhoneNumber), null) },
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
        #endregion

        #region ResourceType Types
        private class Article
        {
            // ReSharper disable UnusedMember.Local
            public int ArticleId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public MailingAddress AuthorMailingAddress { get; set; }
            public List<PhoneNumber> AuthorPhoneNumbers { get; set; }
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
