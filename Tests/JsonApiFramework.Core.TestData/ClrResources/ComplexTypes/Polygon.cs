// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.TestData.ClrResources.ComplexTypes
{
    public class Polygon
    {
        public List<Point> Points { get; set; }

        public CustomData CustomData { get; set; }
    }
}