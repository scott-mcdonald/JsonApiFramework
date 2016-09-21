// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Clock;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests
{
    public class ClockTests : XUnitTest
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
        [MemberData("TestClockData")]
        public void TestClock(string name, Func<IClock> clockFactory, TestClockExpectedData expectedData)
        {
            // Arrange
            var clock = clockFactory();

            var expectedLocalDateTime = expectedData.LocalDateTime;
            var expectedUtcDateTime = expectedData.UtcDateTime;
            var expectedLocalTimeZone = expectedData.LocalTimeZone;

            var expectedLocalDate = expectedLocalDateTime.Date;
            var expectedLocalTimeOfDay = expectedLocalDateTime.TimeOfDay;
            var expectedUtcDate = expectedUtcDateTime.Date;
            var expectedUtcTimeOfDay = expectedUtcDateTime.TimeOfDay;

            this.WriteLine(name);
            this.WriteLine();

            this.WriteLine("Expected Local Date      = {0}", expectedLocalDate);
            this.WriteLine("Expected Local Date/Time = {0}", expectedLocalDateTime);
            this.WriteLine("Expected Local TimeOfDay = {0}", expectedLocalTimeOfDay);
            this.WriteLine("Expected UTC   Date      = {0}", expectedUtcDate);
            this.WriteLine("Expected UTC   Date/Time = {0}", expectedUtcDateTime);
            this.WriteLine("Expected UTC   TimeOfDay = {0}", expectedUtcTimeOfDay);
            this.WriteLine("Expected Local Time Zone = {0}", expectedLocalTimeZone);

            this.WriteLine();

            // Act
            var actualCurrentLocalDateTime = clock.GetCurrentLocalDateTime();
            var actualCurrentUtcDateTime = clock.GetCurrentUtcDateTime();
            var actualLocalTimeZone = clock.GetLocalTimeZone();

            var actualCurrentLocalDate = clock.GetCurrentLocalDate();
            var actualCurrentLocalTimeOfDay = clock.GetCurrentLocalTimeOfDay();

            var actualCurrentUtcDate = clock.GetCurrentUtcDate();
            var actualCurrentUtcTimeOfDay = clock.GetCurrentUtcTimeOfDay();

            this.WriteLine("Actual Local Date      = {0}", actualCurrentLocalDate);
            this.WriteLine("Actual Local Date/Time = {0}", actualCurrentLocalDateTime);
            this.WriteLine("Actual Local TimeOfDay = {0}", actualCurrentLocalTimeOfDay);
            this.WriteLine("Actual UTC   Date      = {0}", actualCurrentUtcDate);
            this.WriteLine("Actual UTC   Date/Time = {0}", actualCurrentUtcDateTime);
            this.WriteLine("Actual UTC   TimeOfDay = {0}", actualCurrentUtcTimeOfDay);
            this.WriteLine("Actual Local Time Zone = {0}", actualLocalTimeZone);

            // Assert
            const int beCloseToPrecisionInMilliseconds = 10000; // 10 seconds

            actualCurrentLocalDate.Should().BeCloseTo(expectedLocalDate, beCloseToPrecisionInMilliseconds);
            actualCurrentLocalDateTime.Should().BeCloseTo(expectedLocalDateTime, beCloseToPrecisionInMilliseconds);
            actualCurrentLocalTimeOfDay.Should().BeCloseTo(expectedLocalTimeOfDay, beCloseToPrecisionInMilliseconds);

            actualCurrentUtcDate.Should().BeCloseTo(expectedUtcDate, beCloseToPrecisionInMilliseconds);
            actualCurrentUtcDateTime.Should().BeCloseTo(expectedUtcDateTime, beCloseToPrecisionInMilliseconds);
            actualCurrentUtcTimeOfDay.Should().BeCloseTo(expectedUtcTimeOfDay, beCloseToPrecisionInMilliseconds);

            actualLocalTimeZone.Should().Be(expectedLocalTimeZone);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public class TestClockExpectedData
        {
            public string Name { get; set; }
            public DateTimeOffset LocalDateTime { get; set; }
            public DateTimeOffset UtcDateTime { get; set; }
            public TimeZoneInfo LocalTimeZone { get; set; }

            public override string ToString() { return this.Name; }
        }

        private static readonly TimeZoneInfo MockTimeZone = TimeZoneInfo.Local;
        private static readonly DateTimeOffset MockUtcDateTime = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
        private static readonly DateTimeOffset MockLocalDateTime = TimeZoneInfo.ConvertTime(MockUtcDateTime, MockTimeZone);

        public static readonly IEnumerable<object[]> TestClockData = new[]
            {
                new object[]
                    {
                        "With SystemClock",
                        new Func<IClock>(() => new SystemClock()),
                        new TestClockExpectedData
                            {
                                Name = "Real Data",
                                LocalDateTime = DateTimeOffset.Now,
                                UtcDateTime = DateTimeOffset.UtcNow,
                                LocalTimeZone = TimeZoneInfo.Local
                            }
                    },

                new object[]
                    {
                        "With MockClock",
                        new Func<IClock>(() => new MockClock
                            {
                                LocalTimeZone = MockTimeZone,
                                CurrentUtcDateTime = MockUtcDateTime
                            }),
                        new TestClockExpectedData
                            {
                                Name = "Mock Data",
                                LocalDateTime = MockLocalDateTime,
                                UtcDateTime = MockUtcDateTime,
                                LocalTimeZone = MockTimeZone
                            }
                    },
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
        #endregion
    }
}
