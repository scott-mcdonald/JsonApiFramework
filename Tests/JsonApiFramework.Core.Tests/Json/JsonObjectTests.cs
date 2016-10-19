// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

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
        [MemberData("JsonObjectTestData")]
        public void TestJsonObjectParse(JsonUnitTestFactoryAndData factoryAndData)
        {
            var data = factoryAndData.Data;
            var factory = factoryAndData.ParseFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("JsonObjectTestData")]
        public void TestJsonObjectToJson(JsonUnitTestFactoryAndData factoryAndData)
        {
            var data = factoryAndData.Data;
            var factory = factoryAndData.ToJsonFactory;
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Empty>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
                                "WithEmptyObject",
                                TestSettings,
                                new Empty(),
                                "{}"))
                    },
                new object[]
                    {
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Person>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Person>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Employee>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Employee>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<BoardOfDirectors>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<BoardOfDirectors>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<People>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<People>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<People>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Company>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                        new JsonUnitTestFactoryAndData(
                            x => new ParseUnitTest<Company>(x),
                            x => new ToJsonUnitTest(x),
                            new JsonUnitTestData(
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
                this.ExpectedObject = (T)data.ExpectedObject;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0}", this.Source);
                this.WriteLine();
                this.WriteLine("Expected Object = {0} ({1})", this.ExpectedObject, typeof(T).Name);
            }

            protected override void Act()
            {
                var source = this.Source;
                var settings = this.Settings;
                var actualObject = JsonObject.Parse<T>(source, settings);
                this.ActualObject = actualObject;
                this.WriteLine();
                this.WriteLine("Actual Object   = {0} ({1})", this.ActualObject, typeof(T).Name);
            }

            protected override void Assert()
            {
                if (typeof(T) == typeof(Empty))
                {
                    this.ActualObject.Should().BeOfType<Empty>();
                    return;
                }

                this.ActualObject.ShouldBeEquivalentTo(this.ExpectedObject);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private T ActualObject { get; set; }
            #endregion

            #region User Supplied Properties
            private string Source { get; set; }
            private JsonSerializerSettings Settings { get; set; }
            private T ExpectedObject { get; set; }
            #endregion
        }

        public class ToJsonUnitTest : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public ToJsonUnitTest(JsonUnitTestData data)
                : base(data.Name)
            {
                this.Settings = data.Settings;
                this.Source = data.ExpectedObject;
                this.ExpectedJson = data.ExpectedJson;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source        = {0} ({1})", this.Source, this.Source.GetType().Name);
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
            private IJsonObject Source { get; set; }
            private JsonSerializerSettings Settings { get; set; }
            private string ExpectedJson { get; set; }
            #endregion
        }
        #endregion
    }
}
