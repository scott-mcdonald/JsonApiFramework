// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json
{
    public class JsonReadOnlyDictionaryTests : XUnitTest
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
        [MemberData("JsonReadOnlyDictionaryTestData")]
        public void TestJsonObjectParse(JsonUnitTestFactoryAndData factoryAndData)
        {
            var data = factoryAndData.Data;
            var factory = factoryAndData.ParseFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("JsonReadOnlyDictionaryTestData")]
        public void TestJsonObjectToJson(JsonUnitTestFactoryAndData factoryAndData)
        {
            var data = factoryAndData.Data;
            var factory = factoryAndData.ToJsonFactory;
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<int>(x),
                            x => new ToJsonUnitTest<int>(x),
                            new JsonUnitTestData(
                                "WithIntDictionaryAnd0Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<int>(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<int>(x),
                            x => new ToJsonUnitTest<int>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<int>(x),
                            x => new ToJsonUnitTest<int>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<int>(x),
                            x => new ToJsonUnitTest<int>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<string>(x),
                            x => new ToJsonUnitTest<string>(x),
                            new JsonUnitTestData(
                                "WithStringDictionaryAnd0Values",
                                TestSettings,
                                new JsonReadOnlyDictionary<string>(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<string>(x),
                            x => new ToJsonUnitTest<string>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<string>(x),
                            x => new ToJsonUnitTest<string>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<string>(x),
                            x => new ToJsonUnitTest<string>(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<object>(x),
                            x => new ToJsonUnitTest<object>(x),
                            new JsonUnitTestData(
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

        #region UnitTest Types
        public class ParseUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public ParseUnitTest(JsonUnitTestData data)
                : base(data.Name)
            {
                this.Settings = data.Settings;
                this.Source = data.ExpectedJson;
                this.ExpectedDictionary = (JsonReadOnlyDictionary<T>)data.ExpectedObject;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source = {0}", this.Source);
                this.WriteLine();
                this.WriteLine("Expected Dictionary");
                foreach (var expectedKeyAndValuePair in this.ExpectedDictionary)
                {
                    this.WriteLine("  {0} {1}", expectedKeyAndValuePair.Key, expectedKeyAndValuePair.Value);
                }
            }

            protected override void Act()
            {
                var source = this.Source;
                var settings = this.Settings;
                var actualDictionary = JsonObject.Parse<JsonReadOnlyDictionary<T>>(source, settings);

                this.ActualDictionary = actualDictionary;
                this.WriteLine();
                this.WriteLine("Actual Dictionary");
                foreach (var actualKeyAndValuePair in this.ActualDictionary)
                {
                    this.WriteLine("  {0} {1}", actualKeyAndValuePair.Key, actualKeyAndValuePair.Value);
                }
            }

            protected override void Assert()
            {
                this.ActualDictionary.ShouldAllBeEquivalentTo(this.ExpectedDictionary);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private JsonReadOnlyDictionary<T> ActualDictionary { get; set; }
            #endregion

            #region User Supplied Properties
            private string Source { get; set; }
            private JsonSerializerSettings Settings { get; set; }
            private JsonReadOnlyDictionary<T> ExpectedDictionary { get; set; }
            #endregion
        }

        public class ToJsonUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public ToJsonUnitTest(JsonUnitTestData data)
                : base(data.Name)
            {
                this.Settings = data.Settings;
                this.Source = (JsonReadOnlyDictionary<T>)data.ExpectedObject;
                this.ExpectedJson = data.ExpectedJson;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source");
                foreach (var expectedKeyAndValuePair in this.Source)
                {
                    this.WriteLine("  {0} {1}", expectedKeyAndValuePair.Key, expectedKeyAndValuePair.Value);
                }
                this.WriteLine();
                this.WriteLine("Expected JSON = {0}", this.ExpectedJson);
            }

            protected override void Act()
            {
                var settings = this.Settings;
                this.ActualJson = Source.ToJson(settings);
                this.WriteLine();
                this.WriteLine("Actual JSON   = {0}", this.ActualJson);
            }

            protected override void Assert()
            {
                this.ActualJson.Should().Be(this.ExpectedJson);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string ActualJson { get; set; }
            #endregion

            #region User Supplied Properties
            private JsonReadOnlyDictionary<T> Source { get; set; }
            private JsonSerializerSettings Settings { get; set; }
            private string ExpectedJson { get; set; }
            #endregion
        }
        #endregion
    }
}
