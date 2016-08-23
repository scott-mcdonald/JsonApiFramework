// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    internal interface IDomResourceIdentity : IGetIsReadOnly
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Meta ApiResourceMeta { get; }
        string ApiResourceType { get; }
        string ApiResourceId { get; }

        Type ClrResourceType { get; }
        #endregion
    }
}