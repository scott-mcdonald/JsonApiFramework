// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomRelationshipsTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomRelationshipsTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomRelationshipsTestData))]
        public void TestJsonDeserialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly DomJsonSerializerSettings TestDomJsonSerializerSettings = new DomJsonSerializerSettings
            {
                NullValueHandlingOverrides = null
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented
            };

        public static readonly IEnumerable<object[]> DomRelationshipsTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithZeroRelationships",
                                TestJsonSerializerSettings,
                                new DomRelationships(),
@"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithOneRelationship",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(ApiPropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic"))))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    }
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTwoRelationships",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(ApiPropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))))),
                                    new DomProperty(ApiPropertyType.Relationship, "author",
                                        new DomRelationship(RelationshipType.ToOneRelationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                            new DomProperty(ApiPropertyType.Data, "data",
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    }
  },
  ""author"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/author"",
      ""related"": ""https://api.example.com/articles/42/author""
    },
    ""data"": {
      ""type"": ""people"",
      ""id"": ""42""
    }
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithThreeRelationships",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(ApiPropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))))),
                                    new DomProperty(ApiPropertyType.Relationship, "author",
                                        new DomRelationship(RelationshipType.ToOneRelationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                            new DomProperty(ApiPropertyType.Data, "data",
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                    new DomProperty(ApiPropertyType.Relationship, "comments",
                                        new DomRelationship(RelationshipType.ToManyRelationship,
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                        new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                            new DomProperty(ApiPropertyType.Data, "data",
                                                new DomArray(
                                                    new DomItem(0,
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                    new DomItem(1,
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    }
  },
  ""author"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/author"",
      ""related"": ""https://api.example.com/articles/42/author""
    },
    ""data"": {
      ""type"": ""people"",
      ""id"": ""42""
    }
  },
  ""comments"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/comments"",
      ""related"": ""https://api.example.com/articles/42/comments""
    },
    ""data"": [
      {
        ""type"": ""comments"",
        ""id"": ""68""
      },
      {
        ""type"": ""comments"",
        ""id"": ""86""
      }
    ]
  }
}"))
                    },

            };
        #endregion
    }
}
