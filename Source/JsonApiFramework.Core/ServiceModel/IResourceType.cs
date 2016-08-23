// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel
{
    public interface IResourceType : IJsonObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Name { get; }
        Type ClrResourceType { get; }

        IHypermediaInfo Hypermedia { get; }
        IResourceIdentityInfo ResourceIdentity { get; }
        IAttributesInfo Attributes { get; }
        IRelationshipsInfo Relationships { get; }
        ILinksInfo Links { get; }
        IMetaInfo Meta { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Initialize(IServiceModel serviceModel);

        ResourceIdentifier CreateApiResourceIdentifier<TResourceId>(TResourceId clrResourceId); 
        object CreateClrResource();

        string GetApiId(object clrResource);
        ResourceIdentifier GetApiResourceIdentifier(object clrResource);
        object GetClrId(object clrResource);
        Relationships GetClrRelationships(object clrResource);
        Links GetClrLinks(object clrResource);
        Meta GetClrMeta(object clrResource);

        IAttributeInfo GetApiAttribute(string apiPropertyName);
        IAttributeInfo GetClrAttribute(string clrPropertyName);
        IRelationshipInfo GetRelationship(string rel);
        ILinkInfo GetLink(string rel);

        bool IsClrIdNull(object clrId);

        void SetClrMeta(object clrResource, Meta apiMeta);
        void SetClrId(object clrResource, object clrId);
        void SetClrRelationships(object clrResource, Relationships apiRelationships);
        void SetClrLinks(object clrResource, Links apiLinks);

        string ToApiId(object clrId);
        object ToClrId(object clrId);

        bool TryGetApiAttribute(string apiPropertyName, out IAttributeInfo attribute);
        bool TryGetClrAttribute(string clrPropertyName, out IAttributeInfo attribute);
        bool TryGetRelationship(string rel, out IRelationshipInfo relationship);
        bool TryGetLink(string rel, out ILinkInfo link);
        #endregion
    }
}