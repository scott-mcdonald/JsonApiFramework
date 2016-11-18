// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Dom
{
    public abstract class DomNode : Node
        , IGetIsReadOnly
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetIsReadOnly Implementation
        public virtual bool IsReadOnly => false;
        #endregion

        #region Properties
        public DomNodeType Type { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Query Methods
        /// <summary>
        /// Returns a collection of the direct child DOM nodes for this DOM
        /// node, in document order.
        /// </summary>
        public IEnumerable<DomNode> DomNodes()
        { return this.Nodes().Cast<DomNode>(); }

        /// <summary>
        /// Returns a collection of the descendant DOM nodes for this DOM
        /// node, in document order.
        /// </summary>
        public IEnumerable<DomNode> DescendantDomNodes()
        { return this.DescendantNodes().Cast<DomNode>(); }
        #endregion

        // PROTECTED CONSTRUCTOR ////////////////////////////////////////////
        #region Constructor
        protected DomNode(DomNodeType type, string name)
            : base(name)
        {
            this.Type = type;
        }
        #endregion
    }
}
