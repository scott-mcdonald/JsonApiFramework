// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.Metadata
{
    /// <summary>
    /// Represents the metadata for a complex (non-primitive) object produced or consumed by a hypermedia API.
    /// </summary>
    /// <remarks>
    /// Complex objects can be used as attributes in resources or other complex objects as part of a whole/part design.
    /// </remarks>
    [JsonConverter(typeof(ComplexTypeConverter))]
    public interface IComplexType : ITypeBase
    { }
}