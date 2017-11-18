// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for all the 'attributes/properties' as a whole on a json:api/CLR complex/resource type.</summary>
    public interface IAttributesInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the collection of all individual 'attribute/property' metadata for the json:api/CLR complex/resource type.</summary>
        IReadOnlyCollection<IAttributeInfo> AttributeInfoCollection { get; }
        #endregion
    }
}