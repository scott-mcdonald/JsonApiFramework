// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentAssertions;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
#pragma warning disable 649
    public class StaticReflectionTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public StaticReflectionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetMemberNameStaticVersionWithReturnValueTestData))]
        public void TestStaticReflectionGetMemberNameStaticVersionWithReturnValue(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GetMemberNameStaticVersionWithNoReturnValueTestData))]
        public void TestStaticReflectionGetMemberNameStaticVersionWithNoReturnValue(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GetMemberNameInstanceVersionWithReturnValueTestData))]
        public void TestStaticReflectionGetMemberNameInstanceVersionWithReturnValue(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GetMemberNameInstanceVersionWithNoReturnValueTestData))]
        public void TestStaticReflectionGetMemberNameInstanceVersionWithNoReturnValue(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetMemberNameStaticVersionWithReturnValueTestData = new[]
            {
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithField", x => x.Field, "Field") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithProperty", x => x.Property, "Property") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithMethodAnd0Argument(s)", x => x.Method(), "Method") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithMethodAnd1Argument(s)", x => x.Method(42), "Method") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithMethodAnd2Argument(s)", x => x.Method(42, 24), "Method") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd0Argument(s)", x => Widget.StaticMethod(), "StaticMethod") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd1Argument(s)", x => Widget.StaticMethod(42), "StaticMethod") },
                new object[] { new GetMemberNameStaticVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd2Argument(s)", x => Widget.StaticMethod(42, 24), "StaticMethod") },
            };

        public static readonly IEnumerable<object[]> GetMemberNameStaticVersionWithNoReturnValueTestData = new[]
            {
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd0Argument(s)", x => x.VoidMethod(), "VoidMethod") },
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd1Argument(s)", x => x.VoidMethod(42), "VoidMethod") },
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd2Argument(s)", x => x.VoidMethod(42, 24), "VoidMethod") },
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd0Argument(s)", x => Widget.StaticVoidMethod(), "StaticVoidMethod") },
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd1Argument(s)", x => Widget.StaticVoidMethod(42), "StaticVoidMethod") },
                new object[] { new GetMemberNameStaticVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd2Argument(s)", x => Widget.StaticVoidMethod(42, 24), "StaticVoidMethod") },
            };

        public static readonly IEnumerable<object[]> GetMemberNameInstanceVersionWithReturnValueTestData = new[]
            {
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithField", new Widget(), x => x.Field, "Field") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithProperty", new Widget(), x => x.Property, "Property") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithMethodAnd0Argument(s)", new Widget(), x => x.Method(), "Method") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithMethodAnd1Argument(s)", new Widget(), x => x.Method(42), "Method") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithMethodAnd2Argument(s)", new Widget(), x => x.Method(42, 24), "Method") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd0Argument(s)", new Widget(), x => Widget.StaticMethod(), "StaticMethod") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd1Argument(s)", new Widget(), x => Widget.StaticMethod(42), "StaticMethod") },
                new object[] { new GetMemberNameInstanceVersionWithReturnValueUnitTest<Widget>("WithStaticMethodAnd2Argument(s)", new Widget(), x => Widget.StaticMethod(42, 24), "StaticMethod") },
            };

        public static readonly IEnumerable<object[]> GetMemberNameInstanceVersionWithNoReturnValueTestData = new[]
            {
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd0Argument(s)", new Widget(), x => x.VoidMethod(), "VoidMethod") },
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd1Argument(s)", new Widget(), x => x.VoidMethod(42), "VoidMethod") },
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithVoidMethodAnd2Argument(s)", new Widget(), x => x.VoidMethod(42, 24), "VoidMethod") },
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd0Argument(s)", new Widget(), x => Widget.StaticVoidMethod(), "StaticVoidMethod") },
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd1Argument(s)", new Widget(), x => Widget.StaticVoidMethod(42), "StaticVoidMethod") },
                new object[] { new GetMemberNameInstanceVersionWithNoReturnValueUnitTest<Widget>("WithStaticVoidMethodAnd2Argument(s)", new Widget(), x => Widget.StaticVoidMethod(42, 24), "StaticVoidMethod") },
            };
        #endregion

        #region Test Types
        public class GetMemberNameStaticVersionWithReturnValueUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetMemberNameStaticVersionWithReturnValueUnitTest(string name, Expression<Func<T, object>> expression, string expected)
                : base(name)
            {
                this.Expression = expression;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expression = {0}", this.Expression.SafeToString());
                this.WriteLine("Expected   = {0}", this.Expected);
            }

            protected override void Act()
            {
                this.Actual = StaticReflection.GetMemberName(this.Expression);
                this.WriteLine("Actual     = {0}", this.Actual);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private Expression<Func<T, object>> Expression { get; set; }
            private string Expected { get; set; }
            #endregion
        }

        public class GetMemberNameStaticVersionWithNoReturnValueUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetMemberNameStaticVersionWithNoReturnValueUnitTest(string name, Expression<Action<T>> expression, string expected)
                : base(name)
            {
                this.Expression = expression;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expression = {0}", this.Expression.SafeToString());
                this.WriteLine("Expected   = {0}", this.Expected);
            }

            protected override void Act()
            {
                this.Actual = StaticReflection.GetMemberName(this.Expression);
                this.WriteLine("Actual     = {0}", this.Actual);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private Expression<Action<T>> Expression { get; set; }
            private string Expected { get; set; }
            #endregion
        }

        public class GetMemberNameInstanceVersionWithReturnValueUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetMemberNameInstanceVersionWithReturnValueUnitTest(string name, T instance, Expression<Func<T, object>> expression, string expected)
                : base(name)
            {
                this.Instance = instance;
                this.Expression = expression;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expression = {0}", this.Expression.SafeToString());
                this.WriteLine("Expected   = {0}", this.Expected);
            }

            protected override void Act()
            {
                this.Actual = this.Instance.GetMemberName(this.Expression);
                this.WriteLine("Actual     = {0}", this.Actual);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private T Instance { get; set; }
            private Expression<Func<T, object>> Expression { get; set; }
            private string Expected { get; set; }
            #endregion
        }

        public class GetMemberNameInstanceVersionWithNoReturnValueUnitTest<T> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetMemberNameInstanceVersionWithNoReturnValueUnitTest(string name, T instance, Expression<Action<T>> expression, string expected)
                : base(name)
            {
                this.Instance = instance;
                this.Expression = expression;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expression = {0}", this.Expression.SafeToString());
                this.WriteLine("Expected   = {0}", this.Expected);
            }

            protected override void Act()
            {
                this.Actual = this.Instance.GetMemberName(this.Expression);
                this.WriteLine("Actual     = {0}", this.Actual);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private T Instance { get; set; }
            private Expression<Action<T>> Expression { get; set; }
            private string Expected { get; set; }
            #endregion
        }

        private class Widget
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Property { get; set; }

            public string Method() { return String.Empty; }
            public string Method(int a) { return String.Empty; }
            public string Method(int a, int b) { return String.Empty; }

            public static string StaticMethod() { return String.Empty; }
            public static string StaticMethod(int a) { return String.Empty; }
            public static string StaticMethod(int a, int b) { return String.Empty; }

            public void VoidMethod() { }
            public void VoidMethod(int a) { }
            public void VoidMethod(int a, int b) { }

            public static void StaticVoidMethod() { }
            public static void StaticVoidMethod(int a) { }
            public static void StaticVoidMethod(int a, int b) { }

            // ReSharper disable once UnusedField.Compiler
            // ReSharper disable once UnassignedField.Compiler
            public string Field;
        }
        #endregion
    }
    #pragma warning restore 649
}
