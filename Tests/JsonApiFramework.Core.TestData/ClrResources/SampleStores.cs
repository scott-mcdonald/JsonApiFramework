// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleStores
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Store Store = new Store
            {
                StoreId = 50,
                StoreName = "Store 50",
                Address = "1234 Main Street",
                City = "Boynton Beach",
                State = "FL",
                ZipCode = "33472"
            };

        public static readonly Store Store50 = new Store
            {
                StoreId = 50,
                StoreName = "Store 50",
                Address = "1234 Main Street",
                City = "Boynton Beach",
                State = "FL",
                ZipCode = "33472"
            };

        public static readonly Store Store51 = new Store
            {
                StoreId = 51,
                StoreName = "Store 51",
                Address = "5678 Secondary Avenue",
                City = "Minneapolis",
                State = "MN",
                ZipCode = "55455"
            };
        #endregion
    }
}