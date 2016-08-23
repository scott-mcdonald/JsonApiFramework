// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only link node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyLink : Node<DomNodeType>, IDomLink
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Link; } }

        public override string Name
        { get { return "ReadOnlyLink ({0})".FormatWith(this.Rel); } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomLink Implementation
        public string Rel
        { get; private set; }

        public Link Link
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyLink Create(string rel, Link link)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(link != null);

            var domReadOnlyLink = new DomReadOnlyLink(rel, link);
            return domReadOnlyLink;
        }

        public static DomReadOnlyLink Create(string rel, IGetLinks getLinks)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(getLinks != null && getLinks.Links != null);

            var link = getLinks.GetLink(rel);

            var domReadOnlyLink = new DomReadOnlyLink(rel, link);
            return domReadOnlyLink;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyLink(string rel, Link link)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(link != null);

            this.Rel = rel;
            this.Link = link;
        }
        #endregion
    }
}
