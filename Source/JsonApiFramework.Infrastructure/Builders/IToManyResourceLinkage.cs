// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework
{
    public interface IToManyResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        bool HasValueCollection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IEnumerable<ResourceIdentifier> CreateApiResourceIdentifierCollection(IResourceType resourceType);
        #endregion
    }

    public interface IToManyResourceLinkage<out TResourceId> : IToManyResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<TResourceId> ValueCollection { get; }
        #endregion
    }
}