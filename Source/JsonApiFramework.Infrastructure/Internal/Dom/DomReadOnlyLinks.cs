// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only links node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyLinks : Node<DomNodeType>, IDomLinks
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Links; } }

        public override string Name
        {
            get
            {
                if (this.Links == null || this.Links.Any() == false)
                {
                    return "ReadOnlyLinks";
                }

                var rels = this.Links
                               .Select(x => x.Key)
                               .Aggregate((current, next) => "{0} {1}".FormatWith(current, next));

                return "ReadOnlyLinks [{0}]".FormatWith(rels);
            }
        }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomLinks Implementation
        public Links Links
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyLinks Create(Links links)
        {
            Contract.Requires(links != null);

            var domReadOnlyLinks = new DomReadOnlyLinks(links);
            return domReadOnlyLinks;
        }

        public static DomReadOnlyLinks Create(IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null && getLinks.Links != null);

            var links = getLinks.Links;

            var domReadOnlyLinks = new DomReadOnlyLinks(links);
            return domReadOnlyLinks;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyLinks(Links links)
        {
            Contract.Requires(links != null);

            this.Links = links;
        }
        #endregion
    }
}
