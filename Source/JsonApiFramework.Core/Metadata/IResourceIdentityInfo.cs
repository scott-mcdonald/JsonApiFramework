// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata of 'resource identity' for a json:api/CLR resource type.</summary>
    public interface IResourceIdentityInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api 'type' for the json:api resoure type.</summary>
        string ApiType { get; }

        /// <summary>Gets the CLR property binding of the 'id' property for the CLR resource type.</summary>
        IClrPropertyBinding ClrIdPropertyBinding { get; }
        #endregion
    }
}