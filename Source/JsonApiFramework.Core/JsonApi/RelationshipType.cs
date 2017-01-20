// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// An enumeration that represents the json:api relationshp type of a DOM "relationship" property in a DOM tree.
    /// </summary>
    /// <remarks>
    /// The enumeration values represent the following:
    /// <list type="table">
    /// <listheader>
    ///     <term>Relationship Type</term>
    ///     <description>Description</description>
    /// </listheader>
    /// 
    /// <item>
    ///     <term>Relationship</term>
    ///     <description>
    ///     The relationship object has no data property, therefore has no resource linkage.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ToOneRelationship</term>
    ///     <description>
    ///     The relationship object has a data property, the data property represents to-one resource linkage.
    ///     </description>
    /// </item>
    /// 
    /// <item>
    ///     <term>ResourceIdentifier</term>
    ///     <description>
    ///     The relationship object has a data property, the data property represents to-many resource linkage.
    ///     </description>
    /// </item>
    /// 
    /// </list>>
    /// </remarks>
    public enum RelationshipType
    {
        Relationship,
        ToOneRelationship,
        ToManyRelationship
    };
}