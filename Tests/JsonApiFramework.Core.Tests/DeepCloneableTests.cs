// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests
{
    public class DeepCloneableTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DeepCloneableTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DeepCopyTestData")]
        public void TestDeepCloneableDeepCopy(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly EmptyObject TestEmptyObject = new EmptyObject();

        private static readonly Person TestPresident = new Person
            {
                PersonId = "1111",
                FirstName = "George",
                LastName = "Washington"
            };

        private static readonly Person TestVicePresident = new Person
            {
                PersonId = "2222",
                FirstName = "John",
                LastName = "Adams"
            };

        private static readonly BoardOfDirectors TestBoardOfDirectors = new BoardOfDirectors
            {
                President = TestPresident,
                VicePresident = TestVicePresident
            };

        private static readonly Employee TestEmployee1 = new Employee
            {
                PersonId = "1234",
                LastName = "Doe",
                FirstName = "John",
                EmployeeNumber = "1234567890"
            };

        private static readonly Employee TestEmployee2 = new Employee
            {
                PersonId = "5678",
                LastName = "Doe",
                FirstName = "Jane",
                EmployeeNumber = "5678901234"
            };

        private static readonly Employees TestEmployees = new Employees
            {
                Collection = new List<Employee>
                    {
                        TestEmployee1,
                        TestEmployee2
                    }
            };

        private static readonly Company TestCompany = new Company
            {
                CompanyId = "Acme",
                CompanyName = "Acme, Inc.",
                BoardOfDirectors = TestBoardOfDirectors,
                CurrentEmployees = TestEmployees
            };

        public static readonly IEnumerable<object[]> DeepCopyTestData = new []
            {
                new object[] { new DeepCopyUnitTest<EmptyObject>("WithNull", null, (expected, actual) => { }) },
                new object[] { new DeepCopyUnitTest<EmptyObject>("WithEmptyObject", TestEmptyObject, (expected, actual) => { }) },
                new object[] { new DeepCopyUnitTest<Person>("WithBasicObject", TestPresident, (expected, actual) => actual.ShouldBeEquivalentTo(expected)) },
                new object[] { new DeepCopyUnitTest<Employees>("WithBasicObjectWithCollection", TestEmployees, (expected, actual) => actual.ShouldBeEquivalentTo(expected)) },
                new object[] { new DeepCopyUnitTest<BoardOfDirectors>("WithCompositeObject", TestBoardOfDirectors, (expected, actual) => actual.ShouldBeEquivalentTo(expected)) },
                new object[] { new DeepCopyUnitTest<Person>("WithDerivedObject", TestEmployee1, (expected, actual) => ((Employee)actual).ShouldBeEquivalentTo(expected)) },
                new object[] { new DeepCopyUnitTest<Company>("WithComplexObject", TestCompany, (expected, actual) => actual.ShouldBeEquivalentTo(expected)) },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        public class DeepCopyUnitTest<T> : UnitTest
            where T : class, IDeepCloneable
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public DeepCopyUnitTest(string name, T expected, Action<T, T> assertAction)
                : base(name)
            {
                this.Expected = expected;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected {0} as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Expected.DeepCopy();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual {0} as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                if (expected == null)
                {
                    actual.Should().BeNull();
                    return;
                }

                actual.Should().NotBeNull();
                actual.Should().NotBeSameAs(expected);

                this.AssertAction(expected, actual);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            T Actual { get; set; }
            #endregion

            #region User Supplied Properties
            T Expected { get; set; }
            Action<T, T> AssertAction { get; set; }
            #endregion
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class EmptyObject : IDeepCloneable
        {
            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Person : IDeepCloneable
        {
            [JsonProperty] public string PersonId { get; set; }
            [JsonProperty] public string LastName { get; set; }
            [JsonProperty] public string FirstName { get; set; }

            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Employee : Person
        {
            [JsonProperty] public string EmployeeNumber { get; set; }
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class BoardOfDirectors : IDeepCloneable
        {
            [JsonProperty] public Person President { get; set; }
            [JsonProperty] public Person VicePresident { get; set; }

            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Employees : IDeepCloneable
        {
            [JsonProperty] public List<Employee> Collection { get; set; }

            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Company : IDeepCloneable
        {
            [JsonProperty] public string CompanyId { get; set; }
            [JsonProperty] public string CompanyName { get; set; }
            [JsonProperty] public BoardOfDirectors BoardOfDirectors { get; set; }
            [JsonProperty] public Employees CurrentEmployees { get; set; }

            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }
        #endregion
    }
}
