// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomTypeAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expected, DomType actual)
        {
            if (String.IsNullOrWhiteSpace(expected))
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualApiType = actual.ApiType;
            Assert.Equal(expected, actualApiType);
        }
        #endregion
    }
}