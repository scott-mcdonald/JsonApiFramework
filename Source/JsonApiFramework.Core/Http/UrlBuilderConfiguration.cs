// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Http;

/// <summary>
/// Implementation of the <see cref="IUrlBuilderConfiguration"/> that
/// stores the configuration information in a simple immutable POCO object.
/// </summary>
public class UrlBuilderConfiguration : IUrlBuilderConfiguration
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public UrlBuilderConfiguration()
    {
    }

    public UrlBuilderConfiguration(string scheme, string host, int? port = null, IEnumerable<string> rootPathSegments = null, bool throwExceptionOnLinkBuildError = false)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(scheme) == false);
        Contract.Requires(string.IsNullOrWhiteSpace(host) == false);

        this.Scheme = scheme;
        this.Host   = host;
        this.Port   = port;
        this.RootPathSegments = rootPathSegments.EmptyIfNull()
                                                .ToList();
        this.ThrowExceptionOnLinkBuildError = throwExceptionOnLinkBuildError;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region IUrlBuilderConfiguration Implementation
    public string              Scheme                         { get; set; }
    public string              Host                           { get; set; }
    public int?                Port                           { get; set; }
    public IEnumerable<string> RootPathSegments               { get; set; }
    public bool                ThrowExceptionOnLinkBuildError { get; set; }
    #endregion
}