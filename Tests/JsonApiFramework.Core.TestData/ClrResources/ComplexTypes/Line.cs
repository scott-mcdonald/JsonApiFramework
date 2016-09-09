// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.TestData.ClrResources.ComplexTypes
{
    public class Line
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public CustomData CustomData { get; set; }
    }
}