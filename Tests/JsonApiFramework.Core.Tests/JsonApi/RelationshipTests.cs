// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class RelationshipTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(RelationshipTestData))]
        public void TestRelationshipSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(RelationshipTestData))]
        public void TestRelationshipDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ToOneRelationshipTestData))]
        public void TestToOneRelationshipSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ToOneRelationshipTestData))]
        public void TestToOneRelationshipDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ToManyRelationshipTestData))]
        public void TestToManyRelationshipSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ToManyRelationshipTestData))]
        public void TestToManyRelationshipDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Include
        };

        private static readonly RelationshipMeta RelationshipMetaTestData = new RelationshipMeta
        {
            CascadeOnDelete = true
        };

        private static readonly ResourceIdentifierMeta ResourceIdentifierMetaTestData = new ResourceIdentifierMeta
        {
            Version = 1.23m
        };

        public static readonly IEnumerable<object[]> RelationshipTestData = new[]
            {
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Relationship>(x),
                            x => new JsonObjectDeserializeUnitTest<Relationship>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithNullObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                default(Relationship),
                                "null"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Relationship>(x),
                            x => new JsonObjectDeserializeUnitTest<Relationship>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithNullObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                default(Relationship),
                                "null"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Relationship>(x),
                            x => new JsonObjectDeserializeUnitTest<Relationship>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new Relationship(
                                    new Links(new Dictionary<string, Link>
                                    {
                                        {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                        {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                                    })),
                                "{\"links\":{\"self\":\"https://api.example.com/articles/42/relationships/author\",\"related\":\"https://api.example.com/articles/42/author\"}}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Relationship>(x),
                            x => new JsonObjectDeserializeUnitTest<Relationship>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new Relationship(
                                    new Links(new Dictionary<string, Link>
                                    {
                                        {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                        {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                                    })),
                                "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"meta\":null}"))
                    },

                new object[]
                {
                    new JsonObjectSerializationUnitTestFactory(
                        x => new JsonObjectSerializeUnitTest<Relationship>(x),
                        x => new JsonObjectDeserializeUnitTest<Relationship>(x),
                        new JsonObjectSerializationUnitTestData(
                            "WithLinksAndMeta",
                            TestJsonSerializerSettings,
                            new Relationship(
                                new Links(new Dictionary<string, Link>
                                {
                                    {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                    {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                                }),
                                new WriteMeta<RelationshipMeta>(RelationshipMetaTestData)),
                            new Relationship(
                                new Links(new Dictionary<string, Link>
                                {
                                    {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                    {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                                }),
                                new ReadMeta(JObject.FromObject(RelationshipMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                            "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"meta\":{\"cascade-on-delete\":true}}"))
                },
            };

        public static readonly IEnumerable<object[]> ToOneRelationshipTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            null,
                            null),
                        "{\"links\":{\"self\":\"https://api.example.com/articles/42/relationships/author\",\"related\":\"https://api.example.com/articles/42/author\"},\"data\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            null,
                            null),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"data\":null,\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndMeta",
                        TestJsonSerializerSettings,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            null,
                            new WriteMeta<RelationshipMeta>(RelationshipMetaTestData)),
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            null,
                            new ReadMeta(JObject.FromObject(RelationshipMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"data\":null,\"meta\":{\"cascade-on-delete\":true}}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            new ResourceIdentifier("people", "42"),
                            null),
                        "{\"links\":{\"self\":\"https://api.example.com/articles/42/relationships/author\",\"related\":\"https://api.example.com/articles/42/author\"},\"data\":{\"type\":\"people\",\"id\":\"42\"}}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            new ResourceIdentifier("people", "42"),
                            null),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"data\":{\"type\":\"people\",\"id\":\"42\",\"meta\":null},\"meta\":null}\n"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToOneRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToOneRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndMeta",
                        TestJsonSerializerSettings,
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            new ResourceIdentifier("people", "42", new WriteMeta<ResourceIdentifierMeta>(ResourceIdentifierMetaTestData)),
                            new WriteMeta<RelationshipMeta>(RelationshipMetaTestData)),
                        new ToOneRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/author")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/author")}
                            }),
                            new ResourceIdentifier("people", "42", new ReadMeta(JObject.FromObject(ResourceIdentifierMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                            new ReadMeta(JObject.FromObject(RelationshipMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/author\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/author\",\"meta\":null}},\"data\":{\"type\":\"people\",\"id\":\"42\",\"meta\":{\"version\":1.23}},\"meta\":{\"cascade-on-delete\":true}}"))
            },
        };

        public static readonly IEnumerable<object[]> ToManyRelationshipTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            null,
                            null),
                        "{\"links\":{\"self\":\"https://api.example.com/articles/42/relationships/comments\",\"related\":\"https://api.example.com/articles/42/comments\"},\"data\":[]}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            null,
                            null),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/comments\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/comments\",\"meta\":null}},\"data\":[],\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithEmptyLinkageAndLinksAndMeta",
                        TestJsonSerializerSettings,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            null,
                            new WriteMeta<RelationshipMeta>(RelationshipMetaTestData)),
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            null,
                            new ReadMeta(JObject.FromObject(RelationshipMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/comments\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/comments\",\"meta\":null}},\"data\":[],\"meta\":{\"cascade-on-delete\":true}}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            new[]
                            {
                                new ResourceIdentifier("comments", "24"),
                                new ResourceIdentifier("comments", "42"),
                            },
                            null),
                        "{\"links\":{\"self\":\"https://api.example.com/articles/42/relationships/comments\",\"related\":\"https://api.example.com/articles/42/comments\"},\"data\":[{\"type\":\"comments\",\"id\":\"24\"},{\"type\":\"comments\",\"id\":\"42\"}]}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            new[]
                            {
                                new ResourceIdentifier("comments", "24"),
                                new ResourceIdentifier("comments", "42"),
                            },
                            null),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/comments\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/comments\",\"meta\":null}},\"data\":[{\"type\":\"comments\",\"id\":\"24\",\"meta\":null},{\"type\":\"comments\",\"id\":\"42\",\"meta\":null}],\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ToManyRelationship>(x),
                    x => new JsonObjectDeserializeUnitTest<ToManyRelationship>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNonEmptyLinkageAndLinksAndMeta",
                        TestJsonSerializerSettings,
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            new[]
                            {
                                new ResourceIdentifier("comments", "24", new WriteMeta<ResourceIdentifierMeta>(ResourceIdentifierMetaTestData)),
                                new ResourceIdentifier("comments", "42", new WriteMeta<ResourceIdentifierMeta>(ResourceIdentifierMetaTestData)),
                            },
                            new WriteMeta<RelationshipMeta>(RelationshipMetaTestData)),
                        new ToManyRelationship(
                            new Links(new Dictionary<string, Link>
                            {
                                {Keywords.Self, new Link("https://api.example.com/articles/42/relationships/comments")},
                                {Keywords.Related, new Link("https://api.example.com/articles/42/comments")}
                            }),
                            new[]
                            {
                                new ResourceIdentifier("comments", "24", new ReadMeta(JObject.FromObject(ResourceIdentifierMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                                new ResourceIdentifier("comments", "42", new ReadMeta(JObject.FromObject(ResourceIdentifierMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                            },
                            new ReadMeta(JObject.FromObject(RelationshipMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"links\":{\"self\":{\"href\":\"https://api.example.com/articles/42/relationships/comments\",\"meta\":null},\"related\":{\"href\":\"https://api.example.com/articles/42/comments\",\"meta\":null}},\"data\":[{\"type\":\"comments\",\"id\":\"24\",\"meta\":{\"version\":1.23}},{\"type\":\"comments\",\"id\":\"42\",\"meta\":{\"version\":1.23}}],\"meta\":{\"cascade-on-delete\":true}}"))
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Meta Types
        [JsonObject(MemberSerialization.OptIn)]
        public class RelationshipMeta : JsonObject
        {
            [JsonProperty("cascade-on-delete")] public bool CascadeOnDelete { get; set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class ResourceIdentifierMeta : JsonObject
        {
            [JsonProperty("version")] public decimal Version { get; set; }
        }
        #endregion
    }
}
