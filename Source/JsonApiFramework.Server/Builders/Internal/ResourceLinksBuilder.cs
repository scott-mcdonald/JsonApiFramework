// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class ResourceLinksBuilder<TParentBuilder> : LinksBuilder<IResourceLinksBuilder<TParentBuilder>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region LinksBuilder<TBuilder, TParentBuilder> Overrides
        public override IResourceLinksBuilder<TParentBuilder> AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(linkDescription, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal ResourceLinksBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, object clrResource)
            : base(parentBuilder, domContainerNode)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);

            this.Builder = this;
            this.ClrResourceType = clrResourceType;
            this.ClrResource = clrResource;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Type ClrResourceType { get; }
        private object ClrResource { get; }
        #endregion
    }

    internal class ResourceLinksBuilder<TParentBuilder, TResource> : LinksBuilder<IResourceLinksBuilder<TParentBuilder, TResource>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region LinksBuilder<TBuilder, TParentBuilder> Overrides
        public override IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(linkDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }
        #endregion

        #region IResourceLinksBuilder<TParentBuilder, TResource> Implementation
        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, Link link)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(link != null);

            var clrResource = this.ClrResource;
            var canAddLink = predicate == null || predicate(clrResource);
            return canAddLink == false ? this : this.AddLink(rel, link);
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(linkDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var clrResource = this.ClrResource;
            var canAddLink = predicate == null || predicate(clrResource);
            return canAddLink == false ? this : this.AddLink(rel);
        }

        public ILinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>> Link(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var clrResource = this.ClrResource;
            var canAddLink = predicate == null || predicate(clrResource);
            var resourceLinkBuilder = canAddLink
                ? this.Link(rel)
                : new NullLinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>>(this);
            return resourceLinkBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal ResourceLinksBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
            : base(parentBuilder, domContainerNode)
        {
            Contract.Requires(clrResource != null);

            this.Builder = this;
            this.ClrResource = clrResource;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TResource ClrResource { get; }
        #endregion
    }
}
