// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class DomReadOnlyMeta : DomNode
        , IDomMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyMeta(Meta meta)
            : base(DomNodeType.Meta, "ReadOnly Meta")
        {
            Contract.Requires(meta != null);

            this.Meta = meta;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IGetMeta Implementation
        public Meta GetMeta()
        { return this.Meta; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Meta Meta { get; }
        #endregion
    }
}
