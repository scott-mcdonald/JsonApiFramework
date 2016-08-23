// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
    public class TypeExtensionsTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeExtensionsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("GetConstructorData")]
        public void TestTypeExtensionsGetConstructor(string name, Type type, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes, string expectedConstructorName)
        {
            // Arrange
            var expected = expectedConstructorName;

            // Act
            var actualMethod = type.GetConstructor(bindingFlags, parameterTypes);
            var actual = actualMethod != null ? actualMethod.Name : null;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("GetConstructorsData")]
        public void TestTypeExtensionsGetConstructors(string name, Type type, BindingFlags bindingFlags, IEnumerable<string> expectedConstructorNames)
        {
            // Arrange
            expectedConstructorNames = expectedConstructorNames.SafeToList();
            var expected = expectedConstructorNames;

            // Act
            var actualConstructors = type.GetConstructors(bindingFlags);
            var actual = actualConstructors
                .Select(x => x.Name)
                .ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("GetMethodData")]
        public void TestTypeExtensionsGetMethod(string name, Type type, string methodName, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes, string expectedMethodName)
        {
            // Arrange
            var expected = expectedMethodName;

            // Act
            var actualMethod = type.GetMethod(methodName, bindingFlags, parameterTypes);
            var actual = actualMethod != null ? actualMethod.Name : null;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("GetMethodsData")]
        public void TestTypeExtensionsGetMethods(string name, Type type, BindingFlags bindingFlags, IEnumerable<string> expectedMethodNames)
        {
            // Arrange
            expectedMethodNames = expectedMethodNames.SafeToList();
            var expectedCount = expectedMethodNames.Count();

            // Act
            var actualMethods = type.GetMethods(bindingFlags);
            var actual = actualMethods
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();
            var actualCount = actual.Count;

            // Assert
            Assert.True(expectedCount <= actualCount);
            foreach (var expected in expectedMethodNames)
            {
                Assert.Contains(expected, actual);
            }
        }

        [Theory]
        [MemberData("GetPropertyData")]
        public void TestTypeExtensionsGetProperty(string name, Type type, string propertyName, BindingFlags bindingFlags, string expectedPropertyName)
        {
            // Arrange
            var expected = expectedPropertyName;

            // Act
            var actualProperty = type.GetProperty(propertyName, bindingFlags);
            var actual = actualProperty != null ? actualProperty.Name : null;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("GetPropertiesData")]
        public void TestTypeExtensionsGetProperties(string name, Type type, BindingFlags bindingFlags, IEnumerable<string> expectedPropertyNames)
        {
            // Arrange
            var expected = expectedPropertyNames.OrderBy(x => x)
                                                .ToList();

            // Act
            var actualProperties = type.GetProperties(bindingFlags);
            var actual = actualProperties
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("IsImplementationOfData")]
        public void TestTypeExtensionsIsImplementationOf(string name, Type derivedType, Type baseType, bool expected)
        {
            // Arrange

            // Act
            var actual = derivedType.IsImplementationOf(baseType);

            // Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [MemberData("IsSubclassOrImplementationOfData")]
        public void TestTypeExtensionsIsSubclassOrImplementationOf(string name, Type derivedType, Type baseOrInterfaceType, bool expected)
        {
            // Arrange

            // Act
            var actual = derivedType.IsSubclassOrImplementationOf(baseOrInterfaceType);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetConstructorData = new[]
            {
                new object[] { "WithPublic", typeof(ClassWithConstructors), BindingFlags.Public, new Type[] {}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), BindingFlags.Public, new Type[] {typeof(int)}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), BindingFlags.Public, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                new object[] { "WithPublic", typeof(ClassWithConstructors), BindingFlags.Public, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), BindingFlags.Public | BindingFlags.NonPublic, new Type[] {}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), BindingFlags.Public | BindingFlags.NonPublic, new Type[] {typeof(int)}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), BindingFlags.Public | BindingFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), BindingFlags.Public | BindingFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},

                new object[] { "WithNonPublic", typeof(ClassWithConstructors), BindingFlags.NonPublic, new Type[] {}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), BindingFlags.NonPublic, new Type[] {typeof(int)}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), BindingFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, null},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), BindingFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},
            };

        public static readonly IEnumerable<object[]> GetConstructorsData = new[]
            {
                new object[] { "WithPublic", typeof(ClassWithConstructors), BindingFlags.Public, new[] {".ctor", ".ctor", ".ctor"}},
                new object[] { "WithPublicAndNonPublic", typeof(ClassWithConstructors), BindingFlags.Public | BindingFlags.NonPublic, new[] {".ctor", ".ctor", ".ctor", ".ctor"}},
                new object[] { "WithNonPublic", typeof(ClassWithConstructors), BindingFlags.NonPublic, new[] {".ctor"}}
            };

        public static readonly IEnumerable<object[]> GetMethodData = new[]
            {
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, new Type[] {}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int)}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, new Type[] {}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethod"},
                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int)}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PrivateMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int)}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int), typeof(string)}, "PrivateStaticMethod"},
                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", BindingFlags.Public | BindingFlags.Instance, new Type[] {}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int)}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethodBase"},
                new object[] { "PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", BindingFlags.Public | BindingFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", BindingFlags.Public | BindingFlags.Static, new Type[] {}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethodBase"},
                new object[] { "PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", BindingFlags.Public | BindingFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int)}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int), typeof(string)}, "ProtectedMethodBase"},
                new object[] { "NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", BindingFlags.NonPublic | BindingFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int)}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int), typeof(string)}, "ProtectedStaticMethodBase"},
                new object[] { "NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", BindingFlags.NonPublic | BindingFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null}
            };

        public static readonly IEnumerable<object[]> GetMethodsData = new[]
            {
                new object[] { "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod"}},

                new object[] { "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static,
                    new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}},

                new object[] { "WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}},


                new object[] { "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod"}},

                new object[] { "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static,
                    new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},

                new object[] { "WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},


                new object[] { "WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}},


                new object[] {"WithPublicAndInstance", typeof(ClassWithMethods), BindingFlags.Public | BindingFlags.Instance,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase"}},

                new object[] {"WithPublicAndStatic", typeof(ClassWithMethods), BindingFlags.Public | BindingFlags.Static,
                    new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}},

                new object[] {"WithPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}},


                new object[] {"WithNonPublicAndInstance", typeof(ClassWithMethods), BindingFlags.NonPublic | BindingFlags.Instance,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase"}},

                new object[] {"WithNonPublicAndStatic", typeof(ClassWithMethods), BindingFlags.NonPublic | BindingFlags.Static,
                    new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},

                new object[] {"WithNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                           "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},


                new object[] {"WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase",
                           "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase",
                           "PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                           "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}},
            };


        public static readonly IEnumerable<object[]> GetPropertyData = new[]
            {
                new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), "PublicProperty", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, "PublicProperty"},
                new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), "PublicStaticProperty", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, "PublicStaticProperty"},
                new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), "PrivateProperty", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance, "PrivateProperty"},
                new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticProperty", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static, "PrivateStaticProperty"},
                new object[] {"WithPublicAndInstance", typeof(ClassWithProperties), "PublicPropertyBase", BindingFlags.Public | BindingFlags.Instance, "PublicPropertyBase"},
                new object[] {"WithPublicAndStatic", typeof(ClassWithProperties), "PublicStaticPropertyBase", BindingFlags.Public | BindingFlags.Static, "PublicStaticPropertyBase"},
                new object[] {"WithNonPublicAndInstance", typeof(ClassWithProperties), "PrivatePropertyBase", BindingFlags.NonPublic | BindingFlags.Instance, "PrivatePropertyBase"},
                new object[] {"WithNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticPropertyBase", BindingFlags.NonPublic | BindingFlags.Static, "PrivateStaticPropertyBase"},
            };

        public static readonly IEnumerable<object[]> GetPropertiesData = new[]
            {
                new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance,
                    new[] { "PublicProperty" }},

                new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static,
                    new[] { "PublicStaticProperty" }},

                new object[] {"WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
                    new[] { "PublicProperty", "PublicStaticProperty" }},


                new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance,
                    new[] { "ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty" }},

                new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static,
                    new[] { "ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty" }},

                new object[] {"WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] { "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty" }},


                new object[] {"WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] { "PublicProperty", "PublicStaticProperty", "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty" }},


                new object[] {"WithPublicAndInstance", typeof(ClassWithProperties), BindingFlags.Public | BindingFlags.Instance,
                    new[] { "PublicProperty", "PublicPropertyBase" }},

                new object[] {"WithPublicAndStatic", typeof(ClassWithProperties), BindingFlags.Public | BindingFlags.Static,
                    new[] { "PublicStaticProperty", "PublicStaticPropertyBase" }},

                new object[] {"WithPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
                    new[] { "PublicProperty", "PublicPropertyBase", "PublicStaticProperty", "PublicStaticPropertyBase" }},


                new object[] {"WithNonPublicAndInstance", typeof(ClassWithProperties), BindingFlags.NonPublic | BindingFlags.Instance,
                    new[] { "ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty",
                            "ProtectedInternalPropertyBase", "ProtectedPropertyBase", "InternalPropertyBase", "PrivatePropertyBase" }},

                new object[] {"WithNonPublicAndStatic", typeof(ClassWithProperties), BindingFlags.NonPublic | BindingFlags.Static,
                    new[] { "ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty",
                            "ProtectedInternalStaticPropertyBase", "ProtectedStaticPropertyBase", "InternalStaticPropertyBase", "PrivateStaticPropertyBase" }},

                new object[] {"WithNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    new[] { "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty",
                            "ProtectedInternalPropertyBase", "ProtectedInternalStaticPropertyBase", "ProtectedPropertyBase", "ProtectedStaticPropertyBase", "InternalPropertyBase", "InternalStaticPropertyBase", "PrivatePropertyBase", "PrivateStaticPropertyBase" }},


                new object[] {"WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
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
