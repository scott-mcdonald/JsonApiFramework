// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
    public class TypeReflectionTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeReflectionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetConstructorData))]
        public void TestTypeExtensionsGetConstructor(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes, string expectedConstructorName)
        {
            // Arrange
            var parameterTypesList = parameterTypes.SafeToList();

            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);
            this.WriteLine("Parameter Types = {0}", parameterTypesList.SafeToDelimitedString(", "));

            var expected = expectedConstructorName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualMethod = TypeReflection.GetConstructor(type, reflectionFlags, parameterTypesList);
            var actual = actualMethod != null ? actualMethod.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetConstructorsData))]
        public void TestTypeExtensionsGetConstructors(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedConstructorNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            expectedConstructorNames = expectedConstructorNames.SafeToList();
            var expected = expectedConstructorNames;
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualConstructors = TypeReflection.GetConstructors(type, reflectionFlags);
            var actual = actualConstructors
                .Select(x => x.Name)
                .ToList();

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(GetMethodData))]
        public void TestTypeExtensionsGetMethod(string name, Type type, string methodName, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes, string expectedMethodName)
        {
            // Arrange
            var parameterTypesList = parameterTypes.SafeToList();

            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Method Name     = {0}", methodName);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);
            this.WriteLine("Parameter Types = {0}", parameterTypesList.SafeToDelimitedString(", "));

            var expected = expectedMethodName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualMethod = TypeReflection.GetMethod(type, methodName, reflectionFlags, parameterTypesList);
            var actual = actualMethod != null ? actualMethod.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetMethodsData))]
        public void TestTypeExtensionsGetMethods(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedMethodNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedMethodNames.SafeToList();
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualMethods = TypeReflection.GetMethods(type, reflectionFlags);
            var actual = actualMethods
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();

            // Assert
            // Ignore the inherited methods from Object.
            // So assert actual at least contains all of expected but may have more.
            actual.Should().Contain(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(GetPropertyData))]
        public void TestTypeExtensionsGetProperty(string name, Type type, string propertyName, ReflectionFlags reflectionFlags, string expectedPropertyName)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Property Name   = {0}", propertyName);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedPropertyName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualProperty = TypeReflection.GetProperty(type, propertyName, reflectionFlags);
            var actual = actualProperty != null ? actualProperty.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetPropertiesData))]
        public void TestTypeExtensionsGetProperties(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedPropertyNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedPropertyNames.OrderBy(x => x)
                                                .ToList();
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualProperties = TypeReflection.GetProperties(type, reflectionFlags);
            var actual = actualProperties
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(IsImplementationOfData))]
        public void TestTypeExtensionsIsImplementationOf(string name, Type derivedType, Type baseType, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Derived Type = {0}", derivedType);
            this.WriteLine("Base Type    = {0}", baseType);
            this.WriteLine("Expected     = {0}", expected);

            // Act
            var actual = TypeReflection.IsImplementationOf(derivedType, baseType);
            this.WriteLine("Actual       = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(IsSubclassOrImplementationOfData))]
        public void TestTypeExtensionsIsSubclassOrImplementationOf(string name, Type derivedType, Type baseOrInterfaceType, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Derived Type         = {0}", derivedType);
            this.WriteLine("BaseOrInterface Type = {0}", baseOrInterfaceType);
            this.WriteLine("Expected             = {0}", expected);

            // Act
            var actual = TypeReflection.IsSubclassOrImplementationOf(derivedType, baseOrInterfaceType);
            this.WriteLine("Actual               = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetConstructorData = new[]
            {
                new object[] { "WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int)}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int)}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},

                new object[] { "WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int)}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},
            };

        public static readonly IEnumerable<object[]> GetConstructorsData = new[]
            {
                new object[] { "WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new[] {".ctor", ".ctor", ".ctor"}},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new[] {".ctor", ".ctor", ".ctor", ".ctor"}},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new[] {".ctor"}}
            };

        public static readonly IEnumerable<object[]> GetMethodData = new[]
            {
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int)}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int)}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int)}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null}
            };

        public static readonly IEnumerable<object[]> GetMethodsData = new[]
            {
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod"}},

                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static,
                    new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}},

                new object[] { "WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}},


                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod"}},

                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static,
                    new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},

                new object[] { "WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},


                new object[] { "WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},


                new object[] {"WithPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Instance,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase"}},

                new object[] {"WithPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Static,
                    new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}},

                new object[] {"WithPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}},


                new object[] {"WithNonPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase"}},

                new object[] {"WithNonPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Static,
                    new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},

                new object[] {"WithNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                           "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},


                new object[] {"WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase",
                           "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase",
                           "PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                           "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},
            };

        public static readonly IEnumerable<object[]> GetPropertyData = new[]
            {
                new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), "PublicProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, "PublicProperty"},
                new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), "PublicStaticProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, "PublicStaticProperty"},
                new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), "PrivateProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, "PrivateProperty"},
                new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static, "PrivateStaticProperty"},
                new object[] {"WithPublicAndInstance", typeof(ClassWithProperties), "PublicPropertyBase", ReflectionFlags.Public | ReflectionFlags.Instance, "PublicPropertyBase"},
                new object[] {"WithPublicAndStatic", typeof(ClassWithProperties), "PublicStaticPropertyBase", ReflectionFlags.Public | ReflectionFlags.Static, "PublicStaticPropertyBase"},
                new object[] {"WithNonPublicAndInstance", typeof(ClassWithProperties), "PrivatePropertyBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, "PrivatePropertyBase"},
                new object[] {"WithNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticPropertyBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, "PrivateStaticPropertyBase"},
            };

        public static readonly IEnumerable<object[]> GetPropertiesData = new[]
            {
                new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance,
                    new[] { "PublicProperty" }},

                new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static,
                    new[] { "PublicStaticProperty" }},

                new object[] {"WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "PublicProperty", "PublicStaticProperty" }},


                new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                    new[] { "ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty" }},

                new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static,
                    new[] { "ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty" }},

                new object[] {"WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty" }},


                new object[] {"WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "PublicProperty", "PublicStaticProperty", "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty" }},


                new object[] {"WithPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Instance,
                    new[] { "PublicProperty", "PublicPropertyBase" }},

                new object[] {"WithPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Static,
                    new[] { "PublicStaticProperty", "PublicStaticPropertyBase" }},

                new object[] {"WithPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "PublicProperty", "PublicPropertyBase", "PublicStaticProperty", "PublicStaticPropertyBase" }},


                new object[] {"WithNonPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                    new[] { "ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty",
                            "ProtectedInternalPropertyBase", "ProtectedPropertyBase", "InternalPropertyBase", "PrivatePropertyBase" }},

                new object[] {"WithNonPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Static,
                    new[] { "ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty",
                            "ProtectedInternalStaticPropertyBase", "ProtectedStaticPropertyBase", "InternalStaticPropertyBase", "PrivateStaticPropertyBase" }},

                new object[] {"WithNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty",
                            "ProtectedInternalPropertyBase", "ProtectedInternalStaticPropertyBase", "ProtectedPropertyBase", "ProtectedStaticPropertyBase", "InternalPropertyBase", "InternalStaticPropertyBase", "PrivatePropertyBase", "PrivateStaticPropertyBase" }},


                new object[] {"WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                    new[] { "PublicProperty", "PublicStaticProperty", "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty",
                            "PublicPropertyBase", "PublicStaticPropertyBase", "ProtectedInternalPropertyBase", "ProtectedInternalStaticPropertyBase", "ProtectedPropertyBase", "ProtectedStaticPropertyBase", "InternalPropertyBase", "InternalStaticPropertyBase", "PrivatePropertyBase", "PrivateStaticPropertyBase" }}
            };

        public static readonly IEnumerable<object[]> IsImplementationOfData = new[]
            {
                new object[] {"NegativeCaseWithGenerics", typeof(Host<>), typeof(ICollection<>), false},
                new object[] {"NegativeCaseWithNonGenerics", typeof(Rectangle), typeof(IAnimal), false},

                new object[] {"PositiveCaseWithGenerics", typeof(Host<>), typeof(IHost<>), true},
                new object[] {"PositiveCaseWithNonGenerics", typeof(Rectangle), typeof(IShape), true}
            };

        public static readonly IEnumerable<object[]> IsSubclassOrImplementationOfData = new[]
            {
                new object[] {"NegativeCaseWithGenericsWithDerivedAndInterface", typeof(Host<>), typeof(ICollection<>), false},
                new object[] {"NegativeCaseWithGenericsWithDerivedAndAbstract", typeof(Host<>), typeof(AbstractCollection<>), false},
                new object[] {"NegativeCaseWithGenericsWithAbstractAndInterface", typeof(AbstractHost<>), typeof(ICollection<>), false},

                new object[] {"NegativeCaseWithNonGenericsWithDerivedAndInterface", typeof(Rectangle), typeof(IAnimal), false},
                new object[] {"NegativeCaseWithNonGenericsWithDerivedAndAbstract", typeof(Rectangle), typeof(AbstractAnimal), false},
                new object[] {"NegativeCaseWithNonGenericsWithAbstractAndInterface", typeof(AbstractShape), typeof(IAnimal), false},

                new object[] {"PositiveCaseWithNonGenericsWithDerivedAndInterface", typeof(Rectangle), typeof(IShape), true},
                new object[] {"PositiveCaseWithNonGenericsWithDerivedAndAbstract", typeof(Rectangle), typeof(AbstractShape), true},
                new object[] {"PositiveCaseWithNonGenericsWithAbstractAndInterface", typeof(AbstractShape), typeof(IShape), true},

                new object[] {"PositiveCaseWithGenericsWithDerivedAndInterface", typeof(Host<>), typeof(IHost<>), true},
                new object[] {"PositiveCaseWithGenericsWithDerivedAndAbstract", typeof(Host<>), typeof(AbstractHost<>), true},
                new object[] {"PositiveCaseWithGenericsWithAbstractAndInterface", typeof(AbstractHost<>), typeof(IHost<>), true}
            };
        #endregion

        #region Test Types
        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedMember.Global
        private interface IAnimal
        {
            void Move();
        }

        private interface IShape
        {
            void Draw();
        }

        private abstract class AbstractAnimal : IAnimal
        {
            public abstract void Move();
        }

        private abstract class AbstractShape : IShape
        {
            public abstract void Draw();
        }

        private class Rectangle : AbstractShape
        {
            public override void Draw()
            { }
        }

        private interface ICollection<out T>
        {
            T [] Items { get; }
        }

        private interface IHost<out T>
        {
            T HostedObject { get; }
        }

        private abstract class AbstractCollection<T> : ICollection<T>
        {
            public abstract T[] Items { get; }
        }

        private abstract class AbstractHost<T> : IHost<T>
        {
            public abstract T HostedObject { get; }
        }

        private class Host<T> : AbstractHost<T>
        {
            public Host(T hostedObject)
            {
                this._hostedObject = hostedObject;
            }

            private readonly T _hostedObject;
            public override T HostedObject { get { return this._hostedObject; } }
        }

        private class ClassWithMethodsBase
        {
            public string PublicMethodBase() { return String.Empty; }
            public string PublicMethodBase(int x) { return String.Empty; }
            public string PublicMethodBase(int x, string y) { return String.Empty; }

            public static string PublicStaticMethodBase() { return String.Empty; }
            public static string PublicStaticMethodBase(int x) { return String.Empty; }
            public static string PublicStaticMethodBase(int x, string y) { return String.Empty; }

            protected string ProtectedMethodBase() { return String.Empty; }
            protected string ProtectedMethodBase(int x) { return String.Empty; }
            protected string ProtectedMethodBase(int x, string y) { return String.Empty; }

            protected static string ProtectedStaticMethodBase() { return String.Empty; }
            protected static string ProtectedStaticMethodBase(int x) { return String.Empty; }
            protected static string ProtectedStaticMethodBase(int x, string y) { return String.Empty; }
        }

        private class ClassWithConstructors
        {
            public ClassWithConstructors() { }
            public ClassWithConstructors(int x) { }
            public ClassWithConstructors(int x, string y) { }

            protected ClassWithConstructors(int x, string y, bool z) { }
        }

        private class ClassWithMethods : ClassWithMethodsBase
        {
            public string PublicMethod() { return String.Empty; }
            public string PublicMethod(int x) { return String.Empty; }
            public string PublicMethod(int x, string y) { return String.Empty; }

            public static string PublicStaticMethod() { return String.Empty; }
            public static string PublicStaticMethod(int x) { return String.Empty; }
            public static string PublicStaticMethod(int x, string y) { return String.Empty; }

            private string PrivateMethod() { return String.Empty; }
            private string PrivateMethod(int x) { return String.Empty; }
            private string PrivateMethod(int x, string y) { return String.Empty; }

            private static string PrivateStaticMethod() { return String.Empty; }
            private static string PrivateStaticMethod(int x) { return String.Empty; }
            private static string PrivateStaticMethod(int x, string y) { return String.Empty; }
        }

        private class ClassWithPropertiesBase
        {
            public string PublicPropertyBase { get; set; }
            public static string PublicStaticPropertyBase { get; set; }

            protected internal string ProtectedInternalPropertyBase { get; set; }
            protected internal static string ProtectedInternalStaticPropertyBase { get; set; }

            protected string ProtectedPropertyBase { get; set; }
            protected static string ProtectedStaticPropertyBase { get; set; }

            internal string InternalPropertyBase { get; set; }
            internal static string InternalStaticPropertyBase { get; set; }

            private string PrivatePropertyBase { get; set; }
            private static string PrivateStaticPropertyBase { get; set; }
        }

        private class ClassWithProperties : ClassWithPropertiesBase
        {
            public string PublicProperty { get; set; }
            public static string PublicStaticProperty { get; set; }

            protected internal string ProtectedInternalProperty { get; set; }
            protected internal static string ProtectedInternalStaticProperty { get; set; }

            protected string ProtectedProperty { get; set; }
            protected static string ProtectedStaticProperty { get; set; }

            internal string InternalProperty { get; set; }
            internal static string InternalStaticProperty { get; set; }

            private string PrivateProperty { get; set; }
            private static string PrivateStaticProperty { get; set; }
        }
        // ReSharper restore UnusedMember.Global
        // ReSharper restore UnusedMember.Local
        #endregion
    }
}
