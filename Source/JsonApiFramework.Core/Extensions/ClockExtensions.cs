// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework
{
    public static class ClockExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        /// <summary>
        /// Gets the current local date only. Time portion will be midnight.
        /// </summary>
        /// <returns>
        /// DateTime object that is the current local date.
        /// </returns>
        public static DateTime GetCurrentLocalDate(this IClock clock)
        {
            Contract.Requires(clock != null);

            var currentLocalDateTime = clock.GetCurrentLocalDateTime();
            var currentLocalDate = currentLocalDateTime.Date;
            return currentLocalDate;
        }

        /// <summary>
        /// Gets the current local time of day only.
        /// </summary>
        /// <returns>
        /// TimeSpan object that is the current local time of day.
        /// </returns>
        public static TimeSpan GetCurrentLocalTimeOfDay(this IClock clock)
        {
            Contract.Requires(clock != null);

            var currentLocalDateTime = clock.GetCurrentLocalDateTime();
            var currentLocalTimeOfDay = currentLocalDateTime.TimeOfDay;
            return currentLocalTimeOfDay;
        }

        /// <summary>
        /// Gets the current UTC date only. Time portion will be midnight.
        /// </summary>
        /// <returns>
        /// DateTime object that is the current UTC date.
        /// </returns>
        public static DateTime GetCurrentUtcDate(this IClock clock)
        {
            Contract.Requires(clock != null);

            return clock.GetCurrentLocalDateTime().Date;
        }

        /// <summary>
        /// Gets the current UTC time of day only.
        /// </summary>
        /// <returns>
        /// TimeSpan object that is the current UTC time of day.
        /// </returns>
        public static TimeSpan GetCurrentUtcTimeOfDay(this IClock clock)
        {
            Contract.Requires(clock != null);

            var currentUtcDateTime = clock.GetCurrentUtcDateTime();
            var currentUtcTimeOfDay = currentUtcDateTime.TimeOfDay;
            return currentUtcTimeOfDay;
        }
        #endregion
    }
}
