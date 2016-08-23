// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel;

using Xunit;

namespace JsonApiFramework.TestAsserts.ServiceModel
{
    public static class LinkInfoAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ILinkInfo expected, ILinkInfo actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.Rel, actual.Rel);
        }
        #endregion
    }
}