// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [Fact]
        public void TestJsonReadOnlyDictionaryToJsonWithEmptyDictionary()
        {
            // Arrange
            var empty = new JsonReadOnlyDictionary<object>(new Dictionary<string, object>());

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = empty.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            Assert.NotNull(jObject);
            Assert.Equal(0, jObject.Count);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryToJsonWithIntDictionary()
        {
            // Arrange
            var innerDictionary = new Dictionary<string, int>
                {
                    {"one", 1},
                    {"two", 2},
                    {"three", 3}
                };
            var intDictionary = new JsonReadOnlyDictionary<int>(innerDictionary);

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = intDictionary.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            Assert.NotNull(jObject);
            Assert.Equal(3, jObject.Count);

            Assert.Equal(1, (int)jObject.SelectToken("one"));
            Assert.Equal(2, (int)jObject.SelectToken("two"));
            Assert.Equal(3, (int)jObject.SelectToken("three"));
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryToJsonWithStringDictionary()
        {
            // Arrange
            var innerDictionary = new Dictionary<string, string>
                {
                    {"one", "1"},
                    {"two", "2"},
                    {"three", "3"}
                };
            var stringDictionary = new JsonReadOnlyDictionary<string>(innerDictionary);

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = stringDictionary.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            Assert.NotNull(jObject);
            Assert.Equal(3, jObject.Count);

            Assert.Equal("1", (string)jObject.SelectToken("one"));
            Assert.Equal("2", (string)jObject.SelectToken("two"));
            Assert.Equal("3", (string)jObject.SelectToken("three"));
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryToJsonWithObjectDictionary()
        {
            // Arrange
            var emptyObject = new Empty();
            var personObject = new Person
                {
                    PersonId = "1234",
                    FirstName = "John",
                    LastName = "Doe"
                };
            var innerDictionary = new Dictionary<string, object>
                {
                    {"oneAsLong", 1},
                    {"twoAsString", "2"},
                    {"emptyObject", emptyObject},
                    {"personObject", personObject},
                };
            var stringDictionary = new JsonReadOnlyDictionary<object>(innerDictionary);

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var json = stringDictionary.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            Assert.NotNull(jObject);
            Assert.Equal(4, jObject.Count);

            Assert.Equal(1, (long)jObject.SelectToken("oneAsLong"));

            Assert.Equal("2", (string)jObject.SelectToken("twoAsString"));

            var jEmptyObject = (JObject)jObject.SelectToken("emptyObject");
            Assert.Equal(1 + 0, jEmptyObject.Count); // 1 + is for the $type property

            var jPersonObject = (JObject)jObject.SelectToken("personObject");
            Assert.Equal(1 + 3, jPersonObject.Count); // 1 + is for the $type property

            Assert.Equal("1234", (string)jPersonObject.SelectToken(personObject.GetMemberName(x => x.PersonId)));
            Assert.Equal("John", (string)jPersonObject.SelectToken(personObject.GetMemberName(x => x.FirstName)));
            Assert.Equal("Doe", (string)jPersonObject.SelectToken(personObject.GetMemberName(x => x.LastName)));
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryParseWithEmptyDictionary()
        {
            // Arrange
            const string json = @"{}";

            // Act
            this.Output.WriteLine(json);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var emptyDictionary = JsonObject.Parse<JsonReadOnlyDictionary<object>>(json, toJsonSerializerSettings);

            // Assert
            Assert.NotNull(emptyDictionary);
            Assert.IsType<JsonReadOnlyDictionary<object>>(emptyDictionary);
            Assert.Equal(0, emptyDictionary.Count);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryParseWithIntDictionary()
        {
            // Arrange
            const string json =
@"
{
  ""one"": 1,
  ""two"": 2,
  ""three"": 3
}
";

            // Act
            this.Output.WriteLine(json);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var intDictionary = JsonObject.Parse<JsonReadOnlyDictionary<int>>(json, toJsonSerializerSettings);

            // Assert
            Assert.NotNull(intDictionary);
            Assert.IsType<JsonReadOnlyDictionary<int>>(intDictionary);
            Assert.Equal(3, intDictionary.Count);

            Assert.Equal(1, intDictionary["one"]);
            Assert.Equal(2, intDictionary["two"]);
            Assert.Equal(3, intDictionary["three"]);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryParseWithStringDictionary()
        {
            // Arrange
            const string json =
@"
{
  ""one"": ""1"",
  ""two"": ""2"",
  ""three"": ""3""
}
";

            // Act
            this.Output.WriteLine(json);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var stringDictionary = JsonObject.Parse<JsonReadOnlyDictionary<string>>(json, toJsonSerializerSettings);

            // Assert
            Assert.NotNull(stringDictionary);
            Assert.IsType<JsonReadOnlyDictionary<string>>(stringDictionary);
            Assert.Equal(3, stringDictionary.Count);

            Assert.Equal("1", stringDictionary["one"]);
            Assert.Equal("2", stringDictionary["two"]);
            Assert.Equal("3", stringDictionary["three"]);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryParseWithObjectDictionary()
        {
            // Arrange
            const string json =
@"
{
  ""oneAsLong"": 1,
  ""twoAsString"": ""2"",
  ""emptyObject"": {
    ""$type"": ""JsonApiFramework.Tests.Json.JsonReadOnlyDictionaryTests+Empty, JsonApiFramework.Core.Tests""
  },
  ""personObject"": {
    ""$type"": ""JsonApiFramework.Tests.Json.JsonReadOnlyDictionaryTests+Person, JsonApiFramework.Core.Tests"",
    ""personId"": ""1234"",
    ""lastName"": ""Doe"",
    ""firstName"": ""John""
  }
}
";

            // Act
            this.Output.WriteLine(json);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var objectDictionary = JsonObject.Parse<JsonReadOnlyDictionary<object>>(json, toJsonSerializerSettings);

            // Assert
            Assert.NotNull(objectDictionary);
            Assert.IsType<JsonReadOnlyDictionary<object>>(objectDictionary);
            Assert.Equal(4, objectDictionary.Count);

            Assert.Equal(1, (long)objectDictionary["oneAsLong"]);
            Assert.Equal("2", (string)objectDictionary["twoAsString"]);

            var emptyObject = objectDictionary["emptyObject"];
            Assert.NotNull(emptyObject);
            Assert.IsType<Empty>(emptyObject);

            var empty = (Empty)emptyObject;
            Assert.NotNull(empty);

            var personObject = objectDictionary["personObject"];
            Assert.NotNull(personObject);
            Assert.IsType<Person>(personObject);

            var person = (Person)personObject;
            Assert.NotNull(person);
            Assert.Equal("1234", person.PersonId);
            Assert.Equal("Doe", person.LastName);
            Assert.Equal("John", person.FirstName);
        }

        [Fact]
        public void TestJsonReadOnlyDictionaryKeyComparisonIsCaseInsensitive()
        {
            // Arrange
            var innerDictionary = new Dictionary<string, int>
                {
                    {"one", 1}
                };
            var jsonDictionary = new JsonReadOnlyDictionary<int>(innerDictionary);

            // Act

            // ReSharper disable InconsistentNaming
            var oneKeyExists = jsonDictionary.ContainsKey("one");
            var OneKeyExists = jsonDictionary.ContainsKey("One");
            var ONEKeyExists = jsonDictionary.ContainsKey("ONE");

            var twoKeyExists = jsonDictionary.ContainsKey("two");
            var TwoKeyExists = jsonDictionary.ContainsKey("Two");
            var TWOKeyExists = jsonDictionary.ContainsKey("TWO");
            // ReSharper restore InconsistentNaming

            // Assert
            Assert.True(oneKeyExists);
            Assert.True(OneKeyExists);
            Assert.True(ONEKeyExists);

            Assert.False(twoKeyExists);
            Assert.False(TwoKeyExists);
            Assert.False(TWOKeyExists);
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Empty : JsonObject
        { }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Person : JsonObject
        {
            [JsonProperty] public string PersonId { get; set; }
            [JsonProperty] public string LastName { get; set; }
            [JsonProperty] public string FirstName { get; set; }
        }
        #endregion
    }
}
