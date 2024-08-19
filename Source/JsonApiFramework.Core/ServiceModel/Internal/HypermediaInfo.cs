// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

namespace JsonApiFramework.ServiceModel.Internal;

internal class HypermediaInfo : JsonObject
    , IHypermediaInfo
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public HypermediaInfo(string apiCollectionPathSegment)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(apiCollectionPathSegment) == false);

        this.ApiCollectionPathSegment = apiCollectionPathSegment;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region IHypermediaInfo Implementation
    public string ApiCollectionPathSegment { get; internal set; }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal HypermediaInfo()
    { }
    #endregion
}