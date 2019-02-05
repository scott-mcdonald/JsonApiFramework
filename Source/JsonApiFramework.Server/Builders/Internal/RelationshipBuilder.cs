// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipBuilder<TParentBuilder, TResource> : RelationshipBuilderBase<TResource>, IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        {
            base.SetMetaInternal(meta);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection)
        {
            base.SetMetaInternal(metaCollection);
            return this;
        }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links()
        {
            var linksBuilder = new RelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>>(this, this.DomReadWriteRelationship, this.Rel);
            return linksBuilder;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IToOneResourceLinkage toOneResourceLinkage)
        {
            base.SetDataInternal(toOneResourceLinkage);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            base.SetDataInternal(toOneResourceLinkageCollection);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IToManyResourceLinkage toManyResourceLinkage)
        {
            base.SetDataInternal(toManyResourceLinkage);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            base.SetDataInternal(toManyResourceLinkageCollection);
            return this;
        }

        public TParentBuilder RelationshipEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, string rel)
            : base(serviceModel, domContainerNode, rel)
        {
            Contract.Requires(parentBuilder != null);

            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; }
        #endregion
    }
}
