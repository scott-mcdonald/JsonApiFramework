// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JsonApiFramework.Json;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Json
{
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
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = EmptyObject.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertEmpty(EmptyObject, jObject);
        }

        [Fact]
        public void TestJsonObjectToJsonWithBasicObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = JohnDoePersonObject.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertPerson(JohnDoePersonObject, jObject);
        }

        [Fact]
        public void TestJsonObjectToJsonWithDerivedObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var json = JohnDoeEmployeeObject.ToJson<Person>(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertEmployee(JohnDoeEmployeeObject, jObject);
        }

        [Fact]
        public void TestJsonObjectToJsonWithCompositeObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = BoardOfDirectorsObject.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertBoardOfDirectors(BoardOfDirectorsObject, jObject);
        }

        [Fact]
        public void TestJsonObjectToJsonWithCollectionObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = PeopleObject.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertPeople(PeopleObject, jObject);
        }

        [Fact]
        public void TestJsonObjectToJsonWithComplexObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var json = CompanyObject.ToJson(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertCompany(CompanyObject, jObject);
        }


        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithEmptyObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = await EmptyObject.ToJsonAsync(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertEmpty(EmptyObject, jObject);
        }

        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithBasicObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = await JohnDoePersonObject.ToJsonAsync(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertPerson(JohnDoePersonObject, jObject);
        }

        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithDerivedObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var json = await JohnDoeEmployeeObject.ToJsonAsync<Person>(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertEmployee(JohnDoeEmployeeObject, jObject);
        }

        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithCompositeObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = await BoardOfDirectorsObject.ToJsonAsync(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertBoardOfDirectors(BoardOfDirectorsObject, jObject);
        }

        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithCollectionObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var json = await PeopleObject.ToJsonAsync(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertPeople(PeopleObject, jObject);
        }

        [Fact]
        public async Task TestJsonObjectToJsonAsyncWithComplexObject()
        {
            // Arrange

            // Act
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var json = await CompanyObject.ToJsonAsync(toJsonSerializerSettings);
            this.Output.WriteLine(json);

            // Assert
            Assert.NotNull(json);
            Assert.False(String.IsNullOrEmpty(json));

            var jObject = JObject.Parse(json);
            AssertCompany(CompanyObject, jObject);
        }


        [Fact]
        public void TestJsonObjectParseWithEmptyObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(EmptyJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var empty = JsonObject.Parse<Empty>(EmptyJson, toJsonSerializerSettings);

            // Assert
            AssertEmpty(EmptyObject, empty);
        }

        [Fact]
        public void TestJsonObjectParseWithBasicObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(BasicJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var basicActual = JsonObject.Parse<Person>(BasicJson, toJsonSerializerSettings);

            // Assert
            AssertPerson(JohnDoePersonObject, basicActual);
        }

        [Fact]
        public void TestJsonObjectParseWithDerivedObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(DerivedJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var actual = JsonObject.Parse<Person>(DerivedJson, toJsonSerializerSettings);

            // Assert
            AssertPerson(JohnDoeEmployeeObject, actual);
        }

        [Fact]
        public void TestJsonObjectParseWithCompositeObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(CompositeJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var compositeActual = JsonObject.Parse<BoardOfDirectors>(CompositeJson, toJsonSerializerSettings);

            // Assert
            AssertBoardOfDirectors(BoardOfDirectorsObject, compositeActual);
        }

        [Fact]
        public void TestJsonObjectParseWithCollectionObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(CollectionJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var collectionActual = JsonObject.Parse<People>(CollectionJson, toJsonSerializerSettings);

            // Assert
            AssertPeople(PeopleObject, collectionActual);
        }

        [Fact]
        public void TestJsonObjectParseWithComplexObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(ComplexJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var complexActual = JsonObject.Parse<Company>(ComplexJson, toJsonSerializerSettings);

            // Assert
            AssertCompany(CompanyObject, complexActual);
        }


        [Fact]
        public async Task TestJsonObjectParseAsyncWithEmptyObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(EmptyJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var empty = await JsonObject.ParseAsync<Empty>(EmptyJson, toJsonSerializerSettings);

            // Assert
            AssertEmpty(EmptyObject, empty);
        }

        [Fact]
        public async Task TestJsonObjectParseAsyncWithBasicObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(BasicJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var basicActual = await JsonObject.ParseAsync<Person>(BasicJson, toJsonSerializerSettings);

            // Assert
            AssertPerson(JohnDoePersonObject, basicActual);
        }

        [Fact]
        public async Task TestJsonObjectParseAsyncWithDerivedObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(DerivedJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var actual = await JsonObject.ParseAsync<Person>(DerivedJson, toJsonSerializerSettings);

            // Assert
            AssertPerson(JohnDoeEmployeeObject, actual);
        }

        [Fact]
        public async Task TestJsonObjectParseAsyncWithCompositeObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(CompositeJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var compositeActual = await JsonObject.ParseAsync<BoardOfDirectors>(CompositeJson, toJsonSerializerSettings);

            // Assert
            AssertBoardOfDirectors(BoardOfDirectorsObject, compositeActual);
        }

        [Fact]
        public async Task TestJsonObjectParseAsyncWithCollectionObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(CollectionJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            var collectionActual = await JsonObject.ParseAsync<People>(CollectionJson, toJsonSerializerSettings);

            // Assert
            AssertPeople(PeopleObject, collectionActual);
        }

        [Fact]
        public async Task TestJsonObjectParseAsyncWithComplexObject()
        {
            // Arrange

            // Act
            this.Output.WriteLine(ComplexJson);

            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
            var complexActual = await JsonObject.ParseAsync<Company>(ComplexJson, toJsonSerializerSettings);

            // Assert
            AssertCompany(CompanyObject, complexActual);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Assert Methods
        // ReSharper disable UnusedParameter.Local
        private static void AssertEmpty(Empty expected, IEnumerable<JToken> actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(0, actual.Count());
        }

        private static void AssertEmpty(Empty expected, Empty actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);
        }

        private static void AssertPerson(Person expected, JToken actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            if (expected.GetType() == typeof(Employee))
            {
                var expectedEmployee = (Employee)expected;
                AssertEmployee(expectedEmployee, actual);
                return;
            }

            Assert.Equal(expected.PersonId, (string)actual.SelectToken(expected.GetMemberName(x => x.PersonId)));
            Assert.Equal(expected.FirstName, (string)actual.SelectToken(expected.GetMemberName(x => x.FirstName)));
            Assert.Equal(expected.LastName, (string)actual.SelectToken(expected.GetMemberName(x => x.LastName)));
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

        private static void AssertEmployee(Employee expected, JToken actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(expected.PersonId, (string)actual.SelectToken(expected.GetMemberName(x => x.PersonId)));
            Assert.Equal(expected.FirstName, (string)actual.SelectToken(expected.GetMemberName(x => x.FirstName)));
            Assert.Equal(expected.LastName, (string)actual.SelectToken(expected.GetMemberName(x => x.LastName)));
            Assert.Equal(expected.EmployeeNumber, (string)actual.SelectToken(expected.GetMemberName(x => x.EmployeeNumber)));
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

        private static void AssertBoardOfDirectors(BoardOfDirectors expected, JToken actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            var jPresidentToken = actual.SelectToken(expected.GetMemberName(x => x.President));
            AssertPerson(expected.President, jPresidentToken);

            var jVicePresidentToken = actual.SelectToken(expected.GetMemberName(x => x.VicePresident));
            AssertPerson(expected.VicePresident, jVicePresidentToken);
        }

        private static void AssertBoardOfDirectors(BoardOfDirectors expected, BoardOfDirectors actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            AssertPerson(expected.President, actual.President);
            AssertPerson(expected.VicePresident, actual.VicePresident);
        }

        private static void AssertPeople(People expected, JToken actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.NotNull(expected.PersonCollection);

            var jToken = actual.SelectToken(expected.GetMemberName(x => x.PersonCollection));
            Assert.NotNull(jToken);
            Assert.Equal(JTokenType.Array, jToken.Type);

            var jTokenArray = (JArray)jToken;

            Assert.Equal(expected.PersonCollection.Count, jTokenArray.Count);
            var count = expected.PersonCollection.Count;
            for (var i = 0; i < count; ++i)
            {
                var expectedPerson = expected.PersonCollection[i];
                var actualPerson = jTokenArray[i];
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

        private static void AssertCompany(Company expected, JToken actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(expected.CompanyId, (string)actual.SelectToken(expected.GetMemberName(x => x.CompanyId)));
            Assert.Equal(expected.CompanyName, (string)actual.SelectToken(expected.GetMemberName(x => x.CompanyName)));

            AssertBoardOfDirectors(expected.BoardOfDirectors, actual.SelectToken(expected.GetMemberName(x => x.BoardOfDirectors)));
            AssertPeople(expected.CurrentEmployees, actual.SelectToken(expected.GetMemberName(x => x.CurrentEmployees)));
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

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Employee : Person
        {
            [JsonProperty] public string EmployeeNumber { get; set; }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class BoardOfDirectors : JsonObject
        {
            [JsonProperty] public Person President { get; set; }
            [JsonProperty] public Person VicePresident { get; set; }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class People : JsonObject
        {
            [JsonProperty] public List<Person> PersonCollection { get; set; }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Company : JsonObject
        {
            [JsonProperty] public string CompanyId { get; set; }
            [JsonProperty] public string CompanyName { get; set; }
            [JsonProperty] public BoardOfDirectors BoardOfDirectors { get; set; }
            [JsonProperty] public People CurrentEmployees { get; set; }
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
}
