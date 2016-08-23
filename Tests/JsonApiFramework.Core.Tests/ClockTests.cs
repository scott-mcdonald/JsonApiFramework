// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

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
        [Fact]
        public void TestClockAsSystemClock()
        {
            // Arrange

            // Act
            var clock = new SystemClock();

            // Assert
            Assert.NotNull(clock);
            Assert.True(clock.GetCurrentLocalDateTime() >= DateTimeOffset.MinValue && clock.GetCurrentLocalDateTime() <= DateTimeOffset.MaxValue);
            Assert.True(clock.GetCurrentUtcDateTime() >= DateTimeOffset.MinValue && clock.GetCurrentUtcDateTime() <= DateTimeOffset.MaxValue);
            Assert.Equal(TimeZoneInfo.Local, clock.GetLocalTimeZone());
        }

        [Fact]
        public void TestClockStandardMethods()
        {
            // Arrange
            var localTimeZone = TimeZoneInfo.Local;
            var currentUtcDateTime = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
            var currentLocalDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, localTimeZone);

            // Act
            var clock = new TestClock
                {
                    LocalTimeZone = localTimeZone,
                    CurrentUtcDateTime = currentUtcDateTime
                };

            // Assert
            Assert.NotNull(clock);
            Assert.Equal(currentLocalDateTime, clock.GetCurrentLocalDateTime());
            Assert.Equal(currentUtcDateTime, clock.GetCurrentUtcDateTime());
            Assert.Equal(localTimeZone, clock.GetLocalTimeZone());
        }

        [Fact]
        public void TestClockExtensionMethods()
        {
            // Arrange
            var localTimeZone = TimeZoneInfo.Local;
            var currentUtcDateTime = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
            var currentLocalDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, localTimeZone);
            var clock = new TestClock
                {
                    LocalTimeZone = localTimeZone,
                    CurrentUtcDateTime = currentUtcDateTime
                };

            // Act
            var currentLocalDate = clock.GetCurrentLocalDate();
            var currentLocalTimeOfDay = clock.GetCurrentLocalTimeOfDay();

            var currentUtcDate = clock.GetCurrentUtcDate();
            var currentUtcTimeOfDay = clock.GetCurrentUtcTimeOfDay();

            // Assert
            Assert.NotNull(clock);
            Assert.Equal(currentLocalDateTime.Date, currentLocalDate);
            Assert.Equal(currentLocalDateTime.TimeOfDay, currentLocalTimeOfDay);
            Assert.Equal(currentUtcDateTime.Date, currentUtcDate);
            Assert.Equal(currentUtcDateTime.TimeOfDay, currentUtcTimeOfDay);
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class TestClock : IClock
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
