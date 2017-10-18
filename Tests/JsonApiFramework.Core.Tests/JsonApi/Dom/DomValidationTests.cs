// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomValidationTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomValidationTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomDeserializationValidationTestData))]
        public void TestDomDeserializationValidation(IUnitTest unitTest)
        {
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


        public static readonly IEnumerable<object[]> DomDeserializationValidationTestData = new[]
            {
                new object[]
                    {
                        new DomDeserializationValidationUnitTest<IDomDocument>(
                            "WithDocumentJsonNotBeingNullOrAnObject",
                            TestJsonSerializerSettings,
                            "1234",
                            1)
                    },

                new object[]
                    {
                        new DomDeserializationValidationUnitTest<IDomDocument>(
                            "WithDocumentJsonContainsBothDataAndErrors",
                            TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""data"": [],
  ""errors"": []
}",
                            1)
                    },

                new object[]
                    {
                        new DomDeserializationValidationUnitTest<IDomDocument>(
                            "WithDocumentJsonContainsUnknownMembers",
                            TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""data"": [],
  ""foo"": {},
  ""bar"": {}
}",
                            2)
                    },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentLinkJsonNotBeingNullOrAnObject",
                        TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": 1243
  },
  ""data"": []
}",
                        1)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataJsonNotBeingNullOrAnObjectOrAnArray",
                        TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""data"": 1234
}",
                        1)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentErrorsJsonNotBeingAnArray",
                        TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles""
  },
  ""errors"": 1234
}",
                        1)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataResourceJsonTypeAndIdNotBeingAString",
                        TestJsonSerializerSettings,
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""links"": {
    ""self"": ""https://api.example.com/articles/42""
  },
  ""data"": {
    ""type"": 1234,
    ""id"": true,
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
}",
                        2)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataResourceRelationshipsLinksJsonNotBeingAStringOrAnObject",
                        TestJsonSerializerSettings,
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
          ""related"": 1234
        }
      },
      ""comments"": {
        ""links"": {
          ""related"": false
        }
      }
    },
    ""links"": {
      ""self"": ""https://api.example.com/articles/42""
    }
  }
}",
                        2)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataResourceRelationshipsDataJsonNotBeingNullOrAnObject",
                        TestJsonSerializerSettings,
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
        ""data"": 1234
      },
      ""comments"": {
        ""links"": {
          ""related"": ""https://api.example.com/articles/42/comments""
        },
        ""data"": [false, ""1234""]
      }
    },
    ""links"": {
      ""self"": ""https://api.example.com/articles/42""
    }
  }
}",
                        3)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataResourceRelationshipsDataJsonNotBeingResourceIdentifiers",
                        TestJsonSerializerSettings,
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
        ""data"": {""foo"": 24, ""bar"": 42}
      },
      ""comments"": {
        ""links"": {
          ""related"": ""https://api.example.com/articles/42/comments""
        },
        ""data"": [{""foo"": 24, ""bar"":42}, {""abc"": {}, ""xyz"":[]}]
      }
    },
    ""links"": {
      ""self"": ""https://api.example.com/articles/42""
    }
  }
}",
                        6)
                },

                new object[]
                {
                    new DomDeserializationValidationUnitTest<IDomDocument>(
                        "WithDocumentDataResourceLinkJsonNotBeingNullOrAnObject",
                        TestJsonSerializerSettings,
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
      ""self"": 1243
    }
  }
}",
                        1)
                },

        };
        #endregion

        #region Test Types
        public class DomDeserializationValidationUnitTest<T> : UnitTest
            where T : IDomNode
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public DomDeserializationValidationUnitTest(string name, JsonSerializerSettings settings, string sourceJson, int expectedErrorsCount)
                : base(name)
            {
                this.Settings = settings;
                this.SourceJson = sourceJson;
                this.ExpectedErrorsCount = expectedErrorsCount;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source JSON");
                this.WriteLine(this.SourceJson);
                this.WriteLine();
                this.WriteLine("Expected Errors");
                this.WriteLine("  Count = {0}", this.ExpectedErrorsCount);
            }

            protected override void Act()
            {
                this.ActualException = new Exception();
                try
                {
                    JsonObject.Parse<T>(this.SourceJson, this.Settings);
                }
                catch (Exception exception)
                {
                    this.ActualException = exception;
                }
            }

            protected override void Assert()
            {
                var actualException = this.ActualException;
                actualException.Should().BeOfType<JsonApiDeserializationException>();

                var jsonApiDeserializationException = (JsonApiDeserializationException)actualException;

                var actualStatusCode = jsonApiDeserializationException.StatusCode;
                actualStatusCode.Should().Be(HttpStatusCode.BadRequest);

                var actualErrors = jsonApiDeserializationException.Errors.ToList();
                var actualErrorsCount = actualErrors.Count;

                this.WriteLine();
                this.WriteLine("Actual Errors");
                this.WriteLine("  Count = {0}", actualErrorsCount);
                this.WriteLine();

                for (var i = 0; i < actualErrorsCount; i++)
                {
                    var actualError = actualErrors[i];
                    this.WriteLine($"[{i}] {actualError}");
                }

                actualErrorsCount.Should().Be(this.ExpectedErrorsCount);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private Exception ActualException { get; set; }
            #endregion

            #region User Supplied Properties
            private JsonSerializerSettings Settings { get; }
            private string SourceJson { get; }
            private int ExpectedErrorsCount { get; }
            #endregion
        }
        #endregion
    }
}
