// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel;

using Xunit;

namespace JsonApiFramework.TestAsserts.ServiceModel
{
    public static class ServiceModelAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IServiceModel expected, IServiceModel actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            ComplexTypeAssert.Equal(expected.ComplexTypes, actual.ComplexTypes);
            ResourceTypeAssert.Equal(expected.ResourceTypes, actual.ResourceTypes);
        }
        #endregion
    }
}