// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework
{
    /// <summary>
    /// Abstracts the concept CLR class representing an individual CLR
    /// resource. This is a marker interface and has no methods.
    /// </summary>
    /// <remarks>
    /// Rationale for requiring this interface is to "mark" resource classes
    /// for compile-time generic constraints purposes. This aids in
    /// compile-time checks and compile-time generic inferences in the
    /// progressive fluent style builder interfaces/classes of the framework.
    /// 
    /// Because this interface is used to identify a set of types at compile
    /// time, it is safe to suppress warning CA1040 per the MSDN documentation.
    /// </remarks>
    public interface IResource
    { }
}
