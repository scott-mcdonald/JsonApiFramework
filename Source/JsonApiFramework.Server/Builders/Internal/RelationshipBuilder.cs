// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipBuilder<TParentBuilder> : RelationshipBuilderBase, IRelationshipBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder> Implementation
        public IRelationshipBuilder<TParentBuilder> SetMeta(Meta meta)
        {
            base.SetMetaInternal(meta);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection)
        {
            base.SetMetaInternal(metaCollection);
            return this;
        }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder>> Links()
        {
            var linksBuilder = new RelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder>>(this, this.DomReadWriteRelationship, this.Rel);
            return linksBuilder;
        }

        public IRelationshipBuilder<TParentBuilder> SetData(IToOneResourceLinkage toOneResourceLinkage)
        {
            base.SetDataInternal(toOneResourceLinkage);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder> SetData(IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            base.SetDataInternal(toOneResourceLinkageCollection);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder> SetData(IToManyResourceLinkage toManyResourceLinkage)
        {
            base.SetDataInternal(toManyResourceLinkage);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder> SetData(IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
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
        internal RelationshipBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, string rel, Type clrResourceType)
            : base(serviceModel, domContainerNode, rel, clrResourceType)
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
