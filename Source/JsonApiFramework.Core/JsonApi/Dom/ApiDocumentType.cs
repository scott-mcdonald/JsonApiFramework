// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Represents the document type for a json:api compliant document.
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
    ///     <term>EmptyDocument</term>
    ///     <description>
    ///     Document that contains primary data but is an empty data array. Only used when deserializing JSON into a DOM tree.
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
    /// <item>
    ///     <term>NullDocument</term>
    ///     <description>
    ///     Document that contains primary data but is a null object. Only used when deserializing JSON into a DOM tree.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceCollectionDocument</term>
    ///     <description>
    ///     Document that contains primary data as a collection of resource objects.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceDocument</term>
    ///     <description>
    ///     Document that contains primary data as a single resource object.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceIdentifierCollectionDocument</term>
    ///     <description>
    ///     Document that contains primary data as a collection of resource identifier objects.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceIdentifierDocument</term>
    ///     <description>
    ///     Document that contains primary data as a single resource identifier object.
    ///     </description>
    /// </item>
    /// </list>>
    /// </remarks>
    public enum ApiDocumentType
    {
        Document,
        EmptyDocument,
        ErrorsDocument,
        NullDocument,
        ResourceCollectionDocument,
        ResourceDocument,
        ResourceIdentifierCollectionDocument,
        ResourceIdentifierDocument
    };
}