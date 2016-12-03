// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Internal
{
    internal abstract class DomReadOnlyNode : DomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override bool IsReadOnly => true;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Add/Remove/Replace Attribute Overrides
        public override void AddAttribute(NodeAttribute newNodeAttribute)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToAddAttribute
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void AddAttributes(IEnumerable<NodeAttribute> newNodeAttributeCollection)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToAddAttributes
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void RemoveAttribute(NodeAttribute oldNodeAttribute)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToRemoveAttribute
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void ReplaceAttribute(NodeAttribute oldNodeAttribute, NodeAttribute newNodeAttribute)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToReplaceAttribute
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }
        #endregion

        #region Add/Remove/Replace Node Overrides
        public override void AddNode(Node newNode)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToAddNode
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void AddNodes(IEnumerable<Node> newNodeCollection)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToAddNodes
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void RemoveNode(Node oldNode)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToRemoveNode
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }

        public override void ReplaceNode(Node oldNode, Node newNode)
        {
            var detail = CoreErrorStrings.DomExceptionDetailReadOnlyNodeUnableToReplaceNode
                                         .FormatWith(this.Type);
            throw new DomException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomReadOnlyNode(DomNodeType type, string name)
            : base(type, name)
        { }
        #endregion
    }
}
