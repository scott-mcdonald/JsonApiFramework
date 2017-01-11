// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// An enumeration that represents the json:api data type of a DOM "data" property in a DOM tree.
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
    ///     <term>Unknown</term>
    ///     <description>
    ///     The data property value is neither a resource or a resource identifier.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>Resource</term>
    ///     <description>
    ///     The data property value is a resource.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceIdentifier</term>
    ///     <description>
    ///     The data property value is a resource identifier.
    ///     </description>
    /// </item>
    /// 
    /// </list>>
    /// </remarks>
    public enum ApiDataType
    {
        Unknown,
        Resource,
        ResourceIdentifier
    };
}