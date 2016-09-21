// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework
{
    /// <summary>
    /// Abstracts a clock for getting the current local or UTC datetime with
    /// local time zone.
    /// </summary>
    public interface IClock
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        DateTimeOffset GetCurrentLocalDateTime();
        DateTimeOffset GetCurrentUtcDateTime();

        TimeZoneInfo GetLocalTimeZone();
        #endregion
    }
}
