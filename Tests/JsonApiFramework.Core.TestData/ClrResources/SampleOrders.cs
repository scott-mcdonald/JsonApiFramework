// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleOrders
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Order Order = new Order
            {
                OrderId = 1,
                TotalPrice = 100.0m
            };

        public static readonly Order Order1 = new Order
            {
                OrderId = 1,
                TotalPrice = 100.0m
            };

        public static readonly Order Order2 = new Order
            {
                OrderId = 2,
                TotalPrice = 200.0m
            };

        public static readonly Order Order3 = new Order
            {
                OrderId = 3,
                TotalPrice = 300.0m
            };

        public static readonly Order Order4 = new Order
            {
                OrderId = 4,
                TotalPrice = 400.0m
            };
        #endregion
    }
}