// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write link node in the DOM tree.
    /// </summary>
    internal class DomReadWriteLink : NodesContainer<DomNodeType>, IDomLink
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Link; } }

        public override string Name
        { get { return "ReadWriteLink ({0})".FormatWith(this.Rel); } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomLink Implementation
        public string Rel
        { get; private set; }

        public Link Link
        { get { return this.GetLink(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteLink Create(string rel, params Node<DomNodeType>[] domNodes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var domReadWriteLink = new DomReadWriteLink(rel, domNodes);
            return domReadWriteLink;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DomReadOnlyMeta SetDomReadOnlyMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            // Validation
            this.ValidateMetaDoesNotExist();

            // Add the one allowed DOM meta node to the DOM document node.
            var domReadOnlyMeta = this.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadOnlyMeta;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteLink(string rel, params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Rel = rel;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Link GetLink()
        {
            var hRef = default(string); // required per specification
            var meta = default(Meta);   // optional per specification

            var domLinkNodes = this.Nodes();
            foreach (var domLinkNode in domLinkNodes)
            {
                var domLinkNodeType = domLinkNode.NodeType;
                switch (domLinkNodeType)
                {
                    case DomNodeType.HRef:
                        {
                            var domHRef = (DomHRef)domLinkNode;
                            hRef = domHRef.HRef;
                        }
                        break;

                    case DomNodeType.Meta:
                        {
                            var domMeta = (IDomMeta)domLinkNode;
                            meta = domMeta.Meta;
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domLinkNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return new Link(hRef, meta);
        }

        private void ValidateMetaDoesNotExist()
        {
            // Validate a DOM meta node has not already been added to the DOM link node.
            var containsMetaNode = this.ContainsNode(DomNodeType.Meta);
            if (!containsMetaNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Meta);
            throw new DomException(detail);
        }
        #endregion
    }
}
