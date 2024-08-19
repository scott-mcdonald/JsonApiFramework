// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.Json;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json;

public class JsonDictionaryTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public JsonDictionaryTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Fact]
    public void TestJsonDictionaryToJsonWithEmptyDictionary()
    {
        // Arrange
        var empty = new JsonDictionary<object>();

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = empty.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(empty, toJsonSerializerOptions);
        Assert.Empty(jsonElement.EnumerateObject());
    }

    [Fact]
    public void TestJsonDictionaryToJsonWithIntDictionary()
    {
        // Arrange
        var intDictionary = new JsonDictionary<int>
            {
                {"one", 1},
                {"two", 2},
                {"three", 3}
            };

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = intDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(intDictionary, toJsonSerializerOptions);
        Assert.Equal(3, jsonElement.EnumerateObject().Count());

        Assert.Equal(1, jsonElement.GetProperty("one").GetInt32());
        Assert.Equal(2, jsonElement.GetProperty("two").GetInt32());
        Assert.Equal(3, jsonElement.GetProperty("three").GetInt32());
    }

    [Fact]
    public void TestJsonDictionaryToJsonWithStringDictionary()
    {
        // Arrange
        var stringDictionary = new JsonDictionary<string>
            {
                {"one", "1"},
                {"two", "2"},
                {"three", "3"}
            };

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {                
                WriteIndented = true
            };
        var json = stringDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(stringDictionary, toJsonSerializerOptions);
        Assert.Equal(3, jsonElement.EnumerateObject().Count());

        Assert.Equal("1", jsonElement.GetProperty("one").GetString());
        Assert.Equal("2", jsonElement.GetProperty("two").GetString());
        Assert.Equal("3", jsonElement.GetProperty("three").GetString());
    }

    [Fact]
    public void TestJsonDictionaryToJsonWithObjectDictionary()
    {
        // Arrange
        var emptyObject = new Empty();
        var personObject = new Person
            {
                PersonId = "1234",
                FirstName = "John",
                LastName = "Doe"
            };
        var objectDictionary = new JsonDictionary<object>
            {
                {"oneAsLong", 1},
                {"twoAsString", "2"},
                {"emptyObject", emptyObject},
                {"personObject", personObject},
            };

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = objectDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(objectDictionary, toJsonSerializerOptions);
        Assert.Equal(4, jsonElement.EnumerateObject().Count());

        Assert.Equal(1, jsonElement.GetProperty("oneAsLong").GetInt64());

        Assert.Equal("2", jsonElement.GetProperty("twoAsString").GetString());

        var emptyJsonElement = jsonElement.GetProperty("emptyObject");
        Assert.Empty(emptyJsonElement.EnumerateObject());

        var personJsonElement = jsonElement.GetProperty("personObject");
        Assert.Equal(3, personJsonElement.EnumerateObject().Count());

        Assert.Equal("1234", personJsonElement.GetProperty(personObject.GetMemberName(x => x.PersonId)).GetString());
        Assert.Equal("John", personJsonElement.GetProperty(personObject.GetMemberName(x => x.FirstName)).GetString());
        Assert.Equal("Doe", personJsonElement.GetProperty(personObject.GetMemberName(x => x.LastName)).GetString());
    }

    [Fact]
    public void TestJsonDictionaryParseWithEmptyDictionary()
    {
        // Arrange
        const string json = @"{}";

        // Act
        this.Output.WriteLine(json);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var emptyDictionary = JsonObject.Parse<JsonDictionary<object>>(json, toJsonSerializerOptions);

        // Assert
        Assert.NotNull(emptyDictionary);
        Assert.IsType<JsonDictionary<object>>(emptyDictionary);
        Assert.Equal(0, emptyDictionary.Count);
    }

    [Fact]
    public void TestJsonDictionaryParseWithIntDictionary()
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

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var intDictionary = JsonObject.Parse<JsonDictionary<int>>(json, toJsonSerializerOptions);

        // Assert
        Assert.NotNull(intDictionary);
        Assert.IsType<JsonDictionary<int>>(intDictionary);
        Assert.Equal(3, intDictionary.Count);

        Assert.Equal(1, intDictionary["one"]);
        Assert.Equal(2, intDictionary["two"]);
        Assert.Equal(3, intDictionary["three"]);
    }

    [Fact]
    public void TestJsonDictionaryParseWithStringDictionary()
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

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var stringDictionary = JsonObject.Parse<JsonDictionary<string>>(json, toJsonSerializerOptions);

        // Assert
        Assert.NotNull(stringDictionary);
        Assert.IsType<JsonDictionary<string>>(stringDictionary);
        Assert.Equal(3, stringDictionary.Count);

        Assert.Equal("1", stringDictionary["one"]);
        Assert.Equal("2", stringDictionary["two"]);
        Assert.Equal("3", stringDictionary["three"]);
    }

    [Fact]
    public void TestJsonDictionaryParseWithObjectDictionary()
    {
        // Arrange
        const string json =
@"
{
  ""oneAsLong"": 1,
  ""twoAsString"": ""2"",
  ""emptyObject"": {
    ""$type"": ""JsonApiFramework.Tests.Json.JsonDictionaryTests+Empty, JsonApiFramework.Core.Tests""
  },
  ""personObject"": {
    ""$type"": ""JsonApiFramework.Tests.Json.JsonDictionaryTests+Person, JsonApiFramework.Core.Tests"",
    ""personId"": ""1234"",
    ""lastName"": ""Doe"",
    ""firstName"": ""John""
  }
}
";

        // Act
        this.Output.WriteLine(json);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var objectDictionary = JsonObject.Parse<JsonDictionary<object>>(json, toJsonSerializerOptions);

        // Assert
        Assert.NotNull(objectDictionary);
        Assert.IsType<JsonDictionary<object>>(objectDictionary);
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
    public void TestJsonDictionaryKeyComparisonIsCaseInsensitive()
    {
        // Arrange
        var jsonDictionary = new JsonDictionary<int>
            {
                {"one", 1}
            };

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
    private class Empty : JsonObject
    { }

    private class Person : JsonObject
    {
        public string PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
    #endregion
}
