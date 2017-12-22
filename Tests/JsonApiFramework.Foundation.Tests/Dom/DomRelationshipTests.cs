// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Dom;
using JsonApiFramework.Dom.Internal;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.JsonApi.Dom;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Dom
{
    public class DomRelationshipTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonDeserialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonDeserializeUnitTestFactory;
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

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                // RELATIONSHIP /////////////////////////////////////////////
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithRelationshipAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomRelationship(RelationshipType.Relationship),
@"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithRelationshipAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithRelationshipAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                            new DomProperty(ApiPropertyType.Link, "related",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                            new DomProperty(ApiPropertyType.Link, "related",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/author"))))))),
@"{
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author"",
    ""related"": ""https://api.example.com/articles/42/author""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithRelationshipAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                            new DomProperty(ApiPropertyType.Link, "related",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author"",
    ""related"": ""https://api.example.com/articles/42/author""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithRelationshipAndLinksAndMeta",
                                TestJsonSerializerSettings,
                                new DomRelationship(RelationshipType.Relationship,
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                            new DomProperty(ApiPropertyType.Link, "related",
                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("cascade-delete", new DomValue<bool>(true))))),
@"{
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author"",
    ""related"": ""https://api.example.com/articles/42/author""
  },
  ""meta"": {
    ""cascade-delete"": true
  }
}"))
                    },

                // TO-ONE RELATIONSHIP //////////////////////////////////////
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Data, "data")),
@"{
  ""data"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": null,
  ""data"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndDataAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomResourceIdentifier(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomResourceIdentifier(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))))),
@"{
  ""data"": {
    ""type"": ""people"",
    ""id"": ""42""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndDataAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.ToOneRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomResourceIdentifier(
                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": null,
  ""data"": {
    ""type"": ""people"",
    ""id"": ""42""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndDataAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
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
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
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
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))))),
@"{
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author"",
    ""related"": ""https://api.example.com/articles/42/author""
  },
  ""data"": {
    ""type"": ""people"",
    ""id"": ""42""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndDataAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
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
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": {
    ""self"": ""https://api.example.com/articles/42/relationships/author"",
    ""related"": ""https://api.example.com/articles/42/author""
  },
  ""data"": {
    ""type"": ""people"",
    ""id"": ""42""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToOneRelationshipAndDataAndLinksAndMeta",
                                TestJsonSerializerSettings,
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
                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("cascade-delete", new DomValue<bool>(true))))),
@"{
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
}"))
                    },

                // TO-MANY RELATIONSHIP /////////////////////////////////////
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.ToManyRelationship,
                                    new DomProperty(ApiPropertyType.Data, "data", new DomArray())),
@"{
  ""data"": []
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.ToManyRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data", new DomArray()),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": null,
  ""data"": [],
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndDataAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomRelationship(RelationshipType.ToManyRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                            new DomItem(1,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomRelationship(RelationshipType.ToManyRelationship,
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                            new DomItem(1,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))),
@"{
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
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndDataAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomRelationship(RelationshipType.ToManyRelationship,
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Data, "data",
                                        new DomArray(
                                            new DomItem(0,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                            new DomItem(1,
                                                new DomResourceIdentifier(
                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""links"": null,
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
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndDataAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
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
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
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
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))),
@"{
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
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndDataAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
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
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
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
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomRelationship>(x),
                            x => new DomJsonDeserializeUnitTest<IDomRelationship>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithToManyRelationshipAndDataAndLinksAndMeta",
                                TestJsonSerializerSettings,
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
                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("cascade-delete", new DomValue<bool>(true))))),
@"{
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
}"))
                    },

            };
        #endregion
    }
}
