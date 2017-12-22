// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Dom
{
    /// <summary>
    /// Represents the json:api document type of DOM document node in a DOM tree.
    /// </summary>
    /// <remarks>
    /// The enumeration values represent the following:
    /// <list type="table">
    /// <listheader>
    ///     <term>Document Type</term>
    ///     <description>Description</description>
    /// </listheader>
    /// 
    /// <item>
    ///     <term>Document</term>
    ///     <description>
    ///     Document that does not contain primary data or errors.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>DataDocument</term>
    ///     <description>
    ///     Document that contains primary data as a single resource or resource identifier object.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>DataCollectionDocument</term>
    ///     <description>
    ///     Document that contains primary data as a collection of resource or resource identifier objects.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ErrorsDocument</term>
    ///     <description>
    ///     Document that contains no primary data but instead contains a collection of error objects.
    ///     </description>
    /// </item>
    /// 
    /// </list>>
    /// </remarks>
    public enum ApiDocumentType
    {
        Document,
        DataDocument,
        DataCollectionDocument,
        ErrorsDocument
    }
}