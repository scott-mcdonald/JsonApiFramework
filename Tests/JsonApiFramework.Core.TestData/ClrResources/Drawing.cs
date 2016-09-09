// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources
{
    public class Drawing : JsonObject, IResource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Line> Lines { get; set; }
        public List<Polygon> Polygons { get; set; }
        public CustomData CustomData { get; set; }
    }
}