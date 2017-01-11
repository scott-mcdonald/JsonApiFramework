// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json
{
    public class JsonReadOnlyDictionaryTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonReadOnlyDictionaryTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(JsonReadOnlyDictionaryTestData))]
        public void TestJsonObjectSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(JsonReadOnlyDictionaryTestData))]
        public void TestJsonObjectDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryKeyComparisonIsCaseInsensitive()
        {
            // Arrange
            var innerDictionary = new Dictionary<string, int>
                {
                    {"one", 1}
                };
            var jsonReadOnlyDictionary = new JsonReadOnlyDictionary<int>(innerDictionary);

            this.WriteLine("Source");
            foreach (var keyAndValuePair in jsonReadOnlyDictionary)
            {
                this.WriteLine("  {0} {1}", keyAndValuePair.Key, keyAndValuePair.Value);
            }
            this.WriteLine();

            // Act

            // ReSharper disable InconsistentNaming
            var oneKeyExists = jsonReadOnlyDictionary.ContainsKey("one");
            var OneKeyExists = jsonReadOnlyDictionary.ContainsKey("One");
            var ONEKeyExists = jsonReadOnlyDictionary.ContainsKey("ONE");

            var twoKeyExists = jsonReadOnlyDictionary.ContainsKey("two");
            var TwoKeyExists = jsonReadOnlyDictionary.ContainsKey("Two");
            var TWOKeyExists = jsonReadOnlyDictionary.ContainsKey("TWO");
            // ReSharper restore InconsistentNaming

            // Assert
            Assert.True(oneKeyExists);
            Assert.True(OneKeyExists);
            Assert.True(ONEKeyExists);

            Assert.False(twoKeyExists);
            Assert.False(TwoKeyExists);
            Assert.False(TWOKeyExists);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryKeyComparisonIsCaseSensitive()
        {
            // Arrange
            var innerDictionary = new Dictionary<string, int>
                {
                    {"one", 1}
                };
            var jsonReadOnlyDictionary = new JsonReadOnlyDictionary<int>(innerDictionary, StringComparer.Ordinal);

            this.WriteLine("Source");
            foreach (var keyAndValuePair in jsonReadOnlyDictionary)
            {
                this.WriteLine("  {0} {1}", keyAndValuePair.Key, keyAndValuePair.Value);
            }
            this.WriteLine();

            // Act

            // ReSharper disable InconsistentNaming
            var oneKeyExists = jsonReadOnlyDictionary.ContainsKey("one");
            var OneKeyExists = jsonReadOnlyDictionary.ContainsKey("One");
            var ONEKeyExists = jsonReadOnlyDictionary.ContainsKey("ONE");

            var twoKeyExists = jsonReadOnlyDictionary.ContainsKey("two");
            var TwoKeyExists = jsonReadOnlyDictionary.ContainsKey("Two");
            var TWOKeyExists = jsonReadOnlyDictionary.ContainsKey("TWO");
            // ReSharper restore InconsistentNaming

            // Assert
            Assert.True(oneKeyExists);
            Assert.False(OneKeyExists);
            Assert.False(ONEKeyExists);

            Assert.False(twoKeyExists);
            Assert.False(TwoKeyExists);
            Assert.False(TWOKeyExists);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings();

        public static readonly JsonSerializerSettings TestSettingsWithAutoTypeNameHandling = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

        public static readonly IEnumerable<object[]> JsonReadOnlyDictionaryTestData = new[]
            {
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<int>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<int>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithIntDictionaryAnd0Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<int>(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<int>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<int>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithIntDictionaryAnd1Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<int>(new List<KeyValuePair<string, int>>
                                    {
                                        new KeyValuePair<string, int>("one", 1)
                                    }),
                                "{\"one\":1}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<int>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<int>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithIntDictionaryAnd2Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<int>(new List<KeyValuePair<string, int>>
                                    {
                                        new KeyValuePair<string, int>("one", 1),
                                        new KeyValuePair<string, int>("two", 2)
                                    }),
                                "{\"one\":1,\"two\":2}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<int>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<int>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithIntDictionaryAnd3Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<int>(new List<KeyValuePair<string, int>>
                                    {
                                        new KeyValuePair<string, int>("one", 1),
                                        new KeyValuePair<string, int>("two", 2),
                                        new KeyValuePair<string, int>("three", 3),
                                    }),
                                "{\"one\":1,\"two\":2,\"three\":3}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<string>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<string>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithStringDictionaryAnd0Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<string>(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<string>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<string>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithStringDictionaryAnd1Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<string>(new List<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("one", "1")
                                    }),
                                "{\"one\":\"1\"}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<string>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<string>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithStringDictionaryAnd2Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<string>(new List<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("one", "1"),
                                        new KeyValuePair<string, string>("two", "2")
                                    }),
                                "{\"one\":\"1\",\"two\":\"2\"}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<string>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<string>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithStringDictionaryAnd3Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<string>(new List<KeyValuePair<string, string>>
                                    {
                                        new KeyValuePair<string, string>("one", "1"),
                                        new KeyValuePair<string, string>("two", "2"),
                                        new KeyValuePair<string, string>("three", "3"),
                                    }),
                                "{\"one\":\"1\",\"two\":\"2\",\"three\":\"3\"}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonReadOnlyDictionarySerializeUnitTest<object>(x),
                            x => new JsonReadOnlyDictionaryDeserializeUnitTest<object>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithObjectDictionary",
                                TestSettingsWithAutoTypeNameHandling,
                                new JsonReadOnlyDictionary<object>(new List<KeyValuePair<string, object>>
                                    {
                                        new KeyValuePair<string, object>("OneAsInt", 1),
                                        new KeyValuePair<string, object>("TwoAsString", "2"),
                                        new KeyValuePair<string, object>("EmptyObject", new Empty()),
                                        new KeyValuePair<string, object>("PersonObject", new Person
                                            {
                                                PersonId = "1234",
                                                FirstName = "John",
                                                LastName = "Doe"
                                            }),
                                    }),
                                "{" +
                                "\"OneAsInt\":1,\"TwoAsString\":\"2\"," +
                                "\"EmptyObject\":{\"$type\":\"JsonApiFramework.Tests.Json.JsonReadOnlyDictionaryTests+Empty, JsonApiFramework.Core.Tests\"}," +
                                "\"PersonObject\":{\"$type\":\"JsonApiFramework.Tests.Json.JsonReadOnlyDictionaryTests+Person, JsonApiFramework.Core.Tests\"," +
                                "\"PersonId\":\"1234\"," +
                                "\"FirstName\":\"John\"," +
                                "\"LastName\":\"Doe\"}" +
                                "}"))
                    },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Empty : JsonObject
        {
            public override string ToString()
            {
                return "Empty";
            }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Person : JsonObject
        {
            [JsonProperty] public string PersonId { get; set; }
            [JsonProperty] public string FirstName { get; set; }
            [JsonProperty] public string LastName { get; set; }

            public override string ToString()
            {
                return "Person [id={0} firstName={1} lastName={2}]".FormatWith(this.PersonId, this.FirstName, this.LastName);
            }
        }
        #endregion
    }
}
