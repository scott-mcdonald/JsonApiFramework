// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Tree;
using JsonApiFramework.Tree.Internal;

namespace JsonApiFramework.JsonApi2.Dom.Internal
{
    /// <summary>Enhances the DOM tree string for user readability.</summary>
    internal class DomTreeStringNodeVisitor : TreeStringNodeVisitor
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region TreeStringNodeVisitor Overrides
        protected override bool ShouldIndent(Node node)
        {
            var domNode = (DomNode)node;

            var isNotDomValue = !domNode.IsValue();
            return isNotDomValue;
        }

        protected override bool ShouldAppendLine(Node node)
        {
            var domNode = (DomNode)node;

            var isNotDomProperty = !domNode.IsProperty();
            var isNotDomItem = !domNode.IsItem();
            if (isNotDomProperty && isNotDomItem)
                return true;

            IDomNode domValue;

            var domNodeType = domNode.Type;
            switch (domNodeType)
            {
                case DomNodeType.Item:
                    {
                        var domItem = (DomItem)domNode;
                        domValue = domItem.DomItemValue();
                    }
                    break;

                case DomNodeType.Property:
                    {
                        var domProperty = (DomProperty)domNode;
                        domValue = domProperty.DomPropertyValue();
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (domValue == null)
                return true;

            var isNotDomValue = !domValue.IsValue();
            return isNotDomValue;
        }
        #endregion
    }
}
