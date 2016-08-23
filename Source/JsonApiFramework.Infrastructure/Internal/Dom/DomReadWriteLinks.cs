// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write links node in the DOM tree.
    /// </summary>
    internal class DomReadWriteLinks : NodesContainer<DomNodeType>, IDomLinks
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Links; } }

        public override string Name
        { get { return "ReadWriteLinks"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomLinks Implementation
        public Links Links
        { get { return this.GetLinks(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteLinks Create(params Node<DomNodeType>[] domNodes)
        {
            var domReadWriteLinks = new DomReadWriteLinks(domNodes);
            return domReadWriteLinks;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DomReadOnlyLink AddDomReadOnlyLink(string rel, Link link)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(link != null);

            this.ValidateLinkDoesNotExist(rel);

            var domReadOnlyLink = this.CreateAndAddNode(() => DomReadOnlyLink.Create(rel, link));
            return domReadOnlyLink;
        }

        internal DomReadWriteLink AddDomReadWriteLink(string rel, Meta meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ValidateLinkDoesNotExist(rel);

            var domReadWriteLink = this.CreateAndAddNode(() => DomReadWriteLink.Create(rel));
            if (meta == null)
            {
                return domReadWriteLink;
            }

            domReadWriteLink.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadWriteLink;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteLinks(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Links GetLinks()
        {
            var links = new Links();

            var domLinksNodes = this.Nodes();
            foreach (var domLinksNode in domLinksNodes)
            {
                var domLinksNodeType = domLinksNode.NodeType;
                switch (domLinksNodeType)
                {
                    case DomNodeType.Link:
                        {
                            var domLink = (IDomLink)domLinksNode;
                            var rel = domLink.Rel;
                            var link = domLink.Link;
                            links.Add(rel, link);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domLinksNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return links;
        }

        private void ValidateLinkDoesNotExist(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            // Validate a DOM link node for the respective relation has not
            // already been added to this DOM links node.
            var containsLinkNode = this.Nodes()
                                       .Cast<IDomLink>()
                                       .Any(x => x.Rel == rel);
            if (!containsLinkNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsRelBasedChildNode
                                                   .FormatWith(DomNodeType.Links, rel, DomNodeType.Link);
            throw new DomException(detail);
        }
        #endregion
    }
}
