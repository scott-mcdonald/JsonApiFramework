// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

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
        [Theory]
        [MemberData(nameof(JsonObjectTestData))]
        public void TestJsonObjectSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(JsonObjectTestData))]
        public void TestJsonObjectDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings();

        public static readonly IEnumerable<object[]> JsonObjectTestData = new[]
            {
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Empty>(x),
                            x => new JsonObjectDeserializeUnitTest<Empty>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithEmptyObject",
                                TestSettings,
                                new Empty(),
                                "{}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Person>(x),
                            x => new JsonObjectDeserializeUnitTest<Person>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithBasicObject",
                                TestSettings,
                                new Person
                                    {
                                        PersonId = "1234",
                                        FirstName = "John",
                                        LastName = "Doe"
                                    },
                                "{\"PersonId\":\"1234\",\"FirstName\":\"John\",\"LastName\":\"Doe\"}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Person>(x),
                            x => new JsonObjectDeserializeUnitTest<Person>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithBasicObjectAndNullProperties",
                                TestSettings,
                                new Person
                                    {
                                        PersonId = null,
                                        FirstName = null,
                                        LastName = null
                                    },
                                "{\"PersonId\":null,\"FirstName\":null,\"LastName\":null}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Employee>(x),
                            x => new JsonObjectDeserializeUnitTest<Employee>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithDerivedObject",
                                TestSettings,
                                new Employee
                                    {
                                        PersonId = "1234",
                                        FirstName = "John",
                                        LastName = "Doe",
                                        EmployeeNumber = "ABC123"
                                    },
                                "{\"EmployeeNumber\":\"ABC123\",\"PersonId\":\"1234\",\"FirstName\":\"John\",\"LastName\":\"Doe\"}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Employee>(x),
                            x => new JsonObjectDeserializeUnitTest<Employee>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithDerivedObjectAndNullProperties",
                                TestSettings,
                                new Employee
                                    {
                                        PersonId = null,
                                        FirstName = null,
                                        LastName = null,
                                        EmployeeNumber = null
                                    },
                                "{\"EmployeeNumber\":null,\"PersonId\":null,\"FirstName\":null,\"LastName\":null}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<BoardOfDirectors>(x),
                            x => new JsonObjectDeserializeUnitTest<BoardOfDirectors>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithCompositeObject",
                                TestSettings,
                                new BoardOfDirectors
                                    {
                                        President = new Person
                                            {
                                                PersonId = "1111",
                                                FirstName = "George",
                                                LastName = "Washington"
                                            },
                                        VicePresident = new Person
                                            {
                                                PersonId = "2222",
                                                FirstName = "John",
                                                LastName = "Adams"
                                            },
                                    },
                                "{\"President\":{\"PersonId\":\"1111\",\"FirstName\":\"George\",\"LastName\":\"Washington\"},\"VicePresident\":{\"PersonId\":\"2222\",\"FirstName\":\"John\",\"LastName\":\"Adams\"}}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<BoardOfDirectors>(x),
                            x => new JsonObjectDeserializeUnitTest<BoardOfDirectors>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithCompositeObjectAndNullProperties",
                                TestSettings,
                                new BoardOfDirectors
                                    {
                                        President = null,
                                        VicePresident = null,
                                    },
                                "{\"President\":null,\"VicePresident\":null}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<People>(x),
                            x => new JsonObjectDeserializeUnitTest<People>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithCollectionObject",
                                TestSettings,
                                new People
                                    {
                                        PersonCollection = new List<Person>
                                            {
                                                new Person
                                                    {
                                                        PersonId = "1234",
                                                        FirstName = "John",
                                                        LastName = "Doe"
                                                    },                                            
                                                new Person
                                                    {
                                                        PersonId = "5678",
                                                        FirstName = "Jane",
                                                        LastName = "Doe"
                                                    },                                            
                                            }
                                    },
                                "{\"PersonCollection\":[{\"PersonId\":\"1234\",\"FirstName\":\"John\",\"LastName\":\"Doe\"},{\"PersonId\":\"5678\",\"FirstName\":\"Jane\",\"LastName\":\"Doe\"}]}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<People>(x),
                            x => new JsonObjectDeserializeUnitTest<People>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithCollectionObjectAndNullCollection",
                                TestSettings,
                                new People
                                    {
                                        PersonCollection = null
                                    },
                                "{\"PersonCollection\":null}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<People>(x),
                            x => new JsonObjectDeserializeUnitTest<People>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithCollectionObjectAndEmptyCollection",
                                TestSettings,
                                new People
                                    {
                                        PersonCollection = new List<Person>()
                                    },
                                "{\"PersonCollection\":[]}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Company>(x),
                            x => new JsonObjectDeserializeUnitTest<Company>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithComplexObject",
                                TestSettings,
                                new Company
                                    {
                                        CompanyId = "Acme",
                                        CompanyName = "Acme, Inc.",
                                        BoardOfDirectors = new BoardOfDirectors
                                            {
                                                President = new Person
                                                    {
                                                        PersonId = "1111",
                                                        FirstName = "George",
                                                        LastName = "Washington"
                                                    },
                                                VicePresident = new Person
                                                    {
                                                        PersonId = "2222",
                                                        FirstName = "John",
                                                        LastName = "Adams"
                                                    },
                                            },
                                        CurrentEmployees = new People
                                            {
                                                PersonCollection = new List<Person>
                                                    {
                                                        new Person
                                                            {
                                                                PersonId = "1234",
                                                                FirstName = "John",
                                                                LastName = "Doe"
                                                            },                                            
                                                        new Person
                                                            {
                                                                PersonId = "5678",
                                                                FirstName = "Jane",
                                                                LastName = "Doe"
                                                            },                                            
                                                    }
                                            }
                                    },
                                "{" +
                                "\"CompanyId\":\"Acme\"," +
                                "\"CompanyName\":\"Acme, Inc.\"," +
                                "\"BoardOfDirectors\":" +
                                "{\"President\":{\"PersonId\":\"1111\",\"FirstName\":\"George\",\"LastName\":\"Washington\"},\"VicePresident\":{\"PersonId\":\"2222\",\"FirstName\":\"John\",\"LastName\":\"Adams\"}}," +
                                "\"CurrentEmployees\":" +
                                "{\"PersonCollection\":" +
                                "[{\"PersonId\":\"1234\",\"FirstName\":\"John\",\"LastName\":\"Doe\"}," +
                                "{\"PersonId\":\"5678\",\"FirstName\":\"Jane\",\"LastName\":\"Doe\"}]}" +
                                "}"))
                    },
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Company>(x),
                            x => new JsonObjectDeserializeUnitTest<Company>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithComplexObjectAndNullProperties",
                                TestSettings,
                                new Company
                                    {
                                        CompanyId = null,
                                        CompanyName = null,
                                        BoardOfDirectors = null,
                                        CurrentEmployees = null
                                    },
                                "{" +
                                "\"CompanyId\":null," +
                                "\"CompanyName\":null," +
                                "\"BoardOfDirectors\":null," +
                                "\"CurrentEmployees\":null" +
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

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Employee : Person
        {
            [JsonProperty] public string EmployeeNumber { get; set; }

            public override string ToString()
            {
                return "Employee [id={0} firstName={1} lastName={2} number={3}]".FormatWith(this.PersonId, this.FirstName, this.LastName, this.EmployeeNumber);
            }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class BoardOfDirectors : JsonObject
        {
            [JsonProperty] public Person President { get; set; }
            [JsonProperty] public Person VicePresident { get; set; }

            public override string ToString()
            {
                return "BoardOfDirectors [president={0} vicePresident={1}]".FormatWith(this.President, this.VicePresident);
            }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class People : JsonObject
        {
            [JsonProperty] public List<Person> PersonCollection { get; set; }

            public override string ToString()
            {
                return "People [count={0}]".FormatWith(this.PersonCollection.EmptyIfNull().Count());
            }
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
    }
}
