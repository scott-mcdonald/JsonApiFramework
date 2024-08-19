// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.Json;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json;

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
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = empty.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(json, toJsonSerializerOptions);
        Assert.Empty(jsonElement.EnumerateObject());
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
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = intDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(json, toJsonSerializerOptions);
        Assert.Equal(3, jsonElement.EnumerateObject().Count());

        Assert.Equal(1, jsonElement.GetProperty("one").GetInt32());
        Assert.Equal(2, jsonElement.GetProperty("two").GetInt32());
        Assert.Equal(3, jsonElement.GetProperty("three").GetInt32());
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
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = stringDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(json, toJsonSerializerOptions);
        Assert.Equal(3, jsonElement.EnumerateObject().Count());

        Assert.Equal("1", jsonElement.GetProperty("one").GetString());
        Assert.Equal("2", jsonElement.GetProperty("two").GetString());
        Assert.Equal("3", jsonElement.GetProperty("three").GetString());
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
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = stringDictionary.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(json, toJsonSerializerOptions);
        Assert.Equal(4, jsonElement.EnumerateObject().Count());

        Assert.Equal(1, jsonElement.GetProperty("oneAsLong").GetInt64());

        Assert.Equal("2", jsonElement.GetProperty("twoAsString").GetString());

        var jEmptyObject = jsonElement.GetProperty("emptyObject");
        Assert.Single(jEmptyObject.EnumerateObject()); // `Assert.Single()` is for the $type property

        var jPersonObject = jsonElement.GetProperty("personObject");
        Assert.Equal(1 + 3, jPersonObject.EnumerateObject().Count()); // 1 + is for the $type property

        Assert.Equal("1234", jPersonObject.GetProperty(personObject.GetMemberName(x => x.PersonId)).GetString());
        Assert.Equal("John", jPersonObject.GetProperty(personObject.GetMemberName(x => x.FirstName)).GetString());
        Assert.Equal("Doe", jPersonObject.GetProperty(personObject.GetMemberName(x => x.LastName)).GetString());
    }

    [Fact]
    public void TestJsonReadOnlyDictionaryParseWithEmptyDictionary()
    {
        // Arrange
        const string json = @"{}";

        // Act
        this.Output.WriteLine(json);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var emptyDictionary = JsonObject.Parse<JsonReadOnlyDictionary<object>>(json, toJsonSerializerOptions);

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

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var intDictionary = JsonObject.Parse<JsonReadOnlyDictionary<int>>(json, toJsonSerializerOptions);

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

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var stringDictionary = JsonObject.Parse<JsonReadOnlyDictionary<string>>(json, toJsonSerializerOptions);

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

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var objectDictionary = JsonObject.Parse<JsonReadOnlyDictionary<object>>(json, toJsonSerializerOptions);

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
