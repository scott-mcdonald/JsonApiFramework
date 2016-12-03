// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Internal
{
    internal abstract class DomNode : Node
        , IDomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomNode Implementation
        public virtual bool IsReadOnly => false;

        public DomNodeType Type { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomNode Implementation
        public IEnumerable<IDomNode> DomNodes()
        { return this.Nodes().Cast<IDomNode>(); }

        public IEnumerable<IDomNode> DescendantDomNodes()
        { return this.DescendantNodes().Cast<IDomNode>(); }
        #endregion

        #region AddOrUpdate Methods
        public void AddOrUpdateDomNode(DomNodeType type, DomNode newDomNode)
        {
            Contract.Requires(newDomNode != null);

            var oldDomNode = this.GetDomNode(type);
            if (oldDomNode == null)
            {
                this.AddNode(newDomNode);
                return;
            }

            this.ReplaceNode(oldDomNode, newDomNode);
        }
        #endregion

        #region Get Methods
        public DomDocument GetDomDocument()
        {
            var rootNode = this.RootNode;

            var domDocument = (DomDocument)rootNode;
            return domDocument;
        }

        public DomNode GetDomNode(DomNodeType type)
        {
            return this.Nodes().Cast<DomNode>().SingleOrDefault(x => x.Type == type);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomNode(DomNodeType type, string name)
            : base(name)
        {
            this.Type = type;
        }

        protected DomNode(DomNodeType type, string name, IEnumerable<DomNode> domNodes)
            : base(name, domNodes)
        {
            this.Type = type;
        }
        #endregion
    }
}
