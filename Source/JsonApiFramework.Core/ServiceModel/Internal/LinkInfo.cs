// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

namespace JsonApiFramework.ServiceModel.Internal;

internal class LinkInfo : JsonObject
    , ILinkInfo
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public LinkInfo(string rel)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(rel) == false);

        this.Rel = rel;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region ILinkInfo Implementation
    public string Rel { get; internal set; }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal LinkInfo()
    { }
    #endregion
}