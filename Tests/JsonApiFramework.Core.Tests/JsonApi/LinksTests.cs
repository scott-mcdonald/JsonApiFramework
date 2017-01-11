// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class LinksTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(LinkTestData))]
        public void TestJsonSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LinkTestData))]
        public void TestJsonDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Fact]
        public void TestLinksTryGetLinkWithLinkThatExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                {
                    {Keywords.Up, JsonApiSampleData.ArticleCollectionLink},
                    {Keywords.Self, JsonApiSampleData.ArticleLink}
                });

            // Act
            var expected = JsonApiSampleData.ArticleLink;
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Self, out actual);

            // Assert
            actualLinkFound.Should().BeTrue();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void TestLinksTryGetLinkWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                    {
                        {Keywords.Up, JsonApiSampleData.ArticleCollectionLink},
                        {Keywords.Self, JsonApiSampleData.ArticleLink}
                    });

            // Act
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Next, out actual);

            // Assert
            actualLinkFound.Should().BeFalse();
            actual.Should().BeNull();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestSettingsIgnoreNull = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestSettingsIncludeNull = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> LinkTestData = new[]
            // ReSharper restore MemberCanBePrivate.Global
                {
                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithNull",
                                    TestSettings,
                                    default(Links),
                                    "null"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithEmptyObject",
                                    TestSettings,
                                    new Links(),
                                    "{}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithOneLinkAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {Keywords.Self, "https://api.example.com/articles/24"}
                                        }),
                                    @"{
  ""self"": ""https://api.example.com/articles/24""
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithOneLinkAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {Keywords.Self, "https://api.example.com/articles/24"}
                                        }),
                                    @"{
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": null
  }
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithOneLinkAndMetaAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {
                                                    Keywords.Self,
                                                    new Link("https://api.example.com/articles/24",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                }
                                        }),
                                    @"{
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithOneLinkAndMetaAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {
                                                    Keywords.Self,
                                                    new Link("https://api.example.com/articles/24",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                }
                                        }),
                                    @"{
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithManyLinksAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {Keywords.Up, "https://api.example.com/articles"},
                                                {Keywords.Self, "https://api.example.com/articles/24"}
                                        }),
                                    @"{
  ""up"": ""https://api.example.com/articles"",
  ""self"": ""https://api.example.com/articles/24""
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithManyLinksAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {Keywords.Up, "https://api.example.com/articles"},
                                                {Keywords.Self, "https://api.example.com/articles/24"}
                                        }),
                                    @"{
  ""up"": {
    ""href"": ""https://api.example.com/articles"",
    ""meta"": null
  },
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": null
  }
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithManyLinksAndMetaAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {
                                                    Keywords.Up,
                                                    new Link("https://api.example.com/articles",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                },
                                                {
                                                    Keywords.Self,
                                                    new Link("https://api.example.com/articles/24",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                }
                                        }),
                                    @"{
  ""up"": {
    ""href"": ""https://api.example.com/articles"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  },
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                        },

                    new object[]
                        {
                            new JsonObjectSerializationUnitTestFactory(
                                x => new JsonObjectSerializeUnitTest<Links>(x),
                                x => new JsonObjectDeserializeUnitTest<Links>(x),
                                new JsonObjectSerializationUnitTestData(
                                    "WithManyLinksAndMetaAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new Links(new Dictionary<string, Link>
                                        {
                                                {
                                                    Keywords.Up,
                                                    new Link("https://api.example.com/articles",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                },
                                                {
                                                    Keywords.Self,
                                                    new Link("https://api.example.com/articles/24",
                                                        Meta.Create(new LinkMeta
                                                            {
                                                                IsPublic = true,
                                                                Version = "2.0"
                                                            }))
                                                }
                                        }),
                                    @"{
  ""up"": {
    ""href"": ""https://api.example.com/articles"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  },
  ""self"": {
    ""href"": ""https://api.example.com/articles/24"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                        },

                };
        #endregion
    }
}
