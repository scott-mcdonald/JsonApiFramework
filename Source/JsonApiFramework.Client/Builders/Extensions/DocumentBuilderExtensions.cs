// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Client
{
    public static class DocumentBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods

        // ResourceIdentifierCollection /////////////////////////////////////
        public static IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>(this IDocumentBuilder documentBuilder, params TResource[] clrResourceCollection)
            where TResource : class, IResource
        {
            Contract.Requires(documentBuilder != null);

            var primaryResourceIdentifierCollectionBuilder = documentBuilder.ResourceIdentifierCollection(clrResourceCollection.AsEnumerable());
            return primaryResourceIdentifierCollectionBuilder;
        }

        public static IDocumentWriter SetResourceIdentifierCollection<TResource>(this IDocumentBuilder documentBuilder, params TResource[] clrResourceCollection)
            where TResource : class, IResource
        {
            Contract.Requires(documentBuilder != null);

            return documentBuilder.SetResourceIdentifierCollection(clrResourceCollection.AsEnumerable());
        }
        #endregion
    }
}