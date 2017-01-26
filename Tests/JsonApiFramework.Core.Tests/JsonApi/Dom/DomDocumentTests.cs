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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document),
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0"))))),
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links")),
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
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright",
                                            new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links", new DomLinks(
                                        new DomProperty(PropertyType.Link, "up",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles")))),
                                        new DomProperty(PropertyType.Link, "self",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href",
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

                #region Resource Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithResourceDocumentAndNullResourceAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.ResourceDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
                                new DomDocument(DocumentType.NullDocument,
                                    new DomProperty(PropertyType.Data, "data")),
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
                                "WithResourceDocumentAndNullResourceAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.ResourceDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
                                new DomDocument(DocumentType.NullDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
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
                                "WithResourceDocumentAndArticleResource",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomResource(
                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                            new DomProperty(PropertyType.Attributes, "attributes",
                                                new DomObject(
                                                    new DomProperty("title",
                                                        new DomValue<string>("JSON API paints my bikeshed!")))),
                                            new DomProperty(PropertyType.Relationships, "relationships",
                                                new DomRelationships(
                                                    new DomProperty(PropertyType.Relationship, "author",
                                                        new DomRelationship(RelationshipType.Relationship,
                                                            new DomProperty(PropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(PropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(PropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/author")))))))),
                                                    new DomProperty(PropertyType.Relationship, "comments",
                                                        new DomRelationship(RelationshipType.Relationship,
                                                            new DomProperty(PropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(PropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(PropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/comments"))))))))
                                                )),
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(
                                                            new DomProperty(PropertyType.HRef, "href",
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
                                "WithResourceDocumentAndArticleResourceAndIncludedResources",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomResource(
                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                            new DomProperty(PropertyType.Attributes, "attributes",
                                                new DomObject(
                                                    new DomProperty("title",
                                                        new DomValue<string>("JSON API paints my bikeshed!")))),
                                            new DomProperty(PropertyType.Relationships, "relationships",
                                                new DomRelationships(
                                                    new DomProperty(PropertyType.Relationship, "author",
                                                        new DomRelationship(RelationshipType.ToOneRelationship,
                                                            new DomProperty(PropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(PropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(PropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/author")))))),
                                                            new DomProperty(PropertyType.Data, "data",
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(PropertyType.Type, "type",
                                                                        new DomValue<string>("people")),
                                                                    new DomProperty(PropertyType.Id, "id",
                                                                        new DomValue<string>("42")))))),
                                                    new DomProperty(PropertyType.Relationship, "comments",
                                                        new DomRelationship(RelationshipType.ToManyRelationship,
                                                            new DomProperty(PropertyType.Links, "links",
                                                                new DomLinks(
                                                                    new DomProperty(PropertyType.Link, "related",
                                                                        new DomLink(new DomProperty(PropertyType.HRef,
                                                                            "href",
                                                                            new DomValue<string>(
                                                                                "https://api.example.com/articles/42/comments")))))),
                                                            new DomProperty(PropertyType.Data, "data",
                                                                new DomArray(
                                                                    new DomItem(0,
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type",
                                                                                new DomValue<string>("comments")),
                                                                            new DomProperty(PropertyType.Id, "id",
                                                                                new DomValue<string>("68")))),
                                                                    new DomItem(1,
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type",
                                                                                new DomValue<string>("comments")),
                                                                            new DomProperty(PropertyType.Id, "id",
                                                                                new DomValue<string>("86"))))))))
                                                )),
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(
                                                            new DomProperty(PropertyType.HRef, "href",
                                                                new DomValue<string>(
                                                                    "https://api.example.com/articles/42")))))))),
                                    new DomProperty(PropertyType.Included, "included",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("people")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("Scott")),
                                                            new DomProperty("last-name",
                                                                new DomValue<string>("McDonald")),
                                                            new DomProperty("twitter", new DomValue<string>("smcdonald")))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/people/42")))))))),
                                            new DomItem(1,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("68")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("My first comment.")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id",
                                                                                new DomValue<string>("2")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/comments/68")))))))),
                                            new DomItem(2,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("86")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body",
                                                                new DomValue<string>("I like C# better.")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type",
                                                                                new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id",
                                                                                new DomValue<string>("42")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href",
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
                #endregion

                #region Resource Collection Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithResourceCollectionDocumentAndEmptyCollectionAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.ResourceCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomArray())),
                                new DomDocument(DocumentType.EmptyDocument,
                                    new DomProperty(PropertyType.Data, "data",
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
                                "WithResourceCollectionDocumentAndEmptyCollectionAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.ResourceCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomArray())),
                                new DomDocument(DocumentType.EmptyDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data",
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
                                "WithResourceCollectionDocumentAndArticleResources",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("24")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my bikeshed!")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef,
                                                                                    "href",
                                                                                    new DomValue<string>(
                                                                                        "https://api.example.com/articles/24/author")))))))),
                                                            new DomProperty(PropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef,
                                                                                    "href",
                                                                                    new DomValue<string>(
                                                                                        "https://api.example.com/articles/24/comments"))))))))
                                                        )),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href",
                                                                        new DomValue<string>(
                                                                            "https://api.example.com/articles/24")))))))),
                                            new DomItem(1,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title",
                                                                new DomValue<string>("JSON API paints my house!")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef,
                                                                                    "href",
                                                                                    new DomValue<string>(
                                                                                        "https://api.example.com/articles/42/author")))))))),
                                                            new DomProperty(PropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.Relationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef,
                                                                                    "href",
                                                                                    new DomValue<string>(
                                                                                        "https://api.example.com/articles/42/comments"))))))))
                                                        )),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href",
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
                                "WithResourceCollectionDocumentAndArticleResourcesAndIncludedResources",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("24")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/24/author")))))),
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("1")))))),
                                                            new DomProperty(PropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/24/comments")))))),
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomArray(
                                                                            new DomItem(0,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("100")))),
                                                                            new DomItem(1,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("101")))))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/24")))))))),
                                            new DomItem(1,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("articles")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("title", new DomValue<string>("JSON API paints my house!")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(
                                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("2")))))),
                                                            new DomProperty(PropertyType.Relationship, "comments",
                                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                                    new DomProperty(PropertyType.Links, "links",
                                                                        new DomLinks(
                                                                            new DomProperty(PropertyType.Link, "related",
                                                                                new DomLink(new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomArray(
                                                                            new DomItem(0,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("200")))),
                                                                            new DomItem(1,
                                                                                new DomResourceIdentifier(
                                                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("201")))))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))))))),
                                    new DomProperty(PropertyType.Included, "included",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("1")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("John")),
                                                            new DomProperty("last-name", new DomValue<string>("Doe")),
                                                            new DomProperty("twitter", new DomValue<string>("johndoe")))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/people/1")))))))),
                                            new DomItem(1,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("2")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("first-name", new DomValue<string>("Jane")),
                                                            new DomProperty("last-name", new DomValue<string>("doe")),
                                                            new DomProperty("twitter", new DomValue<string>("janedoe")))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/people/2")))))))),

                                            new DomItem(2,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("100")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body", new DomValue<string>("What is a bikeshed?")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("10")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/comments/100")))))))),
                                            new DomItem(3,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("101")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body", new DomValue<string>("What color would you paint the bikeshed?")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("10")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/comments/101")))))))),
                                            new DomItem(4,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("200")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body", new DomValue<string>("JSON API rocks!")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("20")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/comments/200")))))))),
                                            new DomItem(5,
                                                new DomResource(
                                                    new DomProperty(PropertyType.Type, "type",
                                                        new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("201")),
                                                    new DomProperty(PropertyType.Attributes, "attributes",
                                                        new DomObject(
                                                            new DomProperty("body", new DomValue<string>("JsonApiFramework kicks ass...")))),
                                                    new DomProperty(PropertyType.Relationships, "relationships",
                                                        new DomRelationships(
                                                            new DomProperty(PropertyType.Relationship, "author",
                                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                                    new DomProperty(PropertyType.Data, "data",
                                                                        new DomResourceIdentifier(
                                                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("20")))))))),
                                                    new DomProperty(PropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(PropertyType.Link, "self",
                                                                new DomLink(
                                                                    new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/comments/201"))))))))))),
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

                #endregion

                #region Resource Identifier Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithResourceIdentifierDocumentAndNullResourceIdentifierAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.ResourceIdentifierDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
                                new DomDocument(DocumentType.NullDocument,
                                    new DomProperty(PropertyType.Data, "data")),
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
                                "WithResourceIdentifierDocumentAndNullResourceIdentifierAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.ResourceIdentifierDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
                                new DomDocument(DocumentType.NullDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data")),
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
                                "WithResourceIdentifierDocumentAndPersonResourceIdentifier",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceIdentifierDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42/relationships/author")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomResourceIdentifier(
                                            new DomProperty(PropertyType.Type, "type", new DomValue<string>("people")),
                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("24"))))),
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

                #region Resource Identifier Collection Document
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithResourceIdentifierCollectionDocumentAndEmptyResourceIdentifiersAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.ResourceIdentifierCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data", new DomArray())),
                                new DomDocument(DocumentType.EmptyDocument,
                                    new DomProperty(PropertyType.Data, "data", new DomArray())),
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
                                "WithResourceIdentifierCollectionDocumentAndEmptyResourceIdentifiersAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.ResourceIdentifierCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data", new DomArray())),
                                new DomDocument(DocumentType.EmptyDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links"),
                                    new DomProperty(PropertyType.Data, "data", new DomArray())),
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
                                "WithResourceIdentifierCollectionDocumentAndCommentResourceIdentifiers",
                                TestJsonSerializerSettings,
                                new DomDocument(DocumentType.ResourceIdentifierCollectionDocument,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(PropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(PropertyType.HRef, "href",
                                                        new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))))),
                                    new DomProperty(PropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResourceIdentifier(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("200")))),
                                            new DomItem(1,
                                                new DomResourceIdentifier(
                                                    new DomProperty(PropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("201")))))
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
            };
        #endregion
    }
}
