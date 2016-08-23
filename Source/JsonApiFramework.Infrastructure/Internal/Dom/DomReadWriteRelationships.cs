// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write relationships node in the DOM tree.
    /// </summary>
    internal class DomReadWriteRelationships : NodesContainer<DomNodeType>, IDomRelationships
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Relationships; } }

        public override string Name
        { get { return "ReadWriteRelationships"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomRelationships Implementation
        public Relationships Relationships
        { get { return this.GetRelationships(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteRelationships Create(params Node<DomNodeType>[] domNodes)
        {
            var domReadWriteRelationships = new DomReadWriteRelationships(domNodes);
            return domReadWriteRelationships;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DomReadOnlyRelationship AddDomReadOnlyRelationship(string rel, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            this.ValidateRelationshipDoesNotExist(rel);

            var domReadOnlyRelationship = this.CreateAndAddNode(() => DomReadOnlyRelationship.Create(rel, relationship));
            return domReadOnlyRelationship;
        }

        internal DomReadWriteRelationship AddDomReadWriteRelationship(string rel, IEnumerable<string> linkRelCollection, Meta meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            this.ValidateRelationshipDoesNotExist(rel);

            var domReadWriteRelationship = this.CreateAndAddNode(() => DomReadWriteRelationship.Create(rel));

            foreach (var linkRel in linkRelCollection)
            {
                var domReadWriteLinks = domReadWriteRelationship.GetOrAddNode(DomNodeType.Links, () => DomReadWriteLinks.Create());
                domReadWriteLinks.AddDomReadWriteLink(linkRel);
            }

            if (meta == null)
            {
                return domReadWriteRelationship;
            }

            domReadWriteRelationship.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadWriteRelationship;
        }

        internal DomReadWriteRelationship AddDomReadWriteRelationship(string rel, Meta meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ValidateRelationshipDoesNotExist(rel);

            var domReadWriteRelationship = this.CreateAndAddNode(() => DomReadWriteRelationship.Create(rel));

            if (meta == null)
            {
                return domReadWriteRelationship;
            }

            domReadWriteRelationship.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadWriteRelationship;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteRelationships(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Relationships GetRelationships()
        {
            var relationships = new Relationships();

            var domRelationshipsNodes = this.Nodes();
            foreach (var domRelationshipsNode in domRelationshipsNodes)
            {
                var domRelationshipsNodeType = domRelationshipsNode.NodeType;
                switch (domRelationshipsNodeType)
                {
                    case DomNodeType.Relationship:
                        {
                            var domRelationship = (IDomRelationship)domRelationshipsNode;
                            var rel = domRelationship.Rel;
                            var relationship = domRelationship.Relationship;
                            relationships.Add(rel, relationship);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domRelationshipsNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return relationships;
        }

        private void ValidateRelationshipDoesNotExist(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            // Validate a DOM relationship node for the respective relation
            // has not already been added to this DOM relationships node.
            var containsRelationshipNode = this.Nodes()
                                               .Cast<IDomRelationship>()
                                               .Any(x => x.Rel == rel);
            if (!containsRelationshipNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsRelBasedChildNode
                                                   .FormatWith(DomNodeType.Relationships, rel, DomNodeType.Relationship);
            throw new DomException(detail);
        }
        #endregion
    }
}
