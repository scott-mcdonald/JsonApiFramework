// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a fluent-style builder of complex types.
    /// </summary>
    /// <typeparam name="TComplex">The type of complex object to build metadata about.</typeparam>
    public interface IComplexTypeBuilder<TComplex> : ITypeBaseBuilder<TComplex>
    { }
}