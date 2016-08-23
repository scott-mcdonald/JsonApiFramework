// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only meta node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyMeta : Node<DomNodeType>, IDomMeta
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Meta; } }

        public override string Name
        { get { return "Meta"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomMeta Implementation
        public Meta Meta
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyMeta Create(Meta meta)
        {
            Contract.Requires(meta != null);

            var domReadOnlyMeta = new DomReadOnlyMeta(meta);
            return domReadOnlyMeta;
        }

        public static DomReadOnlyMeta Create(IGetMeta getMeta)
        {
            Contract.Requires(getMeta.HasMeta());

            var meta = getMeta.Meta;

            var domReadOnlyMeta = new DomReadOnlyMeta(meta);
            return domReadOnlyMeta;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.Meta = meta;
        }
        #endregion
    }
}
