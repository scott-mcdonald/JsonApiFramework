// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.ServiceModel.Configuration.Internal;
using JsonApiFramework.ServiceModel.Conventions;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public class ResourceTypeBuilder<TResource> : IResourceTypeBuilder<TResource>, IResourceTypeFactory
        where TResource : class, IResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IResourceTypeBuilder Implementation
        public Type ClrResourceType { get { return typeof(TResource); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceTypeBuilder Implementation
        public IHypermediaInfoBuilder Hypermedia()
        {
            var clrResourceType = this.ClrResourceType;
            this.HypermediaInfoBuilder = this.HypermediaInfoBuilder ?? new HypermediaInfoBuilder(clrResourceType);
            return this.HypermediaInfoBuilder;
        }

        public IResourceIdentityInfoBuilder ResourceIdentity(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            var clrResourceType = this.ClrResourceType;
            this.ResourceIdentityInfoBuilder = this.ResourceIdentityInfoBuilder ?? new ResourceIdentityInfoBuilder(clrResourceType, clrPropertyName, clrPropertyType);
            return this.ResourceIdentityInfoBuilder;
        }

        public IAttributeInfoBuilder Attribute(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            this.AttributeInfoBuilderDictionary = this.AttributeInfoBuilderDictionary ?? new Dictionary<string, AttributeInfoBuilder>();
            this.AttributeInfoBuilderOrder = this.AttributeInfoBuilderOrder ?? new List<string>();

            AttributeInfoBuilder attributeInfoConfiguration;
            if (this.AttributeInfoBuilderDictionary.TryGetValue(clrPropertyName, out attributeInfoConfiguration))
            {
                return attributeInfoConfiguration;
            }

            attributeInfoConfiguration = new AttributeInfoBuilder(clrPropertyName, clrPropertyType);
            this.AttributeInfoBuilderDictionary.Add(clrPropertyName, attributeInfoConfiguration);
            this.AttributeInfoBuilderOrder.Add(clrPropertyName);

            return attributeInfoConfiguration;
        }

        public IRelationshipsInfoBuilder Relationships(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.RelationshipsInfoBuilder = this.RelationshipsInfoBuilder ?? new RelationshipsInfoBuilder(clrPropertyName);
            return this.RelationshipsInfoBuilder;
        }

        public ILinksInfoBuilder Links(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.LinksInfoBuilder = this.LinksInfoBuilder ?? new LinksInfoBuilder(clrPropertyName);
            return this.LinksInfoBuilder;
        }

        public ILinkInfoBuilder Link(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.CreateLinkInfoBuilder(rel);
        }

        public IMetaInfoBuilder Meta(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.MetaInfoBuilder = this.MetaInfoBuilder ?? new MetaInfoBuilder(clrPropertyName);
            return this.MetaInfoBuilder;
        }
        #endregion

        #region IResourceTypeBuilder<TResource> Implementation
        public IRelationshipInfoBuilder<TResource> ToOneRelationship<TToResource>(string rel)
            where TToResource : class, IResource
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var toClrType = typeof(TToResource);
            const RelationshipCardinality toCardinality = RelationshipCardinality.ToOne;
            return this.CreateRelationshipInfoBuilder(rel, toClrType, toCardinality);
        }

        public IRelationshipInfoBuilder<TResource> ToManyRelationship<TToResource>(string rel)
            where TToResource : class, IResource
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var toClrType = typeof(TToResource);
            const RelationshipCardinality toCardinality = RelationshipCardinality.ToMany;
            return this.CreateRelationshipInfoBuilder(rel, toClrType, toCardinality);
        }
        #endregion

        #region IResourceTypeFactory Implementation
        public IResourceType Create(ConventionSet conventionSet)
        {
            // Aplly all built-in conventions.
            this.ApplyBuiltInConventions();

            // Apply all ResourceTypeConfiguration level conventions if any.
            this.ApplyResourceTypeConventions(conventionSet);

            // Create all the ResourceType parameters needed to construct a ResourceType metadata object.
            var clrResourceType = this.ClrResourceType;
            var hypermediaInfo = this.CreateHypermediaInfo(conventionSet);
            var resourceIdentityInfo = this.CreateResourceIdentityInfo(conventionSet);
            var attributesInfo = this.CreateAttributesInfo(conventionSet);
            var relationshipsInfo = this.CreateRelationshipsInfo();
            var linksInfo = this.CreateLinksInfo();
            var metaInfo = this.CreateMetaInfo();

            var resourceType = new ResourceType(clrResourceType,
                                                hypermediaInfo,
                                                resourceIdentityInfo,
                                                attributesInfo,
                                                relationshipsInfo,
                                                linksInfo,
                                                metaInfo);
            return resourceType;
        }
        #endregion

        // PROTECTED/INTERNAL CONSTRUCTORS //////////////////////////////////
        #region Constructors
        protected internal ResourceTypeBuilder()
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private HypermediaInfoBuilder HypermediaInfoBuilder { get; set; }

        private ResourceIdentityInfoBuilder ResourceIdentityInfoBuilder { get; set; }

        private AttributesInfoBuilder<TResource> AttributesInfoBuilder { get; set; }

        private IDictionary<string, AttributeInfoBuilder> AttributeInfoBuilderDictionary { get; set; }
        private IList<string> AttributeInfoBuilderOrder { get; set; }

        private RelationshipsInfoBuilder RelationshipsInfoBuilder { get; set; }

        private IDictionary<string, RelationshipInfoBuilder<TResource>> RelationshipInfoBuilderDictionary { get; set; }
        private IList<string> RelationshipInfoBuilderOrder { get; set; }

        private LinksInfoBuilder LinksInfoBuilder { get; set; }

        private IDictionary<string, LinkInfoBuilder> LinkInfoBuilderDictionary { get; set; }
        private IList<string> LinkInfoBuilderOrder { get; set; }

        private MetaInfoBuilder MetaInfoBuilder { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyBuiltInConventions()
        {
            // Ensure a default HypermediaInfo is configured.
            this.Hypermedia();
        }

        private void ApplyResourceTypeConventions(ConventionSet conventionSet)
        {
            if (conventionSet == null)
                return;

            var resourceTypeConventions = conventionSet.ResourceTypeConventions ?? Enumerable.Empty<IResourceTypeConvention>();
            foreach (var resourceTypeConvention in resourceTypeConventions)
            {
                resourceTypeConvention.Apply(this);
            }
        }

        private IHypermediaInfo CreateHypermediaInfo(ConventionSet conventionSet)
        {
            if (this.HypermediaInfoBuilder == null)
            {
                var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), typeof(TResource).Name);
                var hypermediaInfoDescription = typeof(HypermediaInfo).Name;
                var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                             .FormatWith(resourceTypeDescription, hypermediaInfoDescription);
                throw new ServiceModelException(detail);
            }

            var hypermediaInfo = this.HypermediaInfoBuilder.CreateHypermediaInfo(conventionSet);
            return hypermediaInfo;
        }

        private IResourceIdentityInfo CreateResourceIdentityInfo(ConventionSet conventionSet)
        {
            if (this.ResourceIdentityInfoBuilder == null)
            {
                var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), typeof(TResource).Name);
                var resourceIdentityInfoDescription = typeof(ResourceIdentityInfo).Name;
                var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                             .FormatWith(resourceTypeDescription, resourceIdentityInfoDescription);
                throw new ServiceModelException(detail);
            }

            var resourceIdentityInfo = this.ResourceIdentityInfoBuilder.CreateResourceIdentityInfo(conventionSet);
            return resourceIdentityInfo;
        }

        private IAttributesInfo CreateAttributesInfo(ConventionSet conventionSet)
        {
            this.AttributesInfoBuilder = this.AttributesInfoBuilder ?? new AttributesInfoBuilder<TResource>();

            var attributeInfoCollection = this.AttributeInfoBuilderOrder
                                              .EmptyIfNull()
                                              .Select(x =>
                                                  {
                                                      var clrPropertyName = x;
                                                      var attributeInfoConfiguration = this.AttributeInfoBuilderDictionary.Single(y => y.Key == clrPropertyName).Value;
                                                      var attributeInfo = attributeInfoConfiguration.CreateAttributeInfo(conventionSet);
                                                      return attributeInfo;
                                                  })
                                              .ToList();
            var attributesInfo = this.AttributesInfoBuilder.CreateAttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }

        private IRelationshipsInfo CreateRelationshipsInfo()
        {
            this.RelationshipsInfoBuilder = this.RelationshipsInfoBuilder ?? new RelationshipsInfoBuilder();

            var relationshipInfoCollection = this.RelationshipInfoBuilderOrder
                                                 .EmptyIfNull()
                                                 .Select(x =>
                                                     {
                                                         var rel = x;
                                                         var relationshipInfoConfiguration = this.RelationshipInfoBuilderDictionary.Single(y => y.Key == rel).Value;
                                                         var relationshipInfo = relationshipInfoConfiguration.CreateRelationshipInfo();
                                                         return relationshipInfo;
                                                     })
                                                 .ToList();
            var relationshipsInfo = this.RelationshipsInfoBuilder.CreateRelationshipsInfo(relationshipInfoCollection);
            return relationshipsInfo;
        }

        private IRelationshipInfoBuilder<TResource> CreateRelationshipInfoBuilder(string rel, Type toClrType, RelationshipCardinality toCardinality)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(toClrType != null);

            this.RelationshipInfoBuilderDictionary = this.RelationshipInfoBuilderDictionary ?? new Dictionary<string, RelationshipInfoBuilder<TResource>>();
            this.RelationshipInfoBuilderOrder = this.RelationshipInfoBuilderOrder ?? new List<string>();

            RelationshipInfoBuilder<TResource> relationshipInfoConfiguration;
            if (this.RelationshipInfoBuilderDictionary.TryGetValue(rel, out relationshipInfoConfiguration))
            {
                return relationshipInfoConfiguration;
            }

            relationshipInfoConfiguration = new RelationshipInfoBuilder<TResource>(rel, toClrType, toCardinality);
            this.RelationshipInfoBuilderDictionary.Add(rel, relationshipInfoConfiguration);
            this.RelationshipInfoBuilderOrder.Add(rel);

            return relationshipInfoConfiguration;
        }

        private ILinksInfo CreateLinksInfo()
        {
            this.LinksInfoBuilder = this.LinksInfoBuilder ?? new LinksInfoBuilder();

            var linkInfoCollection = this.LinkInfoBuilderOrder
                                         .EmptyIfNull()
                                         .Select(x =>
                                             {
                                                 var rel = x;
                                                 var linkInfoConfiguration = this.LinkInfoBuilderDictionary.Single(y => y.Key == rel).Value;
                                                 var linkInfo = linkInfoConfiguration.CreateLinkInfo();
                                                 return linkInfo;
                                             })
                                         .ToList();
            var linksInfo = this.LinksInfoBuilder.CreateLinksInfo(linkInfoCollection);
            return linksInfo;
        }

        private ILinkInfoBuilder CreateLinkInfoBuilder(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.LinkInfoBuilderDictionary = this.LinkInfoBuilderDictionary ?? new Dictionary<string, LinkInfoBuilder>();
            this.LinkInfoBuilderOrder = this.LinkInfoBuilderOrder ?? new List<string>();

            LinkInfoBuilder linkInfoConfiguration;
            if (this.LinkInfoBuilderDictionary.TryGetValue(rel, out linkInfoConfiguration))
            {
                return linkInfoConfiguration;
            }

            linkInfoConfiguration = new LinkInfoBuilder(rel);
            this.LinkInfoBuilderDictionary.Add(rel, linkInfoConfiguration);
            this.LinkInfoBuilderOrder.Add(rel);

            return linkInfoConfiguration;
        }

        private IMetaInfo CreateMetaInfo()
        {
            this.MetaInfoBuilder = this.MetaInfoBuilder ?? new MetaInfoBuilder();

            var metaInfo = this.MetaInfoBuilder.CreateMetaInfo();
            return metaInfo;
        }
        #endregion
    }
}