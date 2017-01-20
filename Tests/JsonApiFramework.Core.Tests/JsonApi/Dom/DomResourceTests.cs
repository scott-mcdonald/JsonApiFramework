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
    public class DomResourceTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomResourceTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomResourceTestData))]
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

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        public static readonly IEnumerable<object[]> DomResourceTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id"),
                                    new DomProperty(PropertyType.Attributes, "attributes"),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles"))),
@"{
  ""type"": ""articles""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id"),
                                    new DomProperty(PropertyType.Attributes, "attributes"),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": null,
  ""attributes"": null,
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes"),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42"))),
@"{
  ""type"": ""articles"",
  ""id"": ""42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes"),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": null,
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            ))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
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
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
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
  },
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
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
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
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
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResource>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResource>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndMeta",
                                TestJsonSerializerSettings,
                                new DomResource(
                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(PropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(PropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(PropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(PropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(PropertyType.Link, "related",
                                                                new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(PropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(PropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
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
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  },
  ""meta"":  {
    ""version"": ""2.0""
  }
}"))
                    },

        };
        #endregion
    }
}
