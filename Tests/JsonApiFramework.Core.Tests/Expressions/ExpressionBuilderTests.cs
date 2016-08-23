// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using JsonApiFramework.Expressions;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Expressions
{
    public class ExpressionBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ExpressionBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestExpressionBuilderCallWithGenericObjects()
        {
            // Arrange
            var isEqualsType = typeof(IsEquals<>);
            var isEqualsMethods = TypeExtensions.GetMethods(isEqualsType)
                                                .Where(x => x.Name == "IsEqual")
                                                .ToList();
            var isEqualMethod = isEqualsMethods.Single(x => x.GetParameters().Count() == 2);
            var genericTypeArguments = new[] { typeof(long) };
            var isEqualsForLong = new IsEquals<long>();

            var isEqualNonGenericExpression = ExpressionBuilder.Call(isEqualsType, isEqualMethod, genericTypeArguments);
            var isEqualNonGenericMethod = isEqualNonGenericExpression.Compile();

            var isEqualGenericExpression = (Expression<Func<IsEquals<long>, long, long, bool>>)(ExpressionBuilder.Call(isEqualsType, isEqualMethod, genericTypeArguments));
            var isEqualGenericMethod = isEqualGenericExpression.Compile();

            // Act
            var actualNonGeneric = isEqualNonGenericMethod.DynamicInvoke(isEqualsForLong, 42, 42);
            var actualGeneric = isEqualGenericMethod(isEqualsForLong, 42, 42);

            // Assert
            Assert.Equal(true, actualNonGeneric);
            Assert.Equal(true, actualGeneric);
        }

        [Fact]
        public void TestExpressionBuilderCallWithGenericMethods()
        {
            // Arrange
            var listOfObjects = new List<object> {1, 2, 3};

            var enumerableType = typeof(Enumerable);
            var enumerableCastMethod = enumerableType.GetMethod("Cast");

            var genericTypeArguments = new[] {typeof(int)};
            var listOfIntsNonGenericExpression = ExpressionBuilder.Call(enumerableType, enumerableCastMethod, genericTypeArguments);
            var listOfIntsNonGenericMethod = listOfIntsNonGenericExpression.Compile();

            var listOfIntsGenericExpression = (Expression<Func<IEnumerable, IEnumerable<int>>>)(ExpressionBuilder.Call(enumerableType, enumerableCastMethod, genericTypeArguments));
            var listOfIntsGenericMethod = listOfIntsGenericExpression.Compile();

            // Act
            var listOfIntsNonGeneric = (IEnumerable<int>)listOfIntsNonGenericMethod.DynamicInvoke(listOfObjects);
            var listOfIntsGeneric = listOfIntsGenericMethod(listOfObjects);

            // Assert
            Assert.NotNull(listOfIntsNonGeneric);
            Assert.NotNull(listOfIntsGeneric);
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderMethodCallGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithMethodCallGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderMethodCallNonGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithMethodCallNonGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderVoidMethodCallGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithVoidMethodCallGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderVoidMethodCallNonGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithVoidMethodCallNonGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderStaticMethodCallGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithStaticMethodCallGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderStaticMethodCallNonGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithStaticMethodCallNonGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderStaticVoidMethodCallGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithStaticVoidMethodCallGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }

        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderStaticVoidMethodCallNonGeneric(string name, ICallTest callTest)
        {
            // Arrange

            // Act
            callTest.ArrangeAndActWithStaticVoidMethodCallNonGeneric();
            callTest.OutputTest(this);

            // Assert
            callTest.AssertTest();
        }


        [Theory]
        [MemberData("CastTestData")]
        public void TestExpressionBuilderCastGeneric(string name, ICastTest castTest)
        {
            // Arrange

            // Act
            castTest.ArrangeAndActWithCastGeneric();
            castTest.OutputTest(this);

            // Assert
            castTest.AssertTest();
        }

        [Theory]
        [MemberData("CastTestData")]
        public void TestExpressionBuilderCastNonGeneric(string name, ICastTest castTest)
        {
            // Arrange

            // Act
            castTest.ArrangeAndActWithCastNonGeneric();
            castTest.OutputTest(this);

            // Assert
            castTest.AssertTest();
        }

        [Theory]
        [MemberData("CastAsTestData")]
        public void TestExpressionBuilderCastAsGeneric(string name, ICastTest castTest)
        {
            // Arrange

            // Act
            castTest.ArrangeAndActWithCastAsGeneric();
            castTest.OutputTest(this);

            // Assert
            castTest.AssertTest();
        }

        [Theory]
        [MemberData("CastAsTestData")]
        public void TestExpressionBuilderCastAsNonGeneric(string name, ICastTest castTest)
        {
            // Arrange

            // Act
            castTest.ArrangeAndActWithCastAsNonGeneric();
            castTest.OutputTest(this);

            // Assert
            castTest.AssertTest();
        }


        [Theory]
        [MemberData("DefaultTestData")]
        public void TestExpressionBuilderDefaultGeneric(string name, IDefaultTest defaultTest)
        {
            // Arrange

            // Act
            defaultTest.ArrangeAndActWithDefaultGeneric();
            defaultTest.OutputTest(this);

            // Assert
            defaultTest.AssertTest();
        }

        [Theory]
        [MemberData("DefaultTestData")]
        public void TestExpressionBuilderDefaultNonGeneric(string name, IDefaultTest defaultTest)
        {
            // Arrange

            // Act
            defaultTest.ArrangeAndActWithDefaultNonGeneric();
            defaultTest.OutputTest(this);

            // Assert
            defaultTest.AssertTest();
        }


        [Theory]
        [MemberData("NewTestData")]
        public void TestExpressionBuilderNewGeneric(string name, INewTest newTest)
        {
            // Arrange

            // Act
            newTest.ArrangeAndActWithNewGeneric();
            newTest.OutputTest(this);

            // Assert
            newTest.AssertTest();
        }

        [Theory]
        [MemberData("NewTestData")]
        public void TestExpressionBuilderNewNonGeneric(string name, INewTest newTest)
        {
            // Arrange

            // Act
            newTest.ArrangeAndActWithNewNonGeneric();
            newTest.OutputTest(this);

            // Assert
            newTest.AssertTest();
        }


        [Theory]
        [MemberData("PropertyGetterTestData")]
        public void TestExpressionBuilderPropertyGetterGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithPropertyGetterGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertyGetterTestData")]
        public void TestExpressionBuilderPropertyGetterNonGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithPropertyGetterNonGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertySetterTestData")]
        public void TestExpressionBuilderPropertySetterGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithPropertySetterGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertySetterTestData")]
        public void TestExpressionBuilderPropertySetterNonGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithPropertySetterNonGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertyGetterTestData")]
        public void TestExpressionBuilderStaticPropertyGetterGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithStaticPropertyGetterGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertyGetterTestData")]
        public void TestExpressionBuilderStaticPropertyGetterNonGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithStaticPropertyGetterNonGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertySetterTestData")]
        public void TestExpressionBuilderStaticPropertySetterGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithStaticPropertySetterGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }

        [Theory]
        [MemberData("PropertySetterTestData")]
        public void TestExpressionBuilderStaticPropertySetterNonGeneric(string name, IPropertyTest propertyTest)
        {
            // Arrange

            // Act
            propertyTest.ArrangeAndActWithStaticPropertySetterNonGeneric();
            propertyTest.OutputTest(this);

            // Assert
            propertyTest.AssertTest();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string GetTypeAsString(Type type)
        {
            if (type == null)
                return "null";

            if (!type.IsNullableType())
                return type.Name;

            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            return "Nullable<{0}>".FormatWith(nullableUnderlyingType.Name);
        }

        private static string GetObjectAsString(object obj)
        {
            return obj != null ? obj.ToString() : "null";   
        }

        private static object GetInstanceProperty(Type objectType, object obj, string propertyName)
        {
            var propertyInfo = objectType.GetProperty(propertyName);

            var getMethod = propertyInfo.GetGetMethod();
            var getMethodResult = getMethod.Invoke(obj, null);
            return getMethodResult;
        }

        private static TProperty GetInstanceProperty<TObject, TProperty>(object obj, string propertyName)
        {
            var objectType = typeof(TObject);

            var getMethodResult = GetInstanceProperty(objectType, obj, propertyName);
            var getMethodResultTyped = (TProperty)getMethodResult;
            return getMethodResultTyped;
        }

        private static object GetStaticProperty(Type objectType, string propertyName)
        {
            var propertyInfo = objectType.GetProperty(propertyName);

            var getMethod = propertyInfo.GetGetMethod();
            var getMethodResult = getMethod.Invoke(null, null);
            return getMethodResult;
        }

        private static TProperty GetStaticProperty<TObject, TProperty>(string propertyName)
        {
            var objectType = typeof(TObject);

            var getMethodResult = GetStaticProperty(objectType, propertyName);
            var getMethodResultTyped = (TProperty)getMethodResult;
            return getMethodResultTyped;
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly string AdderMethodCallName = StaticReflection.GetMemberName<Adder>(x => x.Add());
        public static readonly string AdderStaticMethodCallName = StaticReflection.GetMemberName<Adder>(x => Adder.StaticAdd());
        public static readonly string AdderStaticVoidMethodCallName = StaticReflection.GetMemberName<Adder>(x => Adder.StaticAddNoReturnValue());
        public static readonly string AdderVoidMethodCallName = StaticReflection.GetMemberName<Adder>(x => x.AddNoReturnValue());

        public static readonly string AdderInstanceActualResultPropertyName = StaticReflection.GetMemberName<Adder>(x => x.Sum);
        public static readonly string AdderStaticActualResultPropertyName = StaticReflection.GetMemberName<Adder>(x => Adder.StaticSum);

        //public class IsEquals<T>
        //    where T : IEquatable<T>
        //{
        //    public IsEquals() { this.AreEqual = default(bool); }
        //    static IsEquals() { StaticAreEqual = default(bool); }

        //    public bool AreEqual { get; set; }
        //    public static bool StaticAreEqual { get; set; }

        //    public bool IsEqual() { return AreEqual; }
        //    public bool IsEqual(T x) { return x.Equals(x); }
        //    public bool IsEqual(T x, T y) { return x.Equals(y); }

        //    public void IsEqualNoReturnValue() { this.AreEqual = default(bool); }
        //    public void IsEqualNoReturnValue(T x) { this.AreEqual = x.Equals(x); }
        //    public void IsEqualNoReturnValue(T x, T y) { this.AreEqual = x.Equals(y); }

        //    public static bool StaticIsEqual() { return StaticAreEqual; }
        //    public static bool StaticIsEqual(T x) { return x.Equals(x); }
        //    public static bool StaticIsEqual(T x, T y) { return x.Equals(y); }

        //    public static void StaticIsEqualNoReturnValue() { StaticAreEqual = default(bool); }
        //    public static void StaticIsEqualNoReturnValue(T x) { StaticAreEqual = x.Equals(x); }
        //    public static void StaticIsEqualNoReturnValue(T x, T y) { StaticAreEqual = x.Equals(y); }
        //}

        public static readonly string IsEqualsMethodCallName = StaticReflection.GetMemberName<IsEquals<int>>(x => x.IsEqual());
        public static readonly string IsEqualsStaticMethodCallName = StaticReflection.GetMemberName<IsEquals<int>>(x => IsEquals<int>.StaticIsEqual());
        public static readonly string IsEqualsStaticVoidMethodCallName = StaticReflection.GetMemberName<IsEquals<int>>(x => IsEquals<int>.StaticIsEqualNoReturnValue());
        public static readonly string IsEqualsVoidMethodCallName = StaticReflection.GetMemberName<IsEquals<int>>(x => x.IsEqualNoReturnValue());

        public static readonly string IsEqualsInstanceActualResultPropertyName = StaticReflection.GetMemberName<IsEquals<int>>(x => x.AreEqual);
        public static readonly string IsEqualsStaticActualResultPropertyName = StaticReflection.GetMemberName<IsEquals<int>>(x => IsEquals<int>.StaticAreEqual);

        public static readonly IEnumerable<object[]> CallTestData = new[]
            {
                new object[] {"WithAdderAndZeroArguments", new CallTestWithZeroArguments<Adder, int>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, Adder.DefaultSum, AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},
                new object[] {"WithAdderAndOneArgument", new CallTestWithOneArgument<Adder, int, int>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, 42, 42, AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},
                new object[] {"WithAdderAndTwoArguments", new CallTestWithTwoArguments<Adder, int, int, int>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, 20, 22, 42, AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},

                new object[] {"WithStringAdderAndZeroArguments", new CallTestWithZeroArguments<StringAdder, string>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, StringAdder.DefaultSum, AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},
                new object[] {"WithStringAdderAndOneArgument", new CallTestWithOneArgument<StringAdder, string, string>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, "42", "42", AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},
                new object[] {"WithStringAdderAndTwoArguments", new CallTestWithTwoArguments<StringAdder, string, int, string>(AdderMethodCallName, AdderStaticMethodCallName, AdderStaticVoidMethodCallName, AdderVoidMethodCallName, "20", 22, "20+22=42", AdderInstanceActualResultPropertyName, AdderStaticActualResultPropertyName)},

                new object[] {"WithIsEqualsAndZeroArguments", new CallTestWithZeroArguments<IsEquals<long>, bool>(IsEqualsMethodCallName, IsEqualsStaticMethodCallName, IsEqualsStaticVoidMethodCallName, IsEqualsVoidMethodCallName, false, IsEqualsInstanceActualResultPropertyName, IsEqualsStaticActualResultPropertyName)},
                new object[] {"WithIsEqualsAndOneArgument", new CallTestWithOneArgument<IsEquals<long>, long, bool>(IsEqualsMethodCallName, IsEqualsStaticMethodCallName, IsEqualsStaticVoidMethodCallName, IsEqualsVoidMethodCallName, 42, true, IsEqualsInstanceActualResultPropertyName, IsEqualsStaticActualResultPropertyName)},
                new object[] {"WithIsEqualsAndTwoArguments", new CallTestWithTwoArguments<IsEquals<long>, long, long, bool>(IsEqualsMethodCallName, IsEqualsStaticMethodCallName, IsEqualsStaticVoidMethodCallName, IsEqualsVoidMethodCallName, 20, 22, false, IsEqualsInstanceActualResultPropertyName, IsEqualsStaticActualResultPropertyName)},
            };

        public static readonly IEnumerable<object[]> CastTestData = new[]
            {
                new object[] {"FromShortIntegerToLongInteger", new CastTest<short, long>(42, true, false)},
                new object[] {"FromLongIntegerToShortInteger", new CastTest<long, short>(42, true, false)},

                new object[] {"FromNullObjectToString", new CastTest<object, string>(null, true, true)},
                new object[] {"FromNullObjectToUri", new CastTest<object, Uri>(null, true, true)},

                new object[] {"FromStringObjectToString", new CastTest<object, string>("This is a test", true, false)},
                new object[] {"FromStringObjectToUri", new CastTest<object, Uri>("This is a test", false, true)},

                new object[] {"FromBaseClassToInterface", new CastTest<Foo, IFoo>(new Foo(), true, false)},
                new object[] {"FromDerivedClassToInterface", new CastTest<FooExtended, IFoo>(new FooExtended(), true, false)},
                new object[] {"FromDerivedClassToBaseClass", new CastTest<FooExtended, Foo>(new FooExtended(), true, false)},

                new object[] {"FromBaseClassToDerivedClass", new CastTest<Foo, FooExtended>(new Foo(), false, true)},

                new object[] {"FromNullObjectToNullableInteger", new CastTest<object, int?>(null, true, true)},
                new object[] {"FromIntegerObjectToNullableInteger", new CastTest<object, int?>(42, true, false)}
            };

        public static readonly IEnumerable<object[]> CastAsTestData = new[]
            {
                new object[] {"FromNullObjectToString", new CastTest<object, string>(null, true, true)},
                new object[] {"FromNullObjectToUri", new CastTest<object, Uri>(null, true, true)},

                new object[] {"FromStringObjectToString", new CastTest<object, string>("This is a test", true, false)},
                new object[] {"FromStringObjectToUri", new CastTest<object, Uri>("This is a test", false, true)},

                new object[] {"FromBaseClassToInterface", new CastTest<Foo, IFoo>(new Foo(), true, false)},
                new object[] {"FromDerivedClassToInterface", new CastTest<FooExtended, IFoo>(new FooExtended(), true, false)},
                new object[] {"FromDerivedClassToBaseClass", new CastTest<FooExtended, Foo>(new FooExtended(), true, false)},

                new object[] {"FromBaseClassToDerivedClass", new CastTest<Foo, FooExtended>(new Foo(), false, true)},
                new object[] {"FromBaseClassToString", new CastTest<Foo, string>(new Foo(), false, true)},

                new object[] {"FromNullObjectToNullableInteger", new CastTest<object, int?>(null, true, true)},
                new object[] {"FromIntegerObjectToNullableInteger", new CastTest<object, int?>(42, true, false)}
            };

        public static readonly IEnumerable<object[]> DefaultTestData = new[]
            {
                new object[] {"WithNullableType", new DefaultTest<long?>(default(long?))},
                new object[] {"WithReferenceType", new DefaultTest<string>(default(string))},
                new object[] {"WithValueType", new DefaultTest<long>(default(long))},
            };

        public static readonly IEnumerable<object[]> NewTestData = new[]
            {
                new object[] {"WithFooAndZeroArguments", new NewTestWithZeroArguments<Foo>(new Foo())},
                new object[] {"WithFooExtendedAndZeroArguments", new NewTestWithZeroArguments<FooExtended>(new FooExtended())},
                new object[] {"WithFooAndOneArgument", new NewTestWithOneArgument<int, Foo>(42, new Foo(42))},
                new object[] {"WithFooExtendedAndOneArgument", new NewTestWithOneArgument<int, FooExtended>(42, new FooExtended(42))},
                new object[] {"WithFooAndTwoArguments", new NewTestWithTwoArguments<int, string, Foo>(42, "42", new Foo(42, "42"))},
                new object[] {"WithFooExtendedAndTwoArguments", new NewTestWithTwoArguments<int, string, FooExtended>(42, "42", new FooExtended(42, "42"))},
            };

        public static readonly string IntPropertyName = StaticReflection.GetMemberName<Foo>(x => x.IntProperty);
        public static readonly string StringPropertyName = StaticReflection.GetMemberName<Foo>(x => x.StringProperty);

        public static readonly string StaticIntPropertyName = StaticReflection.GetMemberName<Foo>(x => Foo.StaticIntProperty);
        public static readonly string StaticStringPropertyName = StaticReflection.GetMemberName<Foo>(x => Foo.StaticStringProperty);

        public static readonly ComplexChildC TestComplexChildC = new ComplexChildC(420, "420");
        public static readonly ComplexChildB TestComplexChildB = new ComplexChildB(TestComplexChildC);
        public static readonly ComplexChildA TestComplexChildA = new ComplexChildA(TestComplexChildB);
        public static readonly Complex TestComplex = new Complex(TestComplexChildA, 42, "42");

        public static readonly IEnumerable<object[]> PropertyGetterTestData = new[]
            {
                new object[] {"WithFooAndIntProperty", new PropertyTest<Foo, int>(new Foo(42, "42"), true, 42, IntPropertyName, StaticIntPropertyName)},
                new object[] {"WithFooAndStringProperty", new PropertyTest<Foo, string>(new Foo(42, "42"), true, "42", StringPropertyName, StaticStringPropertyName)},
                new object[] {"WithFooExtendedAndIntProperty", new PropertyTest<FooExtended, int>(new FooExtended(42, "42"), true, 42, IntPropertyName, StaticIntPropertyName)},
                new object[] {"WithFooExtendedAndStringProperty", new PropertyTest<FooExtended, string>(new FooExtended(42, "42"), true, "42", StringPropertyName, StaticStringPropertyName)},
                new object[] {"WithFooAndInvalidProperty", new PropertyTest<Foo, int>(new Foo(42, "42"), false, default(int), "Invalid", "Invalid")},
                new object[] {"WithFooExtendedAndInvalidProperty", new PropertyTest<FooExtended, string>(new FooExtended(42, "42"), false, default(string), "Invalid", "Invalid")},

                //new object[] {"WithComplexAndIntProperty", new PropertyTest<Complex, int>(TestComplex, true, 42, IntPropertyName)},
                //new object[] {"WithComplexAndStringProperty", new PropertyTest<Complex, string>(TestComplex, true, "42", StringPropertyName)},
                //new object[] {"WithComplexAndAProperty", new PropertyTest<Complex, ComplexChildA>(TestComplex, true, TestComplexChildA, "A")},
                //new object[] {"WithComplexAndADotBProperty", new PropertyTest<Complex, ComplexChildB>(TestComplex, true, TestComplexChildB, "A.B")},
                //new object[] {"WithComplexAndADotBDotCProperty", new PropertyTest<Complex, ComplexChildC>(TestComplex, true, TestComplexChildC, "A.B.C")},
                //new object[] {"WithComplexAndADotBDotCDotIntProperty", new PropertyTest<Complex, int>(TestComplex, true, 420, "A.B.C.IntProperty")},
                //new object[] {"WithComplexAndADotBDotCDotStringProperty", new PropertyTest<Complex, string>(TestComplex, true, "420", "A.B.C.StringProperty")}
            };

        public static readonly IEnumerable<object[]> PropertySetterTestData = new[]
            {
                new object[] {"WithFooAndIntProperty", new PropertyTest<Foo, int>(new Foo(), true, 42, IntPropertyName, StaticIntPropertyName)},
                new object[] {"WithFooAndStringProperty", new PropertyTest<Foo, string>(new Foo(), true, "42", StringPropertyName, StaticStringPropertyName)},
                new object[] {"WithFooExtendedAndIntProperty", new PropertyTest<FooExtended, int>(new FooExtended(), true, 42, IntPropertyName, StaticIntPropertyName)},
                new object[] {"WithFooExtendedAndStringProperty", new PropertyTest<FooExtended, string>(new FooExtended(), true, "42", StringPropertyName, StaticStringPropertyName)},
                new object[] {"WithFooAndInvalidProperty", new PropertyTest<Foo, int>(new Foo(), false, default(int), "Invalid", "Invalid")},
                new object[] {"WithFooExtendedAndInvalidProperty", new PropertyTest<FooExtended, string>(new FooExtended(), false, default(string), "Invalid", "Invalid")}
            };
        #endregion

        #region Test Types
        // ReSharper disable MemberCanBePrivate.Global
        public interface ICallTest
        {
            void ArrangeAndActWithMethodCallGeneric();
            void ArrangeAndActWithMethodCallNonGeneric();

            void ArrangeAndActWithStaticMethodCallGeneric();
            void ArrangeAndActWithStaticMethodCallNonGeneric();

            void ArrangeAndActWithStaticVoidMethodCallGeneric();
            void ArrangeAndActWithStaticVoidMethodCallNonGeneric();

            void ArrangeAndActWithVoidMethodCallGeneric();
            void ArrangeAndActWithVoidMethodCallNonGeneric();

            void OutputTest(ExpressionBuilderTests parent);

            void AssertTest();
        }

        public interface ICastTest
        {
            void ArrangeAndActWithCastGeneric();
            void ArrangeAndActWithCastNonGeneric();

            void ArrangeAndActWithCastAsGeneric();
            void ArrangeAndActWithCastAsNonGeneric();

            void OutputTest(ExpressionBuilderTests parent);

            void AssertTest();
        }

        public interface IDefaultTest
        {
            void ArrangeAndActWithDefaultGeneric();
            void ArrangeAndActWithDefaultNonGeneric();

            void OutputTest(ExpressionBuilderTests parent);

            void AssertTest();
        }

        public interface INewTest
        {
            void ArrangeAndActWithNewGeneric();
            void ArrangeAndActWithNewNonGeneric();

            void OutputTest(ExpressionBuilderTests parent);

            void AssertTest();
        }

        public interface IPropertyTest
        {
            void ArrangeAndActWithPropertyGetterGeneric();
            void ArrangeAndActWithPropertyGetterNonGeneric();

            void ArrangeAndActWithPropertySetterGeneric();
            void ArrangeAndActWithPropertySetterNonGeneric();

            void ArrangeAndActWithStaticPropertyGetterGeneric();
            void ArrangeAndActWithStaticPropertyGetterNonGeneric();

            void ArrangeAndActWithStaticPropertySetterGeneric();
            void ArrangeAndActWithStaticPropertySetterNonGeneric();

            void OutputTest(ExpressionBuilderTests parent);

            void AssertTest();
        }

        public class CallTestWithZeroArguments<TObject, TResult> : ICallTest
            where TObject : new()
        {
            #region Constructor
            public CallTestWithZeroArguments(string methodCallName, string staticMethodCallName, string staticVoidMethodCallName, string voidMethodCallName,
                TResult expectedResult, string instanceActualResultPropertyName, string staticActualResultPropertyName)
            {
                this.MethodCallName = methodCallName;
                this.StaticMethodCallName = staticMethodCallName;
                this.StaticVoidMethodCallName = staticVoidMethodCallName;
                this.VoidMethodCallName = voidMethodCallName;

                this.ExpectedResult = expectedResult;

                this.InstanceActualResultPropertyName = instanceActualResultPropertyName;
                this.StaticActualResultPropertyName = staticActualResultPropertyName;
            }
            #endregion

            #region ICallTest Implementation
            public void ArrangeAndActWithMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.MethodCall<TObject, TResult>(this.MethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod(obj);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                // ReSharper disable once InvokeAsExtensionMethod
                var methodInfo = TypeExtensions.GetMethod(objectType, this.MethodCallName);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod.DynamicInvoke(obj);

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticMethodCall<TObject, TResult>(this.StaticMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod();

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                // ReSharper disable once InvokeAsExtensionMethod
                var methodInfo = TypeExtensions.GetMethod(objectType, this.StaticMethodCallName);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod.DynamicInvoke();

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticVoidMethodCall<TObject>(this.StaticVoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod();
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var methodInfo = TypeExtensions.GetMethod(objectType, this.StaticVoidMethodCallName);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod.DynamicInvoke();
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void ArrangeAndActWithVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.VoidMethodCall<TObject>(this.VoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod(obj);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                // ReSharper disable once InvokeAsExtensionMethod
                var methodInfo = TypeExtensions.GetMethod(objectType, this.VoidMethodCallName);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod.DynamicInvoke(obj);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                parent.Output.WriteLine("Lambda={{{0}}} ObjectType={1} MethodName={2} ReturnType={3} ExpectedValue={4} ActualValue={5}"
                    .FormatWith(this.ExpressionString, typeof(TObject), this.MethodName, typeof(TResult), this.ExpectedResult, this.ActualResult));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedResult, this.ActualResult);
            }
            #endregion

            #region User Supplied Properties
            private string MethodCallName { get; set; }
            private string StaticMethodCallName { get; set; }
            private string StaticVoidMethodCallName { get; set; }
            private string VoidMethodCallName { get; set; }

            private TResult ExpectedResult { get; set; }

            private string InstanceActualResultPropertyName { get; set; }
            private string StaticActualResultPropertyName { get; set; }
            #endregion

            #region Calculated Properties
            private TResult ActualResult { get; set; }

            private string MethodName { get; set; }
            private string ExpressionString { get; set; }
            #endregion
        }

        public class CallTestWithOneArgument<TObject, TArgument, TResult> : ICallTest
            where TObject : new()
        {
            #region Constructor
            public CallTestWithOneArgument(string methodCallName, string staticMethodCallName, string staticVoidMethodCallName, string voidMethodCallName,
                TArgument argument, TResult expectedResult, string instanceActualResultPropertyName, string staticActualResultPropertyName)
            {
                this.MethodCallName = methodCallName;
                this.StaticMethodCallName = staticMethodCallName;
                this.StaticVoidMethodCallName = staticVoidMethodCallName;
                this.VoidMethodCallName = voidMethodCallName;

                this.Argument = argument;
                this.ExpectedResult = expectedResult;

                this.InstanceActualResultPropertyName = instanceActualResultPropertyName;
                this.StaticActualResultPropertyName = staticActualResultPropertyName;
            }
            #endregion

            #region ICallTest Implementation
            public void ArrangeAndActWithMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.MethodCall<TObject, TArgument, TResult>(this.MethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod(obj, this.Argument);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argumentType = typeof(TArgument);
                var methodInfo = objectType.GetMethod(this.MethodCallName, argumentType);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod.DynamicInvoke(obj, this.Argument);

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticMethodCall<TObject, TArgument, TResult>(this.StaticMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod(this.Argument);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argumentType = typeof(TArgument);
                var methodInfo = objectType.GetMethod(this.StaticMethodCallName, argumentType);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod.DynamicInvoke(this.Argument);

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticVoidMethodCall<TObject, TArgument>(this.StaticVoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod(this.Argument);
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argumentType = typeof(TArgument);
                var methodInfo = objectType.GetMethod(this.StaticVoidMethodCallName, argumentType);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod.DynamicInvoke(this.Argument);
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void ArrangeAndActWithVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument>(this.VoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod(obj, this.Argument);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argumentType = typeof(TArgument);
                var methodInfo = objectType.GetMethod(this.VoidMethodCallName, argumentType);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod.DynamicInvoke(obj, this.Argument);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                parent.Output.WriteLine("Lambda={{{0}}} ObjectType={1} MethodName={2} ArgumentType={3} ArgumentValue={4} ReturnType={5} ExpectedValue={6} ActualValue={7}"
                    .FormatWith(this.ExpressionString, typeof(TObject), this.MethodName, typeof(TArgument), this.Argument, typeof(TResult), this.ExpectedResult, this.ActualResult));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedResult, this.ActualResult);
            }
            #endregion

            #region User Supplied Properties
            private string MethodCallName { get; set; }
            private string StaticMethodCallName { get; set; }
            private string StaticVoidMethodCallName { get; set; }
            private string VoidMethodCallName { get; set; }

            private TArgument Argument { get; set; }
            private TResult ExpectedResult { get; set; }

            private string InstanceActualResultPropertyName { get; set; }
            private string StaticActualResultPropertyName { get; set; }
            #endregion

            #region Calculated Properties
            private TResult ActualResult { get; set; }

            private string MethodName { get; set; }
            private string ExpressionString { get; set; }
            #endregion
        }

        public class CallTestWithTwoArguments<TObject, TArgument1, TArgument2, TResult> : ICallTest
            where TObject : new()
        {
            #region Constructor
            public CallTestWithTwoArguments(string methodCallName, string staticMethodCallName, string staticVoidMethodCallName, string voidMethodCallName,
                TArgument1 argument1, TArgument2 argument2, TResult expectedResult, string instanceActualResultPropertyName, string staticActualResultPropertyName)
            {
                this.MethodCallName = methodCallName;
                this.StaticMethodCallName = staticMethodCallName;
                this.StaticVoidMethodCallName = staticVoidMethodCallName;
                this.VoidMethodCallName = voidMethodCallName;

                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedResult = expectedResult;

                this.InstanceActualResultPropertyName = instanceActualResultPropertyName;
                this.StaticActualResultPropertyName = staticActualResultPropertyName;
            }
            #endregion

            #region ICallTest Implementation
            public void ArrangeAndActWithMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.MethodCall<TObject, TArgument1, TArgument2, TResult>(this.MethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod(obj, this.Argument1, this.Argument2);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argument1Type = typeof(TArgument1);
                var argument2Type = typeof(TArgument2);
                var methodInfo = objectType.GetMethod(this.MethodCallName, argument1Type, argument2Type);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.MethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                var actualResult = callMethod.DynamicInvoke(obj, this.Argument1, this.Argument2);

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticMethodCall<TObject, TArgument1, TArgument2, TResult>(this.StaticMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod(this.Argument1, this.Argument2);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argument1Type = typeof(TArgument1);
                var argument2Type = typeof(TArgument2);
                var methodInfo = objectType.GetMethod(this.StaticMethodCallName, argument1Type, argument2Type);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                var actualResult = callMethod.DynamicInvoke(this.Argument1, this.Argument2);

                this.ActualResult = (TResult)actualResult;
            }

            public void ArrangeAndActWithStaticVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.StaticVoidMethodCall<TObject, TArgument1, TArgument2>(this.StaticVoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod(this.Argument1, this.Argument2);
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithStaticVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argument1Type = typeof(TArgument1);
                var argument2Type = typeof(TArgument2);
                var methodInfo = objectType.GetMethod(this.StaticVoidMethodCallName, argument1Type, argument2Type);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.StaticVoidMethodCallName;
                this.ExpressionString = callExpressionString;

                // Act
                callMethod.DynamicInvoke(this.Argument1, this.Argument2);
                var actualResult = GetStaticProperty<TObject, TResult>(this.StaticActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void ArrangeAndActWithVoidMethodCallGeneric()
            {
                // Arrange
                var callExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument1, TArgument2>(this.VoidMethodCallName);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod(obj, this.Argument1, this.Argument2);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }
            public void ArrangeAndActWithVoidMethodCallNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);
                var argument1Type = typeof(TArgument1);
                var argument2Type = typeof(TArgument2);
                var methodInfo = objectType.GetMethod(this.VoidMethodCallName, argument1Type, argument2Type);
                var callExpression = ExpressionBuilder.Call(objectType, methodInfo);
                var callExpressionString = callExpression.ToString();
                var callMethod = callExpression.Compile();

                this.MethodName = this.VoidMethodCallName;
                this.ExpressionString = callExpressionString;

                var obj = new TObject();

                // Act
                callMethod.DynamicInvoke(obj, this.Argument1, this.Argument2);
                var actualResult = GetInstanceProperty<TObject, TResult>(obj, this.InstanceActualResultPropertyName);

                this.ActualResult = actualResult;
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                parent.Output.WriteLine("Lambda={{{0}}} ObjectType={1} MethodName={2} ArgumentType1={3} ArgumentValue1={4} ArgumentType1={5} ArgumentValue1={6} ReturnType={7} ExpectedValue={8} ActualValue={9}"
                    .FormatWith(this.ExpressionString, typeof(TObject), this.MethodName, typeof(TArgument1), this.Argument1, typeof(TArgument2), this.Argument2, typeof(TResult), this.ExpectedResult, this.ActualResult));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedResult, this.ActualResult);
            }
            #endregion

            #region User Supplied Properties
            private string MethodCallName { get; set; }
            private string StaticMethodCallName { get; set; }
            private string StaticVoidMethodCallName { get; set; }
            private string VoidMethodCallName { get; set; }

            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TResult ExpectedResult { get; set; }

            private string InstanceActualResultPropertyName { get; set; }
            private string StaticActualResultPropertyName { get; set; }
            #endregion

            #region Calculated Properties
            private TResult ActualResult { get; set; }

            private string MethodName { get; set; }
            private string ExpressionString { get; set; }
            #endregion
        }

        public class CastTest<TFrom, TTo> : ICastTest
        {
            #region Constructor
            public CastTest(TFrom fromValue, bool expectedResult, bool expectedDefaultValue)
            {
                this.FromValue = fromValue;
                this.ExpectedResult = expectedResult;
                this.ExpectedDefaultValue = expectedDefaultValue;
            }
            #endregion

            #region ICastTest Implementation
            public void ArrangeAndActWithCastGeneric()
            {
                // Arrange
                var fromToGenericCastExpression = ExpressionBuilder.Cast<TFrom, TTo>();
                var fromToGenericCastExpressionString = fromToGenericCastExpression.ToString();
                this.ExpressionString = fromToGenericCastExpressionString;
                var fromToGenericCastMethod = fromToGenericCastExpression.Compile();

                // Act
                if (this.ExpectedResult)
                {
                    this.ActualValue = fromToGenericCastMethod(this.FromValue);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => fromToGenericCastMethod(this.FromValue));
                }
            }
            public void ArrangeAndActWithCastNonGeneric()
            {
                // Arrange
                var fromToNonGenericCastExpression = ExpressionBuilder.Cast(typeof(TFrom), typeof(TTo));
                var fromToNonGenericCastExpressionString = fromToNonGenericCastExpression.ToString();
                this.ExpressionString = fromToNonGenericCastExpressionString;
                var fromToNonGenericCastMethod = fromToNonGenericCastExpression.Compile();

                // Act
                if (this.ExpectedResult)
                {
                    this.ActualValue = (TTo)fromToNonGenericCastMethod.DynamicInvoke(this.FromValue);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => fromToNonGenericCastMethod.DynamicInvoke(this.FromValue));
                }
            }

            public void ArrangeAndActWithCastAsGeneric()
            {
                // Arrange
                var fromToGenericCastAsExpression = ExpressionBuilder.CastAs<TFrom, TTo>();
                var fromToGenericCastAsExpressionString = fromToGenericCastAsExpression.ToString();
                this.ExpressionString = fromToGenericCastAsExpressionString;
                var fromToGenericCastAsMethod = fromToGenericCastAsExpression.Compile();

                // Act
                this.ActualValue = fromToGenericCastAsMethod(this.FromValue);
            }
            public void ArrangeAndActWithCastAsNonGeneric()
            {
                // Arrange
                var fromToGenericCastAsExpression = ExpressionBuilder.CastAs(typeof(TFrom), typeof(TTo));
                var fromToGenericCastAsExpressionString = fromToGenericCastAsExpression.ToString();
                this.ExpressionString = fromToGenericCastAsExpressionString;
                var fromToGenericCastAsMethod = fromToGenericCastAsExpression.Compile();

                // Act
                this.ActualValue = (TTo)fromToGenericCastAsMethod.DynamicInvoke(this.FromValue);
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;

                var fromTypeAsString = GetTypeAsString(typeof(TFrom));
                var fromValueAsString = GetObjectAsString(this.FromValue);

                var toTypeAsString = GetTypeAsString(typeof(TTo));

                var expectedResultAsString = this.ExpectedResult.ToString();
                var expectedDefaultValueAsString = this.ExpectedDefaultValue.ToString();

                var actualValueAsString = GetObjectAsString(this.ActualValue);

                parent.Output.WriteLine("Lambda={{{0}}} FromType={1} FromValue={2} ToType={3} ExpectedResult={4} ExpectedDefaultValue={5} ActualValue={6}"
                    .FormatWith(lambdaExpression, fromTypeAsString, fromValueAsString, toTypeAsString, expectedResultAsString, expectedDefaultValueAsString, actualValueAsString));
            }

            public void AssertTest()
            {
                if (this.ExpectedResult == false)
                {
                    Assert.Null(this.ActualValue);
                    return;
                }

                if (this.ExpectedDefaultValue)
                {
                    Assert.Null(this.ActualValue);
                    return;
                }

                Assert.NotNull(this.ActualValue);
            }
            #endregion

            #region Properties
            private TFrom FromValue { get; set; }

            private bool ExpectedResult { get; set; }
            private bool ExpectedDefaultValue { get; set; }

            private TTo ActualValue { get; set; }

            private string ExpressionString { get; set; }
            #endregion
        }

        public class DefaultTest<TObject> : IDefaultTest
        {
            #region Constructor
            public DefaultTest(TObject expectedDefault)
            {
                this.ObjectType = typeof(TObject);
                this.ExpectedDefault = expectedDefault;
            }
            #endregion

            #region IDefaultTest Implementation
            public void ArrangeAndActWithDefaultGeneric()
            {
                // Arrange
                var genericDefaultExpression = ExpressionBuilder.Default<TObject>();
                var genericDefaultExpressionString = genericDefaultExpression.ToString();
                this.ExpressionString = genericDefaultExpressionString;

                var genericDefaultMethod = genericDefaultExpression.Compile();

                // Act
                this.ActualDefault = genericDefaultMethod();
            }

            public void ArrangeAndActWithDefaultNonGeneric()
            {
                // Arrange
                var nonGenericDefaultExpression = ExpressionBuilder.Default(this.ObjectType);
                var nonGenericDefaultExpressionString = nonGenericDefaultExpression.ToString();
                this.ExpressionString = nonGenericDefaultExpressionString;

                var nonGenericDefaultMethod = nonGenericDefaultExpression.Compile();

                // Act
                var actualObject = nonGenericDefaultMethod.DynamicInvoke();
                this.ActualDefault = (TObject)actualObject;
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;

                var objectTypeAsString = GetTypeAsString(typeof(TObject));

                var expectedDefaultAsString = GetObjectAsString(this.ExpectedDefault);
                var actualDefaultAsString = GetObjectAsString(this.ActualDefault);

                parent.Output.WriteLine("Lambda={{{0}}} Type={1} ExpectedDefault={2} ActualDefault={3}"
                    .FormatWith(lambdaExpression, objectTypeAsString, expectedDefaultAsString, actualDefaultAsString));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedDefault, this.ActualDefault);
            }
            #endregion

            #region Properties
            private Type ObjectType { get; set; }

            private TObject ExpectedDefault { get; set; }
            private TObject ActualDefault { get; set; }

            private string ExpressionString { get; set; }
            #endregion
        }

        public class NewTestWithZeroArguments<TObject> : INewTest
        {
            #region Constructor
            public NewTestWithZeroArguments(TObject expectedObject)
            {
                this.ExpectedObject = expectedObject;
            }
            #endregion

            #region INewTest Implementation
            public void ArrangeAndActWithNewGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New<TObject>();
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = newMethod();
            }
            public void ArrangeAndActWithNewNonGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New(typeof(TObject));
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = (TObject)newMethod.DynamicInvoke();
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;
                var objectType = typeof(TObject).Name;
                var expectedObjectAsString = this.ExpectedObject.ToString();
                var actualObjectAsString = this.ActualObject.ToString();

                parent.Output.WriteLine("Lambda={{{0}}} ObjectType={1} ExpectedObject={2} ActualObject={3}".FormatWith(lambdaExpression, objectType, expectedObjectAsString, actualObjectAsString));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedObject, this.ActualObject);
            }
            #endregion

            #region Properties
            private TObject ExpectedObject { get; set; }
            private TObject ActualObject { get; set; }

            private string ExpressionString { get; set; }
            #endregion
        }

        public class NewTestWithOneArgument<TArgument, TObject> : INewTest
        {
            #region Constructor
            public NewTestWithOneArgument(TArgument argument, TObject expectedObject)
            {
                this.Argument = argument;
                this.ExpectedObject = expectedObject;
            }
            #endregion

            #region INewTest Implementation
            public void ArrangeAndActWithNewGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New<TArgument, TObject>();
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = newMethod(this.Argument);
            }
            public void ArrangeAndActWithNewNonGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New(typeof(TArgument), typeof(TObject));
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = (TObject)newMethod.DynamicInvoke(this.Argument);
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;
                var argumentType = typeof(TArgument).Name;
                var argumentAsString = this.Argument.ToString();
                var objectType = typeof(TObject).Name;
                var expectedObjectAsString = this.ExpectedObject.ToString();
                var actualObjectAsString = this.ActualObject.ToString();

                parent.Output.WriteLine("Lambda={{{0}}} ArgumentType={1} ArgumentValue={2} ObjectType={3} ExpectedObject={4} ActualObject={5}"
                    .FormatWith(lambdaExpression, argumentType, argumentAsString, objectType, expectedObjectAsString, actualObjectAsString));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedObject, this.ActualObject);
            }
            #endregion

            #region Properties
            private TArgument Argument { get; set; }
            private TObject ExpectedObject { get; set; }
            private TObject ActualObject { get; set; }

            private string ExpressionString { get; set; }
            #endregion
        }

        public class NewTestWithTwoArguments<TArgument1, TArgument2, TObject> : INewTest
        {
            #region Constructor
            public NewTestWithTwoArguments(TArgument1 argument1, TArgument2 argument2, TObject expectedObject)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedObject = expectedObject;
            }
            #endregion

            #region INewTest Implementation
            public void ArrangeAndActWithNewGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New<TArgument1, TArgument2, TObject>();
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = newMethod(this.Argument1, this.Argument2);
            }
            public void ArrangeAndActWithNewNonGeneric()
            {
                // Arrange
                var newExpression = ExpressionBuilder.New(typeof(TArgument1), typeof(TArgument2), typeof(TObject));
                this.ExpressionString = newExpression.ToString();
                var newMethod = newExpression.Compile();

                // Act
                this.ActualObject = (TObject)newMethod.DynamicInvoke(this.Argument1, this.Argument2);
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;
                var argument1Type = typeof(TArgument1).Name;
                var argument1AsString = this.Argument1.ToString();
                var argument2Type = typeof(TArgument2).Name;
                var argument2AsString = this.Argument2.ToString();
                var objectType = typeof(TObject).Name;
                var expectedObjectAsString = this.ExpectedObject.ToString();
                var actualObjectAsString = this.ActualObject.ToString();

                parent.Output.WriteLine("Lambda={{{0}}} Argument1Type={1} Argument1Value={2} Argument2Type={3} Argument2Value={4} ObjectType={5} ExpectedObject={6} ActualObject={7}"
                    .FormatWith(lambdaExpression, argument1Type, argument1AsString, argument2Type, argument2AsString, objectType, expectedObjectAsString, actualObjectAsString));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedObject, this.ActualObject);
            }
            #endregion

            #region Properties
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TObject ExpectedObject { get; set; }
            private TObject ActualObject { get; set; }

            private string ExpressionString { get; set; }
            #endregion
        }

        public class PropertyTest<TObject, TProperty> : IPropertyTest
        {
            #region Constructor
            public PropertyTest(TObject obj, bool expectedResult, TProperty expectedValue, string propertyName, string staticPropertyName)
            {
                this.Obj = obj;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
                this.PropertyName = propertyName;
                this.StaticPropertyName = staticPropertyName;
            }
            #endregion

            #region IPropertyTest Implementation
            public void ArrangeAndActWithPropertyGetterGeneric()
            {
                // Arrange

                // Act
                if (this.ExpectedResult)
                {
                    var getExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                    var getExpressionString = getExpression.ToString();
                    this.ExpressionString = getExpressionString;
                    var getMethod = getExpression.Compile();

                    this.ActualValue = getMethod(this.Obj);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName));
                }
            }
            public void ArrangeAndActWithPropertyGetterNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);

                // Act
                if (this.ExpectedResult)
                {
                    var getExpression = ExpressionBuilder.PropertyGetter(objectType, this.PropertyName);
                    var getExpressionString = getExpression.ToString();
                    this.ExpressionString = getExpressionString;
                    var getMethod = getExpression.Compile();
                    this.ActualValue = (TProperty)getMethod.DynamicInvoke(this.Obj);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.PropertyGetter(objectType, this.PropertyName));
                }
            }

            public void ArrangeAndActWithPropertySetterGeneric()
            {
                // Arrange

                // Act
                if (this.ExpectedResult)
                {
                    var setExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(this.PropertyName);
                    var setExpressionString = setExpression.ToString();
                    this.ExpressionString = setExpressionString;
                    var setMethod = setExpression.Compile();

                    var getExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                    var getMethod = getExpression.Compile();

                    setMethod(this.Obj, this.ExpectedValue);
                    this.ActualValue = getMethod(this.Obj);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.PropertySetter<TObject, TProperty>(this.PropertyName));
                }
            }
            public void ArrangeAndActWithPropertySetterNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);

                // Act
                if (this.ExpectedResult)
                {
                    var setExpression = ExpressionBuilder.PropertySetter(objectType, this.PropertyName);
                    var setExpressionString = setExpression.ToString();
                    this.ExpressionString = setExpressionString;
                    var setMethod = setExpression.Compile();

                    var getExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                    var getMethod = getExpression.Compile();

                    setMethod.DynamicInvoke(this.Obj, this.ExpectedValue);
                    this.ActualValue = getMethod(this.Obj);
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.PropertySetter(objectType, this.PropertyName));
                }
            }

            public void ArrangeAndActWithStaticPropertyGetterGeneric()
            {
                // Arrange

                // Act
                if (this.ExpectedResult)
                {
                    var getExpression = ExpressionBuilder.StaticPropertyGetter<TObject, TProperty>(this.StaticPropertyName);
                    var getExpressionString = getExpression.ToString();
                    this.ExpressionString = getExpressionString;
                    var getMethod = getExpression.Compile();

                    this.ActualValue = getMethod();
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.StaticPropertyGetter<TObject, TProperty>(this.StaticPropertyName));
                }
            }
            public void ArrangeAndActWithStaticPropertyGetterNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);

                // Act
                if (this.ExpectedResult)
                {
                    var getExpression = ExpressionBuilder.StaticPropertyGetter(objectType, this.StaticPropertyName);
                    var getExpressionString = getExpression.ToString();
                    this.ExpressionString = getExpressionString;
                    var getMethod = getExpression.Compile();
                    this.ActualValue = (TProperty)getMethod.DynamicInvoke();
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.StaticPropertyGetter(objectType, this.StaticPropertyName));
                }
            }

            public void ArrangeAndActWithStaticPropertySetterGeneric()
            {
                // Arrange

                // Act
                if (this.ExpectedResult)
                {
                    var setExpression = ExpressionBuilder.StaticPropertySetter<TObject, TProperty>(this.StaticPropertyName);
                    var setExpressionString = setExpression.ToString();
                    this.ExpressionString = setExpressionString;
                    var setMethod = setExpression.Compile();

                    var getExpression = ExpressionBuilder.StaticPropertyGetter<TObject, TProperty>(this.StaticPropertyName);
                    var getMethod = getExpression.Compile();

                    setMethod(this.ExpectedValue);
                    this.ActualValue = getMethod();
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.StaticPropertySetter<TObject, TProperty>(this.StaticPropertyName));
                }
            }
            public void ArrangeAndActWithStaticPropertySetterNonGeneric()
            {
                // Arrange
                var objectType = typeof(TObject);

                // Act
                if (this.ExpectedResult)
                {
                    var setExpression = ExpressionBuilder.StaticPropertySetter(objectType, this.StaticPropertyName);
                    var setExpressionString = setExpression.ToString();
                    this.ExpressionString = setExpressionString;
                    var setMethod = setExpression.Compile();

                    var getExpression = ExpressionBuilder.StaticPropertyGetter<TObject, TProperty>(this.StaticPropertyName);
                    var getMethod = getExpression.Compile();

                    setMethod.DynamicInvoke(this.ExpectedValue);
                    this.ActualValue = getMethod();
                }
                else
                {
                    Assert.ThrowsAny<Exception>(() => ExpressionBuilder.StaticPropertySetter(objectType, this.StaticPropertyName));
                }
            }

            public void OutputTest(ExpressionBuilderTests parent)
            {
                var lambdaExpression = this.ExpressionString;
                var propertyHostType = typeof(TObject).Name;
                var propertyType = typeof(TProperty).Name;
                var propertyName = this.PropertyName;
                var staticPropertyName = this.StaticPropertyName;
                var expectedResultAsString = this.ExpectedResult.ToString();
                var expectedValueAsString = this.ExpectedResult ? this.ExpectedValue.ToString() : String.Empty;
                var actualValueAsString = this.ExpectedResult ? this.ActualValue.ToString() : String.Empty;

                parent.Output.WriteLine("Lambda={{{0}}} ObjectType={1} PropertyType={2} PropertyName={3} StaticPropertyName={4} ExpectedResult={5} ExpectedValue={6} ActualValue={7}"
                    .FormatWith(lambdaExpression, propertyHostType, propertyType, propertyName, staticPropertyName, expectedResultAsString, expectedValueAsString, actualValueAsString));
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedValue, this.ActualValue);
            }
            #endregion

            #region Properties
            private TObject Obj { get; set; }
            private bool ExpectedResult { get; set; }
            private TProperty ExpectedValue { get; set; }
            private string PropertyName { get; set; }
            private string StaticPropertyName { get; set; }

            private TProperty ActualValue { get; set; }
            private string ExpressionString { get; set; }
            #endregion
        }

        public interface IFoo
        {
            int IntProperty { get; set; }
            string StringProperty { get; set; }

            void Bar();
        }

        public class Foo : IFoo
        {
            public Foo()
            { }

            public Foo(int intValue)
            {
                this.IntProperty = intValue;

                StaticIntProperty = intValue;
            }

            public Foo(int intValue, string stringValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;

                StaticIntProperty = intValue;
                StaticStringProperty = stringValue;
            }

            public int IntProperty { get; set; }
            public string StringProperty { get; set; }

            public static int StaticIntProperty { get; set; }
            public static string StaticStringProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!typeof(Foo).IsAssignableFrom(otherType))
                    return false;

                var otherAsFoo = (Foo)other;
                return this.IntProperty == otherAsFoo.IntProperty && this.StringProperty == otherAsFoo.StringProperty;
            }

            public override int GetHashCode()
            {
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                var stringPropertyHashCode = (this.StringProperty ?? String.Empty).GetHashCode();
                return intPropertyHashCode ^ stringPropertyHashCode;
            }

            public override string ToString()
            { return "{0} [intProperty={1} stringProperty={2}]".FormatWith(this.GetType().Name, this.IntProperty, this.StringProperty); }

            public virtual void Bar() { }
        }

        public class FooExtended : Foo
        {
            public FooExtended()
            { }

            public FooExtended(int intValue)
                : base(intValue)
            { }

            public FooExtended(int intValue, string stringValue)
                : base(intValue, stringValue)
            { }

            public override void Bar() { }
        }

        public class Complex
        {
            public Complex() { }

            public Complex(ComplexChildA a, int intValue, string stringValue)
            {
                this.A = a;
                this.IntProperty = intValue;
                this.StringProperty = stringValue;
            }

            public ComplexChildA A { get; set; }

            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        public class ComplexChildA
        {
            public ComplexChildA() { }

            public ComplexChildA(ComplexChildB b)
            {
                this.B = b;
            }

            public ComplexChildB B { get; set; }
        }

        public class ComplexChildB
        {
            public ComplexChildB() { }

            public ComplexChildB(ComplexChildC c)
            {
                this.C = c;
            }

            public ComplexChildC C { get; set; }
        }

        public class ComplexChildC
        {
            public ComplexChildC() { }

            public ComplexChildC(int intValue, string stringValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;
            }

            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        public class Adder
        {
            public const int DefaultSum = -1;

            public Adder() { this.Sum = DefaultSum; }
            static Adder() { StaticSum = DefaultSum; }

            public TOther Convert<TOther>(int value)
            {
                return default(TOther);
            }

            public int Sum { get; set; }
            public static int StaticSum { get; set; }

            public int Add() { return DefaultSum; }
            public int Add(int x) { return x; }
            public int Add(int x, int y) { return x + y; }

            public void AddNoReturnValue() { this.Sum = DefaultSum; }
            public void AddNoReturnValue(int x) { this.Sum = x; }
            public void AddNoReturnValue(int x, int y) { this.Sum = x + y; }

            public static int StaticAdd() { return DefaultSum; }
            public static int StaticAdd(int x) { return x; }
            public static int StaticAdd(int x, int y) { return x + y; }

            public static void StaticAddNoReturnValue() { StaticSum = DefaultSum; }
            public static void StaticAddNoReturnValue(int x) { StaticSum = x; }
            public static void StaticAddNoReturnValue(int x, int y) { StaticSum = x + y; }
        }

        public class IsEquals<T>
            where T : IEquatable<T>
        {
            public IsEquals() { this.AreEqual = default(bool); }
            static IsEquals() { StaticAreEqual = default(bool); }

            public bool AreEqual { get; set; }
            public static bool StaticAreEqual { get; set; }

            public bool IsEqual() { return this.AreEqual; }
            public bool IsEqual(T x) { return x.Equals(x); }
            public bool IsEqual(T x, T y) { return x.Equals(y); }

            public void IsEqualNoReturnValue() { this.AreEqual = default(bool); }
            public void IsEqualNoReturnValue(T x) { this.AreEqual = x.Equals(x); }
            public void IsEqualNoReturnValue(T x, T y) { this.AreEqual = x.Equals(y); }

            public static bool StaticIsEqual() { return StaticAreEqual; }
            public static bool StaticIsEqual(T x) { return x.Equals(x); }
            public static bool StaticIsEqual(T x, T y) { return x.Equals(y); }

            public static void StaticIsEqualNoReturnValue() { StaticAreEqual = default(bool); }
            public static void StaticIsEqualNoReturnValue(T x) { StaticAreEqual = x.Equals(x); }
            public static void StaticIsEqualNoReturnValue(T x, T y) { StaticAreEqual = x.Equals(y); }
        }

        public class StringAdder
        {
            public const string DefaultSum = "Default";

            public StringAdder() { this.Sum = DefaultSum; }
            static StringAdder() { StaticSum = DefaultSum; }

            public string Sum { get; set; }
            public static string StaticSum { get; set; }

            public string Add() { return DefaultSum; }
            public string Add(string x) { return x; }
            public string Add(string x, int y) { return AddImpl(x, y); }

            public void AddNoReturnValue() { this.Sum = DefaultSum; }
            public void AddNoReturnValue(string x) { this.Sum = x; }
            public void AddNoReturnValue(string x, int y) { this.Sum = AddImpl(x, y); }

            public static string StaticAdd() { return DefaultSum; }
            public static string StaticAdd(string x) { return x; }
            public static string StaticAdd(string x, int y) { return AddImpl(x, y); }

            public static void StaticAddNoReturnValue() { StaticSum = DefaultSum; }
            public static void StaticAddNoReturnValue(string x) { StaticSum = x; }
            public static void StaticAddNoReturnValue(string x, int y) { StaticSum = AddImpl(x, y); }

            private static string AddImpl(string x, int y) { return x + "+" + Convert.ToString(y) + "=" + Convert.ToString(Convert.ToInt32(x) + y); }
        }
        // ReSharper restore MemberCanBePrivate.Global
        #endregion
    }
}
