// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.ServiceModel
{
    public interface IRelationshipsInfo : IPropertyInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<IRelationshipInfo> Collection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipInfo GetRelationship(string rel);
        bool TryGetRelationship(string rel, out IRelationshipInfo relationship);
        #endregion
    }
}