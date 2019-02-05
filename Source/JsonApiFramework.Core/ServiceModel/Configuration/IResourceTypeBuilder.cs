// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public interface IResourceTypeBuilder : IClrTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IHypermediaInfoBuilder Hypermedia();

        IResourceIdentityInfoBuilder ResourceIdentity();
        IResourceIdentityInfoBuilder ResourceIdentity(string clrPropertyName, Type clrPropertyType);

        IRelationshipsInfoBuilder Relationships(string clrPropertyName);

        ILinksInfoBuilder Links(string clrPropertyName);
        ILinkInfoBuilder Link(string rel);

        IMetaInfoBuilder Meta(string clrPropertyName);
        #endregion
    }

    public interface IResourceTypeBuilder<TResource> : IResourceTypeBuilder, IClrTypeBuilder<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipInfoBuilder<TResource> ToOneRelationship<TToResource>(string rel)
            where TToResource : class;

        IRelationshipInfoBuilder<TResource> ToManyRelationship<TToResource>(string rel)
            where TToResource : class;
        #endregion
    }
}