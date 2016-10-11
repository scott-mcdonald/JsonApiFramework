// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Expressions;
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
        [Theory]
        [MemberData("CallTestData")]
        public void TestExpressionBuilderCall(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("CastTestData")]
        public void TestExpressionBuilderCast(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("CastAsTestData")]
        public void TestExpressionBuilderCastAs(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("DefaultTestData")]
        public void TestExpressionBuilderDefault(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("NewTestData")]
        public void TestExpressionBuilderNew(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("PropertyGetterTestData")]
        public void TestExpressionBuilderPropertyGetter(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData("PropertySetterTestData")]
        public void TestExpressionBuilderPropertySetter(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> CallTestData = new[]
            {
                new object[] { new MethodCallWith0ArgumentsUnitTest<Adder, string>("WithAdderObjectAnd0ArgumentsAndReturnValue", new Adder(), "Add", Adder.DefaultSum) },
                new object[] { new MethodCallWith1ArgumentsUnitTest<Adder, string, string>("WithAdderObjectAnd1ArgumentsAndReturnValue", new Adder(), "Add", "42", "42") },
                new object[] { new MethodCallWith2ArgumentsUnitTest<Adder, string, int, string>("WithAdderObjectAnd2ArgumentsAndReturnValue", new Adder(), "Add", "20", 22, "20+22=42") },

                new object[] { new VoidMethodCallWith0ArgumentsUnitTest<Adder>("WithAdderObjectAnd0ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", (x) => x.Sum.Should().Be(Adder.DefaultSum)) },
                new object[] { new VoidMethodCallWith1ArgumentsUnitTest<Adder, string>("WithAdderObjectAnd1ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", "42", (x) => x.Sum.Should().Be("42")) },
                new object[] { new VoidMethodCallWith2ArgumentsUnitTest<Adder, string, int>("WithAdderObjectAnd2ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", "20", 22, (x) => x.Sum.Should().Be("20+22=42")) },

                new object[] { new StaticMethodCallWith0ArgumentsUnitTest<Adder, string>("WithAdderClassAnd0ArgumentsAndReturnValue", "StaticAdd", Adder.DefaultSum) },
                new object[] { new StaticMethodCallWith1ArgumentsUnitTest<Adder, string, string>("WithAdderClassAnd1ArgumentsAndReturnValue", "StaticAdd", "42", "42") },
                new object[] { new StaticMethodCallWith2ArgumentsUnitTest<Adder, string, int, string>("WithAdderClassAnd2ArgumentsAndReturnValue", "StaticAdd", "20", 22, "20+22=42") },

                new object[] { new StaticVoidMethodCallWith0ArgumentsUnitTest<Adder>("WithAdderClassAnd0ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", () => Adder.StaticSum.Should().Be(Adder.DefaultSum)) },
                new object[] { new StaticVoidMethodCallWith1ArgumentsUnitTest<Adder, string>("WithAdderClassAnd1ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", "42", () => Adder.StaticSum.Should().Be("42")) },
                new object[] { new StaticVoidMethodCallWith2ArgumentsUnitTest<Adder, string, int>("WithAdderClassAnd2ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", "20", 22, () => Adder.StaticSum.Should().Be("20+22=42")) },

                new object[] { new MethodCallWith0ArgumentsUnitTest<IsEquals<long>, bool>("WithIsEqualsObjectAnd0ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", false) },
                new object[] { new MethodCallWith1ArgumentsUnitTest<IsEquals<long>, long, bool>("WithIsEqualsObjectAnd1ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", 20, true) },
                new object[] { new MethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long, bool>("WithIsEqualsObjectAnd2ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", 20, 22, false) },

                new object[] { new VoidMethodCallWith0ArgumentsUnitTest<IsEquals<long>>("WithIsEqualsObjectAnd0ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", (x) => x.AreEqual.Should().Be(false)) },
                new object[] { new VoidMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long>("WithIsEqualsObjectAnd1ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", 20, (x) => x.AreEqual.Should().Be(true)) },
                new object[] { new VoidMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long>("WithIsEqualsObjectAnd2ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", 20, 22, (x) => x.AreEqual.Should().Be(false)) },

                new object[] { new StaticMethodCallWith0ArgumentsUnitTest<IsEquals<long>, bool>("WithIsEqualsClassAnd0ArgumentsAndReturnValue", "StaticIsEqual", false) },
                new object[] { new StaticMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long, bool>("WithIsEqualsClassAnd1ArgumentsAndReturnValue", "StaticIsEqual", 20, true) },
                new object[] { new StaticMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long, bool>("WithIsEqualsClassAnd2ArgumentsAndReturnValue", "StaticIsEqual", 20, 22, false) },

                new object[] { new StaticVoidMethodCallWith0ArgumentsUnitTest<IsEquals<long>>("WithIsEqualsClassAnd0ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", () => IsEquals<long>.StaticAreEqual.Should().Be(false)) },
                new object[] { new StaticVoidMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long>("WithIsEqualsClassAnd1ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", 20, () => IsEquals<long>.StaticAreEqual.Should().Be(true)) },
                new object[] { new StaticVoidMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long>("WithIsEqualsClassAnd2ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", 20, 22, () => IsEquals<long>.StaticAreEqual.Should().Be(false)) },
            };

        public static readonly IEnumerable<object[]> CastTestData = new[]
            {
                new object[] { new CastUnitTest<short, long>("FromShortIntegerToLongInteger", 42, true, 42)},
                new object[] { new CastUnitTest<long, short>("FromLongIntegerToShortIntegerWithNoOverflow", 42, true, 42)},
                new object[] { new CastUnitTest<long, short>("FromLongIntegerToShortIntegerWithOverflow", 32767 + 1, false)},

                new object[] { new CastUnitTest<object, string>("FromNullObjectToString", null, true, null)},
                new object[] { new CastUnitTest<object, int>("FromNullObjectToInteger", null, false)},
                new object[] { new CastUnitTest<object, int?>("FromNullObjectToNullableInteger", null, true, new int?())},
                new object[] { new CastUnitTest<object, int?>("FromIntegerObjectToNullableInteger", 42, true, 42)},

                new object[] { new CastUnitTest<string, string>("FromStringToString", "This is a test.", true, "This is a test.")},

                new object[] { new CastUnitTest<Foo, IFoo>("FromBaseClassToInterface", new Foo(42, "42"), true, new Foo(42, "42"))},
                new object[] { new CastUnitTest<Foo, FooExtended>("FromBaseClassToDerivedClass", new Foo(42, "42"), false)},
                new object[] { new CastUnitTest<FooExtended, IFoo>("FromDerivedClassToInterface", new FooExtended(42, "42"), true, new FooExtended(42, "42"))},
                new object[] { new CastUnitTest<FooExtended, Foo>("FromDerivedClassToBaseClass", new FooExtended(42, "42"), true, new Foo(42, "42"))},
            };

        public static readonly IEnumerable<object[]> CastAsTestData = new[]
            {
                new object[] { new CastAsUnitTest<object, string>("FromNullObjectToString", null, true, null)},
                new object[] { new CastAsUnitTest<object, int?>("FromNullObjectToNullableInteger", null, true, new int?())},
                new object[] { new CastUnitTest<object, int?>("FromIntegerObjectToNullableInteger", 42, true, 42)},

                new object[] { new CastAsUnitTest<string, string>("FromStringToString", "This is a test.", true, "This is a test.")},
                new object[] { new CastAsUnitTest<string, Uri>("FromStringToUri", "This is a test.", false)},

                new object[] { new CastAsUnitTest<Foo, IFoo>("FromBaseClassToInterface", new Foo(42, "42"), true, new Foo(42, "42"))},
                new object[] { new CastAsUnitTest<Foo, FooExtended>("FromBaseClassToDerivedClass", new Foo(42, "42"), false)},
                new object[] { new CastAsUnitTest<FooExtended, IFoo>("FromDerivedClassToInterface", new FooExtended(42, "42"), true, new FooExtended(42, "42"))},
                new object[] { new CastAsUnitTest<FooExtended, Foo>("FromDerivedClassToBaseClass", new FooExtended(42, "42"), true, new Foo(42, "42"))},
            };

        public static readonly IEnumerable<object[]> DefaultTestData = new[]
            {
                new object[] { new DefaultUnitTest<long?>("WithNullableType", default(long?)) },
                new object[] { new DefaultUnitTest<string>("WithReferenceType", default(string)) },
                new object[] { new DefaultUnitTest<long>("WithValueType", default(long)) },
            };

        public static readonly IEnumerable<object[]> NewTestData = new[]
            {
                new object[] { new NewWith0ArgumentsUnitTest<Foo>("With0Arguments", new Foo()) },
                new object[] { new NewWith1ArgumentsUnitTest<int, Foo>("With1Arguments", 42, new Foo(42)) },
                new object[] { new NewWith2ArgumentsUnitTest<int, string, Foo>("With2Arguments", 42, "42", new Foo(42, "42")) },
            };

        public static readonly IEnumerable<object[]> PropertyGetterTestData = new[]
            {
                new object[] { new PropertyGetterUnitTest<Foo, int>("WithInstanceAndIntProperty", new Foo(42, "42"), "IntProperty", 42) },
                new object[] { new PropertyGetterUnitTest<Foo, string>("WithInstanceAndStringProperty", new Foo(42, "42"), "StringProperty", "42") },

                new object[] { new StaticPropertyGetterUnitTest<Foo, int>("WithStaticAndIntProperty", () => new Foo(42, "42"), "StaticIntProperty", 42) },
                new object[] { new StaticPropertyGetterUnitTest<Foo, string>("WithStaticAndStringProperty", () => new Foo(42, "42"), "StaticStringProperty", "42") },

                new object[] { new PropertyGetterUnitTest<Complex, int>("WithComplexInstanceAndIntProperty", new Complex(null, 1), "IntProperty", 1) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildA>("WithComplexInstanceAndChildAProperty", new Complex(new ComplexChildA(null, 2), 1), "A", new ComplexChildA(null, 2)) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildB>("WithComplexInstanceAndChildAPropertyDotChildBProperty", new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "A.B", new ComplexChildB(null, 3)) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildC>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C", new ComplexChildC(4)) },
                new object[] { new PropertyGetterUnitTest<Complex, int>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C.IntProperty", 4) },

                new object[] { new StaticPropertyGetterUnitTest<Complex, int>("WithComplexStaticAndIntProperty", () => new Complex(null, 1), "StaticIntProperty", 1) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildA>("WithComplexStaticAndChildAProperty", () => new Complex(new ComplexChildA(null, 2), 1), "StaticA", new ComplexChildA(null, 2)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildB>("WithComplexStaticAndChildAPropertyDotChildBProperty", () => new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "StaticA.StaticB", new ComplexChildB(null, 3)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildC>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC", new ComplexChildC(4)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, int>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC.StaticIntProperty", 4) },
            };

        public static readonly IEnumerable<object[]> PropertySetterTestData = new[]
            {
                new object[] { new PropertySetterUnitTest<Foo, int>("WithInstanceAndIntProperty", new Foo(), "IntProperty", 42) },
                new object[] { new PropertySetterUnitTest<Foo, string>("WithInstanceAndStringProperty", new Foo(), "StringProperty", "42") },

                new object[] { new StaticPropertySetterUnitTest<Foo, int>("WithStaticAndIntProperty", () => new Foo(0, null), "StaticIntProperty", 42) },
                new object[] { new StaticPropertySetterUnitTest<Foo, string>("WithStaticAndStringProperty", () => new Foo(0, null), "StaticStringProperty", "42") },

                new object[] { new PropertySetterUnitTest<Complex, int>("WithComplexInstanceAndIntProperty", new Complex(null, 1), "IntProperty", 10) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildA>("WithComplexInstanceAndChildAProperty", new Complex(new ComplexChildA(null, 2), 1), "A", new ComplexChildA(null, 20)) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildB>("WithComplexInstanceAndChildAPropertyDotChildBProperty", new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "A.B", new ComplexChildB(null, 30)) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildC>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C", new ComplexChildC(40)) },
                new object[] { new PropertySetterUnitTest<Complex, int>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C.IntProperty", 40) },

                new object[] { new StaticPropertySetterUnitTest<Complex, int>("WithComplexStaticAndIntProperty", () => new Complex(null, 1), "StaticIntProperty", 10) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildA>("WithComplexStaticAndChildAProperty", () => new Complex(new ComplexChildA(null, 2), 1), "StaticA", new ComplexChildA(null, 20)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildB>("WithComplexStaticAndChildAPropertyDotChildBProperty", () => new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "StaticA.StaticB", new ComplexChildB(null, 30)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildC>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC", new ComplexChildC(40)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, int>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC.StaticIntProperty", 40) },
            };
        #endregion

        #region Test Types
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
            { return "{0} [IntProperty={1} StringProperty={2}]".FormatWith(this.GetType().Name, this.IntProperty, this.StringProperty); }

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

            public Complex(ComplexChildA a, int intValue)
            {
                this.A = a;
                this.IntProperty = intValue;

                StaticA = a;
                StaticIntProperty = intValue;
            }

            public ComplexChildA A { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildA StaticA { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!typeof(Complex).IsAssignableFrom(otherType))
                    return false;

                var otherAsComplex = (Complex)other;

                var areChildrenEqual = this.A != null ? this.A.Equals(otherAsComplex.A) : otherAsComplex.A == null;
                return areChildrenEqual && this.IntProperty == otherAsComplex.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.A != null ? this.A.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            { return "{0} [A={1} IntProperty={2}]".FormatWith(this.GetType().Name, this.A != null ? this.A.ToString() : "null", this.IntProperty); }
        }

        public class ComplexChildA
        {
            public ComplexChildA() { }

            public ComplexChildA(ComplexChildB b, int intValue)
            {
                this.B = b;
                this.IntProperty = intValue;

                StaticB = b;
                StaticIntProperty = intValue;
            }

            public ComplexChildB B { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildB StaticB { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!typeof(ComplexChildA).IsAssignableFrom(otherType))
                    return false;

                var otherAsComplexChildA = (ComplexChildA)other;

                var areChildrenEqual = this.B != null ? this.B.Equals(otherAsComplexChildA.B) : otherAsComplexChildA.B == null;
                return areChildrenEqual && this.IntProperty == otherAsComplexChildA.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.B != null ? this.B.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            { return "{0} [B={1} IntProperty={2}]".FormatWith(this.GetType().Name, this.B != null ? this.B.ToString() : "null", this.IntProperty); }
        }

        public class ComplexChildB
        {
            public ComplexChildB() { }

            public ComplexChildB(ComplexChildC c, int intValue)
            {
                this.C = c;
                this.IntProperty = intValue;

                StaticC = c;
                StaticIntProperty = intValue;
            }

            public ComplexChildC C { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildC StaticC { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!typeof(ComplexChildB).IsAssignableFrom(otherType))
                    return false;

                var otherAsComplexChildB = (ComplexChildB)other;

                var areChildrenEqual = this.C != null ? this.C.Equals(otherAsComplexChildB.C) : otherAsComplexChildB.C == null;
                return areChildrenEqual && this.IntProperty == otherAsComplexChildB.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.C != null ? this.C.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            { return "{0} [C={1} IntProperty={2}]".FormatWith(this.GetType().Name, this.C != null ? this.C.ToString() : "null", this.IntProperty); }
        }

        public class ComplexChildC
        {
            public ComplexChildC() { }

            public ComplexChildC(int intValue)
            {
                this.IntProperty = intValue;

                StaticIntProperty = intValue;
            }

            public int IntProperty { get; set; }

            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!typeof(ComplexChildC).IsAssignableFrom(otherType))
                    return false;

                var otherAsComplexChildC = (ComplexChildC)other;
                return this.IntProperty == otherAsComplexChildC.IntProperty;
            }

            public override int GetHashCode()
            {
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return intPropertyHashCode;
            }

            public override string ToString()
            { return "{0} [IntProperty={1}]".FormatWith(this.GetType().Name, this.IntProperty); }
        }

        public class Adder
        {
            public const string DefaultSum = "Default";

            public Adder() { this.Sum = DefaultSum; }
            static Adder() { StaticSum = DefaultSum; }

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
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region UnitTest Types
        public class MethodCallWith0ArgumentsUnitTest<TObject, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith0ArgumentsUnitTest(string name, TObject source, string methodName, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class MethodCallWith1ArgumentsUnitTest<TObject, TArgument, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith1ArgumentsUnitTest(string name, TObject source, string methodName, TArgument argument, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument = argument;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TArgument, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source, this.Argument);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class MethodCallWith2ArgumentsUnitTest<TObject, TArgument1, TArgument2, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith2ArgumentsUnitTest(string name, TObject source, string methodName, TArgument1 argument1, TArgument2 argument2, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TArgument1, TArgument2, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source, this.Argument1, this.Argument2);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class VoidMethodCallWith0ArgumentsUnitTest<TObject> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith0ArgumentsUnitTest(string name, TObject source, string methodName, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        public class VoidMethodCallWith1ArgumentsUnitTest<TObject, TArgument> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith1ArgumentsUnitTest(string name, TObject source, string methodName, TArgument argument, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument = argument;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source, this.Argument);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        public class VoidMethodCallWith2ArgumentsUnitTest<TObject, TArgument1, TArgument2> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith2ArgumentsUnitTest(string name, TObject source, string methodName, TArgument1 argument1, TArgument2 argument2, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument1, TArgument2>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source, this.Argument1, this.Argument2);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        public class StaticMethodCallWith0ArgumentsUnitTest<TClass, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith0ArgumentsUnitTest(string name, string methodName, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda();
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class StaticMethodCallWith1ArgumentsUnitTest<TClass, TArgument, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith1ArgumentsUnitTest(string name, string methodName, TArgument argument, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument = argument;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TArgument, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda(this.Argument);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class StaticMethodCallWith2ArgumentsUnitTest<TClass, TArgument1, TArgument2, TResult> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith2ArgumentsUnitTest(string name, string methodName, TArgument1 argument1, TArgument2 argument2, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TArgument1, TArgument2, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda(this.Argument1, this.Argument2);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.ShouldBeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        public class StaticVoidMethodCallWith0ArgumentsUnitTest<TClass> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith0ArgumentsUnitTest(string name, string methodName, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda();
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        public class StaticVoidMethodCallWith1ArgumentsUnitTest<TClass, TArgument> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith1ArgumentsUnitTest(string name, string methodName, TArgument argument, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument = argument;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass, TArgument>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda(this.Argument);
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        public class StaticVoidMethodCallWith2ArgumentsUnitTest<TClass, TArgument1, TArgument2> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith2ArgumentsUnitTest(string name, string methodName, TArgument1 argument1, TArgument2 argument2, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass, TArgument1, TArgument2>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda(this.Argument1, this.Argument2);
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        public class CastUnitTest<TFrom, TTo> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public CastUnitTest(string name, TFrom source, bool expectedResult, TTo expectedValue = default(TTo))
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TFrom).Name);
                this.WriteLine("Expected Result = {0}", this.ExpectedResult);
                this.WriteLine("Expected Value  = {0} ({1})", this.ExpectedValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Act()
            {
                var castLambdaExpression = ExpressionBuilder.Cast<TFrom, TTo>();
                this.WriteLine("Lambda          = {0}", castLambdaExpression);

                var castLambda = castLambdaExpression.Compile();

                try
                {
                    this.ActualValue = castLambda(this.Source);
                    this.ActualResult = true;
                }
                catch (Exception)
                {
                    this.ActualResult = false;
                }

                this.WriteLine("Actual Result   = {0}", this.ActualResult);
                this.WriteLine("Actual Value    = {0} ({1})", this.ActualValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
                if (this.ActualResult)
                {
                    this.ActualValue.ShouldBeEquivalentTo(this.ExpectedValue);
                }
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private bool ActualResult { get; set; }
            private TTo ActualValue { get; set; }
            #endregion

            #region User Supplied Properties
            private TFrom Source { get; set; }
            private bool ExpectedResult { get; set; }
            private TTo ExpectedValue { get; set; }
            #endregion
        }

        public class CastAsUnitTest<TFrom, TTo> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public CastAsUnitTest(string name, TFrom source, bool expectedResult, TTo expectedValue = default(TTo))
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TFrom).Name);
                this.WriteLine("Expected Result = {0}", this.ExpectedResult);
                this.WriteLine("Expected Value  = {0} ({1})", this.ExpectedValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Act()
            {
                var castAsLambdaExpression = ExpressionBuilder.CastAs<TFrom, TTo>();
                this.WriteLine("Lambda          = {0}", castAsLambdaExpression);

                var castAsLambda = castAsLambdaExpression.Compile();

                this.ActualValue = castAsLambda(this.Source);
                this.ActualResult = Object.Equals(null, this.Source)
                    ? Object.Equals(this.ActualValue, null)
                    : !Object.Equals(this.ActualValue, null);

                this.WriteLine("Actual Result   = {0}", this.ActualResult);
                this.WriteLine("Actual Value    = {0} ({1})", this.ActualValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
                if (this.ActualResult)
                {
                    this.ActualValue.ShouldBeEquivalentTo(this.ExpectedValue);
                }
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private bool ActualResult { get; set; }
            private TTo ActualValue { get; set; }
            #endregion

            #region User Supplied Properties
            private TFrom Source { get; set; }
            private bool ExpectedResult { get; set; }
            private TTo ExpectedValue { get; set; }
            #endregion
        }

        public class DefaultUnitTest<TObject> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public DefaultUnitTest(string name, TObject expected)
                : base(name)
            {
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var defaultLambdaExpression = ExpressionBuilder.Default<TObject>();
                this.WriteLine("Lambda   = {0}", defaultLambdaExpression);

                var defaultLambda = defaultLambdaExpression.Compile();
                this.Actual = defaultLambda();
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TObject Expected { get; set; }
            #endregion
        }

        public class NewWith0ArgumentsUnitTest<TObject> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith0ArgumentsUnitTest(string name, TObject expected)
                : base(name)
            {
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith0ArgumentsLambdaExpression = ExpressionBuilder.New<TObject>();
                this.WriteLine("Lambda   = {0}", newWith0ArgumentsLambdaExpression);

                var newWith0ArgumentsLambda = newWith0ArgumentsLambdaExpression.Compile();
                this.Actual = newWith0ArgumentsLambda();
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TObject Expected { get; set; }
            #endregion
        }

        public class NewWith1ArgumentsUnitTest<TArgument, TObject> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith1ArgumentsUnitTest(string name, TArgument argument, TObject expected)
                : base(name)
            {
                this.Argument = argument;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith1ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument, TObject>();
                this.WriteLine("Lambda   = {0}", newWith1ArgumentsLambdaExpression);

                var newWith1ArgumentsLambda = newWith1ArgumentsLambdaExpression.Compile();
                this.Actual = newWith1ArgumentsLambda(this.Argument);
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument Argument { get; set; }
            TObject Expected { get; set; }
            #endregion
        }

        public class NewWith2ArgumentsUnitTest<TArgument1, TArgument2, TObject> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith2ArgumentsUnitTest(string name, TArgument1 argument1, TArgument2 argument2, TObject expected)
                : base(name)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument 1 = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2 = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected   = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith2ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TObject>();
                this.WriteLine("Lambda     = {0}", newWith2ArgumentsLambdaExpression);

                var newWith2ArgumentsLambda = newWith2ArgumentsLambdaExpression.Compile();
                this.Actual = newWith2ArgumentsLambda(this.Argument1, this.Argument2);
                this.WriteLine("Actual     = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument1 Argument1 { get; set; }
            TArgument2 Argument2 { get; set; }
            TObject Expected { get; set; }
            #endregion
        }

        public class PropertyGetterUnitTest<TObject, TProperty> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public PropertyGetterUnitTest(string name, TObject source, string propertyName, TProperty expected)
                : base(name)
            {
                this.Source = source;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source        = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Act()
            {
                var propertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertyGetterLambdaExpression);

                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                this.Actual = propertyGetterLambda(this.Source);
                this.WriteLine("Actual        = {0} ({1})", this.Actual.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Assert()
            {
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        public class PropertySetterUnitTest<TObject, TProperty> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public PropertySetterUnitTest(string name, TObject source, string propertyName, TProperty expected)
                : base(name)
            {
                this.Source = source;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source        = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Act()
            {
                var propertySetterLambdaExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertySetterLambdaExpression);

                var propertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                var actualBefore = propertyGetterLambda(this.Source);
                this.WriteLine("Actual Before = {0} ({1})", actualBefore.SafeToString(), typeof(TProperty).Name);
                this.ActualBefore = actualBefore;

                var propertySetterLambda = propertySetterLambdaExpression.Compile();
                propertySetterLambda(this.Source, this.Expected);

                var actualAfter = propertyGetterLambda(this.Source);
                this.WriteLine("Actual After  = {0} ({1})", actualAfter.SafeToString(), typeof(TProperty).Name);
                this.ActualAfter = actualAfter;
            }

            protected override void Assert()
            {
                this.ActualBefore.Should().NotBe(this.ActualAfter);
                this.ActualAfter.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty ActualBefore { get; set; }
            private TProperty ActualAfter { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        public class StaticPropertyGetterUnitTest<TClass, TProperty> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticPropertyGetterUnitTest(string name, Action staticInitializer, string propertyName, TProperty expected)
                : base(name)
            {
                this.StaticInitializer = staticInitializer;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);

                this.StaticInitializer();
            }

            protected override void Act()
            {
                var staticPropertyGetterLambdaExpression = ExpressionBuilder.StaticPropertyGetter<TClass, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", staticPropertyGetterLambdaExpression);

                var staticPropertyGetterLambda = staticPropertyGetterLambdaExpression.Compile();
                this.Actual = staticPropertyGetterLambda();
                this.WriteLine("Actual        = {0} ({1})", this.Actual.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Assert()
            {
                this.Actual.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private Action StaticInitializer { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        public class StaticPropertySetterUnitTest<TClass, TProperty> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticPropertySetterUnitTest(string name, Action staticInitializer, string propertyName, TProperty expected)
                : base(name)
            {
                this.StaticInitializer = staticInitializer;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);

                this.StaticInitializer();
            }

            protected override void Act()
            {
                var propertySetterLambdaExpression = ExpressionBuilder.StaticPropertySetter<TClass, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertySetterLambdaExpression);

                var propertyGetterLambdaExpression = ExpressionBuilder.StaticPropertyGetter<TClass, TProperty>(this.PropertyName);
                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                var actualBefore = propertyGetterLambda();
                this.WriteLine("Actual Before = {0} ({1})", actualBefore.SafeToString(), typeof(TProperty).Name);
                this.ActualBefore = actualBefore;

                var propertySetterLambda = propertySetterLambdaExpression.Compile();
                propertySetterLambda(this.Expected);

                var actualAfter = propertyGetterLambda();
                this.WriteLine("Actual After  = {0} ({1})", actualAfter.SafeToString(), typeof(TProperty).Name);
                this.ActualAfter = actualAfter;
            }

            protected override void Assert()
            {
                this.ActualBefore.Should().NotBe(this.ActualAfter);
                this.ActualAfter.ShouldBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty ActualBefore { get; set; }
            private TProperty ActualAfter { get; set; }
            #endregion

            #region User Supplied Properties
            private Action StaticInitializer { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }
        #endregion
    }
}
