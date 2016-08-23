// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Server
{
    public static class DocumentBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods

        // Resource /////////////////////////////////////////////////////////

        // ResourceCollection ///////////////////////////////////////////////
        public static IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(this IDocumentBuilder documentBuilder, params TResource[] clrResourceCollection)
            where TResource : class, IResource
        {
            Contract.Requires(documentBuilder != null);

            return documentBuilder.ResourceCollection(clrResourceCollection.AsEnumerable());
        }

        // ResourceIdentifier ///////////////////////////////////////////////

        // ResourceIdentifierCollection /////////////////////////////////////
        public static IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>(this IDocumentBuilder documentBuilder, params TResource[] clrResourceCollection)
            where TResource : class, IResource
        {
            Contract.Requires(documentBuilder != null);

            return documentBuilder.ResourceIdentifierCollection(clrResourceCollection.AsEnumerable());
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