// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleOrderItems
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly OrderItem OrderItem = new OrderItem
            {
                OrderItemId = 1001,
                ProductName = "Widget A",
                Quantity = 2,
                UnitPrice = 25.0m
            };

        public static readonly OrderItem OrderItem1001 = new OrderItem
            {
                OrderItemId = 1001,
                ProductName = "Widget A",
                Quantity = 2,
                UnitPrice = 25.0m
            };

        public static readonly OrderItem OrderItem1002 = new OrderItem
            {
                OrderItemId = 1002,
                ProductName = "Widget B",
                Quantity = 1,
                UnitPrice = 50.0m
            };
        #endregion
    }
}