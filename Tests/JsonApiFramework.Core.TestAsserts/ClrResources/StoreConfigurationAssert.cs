// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources
{
    public static class StoreConfigurationAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(MailingAddress expected, MailingAddress actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.City, actual.City);
            Assert.Equal(expected.State, actual.State);
            Assert.Equal(expected.ZipCode, actual.ZipCode);
        }

        public static void Equal(PhoneNumber expected, PhoneNumber actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.AreaCode, actual.AreaCode);
            Assert.Equal(expected.Number, actual.Number);
        }

        public static void Equal(IEnumerable<PhoneNumber> expected, IEnumerable<PhoneNumber> actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToList();
            var actualCollection = actual.SafeToList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedItem = expectedCollection[index];
                var actualItem = actualCollection[index];

                StoreConfigurationAssert.Equal(expectedItem, actualItem);
            }
        }

        public static void Equal(StoreConfiguration expected, StoreConfiguration actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.StoreConfigurationId, actual.StoreConfigurationId);
            Assert.Equal(expected.IsLive, actual.IsLive);
            StoreConfigurationAssert.Equal(expected.MailingAddress, actual.MailingAddress);
            StoreConfigurationAssert.Equal(expected.PhoneNumbers, actual.PhoneNumbers);
        }

        public static void Equal(IEnumerable<StoreConfiguration> expected, IEnumerable<StoreConfiguration> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToList();
            var actualCollection = actual.SafeToList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedItem = expectedCollection[index];
                var actualItem = actualCollection[index];

                StoreConfigurationAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}