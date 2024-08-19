﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;
using JsonApiFramework.Extension;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel.Internal;

/// <summary>
/// Represents type information of an individual resource in a service model.
/// </summary>
internal class ResourceType : ClrTypeInfo, IResourceType
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public ResourceType(Type                  clrResourceType,
                        IHypermediaInfo       hypermediaInfo,
                        IResourceIdentityInfo resourceIdentityInfo,
                        IAttributesInfo       attributesInfo,
                        IRelationshipsInfo    relationshipsInfo,
                        ILinksInfo            linksInfo,
                        IMetaInfo             metaInfo)
        : base(clrResourceType, attributesInfo)
    {
        Contract.Requires(hypermediaInfo != null);
        Contract.Requires(resourceIdentityInfo != null);
        Contract.Requires(relationshipsInfo != null);
        Contract.Requires(linksInfo != null);

        this.ExtensionDictionary = new ExtensionDictionary<IResourceType>(this);

        // JSON Properties
        this.HypermediaInfo       = hypermediaInfo;
        this.ResourceIdentityInfo = resourceIdentityInfo;
        this.RelationshipsInfo    = relationshipsInfo;
        this.LinksInfo            = linksInfo;
        this.MetaInfo             = metaInfo;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region IResourceType Implementation
    public IHypermediaInfo       HypermediaInfo       { get; private set; }
    public IResourceIdentityInfo ResourceIdentityInfo { get; private set; }
    public IRelationshipsInfo    RelationshipsInfo    { get; private set; }
    public ILinksInfo            LinksInfo            { get; private set; }
    public IMetaInfo             MetaInfo             { get; private set; }
    #endregion

    #region IExtensibleObject<T> Implementation
    [JsonIgnore] public IEnumerable<IExtension<IResourceType>> Extensions => this.ExtensionDictionary.Extensions;
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IResourceType Implementation
    public ResourceIdentifier CreateApiResourceIdentifier<TResourceId>(TResourceId clrResourceId)
    {
        if (this.ResourceIdentityInfo != null)
        {
            var apiType = this.ResourceIdentityInfo.ApiType;
            var apiId   = this.ResourceIdentityInfo.ToApiId(clrResourceId);
            var apiResourceIdentifier = string.IsNullOrWhiteSpace(apiId) == false
                ? new ResourceIdentifier(apiType, apiId)
                : null;
            return apiResourceIdentifier;
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public string GetApiId(object clrResource)
    {
        if (this.ResourceIdentityInfo != null)
        {
            return this.ResourceIdentityInfo.GetApiId(clrResource);
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public ResourceIdentifier GetApiResourceIdentifier(object clrResource)
    {
        if (this.ResourceIdentityInfo != null)
        {
            var apiType = this.ResourceIdentityInfo.ApiType;
            var apiId   = this.ResourceIdentityInfo.GetApiId(clrResource);
            var apiResourceIdentifier = string.IsNullOrWhiteSpace(apiId) == false
                ? new ResourceIdentifier(apiType, apiId)
                : null;
            return apiResourceIdentifier;
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public object GetClrId(object clrResource)
    {
        if (this.ResourceIdentityInfo != null)
        {
            return this.ResourceIdentityInfo.GetClrId(clrResource);
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public Relationships GetClrRelationships(object clrResource)
    {
        if (this.RelationshipsInfo != null)
        {
            var relationships = (Relationships)this.RelationshipsInfo.GetClrProperty(clrResource);
            return relationships;
        }

        var relationshipsInfoMissingException = this.CreateInfoMissingException<RelationshipsInfo>();
        throw relationshipsInfoMissingException;
    }

    public Links GetClrLinks(object clrResource)
    {
        if (this.LinksInfo != null)
        {
            var links = (Links)this.LinksInfo.GetClrProperty(clrResource);
            return links;
        }

        var linksInfoMissingException = this.CreateInfoMissingException<LinksInfo>();
        throw linksInfoMissingException;
    }

    public Meta GetClrMeta(object clrResource)
    {
        if (this.MetaInfo != null)
        {
            var meta = (Meta)this.MetaInfo.GetClrProperty(clrResource);
            return meta;
        }

        var metaInfoMissingException = this.CreateInfoMissingException<MetaInfo>();
        throw metaInfoMissingException;
    }

    public IRelationshipInfo GetRelationshipInfo(string rel)
    {
        if (this.RelationshipsInfo != null)
        {
            return this.RelationshipsInfo.GetRelationshipInfo(rel);
        }

        var relationshipsInfoMissingException = this.CreateInfoMissingException<RelationshipsInfo>();
        throw relationshipsInfoMissingException;
    }

    public ILinkInfo GetLinkInfo(string rel)
    {
        if (this.LinksInfo != null)
        {
            return this.LinksInfo.GetLinkInfo(rel);
        }

        var linksInfoMissingException = this.CreateInfoMissingException<LinksInfo>();
        throw linksInfoMissingException;
    }

    public bool IsClrIdNull(object clrId)
    {
        if (this.ResourceIdentityInfo != null)
        {
            return this.ResourceIdentityInfo.IsClrIdNull(clrId);
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public void SetClrMeta(object clrResource, Meta apiMeta)
    {
        if (this.MetaInfo == null || this.MetaInfo.CanGetOrSetClrProperty() == false)
            return;

        this.MetaInfo.SetClrProperty(clrResource, apiMeta);
    }

    public void SetClrId(object clrResource, object clrId)
    {
        if (this.ResourceIdentityInfo != null)
        {
            this.ResourceIdentityInfo.SetClrId(clrResource, clrId);
            return;
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public void SetClrRelationships(object clrResource, Relationships apiRelationships)
    {
        if (this.RelationshipsInfo == null || this.RelationshipsInfo.CanGetOrSetClrProperty() == false)
            return;

        this.RelationshipsInfo.SetClrProperty(clrResource, apiRelationships);
    }

    public void SetClrLinks(object clrResource, Links links)
    {
        if (this.LinksInfo == null || this.LinksInfo.CanGetOrSetClrProperty() == false)
            return;

        this.LinksInfo.SetClrProperty(clrResource, links);
    }

    public string ToApiId(object clrId)
    {
        if (this.ResourceIdentityInfo != null)
        {
            return this.ResourceIdentityInfo.ToApiId(clrId);
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public object ToClrId(object clrId)
    {
        if (this.ResourceIdentityInfo != null)
        {
            return this.ResourceIdentityInfo.ToClrId(clrId);
        }

        var resourceIdentityInfoMissingException = this.CreateInfoMissingException<ResourceIdentityInfo>();
        throw resourceIdentityInfoMissingException;
    }

    public bool TryGetRelationshipInfo(string rel, out IRelationshipInfo relationshipInfo)
    {
        relationshipInfo = null;
        return this.RelationshipsInfo != null && this.RelationshipsInfo.TryGetRelationshipInfo(rel, out relationshipInfo);
    }

    public bool TryGetLinkInfo(string rel, out ILinkInfo linkInfo)
    {
        linkInfo = null;
        return this.LinksInfo != null && this.LinksInfo.TryGetLinkInfo(rel, out linkInfo);
    }
    #endregion

    #region IExtensibleObject<T> Implementation
    public void AddExtension(IExtension<IResourceType> extension)
    {
        Contract.Requires(extension != null);

        this.ExtensionDictionary.AddExtension(extension);
    }

    public void RemoveExtension(Type extensionType)
    {
        Contract.Requires(extensionType != null);

        this.ExtensionDictionary.RemoveExtension(extensionType);
    }

    public bool TryGetExtension(Type extensionType, out IExtension<IResourceType> extension)
    {
        Contract.Requires(extensionType != null);

        return this.ExtensionDictionary.TryGetExtension(extensionType, out extension);
    }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal ResourceType()
    {
        this.ExtensionDictionary = new ExtensionDictionary<IResourceType>(this);
    }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private ExtensionDictionary<IResourceType> ExtensionDictionary { get; }
    #endregion
}