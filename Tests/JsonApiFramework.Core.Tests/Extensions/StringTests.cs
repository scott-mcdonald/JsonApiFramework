// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;

using FluentAssertions;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests
{
    public class StringTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public StringTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ContainsTestData")]
        public void TestStringContains(string name, string str, string value, StringComparison stringComparison, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Source           = {0}", str);
            this.WriteLine("Value            = {0}", value);
            this.WriteLine("StringComparison = {0}", stringComparison);
            this.WriteLine("Expected         = {0}", expected);

            // Act
            var actual = str.Contains(value, stringComparison);
            this.WriteLine("Actual           = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData("FormatWithTestData")]
        public void TestStringFormatWith(string name, string str, object[] args, string expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Source       = {0}", str ?? "null");
            if (args != null)
            {
                for (var i = 0; i < args.Length; ++i)
                {
                    this.WriteLine("Argument [{0}] = {1}", i, args[i]);
                }
            }
            this.WriteLine("Expected     = {0}", expected);

            // Act
            if (str == null || args == null)
            {
                Action action = () => str.FormatWith(args);
                action.ShouldThrow<ArgumentException>();
                return;
            }
            var actual = str.FormatWith(args);
            this.WriteLine("Actual       = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData("FormatWithTestData")]
        public void TestStringFormatWithWithProvider(string name, string str, object[] args, string expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Source       = {0}", str ?? "null");
            if (args != null)
            {
                for (var i = 0; i < args.Length; ++i)
                {
                    this.WriteLine("Argument [{0}] = {1}", i, args[i]);
                }
            }
            this.WriteLine("Expected     = {0}", expected);

            // Act
            if (str == null || args == null)
            {
                Action action = () => str.FormatWith(args);
                action.ShouldThrow<ArgumentException>();
                return;
            }
            var actual = str.FormatWith(CultureInfo.InvariantCulture, args);
            this.WriteLine("Actual       = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData("ParseEnumTestData")]
        public void TestStringParseEnum(string name, string str, bool throwsArgumentException, bool ignoreCase, StopLightColors expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Source                  = {0}", str ?? "null");
            this.WriteLine("ThrowsArgumentException = {0}", throwsArgumentException);
            this.WriteLine("IgnoreCase              = {0}", ignoreCase);
            this.WriteLine("Expected                = {0}", expected);

            // Act
            if (throwsArgumentException)
            {
                Action action = () => str.ParseEnum<StopLightColors>(ignoreCase);
                action.ShouldThrow<ArgumentException>();
                return;
            }
            var actual = str.ParseEnum<StopLightColors>(ignoreCase);
            this.WriteLine("Actual                  = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData("TryParseEnumTestData")]
        public void TestStringTryParseEnum(string name, string str, bool ignoreCase, bool expectedResult, StopLightColors expectedEnum)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Source          = {0}", str ?? "null");
            this.WriteLine("IgnoreCase      = {0}", ignoreCase);
            this.WriteLine("ExpectedResult  = {0}", expectedResult);
            this.WriteLine("ExpectedEnum    = {0}", expectedEnum);

            // Act
            StopLightColors actualEnum;
            var actualResult = str.TryParseEnum(out actualEnum, ignoreCase);
            this.WriteLine("ActualResult    = {0}", actualResult);
            this.WriteLine("ActualEnum      = {0}", actualEnum);

            // Assert
            actualResult.Should().Be(expectedResult);
            actualEnum.Should().Be(expectedEnum);
        }
        #endregion

        // PUBLIC TYPES /////////////////////////////////////////////////////
        #region Test Types
        public enum StopLightColors
        {
            // ReSharper disable UnusedMember.Local
            Unspecified,
            Red,
            Green,
            Yellow
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        const string ContainsTestString = "The quick brown fox jumps over the lazy dog";

        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> ContainsTestData = new[]
            {
                new object[] {"WithNullValue", ContainsTestString, null, StringComparison.OrdinalIgnoreCase, false},
                new object[] {"WithEmptyValue", ContainsTestString, String.Empty, StringComparison.OrdinalIgnoreCase, false},
                new object[] {"WithValidSubString", ContainsTestString, "quick", StringComparison.Ordinal, true},
                new object[] {"WithValidSubString", ContainsTestString, "QUICK", StringComparison.Ordinal, false},
                new object[] {"WithValidSubStringIgnoreCase", ContainsTestString, "quick", StringComparison.OrdinalIgnoreCase, true},
                new object[] {"WithValidSubStringIgnoreCase", ContainsTestString, "QUICK", StringComparison.OrdinalIgnoreCase, true},
                new object[] {"WithInvalidSubString", ContainsTestString, "foo", StringComparison.Ordinal, false},
                new object[] {"WithInvalidSubString", ContainsTestString, "FOO", StringComparison.Ordinal, false},
                new object[] {"WithInvalidSubStringIgnoreCase", ContainsTestString, "foo", StringComparison.OrdinalIgnoreCase, false},
                new object[] {"WithInvalidSubStringIgnoreCase", ContainsTestString, "FOO", StringComparison.OrdinalIgnoreCase, false}
            };

        public static readonly IEnumerable<object[]> FormatWithTestData = new[]
            {
                new object[] {"WithNullString", null, null, null},
                new object[] {"WithEmptyString", String.Empty, new object[0], String.Empty},
                new object[] {"WithValidStringAndNullArguments", "Test", null, null},
                new object[] {"WithValidStringAndZeroArguments", "Test", new object[0], "Test"},
                new object[] {"WithValidStringAndOneArgument", "Test {0}", new object[] {42}, "Test 42"},
                new object[] {"WithValidStringAndManyArguments", "Test {0} {1}", new object[] {42, 24}, "Test 42 24"}
            };

        public static readonly IEnumerable<object[]> ParseEnumTestData = new[]
            {
                new object[] {"WithNullString", null, true, false, StopLightColors.Green},
                new object[] {"WithEmptyString", String.Empty, true, false, StopLightColors.Green},

                new object[] {"WithValidEnumString", "Green", false, false, StopLightColors.Green},
                new object[] {"WithValidEnumString", "GREEN", true, false, StopLightColors.Green},
                new object[] {"WithValidEnumStringIgnoreCase", "Green", false, true, StopLightColors.Green},
                new object[] {"WithValidEnumStringIgnoreCase", "GREEN", false, true, StopLightColors.Green},

                new object[] {"WithInvalidEnumString", "Purple", true, false, StopLightColors.Green},
                new object[] {"WithInvalidEnumString", "PURPLE", true, false, StopLightColors.Green},
                new object[] {"WithInvalidEnumStringIgnoreCase", "Purple", true, true, StopLightColors.Green},
                new object[] {"WithInvalidEnumStringIgnoreCase", "PURPLE", true, true, StopLightColors.Green}
            };

        public static readonly IEnumerable<object[]> TryParseEnumTestData = new[]
            {
                new object[] {"WithNullString", null, false, false, StopLightColors.Unspecified},
                new object[] {"WithEmptyString", String.Empty, false, false, StopLightColors.Unspecified},

                new object[] {"WithValidEnumString", "Green", false, true, StopLightColors.Green},
                new object[] {"WithValidEnumString", "GREEN", false, false, StopLightColors.Unspecified},
                new object[] {"WithValidEnumStringIgnoreCase", "Green", true, true, StopLightColors.Green},
                new object[] {"WithValidEnumStringIgnoreCase", "GREEN", true, true, StopLightColors.Green},

                new object[] {"WithInvalidEnumString", "Purple", false, false, StopLightColors.Unspecified},
                new object[] {"WithInvalidEnumString", "PURPLE", false, false, StopLightColors.Unspecified},
                new object[] {"WithInvalidEnumStringIgnoreCase", "Purple", true, false, StopLightColors.Unspecified},
                new object[] {"WithInvalidEnumStringIgnoreCase", "PURPLE", true, false, StopLightColors.Unspecified}
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
