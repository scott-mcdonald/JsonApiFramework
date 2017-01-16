// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>Extension methods for any object that implements the <c>IDomNode</c> interface.</summary>
    public static class DomNodeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static bool IsArray(this IDomNode domNode)
        {
            Contract.Requires(domNode != null);

            return domNode.Type == DomNodeType.Array;
        }

        public static bool IsItem(this IDomNode domNode)
        {
            Contract.Requires(domNode != null);

            return domNode.Type == DomNodeType.Item;
        }

        public static bool IsObject(this IDomNode domNode)
        {
            Contract.Requires(domNode != null);

            return domNode.Type == DomNodeType.Object;
        }

        public static bool IsProperty(this IDomNode domNode)
        {
            Contract.Requires(domNode != null);

            return domNode.Type == DomNodeType.Property;
        }

        public static bool IsValue(this IDomNode domNode)
        {
            Contract.Requires(domNode != null);

            return domNode.Type == DomNodeType.Value;
        }
        #endregion
    }
}