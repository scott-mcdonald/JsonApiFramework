// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    internal interface IDomJsonApiVersion : IGetIsReadOnly
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        JsonApiVersion JsonApiVersion { get; }
        #endregion
    }
}