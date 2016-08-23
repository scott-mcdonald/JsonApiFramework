// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
    public class ObjectFactoryTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ObjectFactoryTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestObjectFactoryCreateTypedObjectWithZeroArguments()
        {
            // Arrange
            var type = typeof(TestClass);
            var constructorInfo = type.GetConstructor(Type.EmptyTypes); // new TestClass()
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod();

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            Assert.Equal(TestClass.DefaultIntValue, actual.X);
            Assert.Equal(TestClass.DefaultIntValue, actual.Y);
        }

        [Fact]
        public void TestObjectFactoryCreateTypedObjectWithOneArgument()
        {
            // Arrange
            var type = typeof(TestClass);

            var parameterTypes = new Type[1];
            parameterTypes[0] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int)
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod(42);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            Assert.Equal(42, actual.X);
            Assert.Equal(TestClass.DefaultIntValue, actual.Y);
        }

        [Fact]
        public void TestObjectFactoryCreateTypedObjectWithTwoArguments()
        {
            // Arrange
            var type = typeof(TestClass);

            var parameterTypes = new Type[2];
            parameterTypes[0] = typeof(int);
            parameterTypes[1] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int, int)
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod(42, 24);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            Assert.Equal(42, actual.X);
            Assert.Equal(24, actual.Y);
        }


        [Fact]
        public void TestObjectFactoryCreateObjectWithZeroArguments()
        {
            // Arrange
            var type = typeof(TestClass);
            var constructorInfo = type.GetConstructor(Type.EmptyTypes); // new TestClass()
            var factoryMethod = ObjectFactory.Create<object>(constructorInfo);

            // Act
            var actual = factoryMethod();

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            var actualAsTestClass = (TestClass)actual;
            Assert.Equal(TestClass.DefaultIntValue, actualAsTestClass.X);
            Assert.Equal(TestClass.DefaultIntValue, actualAsTestClass.Y);
        }

        [Fact]
        public void TestObjectFactoryCreateObjectWithOneArgument()
        {
            // Arrange
            var type = typeof(TestClass);

            var parameterTypes = new Type[1];
            parameterTypes[0] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int)
            var factoryMethod = ObjectFactory.Create<object>(constructorInfo);

            // Act
            var actual = factoryMethod(42);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            var actualAsTestClass = (TestClass)actual;
            Assert.Equal(42, actualAsTestClass.X);
            Assert.Equal(TestClass.DefaultIntValue, actualAsTestClass.Y);
        }

        [Fact]
        public void TestObjectFactoryCreateObjectWithTwoArguments()
        {
            // Arrange
            var type = typeof(TestClass);

            var parameterTypes = new Type[2];
            parameterTypes[0] = typeof(int);
            parameterTypes[1] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int, int)
            var factoryMethod = ObjectFactory.Create<object>(constructorInfo);

            // Act
            var actual = factoryMethod(42, 24);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<TestClass>(actual);

            var actualAsTestClass = (TestClass)actual;
            Assert.Equal(42, actualAsTestClass.X);
            Assert.Equal(24, actualAsTestClass.Y);
        }


        [Fact]
        public void TestObjectFactoryCreateDerivedTypedObjectWithZeroArguments()
        {
            // Arrange
            var type = typeof(DerivedTestClass);
            var constructorInfo = type.GetConstructor(Type.EmptyTypes); // new TestClass()
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod();

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TestClass>(actual);

            Assert.Equal(TestClass.DefaultIntValue, actual.X);
            Assert.Equal(TestClass.DefaultIntValue, actual.Y);

            Assert.IsType<DerivedTestClass>(actual);
            var actualAsDerivedTestClass = (DerivedTestClass)actual;
            Assert.Equal(TestClass.DefaultIntValue, actualAsDerivedTestClass.Z);
        }

        [Fact]
        public void TestObjectFactoryCreateDerivedTypedObjectWithOneArgument()
        {
            // Arrange
            var type = typeof(DerivedTestClass);

            var parameterTypes = new Type[1];
            parameterTypes[0] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int)
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod(42);

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TestClass>(actual);

            Assert.Equal(42, actual.X);
            Assert.Equal(TestClass.DefaultIntValue, actual.Y);

            Assert.IsType<DerivedTestClass>(actual);
            var actualAsDerivedTestClass = (DerivedTestClass)actual;
            Assert.Equal(TestClass.DefaultIntValue, actualAsDerivedTestClass.Z);
        }

        [Fact]
        public void TestObjectFactoryCreateDerivedTypedObjectWithTwoArguments()
        {
            // Arrange
            var type = typeof(DerivedTestClass);

            var parameterTypes = new Type[2];
            parameterTypes[0] = typeof(int);
            parameterTypes[1] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int, int)
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod(42, 24);

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TestClass>(actual);

            Assert.Equal(42, actual.X);
            Assert.Equal(24, actual.Y);

            Assert.IsType<DerivedTestClass>(actual);
            var actualAsDerivedTestClass = (DerivedTestClass)actual;
            Assert.Equal(TestClass.DefaultIntValue, actualAsDerivedTestClass.Z);
        }

        [Fact]
        public void TestObjectFactoryCreateDerivedTypedObjectWithThreeArguments()
        {
            // Arrange
            var type = typeof(DerivedTestClass);

            var parameterTypes = new Type[3];
            parameterTypes[0] = typeof(int);
            parameterTypes[1] = typeof(int);
            parameterTypes[2] = typeof(int);

            var constructorInfo = type.GetConstructor(parameterTypes); // new TestClass(int, int, int)
            var factoryMethod = ObjectFactory.Create<TestClass>(constructorInfo);

            // Act
            var actual = factoryMethod(42, 24, 4224);

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<TestClass>(actual);

            Assert.Equal(42, actual.X);
            Assert.Equal(24, actual.Y);

            Assert.IsType<DerivedTestClass>(actual);
            var actualAsDerivedTestClass = (DerivedTestClass)actual;
            Assert.Equal(4224, actualAsDerivedTestClass.Z);
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class TestClass
        {
            // ReSharper disable UnusedMember.Local
            // ReSharper disable MemberCanBeProtected.Local
            public TestClass()
            {
                this._x = DefaultIntValue;
                this._y = DefaultIntValue;
            }

            public TestClass(int x)
            {
                this._x = x;
                this._y = DefaultIntValue;
            }

            public TestClass(int x, int y)
            {
                this._x = x;
                this._y = y;
            }
            // ReSharper restore MemberCanBeProtected.Local
            // ReSharper restore UnusedMember.Local

            public int X { get { return this._x; } }
            public int Y { get { return this._y; } }

            public const int DefaultIntValue = -1;

            private readonly int _x;
            private readonly int _y;
        }

        private class DerivedTestClass : TestClass
        {
            // ReSharper disable UnusedMember.Local
            public DerivedTestClass()
            {
                this._z = DefaultIntValue;
            }

            public DerivedTestClass(int x)
                : base(x)
            {
                this._z = DefaultIntValue;
            }

            public DerivedTestClass(int x, int y)
                : base(x, y)
            {
                this._z = DefaultIntValue;
            }

            public DerivedTestClass(int x, int y, int z)
                : base(x, y)
            {
                this._z = z;
            }
            // ReSharper restore UnusedMember.Local

            public int Z { get { return this._z; } }

            private readonly int _z;
        }
        #endregion
    }
}