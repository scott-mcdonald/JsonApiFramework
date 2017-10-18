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
    public class DomDocumentTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocumentTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomDocumentTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomDocumentTestData))]
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
            DateParseHandling = DateParseHandling.DateTimeOffset,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
        {
            ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
            DateParseHandling = DateParseHandling.DateTimeOffset,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
        {
            ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
            DateParseHandling = DateParseHandling.DateTimeOffset,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include
        };

        public static readonly IEnumerable<object[]> DomDocumentTestData = new[]
            {
                #region Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
                                new DomDocument(ApiDocumentType.Document),
@"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndJsonApiAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0"))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndJsonApiAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": null,
  ""links"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndJsonApiAndMetaAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(ApiPropertyType.Links, "links")),
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe"))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndJsonApiAndMetaAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(ApiPropertyType.Links, "links")),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  },
  ""links"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndJsonApiAndMetaAndLinks",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(ApiPropertyType.Links, "links", new DomLinks(
                                        new DomProperty(ApiPropertyType.Link, "up",
                                            new DomLink(
                                                new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles")))),
                                        new DomProperty(ApiPropertyType.Link, "self",
                                            new DomLink(
                                                new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42"))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  },
  ""links"": {
    ""up"": ""https://api.example.com/articles"",
    ""self"": ""https://api.example.com/articles/42""
  }
}"))
                    },
                #endregion

                #region Data Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataDocumentAndNullDataAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data")),
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.Data, "data")),
@"{
  ""data"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataDocumentAndNullDataAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data")),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null,
  ""data"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataDocumentAndArticleResource",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomData(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                            new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                new DomObject(
                                                    new DomProperty("title",
                                                        new DomValue<string>("JSON API paints my bikeshed!")))),
                                            new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                new DomRelationships(
                                                    new DomProperty(ApiPropertyType.Relationship, "author",
                                                        new DomRelationship(RelationshipType.Relationship,
                                                            new DomProperty(ApiPropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(ApiPropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/author")))))))),
                                                    new DomProperty(ApiPropertyType.Relationship, "comments",
                                                        new DomRelationship(RelationshipType.Relationship,
                                                            new DomProperty(ApiPropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(ApiPropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/comments"))))))))
                                                )),
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(
                                                            new DomProperty(ApiPropertyType.HRef, "href",
                                                                new DomValue<string>(
                                                                    "https://api.example.com/articles/42"))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles/42""
  },
  ""data"": {
    ""type"": ""articles"",
    ""id"": ""42"",
    ""attributes"": {
      ""title"": ""JSON API paints my bikeshed!""
    },
    ""relationships"": {
      ""author"": {
        ""links"": {
          ""related"": ""https://api.example.com/articles/42/author""
        }
      },
      ""comments"": {
        ""links"": {
          ""related"": ""https://api.example.com/articles/42/comments""
        }
      }
    },
    ""links"": {
      ""self"": ""https://api.example.com/articles/42""
    }
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataDocumentAndArticleResourceAndIncludedResources",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomData(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                            new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                new DomObject(
                                                    new DomProperty("title",
                                                        new DomValue<string>("JSON API paints my bikeshed!")))),
                                            new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                new DomRelationships(
                                                    new DomProperty(ApiPropertyType.Relationship, "author",
                                                        new DomRelationship(RelationshipType.ToOneRelationship,
                                                            new DomProperty(ApiPropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(ApiPropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/author")))))),
                                                            new DomProperty(ApiPropertyType.Data, "data",
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                                        new DomValue<string>("people")),
                                                                    new DomProperty(ApiPropertyType.Id, "id",
                                                                        new DomValue<string>("42")))))),
                                                    new DomProperty(ApiPropertyType.Relationship, "comments",
                                                        new DomRelationship(RelationshipType.ToManyRelationship,
                                                            new DomProperty(ApiPropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(ApiPropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(ApiPropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/comments")))))),
                                                            new DomProperty(ApiPropertyType.Data, "data",
                                                                new DomArray(
                                                                    new DomItem(0,
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("comments")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("68")))),
                                                                    new DomItem(1,
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("comments")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("86"))))))))
                                                )),
                                            new DomProperty(ApiPropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(ApiPropertyType.Link, "self",
                                                        new DomLink(
                                                            new DomProperty(ApiPropertyType.HRef, "href",
                                                                new DomValue<string>(
                                                                    "https://api.example.com/articles/42")))))))),
                                    new DomProperty(ApiPropertyType.Included, "included",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("people")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("Scott")),
                                                            new DomProperty("last-name",
                                                                new DomValue<string>("McDonald")),
                                                            new DomProperty("twitter", new DomValue<string>("smcdonald")))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/people/42")))))))),
                                            new DomItem(1,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("My first comment.")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("2")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/68")))))))),
                                            new DomItem(2,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("I like C# better.")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("42")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/86"))))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles/42""
  },
  ""data"": {
    ""type"": ""articles"",
    ""id"": ""42"",
    ""attributes"": {
      ""title"": ""JSON API paints my bikeshed!""
    },
    ""relationships"": {
      ""author"": {
        ""links"": {
          ""related"": ""https://api.example.com/articles/42/author""
        },
        ""data"": {
          ""type"": ""people"",
          ""id"": ""42""
        }
      },
      ""comments"": {
        ""links"": {
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
      ""self"": ""https://api.example.com/articles/42""
    }
  },
  ""included"": [
    {
      ""type"": ""people"",
      ""id"": ""42"",
      ""attributes"": {
        ""first-name"": ""Scott"",
        ""last-name"": ""McDonald"",
        ""twitter"": ""smcdonald""
      },
      ""links"": {
        ""self"": ""https://api.example.com/people/42""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""68"",
      ""attributes"": {
        ""body"": ""My first comment.""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""2""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/68""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""86"",
      ""attributes"": {
        ""body"": ""I like C# better.""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""42""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/86""
      }
    }
  ]
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataDocumentAndPersonResourceIdentifier",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>(
                                                            "https://api.example.com/articles/42/relationships/author")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomData(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("24"))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author""
  },
  ""data"": {
    ""type"": ""people"",
    ""id"": ""24""
  }
}"))
                    },
                #endregion

                #region Data Collection Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataCollectionDocumentAndEmptyCollectionAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray())),
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray())),
@"{
  ""data"": []
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataCollectionDocumentAndEmptyCollectionAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray())),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null,
  ""data"": []
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataCollectionDocumentAndArticleResources",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("articles")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("24")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my bikeshed!")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/24/author")))))))),
                                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/24/comments"))))))))
                                                        )),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/articles/24")))))))),
                                            new DomItem(1,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("articles")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my house!")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/42/author")))))))),
                                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/42/comments"))))))))
                                                        )),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/articles/42"))))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""data"": [
    {
      ""type"": ""articles"",
      ""id"": ""24"",
      ""attributes"": {
        ""title"": ""JSON API paints my bikeshed!""
      },
      ""relationships"": {
        ""author"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/24/author""
          }
        },
        ""comments"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/24/comments""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/articles/24""
      }
    },
    {
      ""type"": ""articles"",
      ""id"": ""42"",
      ""attributes"": {
        ""title"": ""JSON API paints my house!""
      },
      ""relationships"": {
        ""author"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/42/author""
          }
        },
        ""comments"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/42/comments""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/articles/42""
      }
    }
  ]
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataCollectionDocumentAndArticleResourcesAndIncludedResources",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("articles")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("24")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my bikeshed!")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/24/author")))))),
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("1")))))),
                                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/24/comments")))))),
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomArray(
                                                                            new DomItem(0,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(ApiPropertyType.Type,
                                                                                        "type",
                                                                                        new DomValue<string>("comments")),
                                                                                    new DomProperty(ApiPropertyType.Id,
                                                                                        "id",
                                                                                        new DomValue<string>("100")))),
                                                                            new DomItem(1,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(ApiPropertyType.Type,
                                                                                        "type",
                                                                                        new DomValue<string>("comments")),
                                                                                    new DomProperty(ApiPropertyType.Id,
                                                                                        "id",
                                                                                        new DomValue<string>("101")))))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/articles/24")))))))),
                                            new DomItem(1,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("articles")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my house!")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/42/author")))))),
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("2")))))),
                                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(ApiPropertyType.HRef,
                                                                                        "href",
                                                                                        new DomValue<string>(
                                                                                            "https://api.example.com/articles/42/comments")))))),
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomArray(
                                                                            new DomItem(0,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(ApiPropertyType.Type,
                                                                                        "type",
                                                                                        new DomValue<string>("comments")),
                                                                                    new DomProperty(ApiPropertyType.Id,
                                                                                        "id",
                                                                                        new DomValue<string>("200")))),
                                                                            new DomItem(1,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(ApiPropertyType.Type,
                                                                                        "type",
                                                                                        new DomValue<string>("comments")),
                                                                                    new DomProperty(ApiPropertyType.Id,
                                                                                        "id",
                                                                                        new DomValue<string>("201")))))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/articles/42")))))))))),
                                    new DomProperty(ApiPropertyType.Included, "included",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("people")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("1")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("John")),
                                                            new DomProperty("last-name", new DomValue<string>("Doe")),
                                                            new DomProperty("twitter", new DomValue<string>("johndoe")))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/people/1")))))))),
                                            new DomItem(1,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("people")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("2")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("Jane")),
                                                            new DomProperty("last-name", new DomValue<string>("doe")),
                                                            new DomProperty("twitter", new DomValue<string>("janedoe")))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/people/2")))))))),

                                            new DomItem(2,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("100")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("What is a bikeshed?")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("10")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/100")))))))),
                                            new DomItem(3,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("101")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>(
                                                                    "What color would you paint the bikeshed?")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("10")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/101")))))))),
                                            new DomItem(4,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("200")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("JSON API rocks!")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("20")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/200")))))))),
                                            new DomItem(5,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("201")),
                                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("JsonApiFramework kicks ass...")))),
                                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(ApiPropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(ApiPropertyType.Id, "id",
                                                                                new DomValue<string>("20")))))))),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/201"))))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""data"": [
    {
      ""type"": ""articles"",
      ""id"": ""24"",
      ""attributes"": {
        ""title"": ""JSON API paints my bikeshed!""
      },
      ""relationships"": {
        ""author"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/24/author""
          },
          ""data"": {
            ""type"": ""people"",
            ""id"": ""1""
          }
        },
        ""comments"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/24/comments""
          },
          ""data"": [
            {
              ""type"": ""comments"",
              ""id"": ""100""
            },
            {
              ""type"": ""comments"",
              ""id"": ""101""
            }
          ]
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/articles/24""
      }
    },
    {
      ""type"": ""articles"",
      ""id"": ""42"",
      ""attributes"": {
        ""title"": ""JSON API paints my house!""
      },
      ""relationships"": {
        ""author"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/42/author""
          },
          ""data"": {
            ""type"": ""people"",
            ""id"": ""2""
          }
        },
        ""comments"": {
          ""links"": {
            ""related"": ""https://api.example.com/articles/42/comments""
          },
          ""data"": [
            {
              ""type"": ""comments"",
              ""id"": ""200""
            },
            {
              ""type"": ""comments"",
              ""id"": ""201""
            }
          ]
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/articles/42""
      }
    }
  ],
  ""included"": [
    {
      ""type"": ""people"",
      ""id"": ""1"",
      ""attributes"": {
        ""first-name"": ""John"",
        ""last-name"": ""Doe"",
        ""twitter"": ""johndoe""
      },
      ""links"": {
        ""self"": ""https://api.example.com/people/1""
      }
    },
    {
      ""type"": ""people"",
      ""id"": ""2"",
      ""attributes"": {
        ""first-name"": ""Jane"",
        ""last-name"": ""doe"",
        ""twitter"": ""janedoe""
      },
      ""links"": {
        ""self"": ""https://api.example.com/people/2""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""100"",
      ""attributes"": {
        ""body"": ""What is a bikeshed?""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""10""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/100""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""101"",
      ""attributes"": {
        ""body"": ""What color would you paint the bikeshed?""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""10""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/101""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""200"",
      ""attributes"": {
        ""body"": ""JSON API rocks!""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""20""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/200""
      }
    },
    {
      ""type"": ""comments"",
      ""id"": ""201"",
      ""attributes"": {
        ""body"": ""JsonApiFramework kicks ass...""
      },
      ""relationships"": {
        ""author"": {
          ""data"": {
            ""type"": ""people"",
            ""id"": ""20""
          }
        }
      },
      ""links"": {
        ""self"": ""https://api.example.com/comments/201""
      }
    }
  ]
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDataCollectionDocumentAndCommentResourceIdentifiers",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.DataCollectionDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>(
                                                            "https://api.example.com/articles/42/relationships/comments")))))),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("200")))),
                                            new DomItem(1,
                                                new DomData(
                                                    new DomProperty(ApiPropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("201")))))
                                    )),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/comments""
  },
  ""data"": [
    {
      ""type"": ""comments"",
      ""id"": ""200""
    },
    {
      ""type"": ""comments"",
      ""id"": ""201""
    }
  ]
}"))
                    },
                #endregion

                #region Errors Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithErrorsDocumentAndEmptyCollectionAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(ApiDocumentType.ErrorsDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Errors, "errors", new DomArray())),
                                new DomDocument(ApiDocumentType.ErrorsDocument,
                                    new DomProperty(ApiPropertyType.Errors, "errors", new DomArray())),
@"{
  ""errors"": []
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithErrorsDocumentAndEmptyCollectionAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(ApiDocumentType.ErrorsDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Errors, "errors", new DomArray())),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null,
  ""errors"": []
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithErrorsDocumentAnd1ErrorObjects",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.ErrorsDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>(
                                                            "https://api.example.com/articles")))))),
                                    new DomProperty(ApiPropertyType.Errors, "errors",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomError(
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "about",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/about/first-name-minimum-requirement")))))),
                                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("422")),
                                                    new DomProperty(ApiPropertyType.Code, "code", new DomValue<string>("24")),
                                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                                    new DomProperty(ApiPropertyType.Source, "source",
                                                        new DomObject(
                                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name")))),
                                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                                        new DomObject(
                                                            new DomProperty("stack-trace", new DomValue<string>("Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68"))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""errors"": [
    {
      ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
      ""links"": {
        ""about"": ""https://api.example.com/about/first-name-minimum-requirement""
      },
      ""status"": ""422"",
      ""code"": ""24"",
      ""title"": ""Invalid Attribute"",
      ""detail"": ""First name must contain at least three characters."",
      ""source"": {
        ""pointer"": ""/data/attributes/first-name""
      },
      ""meta"": {
        ""stack-trace"": ""Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68""
      }
    }
  ]
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithErrorsDocumentAnd2ErrorObjects",
                                TestJsonSerializerSettings,
                                new DomDocument(ApiDocumentType.ErrorsDocument,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href",
                                                        new DomValue<string>(
                                                            "https://api.example.com/articles?include=author")))))),
                                    new DomProperty(ApiPropertyType.Errors, "errors",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomError(
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "about",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/about/first-name-minimum-requirement")))))),
                                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("422")),
                                                    new DomProperty(ApiPropertyType.Code, "code", new DomValue<string>("24")),
                                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                                    new DomProperty(ApiPropertyType.Source, "source",
                                                        new DomObject(
                                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name")))),
                                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                                        new DomObject(
                                                            new DomProperty("stack-trace", new DomValue<string>("Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68")))))),
                                            new DomItem(1,
                                                new DomError(
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("192ca96c-1b36-4721-aa12-110ee9c43958")),
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "about",
                                                                new DomLink(
                                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/about/include-query-parameter")))))),
                                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("400")),
                                                    new DomProperty(ApiPropertyType.Code, "code", new DomValue<string>("42")),
                                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Query Parameter")),
                                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("The resource does not have an `author` relationship path.")),
                                                    new DomProperty(ApiPropertyType.Source, "source",
                                                        new DomObject(
                                                            new DomProperty("parameter", new DomValue<string>("include")))),
                                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                                        new DomObject(
                                                            new DomProperty("stack-trace", new DomValue<string>("Foo.Method12 line 84\nFoo.Method22 line 164\nBar.Method32 line 256\nBar.Method64 line 8"))))))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles?include=author""
  },
  ""errors"": [
    {
      ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
      ""links"": {
        ""about"": ""https://api.example.com/about/first-name-minimum-requirement""
      },
      ""status"": ""422"",
      ""code"": ""24"",
      ""title"": ""Invalid Attribute"",
      ""detail"": ""First name must contain at least three characters."",
      ""source"": {
        ""pointer"": ""/data/attributes/first-name""
      },
      ""meta"": {
        ""stack-trace"": ""Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68""
      }
    },
    {
      ""id"": ""192ca96c-1b36-4721-aa12-110ee9c43958"",
      ""links"": {
        ""about"": ""https://api.example.com/about/include-query-parameter""
      },
      ""status"": ""400"",
      ""code"": ""42"",
      ""title"": ""Invalid Query Parameter"",
      ""detail"": ""The resource does not have an `author` relationship path."",
      ""source"": {
        ""parameter"": ""include""
      },
      ""meta"": {
        ""stack-trace"": ""Foo.Method12 line 84\nFoo.Method22 line 164\nBar.Method32 line 256\nBar.Method64 line 8""
      }
    }
  ]
}"))
                    },
                #endregion
        };
        #endregion
    }
}
