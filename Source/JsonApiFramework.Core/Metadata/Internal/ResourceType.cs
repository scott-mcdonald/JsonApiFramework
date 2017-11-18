// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ResourceType<TObject> : TypeBase<TObject>, IResourceType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceType(IResourceIdentityInfo resourceIdentityInfo, IAttributesInfo attributesInfo, IRelationshipsInfo relationshipsInfo, ILinksInfo linksInfo)
            : base(attributesInfo)
        {
            Contract.Requires(resourceIdentityInfo != null);
            Contract.Requires(relationshipsInfo != null);
            Contract.Requires(linksInfo != null);

            this.ResourceIdentityInfo = resourceIdentityInfo;
            this.RelationshipsInfo = relationshipsInfo;
            this.LinksInfo = linksInfo;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IResourceType Implementation
        public IResourceIdentityInfo ResourceIdentityInfo { get; }

        public IRelationshipsInfo RelationshipsInfo { get; }

        public ILinksInfo LinksInfo { get; }
        #endregion
    }
}