// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Metadata.Configuration.Internal;
using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.Properties;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents the configuration to build and create a resource type in a service model.
    /// </summary>
    /// <typeparam name="TResource">The type of resource object to build and create metadata about.</typeparam>
    public class ResourceTypeConfiguration<TResource> : TypeBaseBuilder<TResource>
        , IResourceTypeBuilder<TResource>
        , IResourceTypeFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceTypeConfiguration()
            : base(ResourceTypeConfigurationName)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceTypeBuilder<TResource> Implementation
        public void ResourceIdentity<TProperty>(Expression<Func<TResource, TProperty>> clrIdPropertySelector)
        {
            Contract.Requires(clrIdPropertySelector != null);

            this.ResourceIdentity(clrIdPropertySelector, null);
        }

        public void ResourceIdentity<TProperty>(Expression<Func<TResource, TProperty>> clrIdPropertySelector, Func<IResourceIdentityInfoBuilder<TResource>, IResourceIdentityInfoBuilder<TResource>> configuration)
        {
            Contract.Requires(clrIdPropertySelector != null);

            if (this.ResourceIdentityInfoBuilder != null)
            {
                var detail = CoreErrorStrings.MetadataExceptionAlreadyConfiguredDetail
                                             .FormatWith(this.Name, "resource identity");
                throw new MetadataException(detail);
            }

            var clrIdPropertyBinding = Factory<TResource>.CreateClrPropertyBinding(clrIdPropertySelector);
            this.ResourceIdentityInfoBuilder = new ResourceIdentityInfoBuilder<TResource>(clrIdPropertyBinding);
            configuration?.Invoke(this.ResourceIdentityInfoBuilder);
        }

        public IRelationshipsInfoBuilder<TResource> Relationships()
        { return this.Relationships(null); }

        public IRelationshipsInfoBuilder<TResource> Relationships(Expression<Func<TResource, Relationships>> clrRelationshipsPropertySelector)
        {
            if (this.RelationshipsInfoBuilder != null)
            {
                var detail = CoreErrorStrings.MetadataExceptionAlreadyConfiguredDetail
                                             .FormatWith(this.Name, "relationships");
                throw new MetadataException(detail);
            }

            var clrRelationshipsPropertyBinding = clrRelationshipsPropertySelector != null ? Factory<TResource>.CreateClrPropertyBinding(clrRelationshipsPropertySelector) : default(IClrPropertyBinding);
            this.RelationshipsInfoBuilder = new RelationshipsInfoBuilder<TResource>(this, clrRelationshipsPropertyBinding);
            return this.RelationshipsInfoBuilder;
        }

        public void Links(Expression<Func<TResource, Links>> clrLinksPropertySelector)
        {
            if (this.LinksInfoBuilder != null)
            {
                var detail = CoreErrorStrings.MetadataExceptionAlreadyConfiguredDetail
                                             .FormatWith(this.Name, "links");
                throw new MetadataException(detail);
            }

            var clrLinksPropertyBinding = clrLinksPropertySelector != null ? Factory<TResource>.CreateClrPropertyBinding(clrLinksPropertySelector) : default(IClrPropertyBinding);
            this.LinksInfoBuilder = new LinksInfoBuilder(clrLinksPropertyBinding);
        }
        #endregion

        #region IResourceTypeFactory Implementation
        public IResourceType Create(IMetadataConventions metadataConventions)
        {
            // Apply all ResourceType level conventions if any.
            this.ApplyResourceTypeConventions(metadataConventions);

            // Create all the ResourceType parameters needed to construct a ResourceType metadata object.
            var resourceIdentityInfo = this.CreateResourceIdentityInfo(metadataConventions);
            var attributesInfo = this.CreateAttributesInfo(metadataConventions);
            var relationshipsInfo = this.CreateRelationshipsInfo(metadataConventions);
            var linksInfo = this.CreateLinksInfo(metadataConventions);
            var resourceType = new ResourceType<TResource>(resourceIdentityInfo, attributesInfo, relationshipsInfo, linksInfo);
            return resourceType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static readonly string ResourceTypeConfigurationName = "ResourceTypeConfiguration [clrType={0}]".FormatWith(typeof(TResource).Name);
        private IResourceIdentityInfoBuilder<TResource> ResourceIdentityInfoBuilder { get; set; }
        private IRelationshipsInfoBuilder<TResource> RelationshipsInfoBuilder { get; set; }
        private ILinksInfoFactory LinksInfoBuilder { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyResourceTypeConventions(IMetadataConventions metadataConventions)
        {
            if (metadataConventions == null)
                return;

            //var resourceTypeConventions = metadataConventions.ResourceTypeConventions ?? Enumerable.Empty<IResourceTypeConvention>();
            //foreach (var resourceTypeConvention in resourceTypeConventions)
            //{
            //    resourceTypeConvention.Apply(this);
            //}
        }

        private IResourceIdentityInfo CreateResourceIdentityInfo(IMetadataConventions metadataConventions)
        {
            if (this.ResourceIdentityInfoBuilder == null)
            {
                var detail = CoreErrorStrings.MetadataExceptionMissingRequiredConfigurationDetail
                                             .FormatWith(this.Name, "resource identity");
                throw new MetadataException(detail);
            }

            var resourceIdentityInfoFactory = (IResourceIdentityInfoFactory)this.ResourceIdentityInfoBuilder;
            var resourceIdentityInfo = resourceIdentityInfoFactory.Create(metadataConventions);
            return resourceIdentityInfo;
        }

        private IRelationshipsInfo CreateRelationshipsInfo(IMetadataConventions metadataConventions)
        {
            if (this.RelationshipsInfoBuilder == null)
                return null;

            var relationshipsInfoFactory = (IRelationshipsInfoFactory)this.RelationshipsInfoBuilder;
            var relationshipsInfo = relationshipsInfoFactory.Create(metadataConventions);
            return relationshipsInfo;
        }

        private ILinksInfo CreateLinksInfo(IMetadataConventions metadataConventions)
        {
            if (this.LinksInfoBuilder == null)
                return null;

            var linksInfoFactory = (ILinksInfoFactory)this.LinksInfoBuilder;
            var linksInfo = linksInfoFactory.Create(metadataConventions);
            return linksInfo;
        }
        #endregion
    }
}
