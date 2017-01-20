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
                                "WithOneRelationshipAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
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
                                "WithOneRelationshipAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": null
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithOneRelationshipAndMeta",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": {
      ""cascade-delete"": true
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
                                "WithTwoRelationshipsAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42"))))))),
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
                                "WithTwoRelationshipsAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": null
  },
  ""author"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/author"",
      ""related"": ""https://api.example.com/articles/42/author""
    },
    ""data"": {
      ""type"": ""people"",
      ""id"": ""42""
    },
    ""meta"": null
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTwoRelationshipsAndMeta",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true)))))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": {
      ""cascade-delete"": true
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
    },
    ""meta"": {
      ""cascade-delete"": true
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
                                "WithThreeRelationshipsAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("86")))))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))))),
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
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("86"))))))))),
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

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithThreeRelationshipsAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta"))),
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
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("86")))))),
                                            new DomProperty(PropertyType.Meta, "meta")))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": null
  },
  ""author"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/author"",
      ""related"": ""https://api.example.com/articles/42/author""
    },
    ""data"": {
      ""type"": ""people"",
      ""id"": ""42""
    },
    ""meta"": null
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
    ],
    ""meta"": null
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationships>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationships>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithThreeRelationshipsAndMeta",
                                TestJsonSerializerSettings,
                                new DomRelationships(
                                    new DomProperty(PropertyType.Relationship, "topic",
                                        new DomRelationship(RelationshipType.Relationship,
                                            new DomProperty(PropertyType.Links, "links",
                                                new DomLinks(
                                                    new DomProperty(PropertyType.Link, "self",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/relationships/topic")))),
                                                    new DomProperty(PropertyType.Link, "related",
                                                        new DomLink(new DomProperty(PropertyType.HRef, "href",
                                                            new DomValue<string>("https://api.example.com/articles/42/topic")))))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true)))))),
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
                                                    new DomProperty(PropertyType.Id, "id", new DomValue<string>("42")))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true)))))),
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
                                                            new DomProperty(PropertyType.Id, "id", new DomValue<string>("86")))))),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("cascade-delete", new DomValue<bool>(true))))))),
@"{
  ""topic"": {
    ""links"": {
      ""self"": ""https://api.example.com/articles/42/relationships/topic"",
      ""related"": ""https://api.example.com/articles/42/topic""
    },
    ""meta"": {
      ""cascade-delete"": true
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
    },
    ""meta"": {
      ""cascade-delete"": true
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
    ],
    ""meta"": {
      ""cascade-delete"": true
    }
  }
}"))
                    },

            };
        #endregion
    }
}
