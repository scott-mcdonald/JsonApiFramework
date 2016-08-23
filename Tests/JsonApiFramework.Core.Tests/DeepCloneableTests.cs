// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

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
        [Fact]
        public void TestDeepCloneableDeepCopyWithNullObject()
        {
            // Arrange
            var person = default(Person);

            // Act

            // ReSharper disable once ExpressionIsAlwaysNull
            var deepCopy = person.DeepCopy();

            // Assert
            Assert.Null(deepCopy);
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithEmptyObject()
        {
            // Arrange
            var emptyObject = new EmptyObject();

            // Act
            var deepCopy = emptyObject.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(emptyObject, deepCopy));
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithBasicObject()
        {
            // Arrange
            var person = new Person
                {
                    PersonId = "1234",
                    LastName = "Doe",
                    FirstName = "John"
                };

            // Act
            var deepCopy = person.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(person, deepCopy));
            Assert.Equal(person.PersonId, deepCopy.PersonId);
            Assert.Equal(person.LastName, deepCopy.LastName);
            Assert.Equal(person.FirstName, deepCopy.FirstName);
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithCompositeObject()
        {
            // Arrange
            var president = new Person
                {
                    PersonId = "1234",
                    LastName = "Doe",
                    FirstName = "John"
                };
            var vicePresident = new Person
                {
                    PersonId = "5678",
                    LastName = "Doe",
                    FirstName = "Jane"
                };
            var boardOfDirectors = new BoardOfDirectors
                {
                    President = president,
                    VicePresident = vicePresident
                };

            // Act
            var deepCopy = boardOfDirectors.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(boardOfDirectors, deepCopy));

            Assert.Equal(president.PersonId, deepCopy.President.PersonId);
            Assert.Equal(president.LastName, deepCopy.President.LastName);
            Assert.Equal(president.FirstName, deepCopy.President.FirstName);

            Assert.Equal(vicePresident.PersonId, deepCopy.VicePresident.PersonId);
            Assert.Equal(vicePresident.LastName, deepCopy.VicePresident.LastName);
            Assert.Equal(vicePresident.FirstName, deepCopy.VicePresident.FirstName);
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithDerivedObject()
        {
            // Arrange
            var employee = new Employee
                {
                    PersonId = "1234",
                    LastName = "Doe",
                    FirstName = "John",
                    EmployeeNumber = "1234567890"
                };
            var person = (Person)employee;

            // Act
            var deepCopy = person.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(employee, deepCopy));

            Assert.Equal(employee.PersonId, deepCopy.PersonId);
            Assert.Equal(employee.LastName, deepCopy.LastName);
            Assert.Equal(employee.FirstName, deepCopy.FirstName);

            Assert.IsType<Employee>(deepCopy);
            var deepCopyAsEmployee = (Employee)deepCopy;
            Assert.Equal(employee.EmployeeNumber, deepCopyAsEmployee.EmployeeNumber);
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithBasicCollectionObject()
        {
            // Arrange
            var person0 = new Person
                {
                    PersonId = "1234",
                    LastName = "Doe",
                    FirstName = "John"
                };
            var person1 = new Person
                {
                    PersonId = "5678",
                    LastName = "Doe",
                    FirstName = "Jane"
                };
            var people = new People
                {
                    PersonCollection = new List<Person>
                        {
                            person0,
                            person1
                        }
                };

            // Act
            var deepCopy = people.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(people, deepCopy));

            Assert.NotNull(deepCopy.PersonCollection);
            // ReSharper disable once PossibleNullReferenceException
            Assert.Equal(2, deepCopy.PersonCollection.Count);

            Assert.Equal(person0.PersonId, deepCopy.PersonCollection[0].PersonId);
            Assert.Equal(person0.LastName, deepCopy.PersonCollection[0].LastName);
            Assert.Equal(person0.FirstName, deepCopy.PersonCollection[0].FirstName);

            Assert.Equal(person1.PersonId, deepCopy.PersonCollection[1].PersonId);
            Assert.Equal(person1.LastName, deepCopy.PersonCollection[1].LastName);
            Assert.Equal(person1.FirstName, deepCopy.PersonCollection[1].FirstName);
        }

        [Fact]
        public void TestDeepCloneableDeepCopyWithComplexObject()
        {
            // Arrange
            var president = new Person
                {
                    PersonId = "1111",
                    FirstName = "George",
                    LastName = "Washington"
                };
            var vicePresident = new Person
                {
                    PersonId = "2222",
                    FirstName = "John",
                    LastName = "Adams"
                };
            var boardOfDirectors = new BoardOfDirectors
                {
                    President = president,
                    VicePresident = vicePresident
                };

            var employee0 = new Employee
                {
                    PersonId = "1234",
                    LastName = "Doe",
                    FirstName = "John",
                    EmployeeNumber = "1111111111"
                };
            var employee1 = new Employee
                {
                    PersonId = "5678",
                    LastName = "Doe",
                    FirstName = "Jane",
                    EmployeeNumber = "2222222222"
                };
            var employees = new People
                {
                    PersonCollection = new List<Person>
                        {
                            employee0,
                            employee1
                        }
                };

            var company = new Company
                {
                    CompanyId = "Acme",
                    CompanyName = "Acme, Inc.",
                    BoardOfDirectors = boardOfDirectors,
                    CurrentEmployees = employees
                };

            // Act
            var deepCopy = company.DeepCopy();

            // Assert
            Assert.NotNull(deepCopy);
            Assert.False(Object.ReferenceEquals(company, deepCopy));

            Assert.Equal(company.CompanyId, deepCopy.CompanyId);
            Assert.Equal(company.CompanyName, deepCopy.CompanyName);

            Assert.NotNull(deepCopy.BoardOfDirectors);

            Assert.Equal(president.PersonId, deepCopy.BoardOfDirectors.President.PersonId);
            Assert.Equal(president.LastName, deepCopy.BoardOfDirectors.President.LastName);
            Assert.Equal(president.FirstName, deepCopy.BoardOfDirectors.President.FirstName);

            Assert.Equal(vicePresident.PersonId, deepCopy.BoardOfDirectors.VicePresident.PersonId);
            Assert.Equal(vicePresident.LastName, deepCopy.BoardOfDirectors.VicePresident.LastName);
            Assert.Equal(vicePresident.FirstName, deepCopy.BoardOfDirectors.VicePresident.FirstName);

            Assert.NotNull(deepCopy.CurrentEmployees);
            Assert.NotNull(deepCopy.CurrentEmployees.PersonCollection);
            Assert.Equal(2, deepCopy.CurrentEmployees.PersonCollection.Count);

            Assert.Equal(employee0.PersonId, deepCopy.CurrentEmployees.PersonCollection[0].PersonId);
            Assert.Equal(employee0.LastName, deepCopy.CurrentEmployees.PersonCollection[0].LastName);
            Assert.Equal(employee0.FirstName, deepCopy.CurrentEmployees.PersonCollection[0].FirstName);

            Assert.IsType<Employee>(deepCopy.CurrentEmployees.PersonCollection[0]);
            var deepCopyAsEmployee0 = (Employee)deepCopy.CurrentEmployees.PersonCollection[0];
            Assert.Equal(employee0.EmployeeNumber, deepCopyAsEmployee0.EmployeeNumber);

            Assert.Equal(employee1.PersonId, deepCopy.CurrentEmployees.PersonCollection[1].PersonId);
            Assert.Equal(employee1.LastName, deepCopy.CurrentEmployees.PersonCollection[1].LastName);
            Assert.Equal(employee1.FirstName, deepCopy.CurrentEmployees.PersonCollection[1].FirstName);

            Assert.IsType<Employee>(deepCopy.CurrentEmployees.PersonCollection[1]);
            var deepCopyAsEmployee1 = (Employee)deepCopy.CurrentEmployees.PersonCollection[1];
            Assert.Equal(employee1.EmployeeNumber, deepCopyAsEmployee1.EmployeeNumber);
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
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
        private class People : IDeepCloneable
        {
            [JsonProperty] public List<Person> PersonCollection { get; set; }

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
            [JsonProperty] public People CurrentEmployees { get; set; }

            #region IDeepCloneable Implementation
            public object DeepClone()
            { return this.DeepCloneWithJson(); }
            #endregion
        }
        #endregion
    }
}
