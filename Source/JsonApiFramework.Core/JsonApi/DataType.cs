// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the data type for a json:api compliant data property (object or array).
    /// </summary>
    /// <remarks>
    /// The enumeration values represent the following:
    /// <list type="table">
    /// <listheader>
    ///     <term>Data Type</term>
    ///     <description>Description</description>
    /// </listheader>
    /// 
    /// <item>
    ///     <term>None</term>
    ///     <description>
    ///     The data property value is neither a resource nor a resource identifier.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>Resource</term>
    ///     <description>
    ///     The data property value of the object or array is a resource.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceIdentifier</term>
    ///     <description>
    ///     The data property value of the object or array is a resource identifier.
    ///     </description>
    /// </item>
    /// 
    /// </list>>
    /// </remarks>
    public enum DataType
    {
        None,
        Resource,
        ResourceIdentifier
    };
}