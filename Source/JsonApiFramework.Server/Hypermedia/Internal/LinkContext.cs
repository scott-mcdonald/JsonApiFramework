// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class LinkContext : ILinkContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinkContext(string rel, Meta meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Rel = rel;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ILinkContext Implementation
        public string Rel { get; private set; }
        public Meta Meta { get; private set; }
        #endregion
    }
}
