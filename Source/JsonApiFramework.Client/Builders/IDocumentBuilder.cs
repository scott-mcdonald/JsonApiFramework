// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Client
{
    public interface IDocumentBuilder : IDocumentWriter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentBuilder SetMeta(Meta meta);
        #endregion

        // Resource /////////////////////////////////////////////////////////
        #region Generic Versions
        IPrimaryResourceBuilder<TResource> Resource<TResource>()
            where TResource : class;

        IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
            where TResource : class;
        #endregion

        #region Non-Generic Versions
        IPrimaryResourceBuilder Resource(Type clrResourceType);

        IPrimaryResourceBuilder Resource(Type clrResourceType, object clrResource);

        IPrimaryResourceBuilder Resource(object clrResource);
        #endregion

        // ResourceIdentifierCollection /////////////////////////////////////
        #region Generic Versions
        IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>()
            where TResource : class;

        IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class;

        IDocumentWriter SetResourceIdentifierCollection<TResource>()
            where TResource : class;

        IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class;
        #endregion

        #region Non-Generic Versions
        IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType);

        IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection);

        IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType);

        IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection);
        #endregion

        // ResourceIdentifier ///////////////////////////////////////////////
        #region Generic Versions
        IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>()
            where TResource : class;

        IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class;

        IDocumentWriter SetResourceIdentifier<TResource>()
            where TResource : class;

        IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class;
        #endregion

        #region Non-Generic Versions
        IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType);

        IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType, object clrResource);

        IPrimaryResourceIdentifierBuilder ResourceIdentifier(object clrResource);

        IDocumentWriter SetResourceIdentifier(Type clrResourceType);

        IDocumentWriter SetResourceIdentifier(Type clrResourceType, object clrResource);

        IDocumentWriter SetResourceIdentifier(object clrResource);
        #endregion
    }
}
