// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Infrastructure;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.Tests.Infrastructure
{
    public class ClockTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClockTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(TestClockData))]
        public void TestClock(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly DateTimeOffset MockUtcDateTime = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);

        private static readonly TimeZoneInfo MockLocalTimeZone = TimeZoneInfo.Local;
        private static readonly DateTimeOffset MockLocalDateTime = TimeZoneInfo.ConvertTime(MockUtcDateTime, MockLocalTimeZone);

        public static readonly IEnumerable<object[]> TestClockData = new[]
            {
                new object[] { new ClockUnitTest("With SystemClock", () => new SystemClock(), DateTimeOffset.UtcNow, DateTimeOffset.Now, TimeZoneInfo.Local) },
                new object[] { new ClockUnitTest("With MockClock", () => new MockClock { LocalTimeZone = MockLocalTimeZone, CurrentUtcDateTime = MockUtcDateTime }, MockUtcDateTime, MockLocalDateTime, MockLocalTimeZone) },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class MockClock : IClock
        {
            #region Properties
            public TimeZoneInfo LocalTimeZone { get; set; }
            public DateTimeOffset CurrentUtcDateTime { get; set; }
            #endregion

            #region IClock Implementation
            public DateTimeOffset GetCurrentLocalDateTime()
            {
                var currentLocalDateTime = TimeZoneInfo.ConvertTime(this.CurrentUtcDateTime, this.LocalTimeZone);
                return currentLocalDateTime;
            }

            public DateTimeOffset GetCurrentUtcDateTime()
            { return this.CurrentUtcDateTime; }

            public TimeZoneInfo GetLocalTimeZone()
            { return this.LocalTimeZone; }
            #endregion
        }

        public class ClockUnitTest : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public ClockUnitTest(string name, Func<IClock> clockFactory, DateTimeOffset expectedUtcDateTime, DateTimeOffset expectedLocalDateTime, TimeZoneInfo expectedLocalTimeZone)
                : base(name)
            {
                this.ClockFactory = clockFactory;
                this.ExpectedUtcDateTime = expectedUtcDateTime;
                this.ExpectedLocalDateTime = expectedLocalDateTime;
                this.ExpectedLocalTimeZone = expectedLocalTimeZone;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                var clock = this.ClockFactory();
                this.Clock = clock;

                var expectedUtcDateTime = this.ExpectedUtcDateTime;
                var expectedLocalDateTime = this.ExpectedLocalDateTime;
                var expectedLocalTimeZone = this.ExpectedLocalTimeZone;

                var expectedUtcDate = expectedUtcDateTime.Date;
                var expectedUtcTimeOfDay = expectedUtcDateTime.TimeOfDay;
                var expectedLocalDate = expectedLocalDateTime.Date;
                var expectedLocalTimeOfDay = expectedLocalDateTime.TimeOfDay;

                this.WriteLine();

                this.WriteLine("Expected UTC   Date      = {0}", expectedUtcDate);
                this.WriteLine("Expected UTC   Date/Time = {0}", expectedUtcDateTime);
                this.WriteLine("Expected UTC   TimeOfDay = {0}", expectedUtcTimeOfDay);
                this.WriteLine("Expected Local Date      = {0}", expectedLocalDate);
                this.WriteLine("Expected Local Date/Time = {0}", expectedLocalDateTime);
                this.WriteLine("Expected Local TimeOfDay = {0}", expectedLocalTimeOfDay);
                this.WriteLine("Expected Local Time Zone = {0}", expectedLocalTimeZone);

                this.WriteLine();
            }

            protected override void Act()
            {
                var clock = this.Clock;

                var actualCurrentUtcDateTime = clock.GetCurrentUtcDateTime();
                var actualCurrentLocalDateTime = clock.GetCurrentLocalDateTime();
                var actualLocalTimeZone = clock.GetLocalTimeZone();

                this.ActualUtcDateTime = actualCurrentUtcDateTime;
                this.ActualLocalDateTime = actualCurrentLocalDateTime;
                this.ActualLocalTimeZone = actualLocalTimeZone;
            }

            protected override void Assert()
            {
                var actualUtcDateTime = this.ActualUtcDateTime;
                var actualLocalDateTime = this.ActualLocalDateTime;
                var actualLocalTimeZone = this.ActualLocalTimeZone;

                var actualUtcDate = actualUtcDateTime.Date;
                var actualUtcTimeOfDay = actualUtcDateTime.TimeOfDay;
                var actualLocalDate = actualLocalDateTime.Date;
                var actualLocalTimeOfDay = actualLocalDateTime.TimeOfDay;

                this.WriteLine();

                this.WriteLine("Actual UTC   Date      = {0}", actualUtcDate);
                this.WriteLine("Actual UTC   Date/Time = {0}", actualUtcDateTime);
                this.WriteLine("Actual UTC   TimeOfDay = {0}", actualUtcTimeOfDay);
                this.WriteLine("Actual Local Date      = {0}", actualLocalDate);
                this.WriteLine("Actual Local Date/Time = {0}", actualLocalDateTime);
                this.WriteLine("Actual Local TimeOfDay = {0}", actualLocalTimeOfDay);
                this.WriteLine("Actual Local Time Zone = {0}", actualLocalTimeZone);

                const int timePrecisionInMilliseconds = 60 * 1000;              // 60 seconds
                const int datePrecisionInMillicseonds = 28 * 60 * 60 * 1000;    // 2 days

                var expectedUtcDateTime = this.ExpectedUtcDateTime;
                var expectedLocalDateTime = this.ExpectedLocalDateTime;
                var expectedLocalTimeZone = this.ExpectedLocalTimeZone;

                var expectedUtcDate = expectedUtcDateTime.Date;
                var expectedUtcTimeOfDay = expectedUtcDateTime.TimeOfDay;
                var expectedLocalDate = expectedLocalDateTime.Date;
                var expectedLocalTimeOfDay = expectedLocalDateTime.TimeOfDay;

                actualLocalDate.Should().BeCloseTo(expectedLocalDate, datePrecisionInMillicseonds);
                actualLocalDateTime.Should().BeCloseTo(expectedLocalDateTime, timePrecisionInMilliseconds);
                actualLocalTimeOfDay.Should().BeCloseTo(expectedLocalTimeOfDay, timePrecisionInMilliseconds);

                actualUtcDate.Should().BeCloseTo(expectedUtcDate, datePrecisionInMillicseonds);
                actualUtcDateTime.Should().BeCloseTo(expectedUtcDateTime, timePrecisionInMilliseconds);
                actualUtcTimeOfDay.Should().BeCloseTo(expectedUtcTimeOfDay, timePrecisionInMilliseconds);

                actualLocalTimeZone.Should().Be(expectedLocalTimeZone);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private IClock Clock { get; set; }

            private DateTimeOffset ActualUtcDateTime { get; set; }
            private DateTimeOffset ActualLocalDateTime { get; set; }
            private TimeZoneInfo ActualLocalTimeZone { get; set; }
            #endregion

            #region User Supplied Properties
            private Func<IClock> ClockFactory { get; set; }

            private DateTimeOffset ExpectedUtcDateTime { get; set; }
            private DateTimeOffset ExpectedLocalDateTime { get; set; }
            private TimeZoneInfo ExpectedLocalTimeZone { get; set; }
            #endregion
        }
        #endregion
    }
}
