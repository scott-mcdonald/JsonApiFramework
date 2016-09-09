// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources
{
    public static class ClrResourceAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(object expected, object actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedType = expected.GetType();
            var actualType = actual.GetType();
            Assert.Equal(expectedType, actualType);

            if (expectedType == typeof(Article))
                ArticleAssert.Equal((Article)expected, (Article)actual);
            else if (expectedType == typeof(Blog))
                BlogAssert.Equal((Blog)expected, (Blog)actual);
            else if (expectedType == typeof(Comment))
                CommentAssert.Equal((Comment)expected, (Comment)actual);
            else if (expectedType == typeof(Order))
                OrderAssert.Equal((Order)expected, (Order)actual);
            else if (expectedType == typeof(OrderItem))
                OrderItemAssert.Equal((OrderItem)expected, (OrderItem)actual);
            else if (expectedType == typeof(Payment))
                PaymentAssert.Equal((Payment)expected, (Payment)actual);
            else if (expectedType == typeof(Person))
                PersonAssert.Equal((Person)expected, (Person)actual);
            else if (expectedType == typeof(PosSystem))
                PosSystemAssert.Equal((PosSystem)expected, (PosSystem)actual);
            else if (expectedType == typeof(Product))
                ProductAssert.Equal((Product)expected, (Product)actual);
            else if (expectedType == typeof(Store))
                StoreAssert.Equal((Store)expected, (Store)actual);
            else if (expectedType == typeof(StoreConfiguration))
                StoreConfigurationAssert.Equal((StoreConfiguration)expected, (StoreConfiguration)actual);
            else if (expectedType == typeof(Drawing))
                DrawingAssert.Equal((Drawing)expected, (Drawing)actual);
            else
                Assert.True(false, "Unknown resource [type={0}]".FormatWith(expectedType.Name));
        }

        public static void Equal(IEnumerable<object> expected, IEnumerable<object> actual)
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

                ClrResourceAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}