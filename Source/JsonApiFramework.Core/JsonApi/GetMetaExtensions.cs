// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>IGetMeta</c> interface.</summary>
    public static class GetMetaExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static bool HasMeta(this IGetMeta getMeta)
        {
            Contract.Requires(getMeta != null);

            return getMeta.Meta != null;
        }
        #endregion
    }
}