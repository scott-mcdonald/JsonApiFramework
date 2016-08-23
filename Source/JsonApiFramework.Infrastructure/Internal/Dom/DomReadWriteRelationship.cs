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
    /// Represents a read/write relationship node in the DOM tree.
    /// </summary>
    internal class DomReadWriteRelationship : NodesContainer<DomNodeType>, IDomRelationship
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Relationship; } }

        public override string Name
        { get { return "ReadWriteRelationship ({0})".FormatWith(this.Rel); } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomRelationship Implementation
        public string Rel
        { get; private set; }

        public Relationship Relationship
        { get { return this.GetRelationship(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteRelationship Create(string rel, params Node<DomNodeType>[] domNodes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var domReadWriteRelationship = new DomReadWriteRelationship(rel, domNodes);
            return domReadWriteRelationship;
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

        internal DomData SetDomData(ResourceIdentifier apiResourceIdentifier, Type clrResourceType)
        {
            Contract.Requires(apiResourceIdentifier != null);
            Contract.Requires(clrResourceType != null);

            // Validation
            this.ValidateDataDoesNotExist();

            // Add the one allowed DOM data node to the DOM document node.
            var domData = this.CreateAndAddNode(() => DomData.Create());

            // Add the one allowed DOM resource identifier node to the DOM document node.
            var domReadOnlyResourceIdentifier = domData.CreateAndAddNode(() => DomReadOnlyResourceIdentifier.Create(apiResourceIdentifier, clrResourceType));
            return domData;
        }

        internal DomData SetDomDataNull()
        {
            // Validation
            this.ValidateDataDoesNotExist();

            // Add the one allowed DOM data node to the DOM document node.
            var domData = this.CreateAndAddNode(() => DomData.Create());
            return domData;
        }

        internal DomDataCollection SetDomDataCollection(IEnumerable<ResourceIdentifier> apiResourceIdentifierCollection, Type clrResourceType)
        {
            Contract.Requires(apiResourceIdentifierCollection != null);
            Contract.Requires(clrResourceType != null);

            // Validation
            this.ValidateDataDoesNotExist();

            // Add the one allowed DOM data collection node to the DOM document node.
            var domDataCollection = this.CreateAndAddNode(() => DomDataCollection.Create());

            // Add the many allowed DOM resource identifier nodes to the DOM document node.
            var domReadOnlyResourceIdentifierCollection = 
                apiResourceIdentifierCollection.Select(x => domDataCollection.CreateAndAddNode(() => DomReadOnlyResourceIdentifier.Create(x, clrResourceType)))
                                               .ToList();
            return domDataCollection;
        }

        internal DomDataCollection SetDomDataCollectionEmpty()
        {
            // Validation
            this.ValidateDataDoesNotExist();

            // Add the one allowed DOM data node to the DOM document node.
            var domDataCollection = this.CreateAndAddNode(() => DomDataCollection.Create());
            return domDataCollection;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteRelationship(string rel, params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Rel = rel;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Relationship GetRelationship()
        {
            var links = default(Links);

            var toOneResourceLinkage = default(ResourceIdentifier);
            var toOneResourceLinkagePresent = default(bool);

            var toManyResourceLinkage = default(List<ResourceIdentifier>);
            var toManyResourceLinkagePresent = default(bool);

            var meta = default(Meta);

            var domRelationshipNodes = this.Nodes();
            foreach (var domRelationshipNode in domRelationshipNodes)
            {
                var domRelationshipNodeType = domRelationshipNode.NodeType;
                switch (domRelationshipNodeType)
                {
                    case DomNodeType.Links:
                        {
                            var domLinks = (IDomLinks)domRelationshipNode;
                            links = domLinks.Links;
                        }
                        break;

                    case DomNodeType.Data:
                        {
                            toOneResourceLinkagePresent = true;
                            var domDataNode = (DomData)domRelationshipNode;
                            var domResourceIdentifierNode = domDataNode.Node;
                            if (domResourceIdentifierNode != null)
                            {
                                var apiResourceIdentifier = GetApiResourceIdentifier(domResourceIdentifierNode);
                                toOneResourceLinkage = apiResourceIdentifier;
                            }
                        }
                        break;

                    case DomNodeType.DataCollection:
                        {
                            toManyResourceLinkagePresent = true;
                            var domDataCollectionNode = (DomDataCollection)domRelationshipNode;
                            var domResourceIdentifierNodes = domDataCollectionNode.Nodes();
                            toManyResourceLinkage = domResourceIdentifierNodes.Select(GetApiResourceIdentifier)
                                                                              .ToList();
                        }
                        break;

                    case DomNodeType.Meta:
                        {
                            var domMeta = (IDomMeta)domRelationshipNode;
                            meta = domMeta.Meta;
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domRelationshipNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            if (toOneResourceLinkagePresent && toManyResourceLinkagePresent)
            {
                var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasIncompatibleChildNodes
                                                       .FormatWith(this.NodeType, DomNodeType.Data, DomNodeType.DataCollection);
                throw new DomException(detail);
            }

            // Create the concrete relationship object and initialize the data property if needed.
            Relationship relationship;
            if (toOneResourceLinkagePresent)
            {
                relationship = new ToOneRelationship
                    {
                        Data = toOneResourceLinkage
                    };
            }
            else if (toManyResourceLinkagePresent)
            {
                relationship = new ToManyRelationship
                    {
                        Data = toManyResourceLinkage
                    };
            }
            else
            {
                relationship = new Relationship();
            }

            // Initialize remaining common relationship properties.
            relationship.Links = links;
            relationship.Meta = meta;

            return relationship;
        }

        private static ResourceIdentifier GetApiResourceIdentifier(Node<DomNodeType> domResourceIdentifierNode)
        {
            Contract.Requires(domResourceIdentifierNode != null);

            var domResourceIdentifierNodeType = domResourceIdentifierNode.NodeType;
            if (domResourceIdentifierNodeType != DomNodeType.ResourceIdentifier)
            {
                var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                       .FormatWith(DomNodeType.ResourceIdentifier, domResourceIdentifierNodeType);
                throw new DomException(detail);
            }

            var domResourceIdentifier = (IDomResourceIdentifier)domResourceIdentifierNode;
            var apiResourceIdentifier = domResourceIdentifier.ApiResourceIdentifier;
            return apiResourceIdentifier;
        }

        private void ValidateDataDoesNotExist()
        {
            // Validate a DOM data or data collection node has not already been added to the DOM relationship node.
            var containsDataNode = this.ContainsNode(DomNodeType.Data);
            var containsDataCollectionNode = this.ContainsNode(DomNodeType.DataCollection);
            if (!containsDataNode && !containsDataCollectionNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Data);
            throw new DomException(detail);
        }

        private void ValidateMetaDoesNotExist()
        {
            // Validate a DOM meta node has not already been added to the DOM relationship node.
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
