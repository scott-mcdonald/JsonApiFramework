// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.TestData.ClrResources
{
    public static class SamplePosSystems
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly PosSystem PosSystem = new PosSystem
            {
                PosSystemId = "RadiantRest",
                PosSystemName = "Radiant REST-Based Api"
            };

        public static readonly PosSystem PosSystemRadiantRest = new PosSystem
            {
                PosSystemId = "RadiantRest",
                PosSystemName = "Radiant REST-Based Api"
            };

        public static readonly PosSystem PosSystemRadiantWc = new PosSystem
            {
                PosSystemId = "RadiantWcf",
                PosSystemName = "Radiant WCF-Based Api"
            };
        #endregion
    }
}