// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    internal interface IDomResource : IDomResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Resource ApiResource { get; }
        // ReSharper disable ReturnTypeCanBeEnumerable.Global
        Links ApiResourceLinks { get; }
        Relationships ApiResourceRelationships { get; }
        // ReSharper restore ReturnTypeCanBeEnumerable.Global

        object ClrResource { get; }
        #endregion
    }
}