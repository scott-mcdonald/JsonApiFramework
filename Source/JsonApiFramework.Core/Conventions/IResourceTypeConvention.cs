// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Conventions
{
    public interface IResourceTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IResourceTypeBuilder Apply(IResourceTypeBuilder resourceTypeConfiguration);
        #endregion
    }
}