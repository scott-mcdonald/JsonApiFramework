// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Tree;

namespace JsonApiFramework.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node in the DOM tree.
    /// </summary>
    public interface IDomNode : INode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the DOM node type of this DOM node.</summary>
        DomNodeType Type { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the root DOM document node of this DOM node.</summary>
        IDomDocument DomDocument();

        /// <summary>Gets the direct child DOM node collection contained by this DOM node, in document order.</summary>
        IEnumerable<IDomNode> DomNodes();

        /// <summary>Gets this DOM node and the direct child DOM node collection contained by this DOM node, in document order.</summary>
        IEnumerable<IDomNode> DomNodesIncludeSelf();

        /// <summary>Gets the descendant child DOM node collection contained by this DOM node, in document order.</summary>
        IEnumerable<IDomNode> DescendantDomNodes();

        /// <summary>Gets this DOM node and the descendant child DOM node collection contained by this DOM node, in document order.</summary>
        IEnumerable<IDomNode> DescendantDomNodesIncludeSelf();
        #endregion
    }
}
