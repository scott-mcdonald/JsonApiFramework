// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>ISetMeta</c> interface.</summary>
    public static class SetMetaExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ISetMeta Extension Methods
        public static void SetMetadata<TMetadata>(this ISetMeta setMeta, TMetadata metadata)
        {
            Contract.Requires(setMeta != null);

            var meta = Meta.Create(metadata);
            setMeta.Meta = meta;
        }
        #endregion
    }
}