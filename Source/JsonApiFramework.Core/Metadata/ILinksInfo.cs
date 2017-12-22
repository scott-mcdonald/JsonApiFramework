// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for all the 'links' as a whole on a json:api/CLR resource type.</summary>
    public interface ILinksInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the optional CLR property binding of a <see cref="JsonApiFramework.JsonApi.Links"/> property for the CLR resource type.</summary>
        IClrPropertyBinding ClrLinksPropertyBinding { get; }
        #endregion
    }
}