// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Server.Hypermedia;

namespace JsonApiFramework.Server.Internal.Dom
{
    internal static class DomExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region DomDocument Extensions
        public static IDocumentPathContext GetDocumentPathContext(this DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            var documentPathContext = domDocument.GetSingleAttribute<IDocumentPathContext>(DocumentPathContextNodeAttributeName);
            return documentPathContext;
        }

        public static void SetDocumentPathContext(this DomDocument domDocument, IDocumentPathContext documentPathContext)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(documentPathContext != null);

            domDocument.SetSingleAttribute(DocumentPathContextNodeAttributeName, documentPathContext);
        }
        #endregion

        #region DomReadWriteResource Extensions
        public static IResourcePathContext GetResourcePathContext(this DomReadWriteResource domReadWriteResource)
        {
            Contract.Requires(domReadWriteResource != null);

            var resourcePathContext = domReadWriteResource.GetSingleAttribute<IResourcePathContext>(ResourcePathContextNodeAttributeName);
            return resourcePathContext;
        }

        public static void SetResourcePathContext(this DomReadWriteResource domReadWriteResource, IResourcePathContext resourcePathContext)
        {
            Contract.Requires(domReadWriteResource != null);
            Contract.Requires(resourcePathContext != null);

            domReadWriteResource.SetSingleAttribute(ResourcePathContextNodeAttributeName, resourcePathContext);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string DocumentPathContextNodeAttributeName = "document-path-context";
        private const string ResourcePathContextNodeAttributeName = "resource-path-context";
        #endregion
    }
}