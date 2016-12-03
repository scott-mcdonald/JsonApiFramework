// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts queryable access (non-mutating) to a DOM node.
    /// </summary>
    public interface IDomNode : INode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets if this DomNode is read only or not.</summary>
        bool IsReadOnly { get; }

        /// <summary>Gets the DomNode type as an enumeration.</summary>
        DomNodeType Type { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Returns a collection of the direct child DOM nodes for this DOM
        /// node, in document order.
        /// </summary>
        IEnumerable<IDomNode> DomNodes();

        /// <summary>
        /// Returns a collection of the descendant DOM nodes for this DOM
        /// node, in document order.
        /// </summary>
        IEnumerable<IDomNode> DescendantDomNodes();
        #endregion
    }
}
