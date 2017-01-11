// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class DomReadOnlyLinks : DomReadOnlyNode
        , IDomLinks
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyLinks(Links links)
            : base(DomNodeType.Links, "ReadOnlyLinks")
        {
            Contract.Requires(links != null);

            this.Links = links;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IGetLinks Implementation
        public Links GetLinks()
        { return this.Links; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Links Links { get; }
        #endregion
    }
}
