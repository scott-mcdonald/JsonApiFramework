// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi.Dom
{
    public class DomReadOnlyMeta : DomNode
        , IDomMeta
    {
        // PUBLIC CONSTRUCTOR ///////////////////////////////////////////////
        #region Constructor
        public DomReadOnlyMeta(Meta meta)
            : base(DomNodeType.Meta, "Meta")
        {
            Contract.Requires(meta != null);

            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomMeta Implementation
        public Meta Meta { get; }
        #endregion
    }
}
