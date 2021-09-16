// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public interface IResourceIdentityInfoBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IResourceIdentityInfoBuilder SetApiIdConverter(Func<object, string> apiIdConverter);

        IResourceIdentityInfoBuilder SetApiType(string apiType);
        #endregion
    }
}