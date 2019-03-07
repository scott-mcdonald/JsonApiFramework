// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Extension;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel
{
    public interface IResourceType : IClrTypeInfo, IExtensibleObject<IResourceType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IHypermediaInfo       HypermediaInfo       { get; }
        IResourceIdentityInfo ResourceIdentityInfo { get; }
        IRelationshipsInfo    RelationshipsInfo    { get; }
        ILinksInfo            LinksInfo            { get; }
        IMetaInfo             MetaInfo             { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        ResourceIdentifier CreateApiResourceIdentifier<TResourceId>(TResourceId clrResourceId);

        string             GetApiId(object                 clrResource);
        ResourceIdentifier GetApiResourceIdentifier(object clrResource);
        object             GetClrId(object                 clrResource);
        Relationships      GetClrRelationships(object      clrResource);
        Links              GetClrLinks(object              clrResource);
        Meta               GetClrMeta(object               clrResource);

        IRelationshipInfo GetRelationshipInfo(string rel);
        ILinkInfo         GetLinkInfo(string         rel);

        bool IsClrIdNull(object clrId);

        void SetClrMeta(object          clrResource, Meta          apiMeta);
        void SetClrId(object            clrResource, object        clrId);
        void SetClrRelationships(object clrResource, Relationships apiRelationships);
        void SetClrLinks(object         clrResource, Links         apiLinks);

        string ToApiId(object clrId);
        object ToClrId(object clrId);

        bool TryGetRelationshipInfo(string rel, out IRelationshipInfo relationshipInfo);
        bool TryGetLinkInfo(string         rel, out ILinkInfo         linkInfo);
        #endregion
    }
}