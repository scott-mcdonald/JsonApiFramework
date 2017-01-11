// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Tree;
using JsonApiFramework.Tree.Internal;

namespace JsonApiFramework.JsonApi2.Dom.Internal
{
    /// <summary>
    /// Augments the standard tree string node visitor by keeping the property
    /// node and property value node on the same line for better readability purposes.
    /// </summary>
    internal class DomTreeStringNodeVisitor : TreeStringNodeVisitor
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region TreeStringNodeVisitor Overrides
        protected override bool ShouldAppendLine(Node node)
        {
            var domNode = (DomNode)node;
            if (!domNode.IsProperty())
                return true;

            var domProperty = (DomProperty)domNode;
            var domPropertyValue = domProperty.DomPropertyValue();
            if (domPropertyValue == null)
                return true;

            return !domPropertyValue.IsValue();
        }

        protected override bool ShouldIdent(Node node)
        {
            var domNode = (DomNode)node;
            return !domNode.IsValue();
        }
        #endregion
    }
}
