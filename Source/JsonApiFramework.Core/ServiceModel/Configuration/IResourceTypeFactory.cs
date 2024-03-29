// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Conventions;

namespace JsonApiFramework.ServiceModel.Configuration;

public interface IResourceTypeFactory
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IResourceType Create(IConventions conventions);
    #endregion
}