// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.Server.Hypermedia
{
    public interface IDocumentPathContext : IPathContext
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<IHypermediaPath> DocumentSelfPath { get; }
        string DocumentSelfQuery { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Get the document primary data CLR resource type.</summary>
        /// <returns>CLR resource type that the primary data is composed of.</returns>
        Type GetPrimaryClrResourceType();
        #endregion
    }
}
