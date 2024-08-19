// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using JsonApiFramework.Json;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json;

public class JsonObjectTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public JsonObjectTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Fact]
    public void TestJsonObjectToJsonWithEmptyObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = EmptyObject.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(EmptyObject);
        AssertEmpty(EmptyObject, jsonElement.EnumerateObject());
    }

    [Fact]
    public void TestJsonObjectToJsonWithBasicObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = JohnDoePersonObject.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(JohnDoePersonObject, toJsonSerializerOptions);
        AssertPerson(JohnDoePersonObject, jsonElement);
    }

    [Fact]
    public void TestJsonObjectToJsonWithDerivedObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = JohnDoeEmployeeObject.ToJson<Person>(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(JohnDoeEmployeeObject, toJsonSerializerOptions);
        AssertEmployee(JohnDoeEmployeeObject, jsonElement);
    }

    [Fact]
    public void TestJsonObjectToJsonWithCompositeObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = BoardOfDirectorsObject.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(BoardOfDirectorsObject, toJsonSerializerOptions);
        AssertBoardOfDirectors(BoardOfDirectorsObject, jsonElement);
    }

    [Fact]
    public void TestJsonObjectToJsonWithCollectionObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = PeopleObject.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(PeopleObject, toJsonSerializerOptions);
        AssertPeople(PeopleObject, jsonElement);
    }

    [Fact]
    public void TestJsonObjectToJsonWithComplexObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = CompanyObject.ToJson(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(CompanyObject, toJsonSerializerOptions);
        AssertCompany(CompanyObject, jsonElement);
    }


    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithEmptyObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await EmptyObject.ToJsonAsync(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(EmptyObject, toJsonSerializerOptions);
        AssertEmpty(EmptyObject, jsonElement.EnumerateObject().ToList());
    }

    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithBasicObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await JohnDoePersonObject.ToJsonAsync(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(JohnDoePersonObject, toJsonSerializerOptions);
        AssertPerson(JohnDoePersonObject, jsonElement);
    }

    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithDerivedObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await JohnDoeEmployeeObject.ToJsonAsync<Person>(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(JohnDoeEmployeeObject, toJsonSerializerOptions);
        AssertEmployee(JohnDoeEmployeeObject, jsonElement);
    }

    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithCompositeObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await BoardOfDirectorsObject.ToJsonAsync(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(BoardOfDirectorsObject, toJsonSerializerOptions);
        AssertBoardOfDirectors(BoardOfDirectorsObject, jsonElement);
    }

    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithCollectionObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await PeopleObject.ToJsonAsync(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(PeopleObject, toJsonSerializerOptions);
        AssertPeople(PeopleObject, jsonElement);
    }

    [Fact]
    public async Task TestJsonObjectToJsonAsyncWithComplexObject()
    {
        // Arrange

        // Act
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var json = await CompanyObject.ToJsonAsync(toJsonSerializerOptions);
        this.Output.WriteLine(json);

        // Assert
        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json));

        var jsonElement = JsonSerializer.SerializeToElement(CompanyObject, toJsonSerializerOptions);
        AssertCompany(CompanyObject, jsonElement);
    }


    [Fact]
    public void TestJsonObjectParseWithEmptyObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(EmptyJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var empty = JsonObject.Parse<Empty>(EmptyJson, toJsonSerializerOptions);

        // Assert
        AssertEmpty(EmptyObject, empty);
    }

    [Fact]
    public void TestJsonObjectParseWithBasicObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(BasicJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var basicActual = JsonObject.Parse<Person>(BasicJson, toJsonSerializerOptions);

        // Assert
        AssertPerson(JohnDoePersonObject, basicActual);
    }

    [Fact]
    public void TestJsonObjectParseWithDerivedObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(DerivedJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var actual = JsonObject.Parse<Person>(DerivedJson, toJsonSerializerOptions);

        // Assert
        AssertPerson(JohnDoeEmployeeObject, actual);
    }

    [Fact]
    public void TestJsonObjectParseWithCompositeObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(CompositeJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var compositeActual = JsonObject.Parse<BoardOfDirectors>(CompositeJson, toJsonSerializerOptions);

        // Assert
        AssertBoardOfDirectors(BoardOfDirectorsObject, compositeActual);
    }

    [Fact]
    public void TestJsonObjectParseWithCollectionObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(CollectionJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var collectionActual = JsonObject.Parse<People>(CollectionJson, toJsonSerializerOptions);

        // Assert
        AssertPeople(PeopleObject, collectionActual);
    }

    [Fact]
    public void TestJsonObjectParseWithComplexObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(ComplexJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var complexActual = JsonObject.Parse<Company>(ComplexJson, toJsonSerializerOptions);

        // Assert
        AssertCompany(CompanyObject, complexActual);
    }


    [Fact]
    public async Task TestJsonObjectParseAsyncWithEmptyObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(EmptyJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var empty = await JsonObject.ParseAsync<Empty>(EmptyJson, toJsonSerializerOptions);

        // Assert
        AssertEmpty(EmptyObject, empty);
    }

    [Fact]
    public async Task TestJsonObjectParseAsyncWithBasicObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(BasicJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var basicActual = await JsonObject.ParseAsync<Person>(BasicJson, toJsonSerializerOptions);

        // Assert
        AssertPerson(JohnDoePersonObject, basicActual);
    }

    [Fact]
    public async Task TestJsonObjectParseAsyncWithDerivedObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(DerivedJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var actual = await JsonObject.ParseAsync<Person>(DerivedJson, toJsonSerializerOptions);

        // Assert
        AssertPerson(JohnDoeEmployeeObject, actual);
    }

    [Fact]
    public async Task TestJsonObjectParseAsyncWithCompositeObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(CompositeJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var compositeActual = await JsonObject.ParseAsync<BoardOfDirectors>(CompositeJson, toJsonSerializerOptions);

        // Assert
        AssertBoardOfDirectors(BoardOfDirectorsObject, compositeActual);
    }

    [Fact]
    public async Task TestJsonObjectParseAsyncWithCollectionObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(CollectionJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var collectionActual = await JsonObject.ParseAsync<People>(CollectionJson, toJsonSerializerOptions);

        // Assert
        AssertPeople(PeopleObject, collectionActual);
    }

    [Fact]
    public async Task TestJsonObjectParseAsyncWithComplexObject()
    {
        // Arrange

        // Act
        this.Output.WriteLine(ComplexJson);

        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        var complexActual = await JsonObject.ParseAsync<Company>(ComplexJson, toJsonSerializerOptions);

        // Assert
        AssertCompany(CompanyObject, complexActual);
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Assert Methods
    // ReSharper disable UnusedParameter.Local
    private static void AssertEmpty(Empty expected, IEnumerable<JsonProperty> actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.Empty(actual);
    }

    private static void AssertEmpty(Empty expected, Empty actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);
    }

    private static void AssertPerson(Person expected, JsonElement actual)
    {
        Assert.NotNull(expected);

        if (expected.GetType() == typeof(Employee))
        {
            var expectedEmployee = (Employee)expected;
            AssertEmployee(expectedEmployee, actual);
            return;
        }

        Assert.Equal(expected.PersonId, actual.GetProperty(expected.GetMemberName(x => x.PersonId)).GetString());
        Assert.Equal(expected.FirstName, actual.GetProperty(expected.GetMemberName(x => x.FirstName)).GetString());
        Assert.Equal(expected.LastName, actual.GetProperty(expected.GetMemberName(x => x.LastName)).GetString());
    }

    private static void AssertPerson(Person expected, Person actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        if (expected.GetType() == typeof(Employee))
        {
            var expectedEmployee = (Employee)expected;
            var actualEmployee = (Employee)actual;
            AssertEmployee(expectedEmployee, actualEmployee);
            return;
        }

        Assert.Equal(expected.PersonId, actual.PersonId);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
    }

    private static void AssertEmployee(Employee expected, JsonElement actual)
    {
        Assert.NotNull(expected);

        Assert.Equal(expected.PersonId, actual.GetProperty(expected.GetMemberName(x => x.PersonId)).GetString());
        Assert.Equal(expected.FirstName, actual.GetProperty(expected.GetMemberName(x => x.FirstName)).GetString());
        Assert.Equal(expected.LastName, actual.GetProperty(expected.GetMemberName(x => x.LastName)).GetString());
        Assert.Equal(expected.EmployeeNumber, actual.GetProperty(expected.GetMemberName(x => x.EmployeeNumber)).GetString());
    }

    private static void AssertEmployee(Employee expected, Employee actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.Equal(expected.PersonId, actual.PersonId);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.EmployeeNumber, actual.EmployeeNumber);
    }

    private static void AssertBoardOfDirectors(BoardOfDirectors expected, JsonElement actual)
    {
        Assert.NotNull(expected);

        var jPresidentToken = actual.GetProperty(expected.GetMemberName(x => x.President));
        AssertPerson(expected.President, jPresidentToken);

        var jVicePresidentToken = actual.GetProperty(expected.GetMemberName(x => x.VicePresident));
        AssertPerson(expected.VicePresident, jVicePresidentToken);
    }

    private static void AssertBoardOfDirectors(BoardOfDirectors expected, BoardOfDirectors actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        AssertPerson(expected.President, actual.President);
        AssertPerson(expected.VicePresident, actual.VicePresident);
    }

    private static void AssertPeople(People expected, JsonElement actual)
    {
        Assert.NotNull(expected);

        Assert.NotNull(expected.PersonCollection);

        var jsonElement = actual.GetProperty(expected.GetMemberName(x => x.PersonCollection));
        Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);

        var JsonElementArray = jsonElement.EnumerateArray();

        Assert.Equal(expected.PersonCollection.Count, JsonElementArray.Count());
        var count = expected.PersonCollection.Count;
        for (var i = 0; i < count; ++i)
        {
            var expectedPerson = expected.PersonCollection[i];
            var actualPerson = JsonElementArray.ToArray()[i];
            AssertPerson(expectedPerson, actualPerson);
        }
    }

    private static void AssertPeople(People expected, People actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.NotNull(expected.PersonCollection);
        Assert.NotNull(actual.PersonCollection);

        Assert.Equal(expected.PersonCollection.Count, actual.PersonCollection.Count);
        var count = expected.PersonCollection.Count;
        for (var i = 0; i < count; ++i)
        {
            var expectedPerson = expected.PersonCollection[i];
            var actualPerson = actual.PersonCollection[i];
            AssertPerson(expectedPerson, actualPerson);
        }
    }

    private static void AssertCompany(Company expected, JsonElement actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.Equal(expected.CompanyId, actual.GetProperty(expected.GetMemberName(x => x.CompanyId)).GetString());
        Assert.Equal(expected.CompanyName, actual.GetProperty(expected.GetMemberName(x => x.CompanyName)).GetString());

        AssertBoardOfDirectors(expected.BoardOfDirectors, actual.GetProperty(expected.GetMemberName(x => x.BoardOfDirectors)));
        AssertPeople(expected.CurrentEmployees, actual.GetProperty(expected.GetMemberName(x => x.CurrentEmployees)));
    }

    private static void AssertCompany(Company expected, Company actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.Equal(expected.CompanyId, actual.CompanyId);
        Assert.Equal(expected.CompanyName, actual.CompanyName);

        AssertBoardOfDirectors(expected.BoardOfDirectors, actual.BoardOfDirectors);
        AssertPeople(expected.CurrentEmployees, actual.CurrentEmployees);
    }
    // ReSharper restore UnusedParameter.Local
    #endregion

    // PRIVATE TYPES ////////////////////////////////////////////////////
    #region Test Types
    private class Empty : JsonObject
    { }

    [JsonDerivedType(typeof(Employee))]
    private class Person : JsonObject
    {
        public string PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    private class Employee : Person
    {
        public string EmployeeNumber { get; set; }
    }

    private class BoardOfDirectors : JsonObject
    {
        public Person President { get; set; }
        public Person VicePresident { get; set; }
    }

    private class People : JsonObject
    {
        public List<Person> PersonCollection { get; set; }
    }

    private class Company : JsonObject
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public BoardOfDirectors BoardOfDirectors { get; set; }
        public People CurrentEmployees { get; set; }
    }
    #endregion

    // PRIVATE FIELDS ////////////////////////////////////////////////////
    #region Test Data
    private static readonly Empty EmptyObject = new Empty();

    private static readonly Person JohnDoePersonObject = new Person
        {
            PersonId = "1234",
            FirstName = "John",
            LastName = "Doe"
        };
    private static readonly Employee JohnDoeEmployeeObject = new Employee
        {
            PersonId = "1234",
            FirstName = "John",
            LastName = "Doe",
            EmployeeNumber = "1111111111"
        };

    private static readonly Person JaneDoePersonObject = new Person
        {
            PersonId = "5678",
            FirstName = "Jane",
            LastName = "Doe"
        };
    private static readonly Employee JaneDoeEmployeeObject = new Employee
        {
            PersonId = "5678",
            FirstName = "Jane",
            LastName = "Doe",
            EmployeeNumber = "2222222222"
        };

    private static readonly Person GeorgeWashingtonPersonObject = new Person
        {
            PersonId = "1111",
            FirstName = "George",
            LastName = "Washington"
        };

    private static readonly Person JohnAdamsPersonObject = new Person
        {
            PersonId = "2222",
            FirstName = "John",
            LastName = "Adams"
        };

    private static readonly BoardOfDirectors BoardOfDirectorsObject = new BoardOfDirectors
        {
            President = GeorgeWashingtonPersonObject,
            VicePresident = JohnAdamsPersonObject
        };

    private static readonly People PeopleObject = new People
        {
            PersonCollection = new List<Person> { JohnDoePersonObject, JaneDoePersonObject }
        };

    private static readonly People CurrentEmployeesObject = new People
        {
            PersonCollection = new List<Person> { JohnDoeEmployeeObject, JaneDoeEmployeeObject }
        };

    private static readonly Company CompanyObject = new Company
        {
            CompanyId = "Acme",
            CompanyName = "Acme, Inc.",
            BoardOfDirectors = BoardOfDirectorsObject,
            CurrentEmployees = CurrentEmployeesObject
        };

    const string EmptyJson =
@"{}";

    const string BasicJson =
@"
{
    ""personId"": ""1234"",
    ""lastName"": ""Doe"",
    ""firstName"": ""John""
}
";

    const string DerivedJson =
@"
{
    ""$type"": ""JsonApiFramework.Tests.Json.JsonObjectTests+Employee, JsonApiFramework.Core.Tests"",
    ""personId"": ""1234"",
    ""lastName"": ""Doe"",
    ""firstName"": ""John"",
    ""employeeNumber"": ""1111111111""
}
";

    const string CompositeJson =
@"
{
    ""president"": {
        ""personId"": ""1111"",
        ""lastName"": ""Washington"",
        ""firstName"": ""George""
    },
    ""vicePresident"": {
        ""personId"": ""2222"",
        ""lastName"": ""Adams"",
        ""firstName"": ""John""
    }
}
";

    const string CollectionJson =
@"
{
  ""personCollection"": [
    {
      ""personId"": ""1234"",
      ""lastName"": ""Doe"",
      ""firstName"": ""John""
    },
    {
      ""personId"": ""5678"",
      ""lastName"": ""Doe"",
      ""firstName"": ""Jane""
    }
  ]
}
";

    const string ComplexJson =
@"
{
  ""companyId"": ""Acme"",
  ""companyName"": ""Acme, Inc."",
  ""boardOfDirectors"": {
    ""president"": {
      ""personId"": ""1111"",
      ""lastName"": ""Washington"",
      ""firstName"": ""George""
    },
    ""vicePresident"": {
      ""personId"": ""2222"",
      ""lastName"": ""Adams"",
      ""firstName"": ""John""
    }
  },
  ""currentEmployees"": {
    ""personCollection"": [
      {
        ""$type"": ""JsonApiFramework.Tests.Json.JsonObjectTests+Employee, JsonApiFramework.Core.Tests"",
        ""personId"": ""1234"",
        ""lastName"": ""Doe"",
        ""firstName"": ""John"",
        ""employeeNumber"": ""1111111111""
      },
      {
        ""$type"": ""JsonApiFramework.Tests.Json.JsonObjectTests+Employee, JsonApiFramework.Core.Tests"",
        ""personId"": ""5678"",
        ""lastName"": ""Doe"",
        ""firstName"": ""Jane"",
        ""employeeNumber"": ""2222222222""
      }
    ]
  }
}
";
    #endregion
}
