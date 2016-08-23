// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Clock
{
    /// <summary>
    /// Implementation of the <c>IClock</c> interface that uses the standard
    /// <code>Now</code> properties of the .NET <c>DateTimeOffset</c> class.
    /// </summary>
    public class SystemClock : IClock
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public DateTimeOffset GetCurrentLocalDateTime() { return DateTimeOffset.Now; }
        public DateTimeOffset GetCurrentUtcDateTime() { return DateTimeOffset.UtcNow; }
        public TimeZoneInfo GetLocalTimeZone() { return TimeZoneInfo.Local; }
        #endregion
    }
}
