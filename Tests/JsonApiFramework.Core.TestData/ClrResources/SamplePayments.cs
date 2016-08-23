// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SamplePayments
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Payment Payment = new Payment
            {
                PaymentId = 101,
                Amount = 100.0m
            };

        public static readonly Payment Payment101 = new Payment
            {
                PaymentId = 101,
                Amount = 75.0m
            };

        public static readonly Payment Payment102 = new Payment
            {
                PaymentId = 102,
                Amount = 25.0m
            };
        #endregion
    }
}