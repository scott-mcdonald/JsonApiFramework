// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Abstracts a read/write resource identity type of node in the DOM tree.
    /// </summary>
    internal abstract class DomReadWriteResourceIdentity : NodesContainer<DomNodeType>, IDomResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomResourceIdentity Implementation
        public Meta ApiResourceMeta
        { get { return this.GetApiResourceMeta(); } }

        public string ApiResourceType
        { get { return this.GetApiResourceType(); } }

        public string ApiResourceId
        { get { return this.GetApiResourceId(); } }

        public Type ClrResourceType
        { get { return this.GetClrResourceType(); } }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomReadWriteResourceIdentity(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DomReadOnlyMeta SetDomReadOnlyMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            // Validation
            this.ValidateMetaDoesNotExist();

            // Add the one allowed DOM meta node to the DOM resource
            // identifier node.
            var domReadOnlyMeta = this.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadOnlyMeta;
        }

        internal DomId SetDomIdFromClrResource<TResource>(IResourceType resourceType, TResource clrResource)
            where TResource : class
        {
            Contract.Requires(resourceType != null);

            // Validation
            this.ValidateIdDoesNotExist();

            // Add the one allowed DOM identifier node to the DOM resource.
            var domId = this.CreateAndAddNode(() => DomId.CreateFromClrResource(resourceType, clrResource));
            return domId;
        }

        internal DomId SetDomIdFromClrResourceId<TResourceId>(IResourceType resourceType, TResourceId clrResourceId)
        {
            Contract.Requires(resourceType != null);

            // Validation
            this.ValidateIdDoesNotExist();

            // Add the one allowed DOM identifier node to the DOM resource.
            var domId = this.CreateAndAddNode(() => DomId.CreateFromClrResourceId(resourceType, clrResourceId));
            return domId;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Meta GetApiResourceMeta()
        {
            var domReadOnlyMetaNode = this.GetNode<DomNodeType, DomReadOnlyMeta>(DomNodeType.Meta);
            return domReadOnlyMetaNode != null ? domReadOnlyMetaNode.Meta : null;
        }

        private string GetApiResourceType()
        {
            var domTypeNode = this.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            return domTypeNode != null ? domTypeNode.ApiType : null;
        }

        private string GetApiResourceId()
        {
            var domIdNode = this.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            return domIdNode != null ? domIdNode.ApiId : null;
        }

        private Type GetClrResourceType()
        {
            var domTypeNode = this.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            return domTypeNode != null ? domTypeNode.ClrType : null;
        }

        private void ValidateIdDoesNotExist()
        {
            // Validate a DOM identifier node has not already been added to
            // the DOM resource identity node.
            var containsIdNode = this.ContainsNode(DomNodeType.Id);
            if (!containsIdNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Id);
            throw new DomException(detail);
        }

        private void ValidateMetaDoesNotExist()
        {
            // Validate a DOM meta node has not already been added to the DOM
            // resource identity node.
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
