// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Http
{
    /// <summary>
    /// Abstracts the configuration needed by the <see cref="UrlBuilder"/>
    /// progressive fluent builder of HTTP URL strings.
    /// </summary>
    public interface IUrlBuilderConfiguration
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Scheme { get; }
        string Host { get; }
        int? Port { get; }
        IEnumerable<string> RootPathSegments { get; }
        #endregion
    }
}
