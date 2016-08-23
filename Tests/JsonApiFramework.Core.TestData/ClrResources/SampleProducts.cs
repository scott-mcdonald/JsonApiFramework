// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleProducts
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Product Product = new Product
            {
                ProductId = 501,
                Name = "Widget A",
                UnitPrice = 25.0m
            };

        public static readonly Product Product501 = new Product
            {
                ProductId = 501,
                Name = "Widget A",
                UnitPrice = 25.0m
            };

        public static readonly Product Product502 = new Product
            {
                ProductId = 502,
                Name = "Widget B",
                UnitPrice = 50.0m
            };
        #endregion
    }
}