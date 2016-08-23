// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    /// <summary>
    /// Represents type information of an individual resource in a service
    /// model.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ResourceType : JsonObject
        , IResourceType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceType(Type clrResourceType,
                            IHypermediaInfo hypermedia,
                            IResourceIdentityInfo resourceIdentity,
                            IAttributesInfo attributes,
                            IRelationshipsInfo relationships,
                            ILinksInfo links,
                            IMetaInfo meta)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(hypermedia != null);
            Contract.Requires(resourceIdentity != null);
            Contract.Requires(attributes != null);
            Contract.Requires(relationships != null);
            Contract.Requires(links != null);

            // JSON Properties
            this.ClrResourceType = clrResourceType;
            this.Hypermedia = hypermedia;
            this.ResourceIdentity = resourceIdentity;
            this.Attributes = attributes;
            this.Relationships = relationships;
            this.Links = links;
            this.Meta = meta;

            // Non-JSON Properties
            this.Name = clrResourceType.Name;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IResourceType Implementation
        [JsonProperty] public Type ClrResourceType { get; private set; }
        [JsonProperty] public IHypermediaInfo Hypermedia { get; private set; }
        [JsonProperty] public IResourceIdentityInfo ResourceIdentity { get; private set; }
        [JsonProperty] public IAttributesInfo Attributes { get; private set; }
        [JsonProperty] public IRelationshipsInfo Relationships { get; private set; }
        [JsonProperty] public ILinksInfo Links { get; private set; }
        [JsonProperty] public IMetaInfo Meta { get; private set; }
        #endregion

        #region Properties
        public string Name { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceType Implementation
        public void Initialize(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            // Initialize all aggregate objects.
            this.InitializeResourceIdentity(serviceModel);
            this.InitializeAttributes(serviceModel);
            this.InitializeRelationships(serviceModel);
            this.InitializeLinks(serviceModel);
            this.InitializeMeta(serviceModel);

            // Initialize all internal objects.
            this.InitializeClrResourceNewMethod();
        }

        public ResourceIdentifier CreateApiResourceIdentifier<TResourceId>(TResourceId clrResourceId)
        {
            if (this.ResourceIdentity != null)
            {
                var apiType = this.ResourceIdentity.ApiType;
                var apiId = this.ResourceIdentity.ToApiId(clrResourceId);
                var apiResourceIdentifier = String.IsNullOrWhiteSpace(apiId) == false
                    ? new ResourceIdentifier(apiType, apiId)
                    : null;
                return apiResourceIdentifier;
            }


            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public object CreateClrResource()
        {
            var clrResource = this.ClrResourceNewMethod.DynamicInvoke();
            return clrResource;
        }

        public string GetApiId(object clrResource)
        {
            if (this.ResourceIdentity != null)
            {
                return this.ResourceIdentity.GetApiId(clrResource);
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public ResourceIdentifier GetApiResourceIdentifier(object clrResource)
        {
            if (this.ResourceIdentity != null)
            {
                var apiType = this.ResourceIdentity.ApiType;
                var apiId = this.ResourceIdentity.GetApiId(clrResource);
                var apiResourceIdentifier = String.IsNullOrWhiteSpace(apiId) == false
                    ? new ResourceIdentifier(apiType, apiId)
                    : null;
                return apiResourceIdentifier;
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public object GetClrId(object clrResource)
        {
            if (this.ResourceIdentity != null)
            {
                return this.ResourceIdentity.GetClrId(clrResource);
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public Relationships GetClrRelationships(object clrResource)
        {
            if (this.Relationships != null)
            {
                var relationships = (Relationships)this.Relationships.GetClrProperty(clrResource);
                return relationships;
            }

            var relationshipsInfoMissingException = this.CreateInfoMissingException<RelationshipsInfo>();
            throw relationshipsInfoMissingException;
        }

        public Links GetClrLinks(object clrResource)
        {
            if (this.Links != null)
            {
                var links = (Links)this.Links.GetClrProperty(clrResource);
                return links;
            }

            var linksInfoMissingException = this.CreateInfoMissingException<LinksInfo>();
            throw linksInfoMissingException;
        }

        public Meta GetClrMeta(object clrResource)
        {
            if (this.Meta != null)
            {
                var meta = (Meta)this.Meta.GetClrProperty(clrResource);
                return meta;
            }

            var metaInfoMissingException = this.CreateInfoMissingException<MetaInfo>();
            throw metaInfoMissingException;
        }

        public IAttributeInfo GetApiAttribute(string apiPropertyName)
        {
            if (this.Attributes != null)
            {
                return this.Attributes.GetApiAttribute(apiPropertyName);
            }

            var attributesInfoMissingException = this.CreateInfoMissingException<AttributesInfo>();
            throw attributesInfoMissingException;
        }

        public IAttributeInfo GetClrAttribute(string clrPropertyName)
        {
            if (this.Attributes != null)
            {
                return this.Attributes.GetClrAttribute(clrPropertyName);
            }

            var attributesInfoMissingException = this.CreateInfoMissingException<AttributesInfo>();
            throw attributesInfoMissingException;
        }

        public IRelationshipInfo GetRelationship(string rel)
        {
            if (this.Relationships != null)
            {
                return this.Relationships.GetRelationship(rel);
            }

            var relationshipsInfoMissingException = this.CreateInfoMissingException<RelationshipsInfo>();
            throw relationshipsInfoMissingException;
        }

        public ILinkInfo GetLink(string rel)
        {
            if (this.Links != null)
            {
                return this.Links.GetLink(rel);
            }

            var linksInfoMissingException = this.CreateInfoMissingException<LinksInfo>();
            throw linksInfoMissingException;
        }

        public bool IsClrIdNull(object clrId)
        {
            if (this.ResourceIdentity != null)
            {
                return this.ResourceIdentity.IsClrIdNull(clrId);
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public void SetClrMeta(object clrResource, Meta apiMeta)
        {
            if (this.Meta == null || this.Meta.CanGetOrSetClrProperty() == false)
                return;

            this.Meta.SetClrProperty(clrResource, apiMeta);
        }

        public void SetClrId(object clrResource, object clrId)
        {
            if (this.ResourceIdentity != null)
            {
                this.ResourceIdentity.SetClrId(clrResource, clrId);
                return;
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public void SetClrRelationships(object clrResource, Relationships apiRelationships)
        {
            if (this.Relationships == null || this.Relationships.CanGetOrSetClrProperty() == false)
                return;

            this.Relationships.SetClrProperty(clrResource, apiRelationships);
        }

        public void SetClrLinks(object clrResource, Links links)
        {
            if (this.Links == null || this.Links.CanGetOrSetClrProperty() == false)
                return;

            this.Links.SetClrProperty(clrResource, links);
        }

        public string ToApiId(object clrId)
        {
            if (this.ResourceIdentity != null)
            {
                return this.ResourceIdentity.ToApiId(clrId);
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public object ToClrId(object clrId)
        {
            if (this.ResourceIdentity != null)
            {
                return this.ResourceIdentity.ToClrId(clrId);
            }

            var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
            throw resourceIdentityInfoMissingException;
        }

        public bool TryGetApiAttribute(string apiPropertyName, out IAttributeInfo attribute)
        {
            attribute = null;
            return this.Attributes != null &&
                   this.Attributes.TryGetApiAttribute(apiPropertyName, out attribute);
        }

        public bool TryGetClrAttribute(string clrPropertyName, out IAttributeInfo attribute)
        {
            attribute = null;
            return this.Attributes != null &&
                   this.Attributes.TryGetClrAttribute(clrPropertyName, out attribute);
        }

        public bool TryGetRelationship(string rel, out IRelationshipInfo relationship)
        {
            relationship = null;
            return this.Relationships != null && this.Relationships.TryGetRelationship(rel, out relationship);
        }

        public bool TryGetLink(string rel, out ILinkInfo link)
        {
            link = null;
            return this.Links != null && this.Links.TryGetLink(rel, out link);
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ResourceType()
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Delegate ClrResourceNewMethod { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private void InitializeResourceIdentity(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            if (this.ResourceIdentity == null)
                return;

            this.ResourceIdentity.Initialize(serviceModel, this);
        }

        private void InitializeAttributes(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            if (this.Attributes == null)
                return;

            this.Attributes.Initialize(serviceModel, this);
        }

        private void InitializeRelationships(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            if (this.Relationships == null)
                return;

            this.Relationships.Initialize(serviceModel, this);
        }

        private void InitializeLinks(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            if (this.Links == null)
                return;

            this.Links.Initialize(serviceModel, this);
        }

        private void InitializeMeta(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            if (this.Meta == null)
                return;

            this.Meta.Initialize(serviceModel, this);
        }

        private void InitializeClrResourceNewMethod()
        {
            var clrResourceNewExpression = ExpressionBuilder.New(this.ClrResourceType);
            var clrResourceNewMethod = clrResourceNewExpression.Compile();
            this.ClrResourceNewMethod = clrResourceNewMethod;
        }
        #endregion

        #region Helper Methods
        private ServiceModelException CreateInfoMissingException<TInfo>()
        {
            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType),
                this.ClrResourceType.Name);

            var infoDescription = typeof(TInfo).Name;

            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(resourceTypeDescription, infoDescription);
            var infoMissingException = new ServiceModelException(detail);
            return infoMissingException;
        }
        #endregion
    }
}