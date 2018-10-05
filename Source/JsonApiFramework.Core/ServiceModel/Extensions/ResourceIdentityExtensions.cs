// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.ServiceModel
{
    public static class ResourceIdentityExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static bool IsSingleton(this IResourceIdentityInfo resourceIdentityInfo)
        {
            Contract.Requires(resourceIdentityInfo != null);

            return resourceIdentityInfo.Id == null;
        }
        #endregion
    }
}
