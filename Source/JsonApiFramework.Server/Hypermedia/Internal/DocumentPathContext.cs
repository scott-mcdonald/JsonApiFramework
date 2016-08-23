// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class DocumentPathContext : IDocumentPathContext, INodeAttributeValue
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentPathContext(IEnumerable<IHypermediaPath> documentSelfPath)
        {
            Contract.Requires(documentSelfPath != null);

            this.DocumentSelfPath = documentSelfPath;
        }

        public DocumentPathContext(IHypermediaContext hypermediaContext, Uri url)
        {
            Contract.Requires(url != null);
            Contract.Requires(hypermediaContext != null);

            var serviceModel = hypermediaContext.ServiceModel;
            var urlBuilderConfiguration = hypermediaContext.UrlBuilderConfiguration;

            var documentSelfPath = url.ParseDocumentSelfPath(serviceModel, urlBuilderConfiguration);
            this.DocumentSelfPath = documentSelfPath;
        }

        public DocumentPathContext(IHypermediaContext hypermediaContext, string urlString)
            : this(hypermediaContext, new Uri(urlString))
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPathContext Implementation
        public IEnumerable<Type> ClrResourceTypes
        { get { return this.DocumentSelfPath.GetClrResourceTypes(); } }
        #endregion

        #region IDocumentPathContext Implementation
        public IEnumerable<IHypermediaPath> DocumentSelfPath
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region INodeAttributeValue Implementation
        public string ToNodeAttributeValueString()
        {
            // Create a DocumentSelfPath string representation.
            var documentSelfPathSegments = this.DocumentSelfPath
                .EmptyIfNull()
                .SelectMany(x => x.PathSegments)
                .ToList();

            if (!documentSelfPathSegments.Any())
                return String.Empty;

            var documentSelfPathAsString = documentSelfPathSegments.Aggregate((current, next) => current + "/" + next);
            return documentSelfPathAsString;
        }
        #endregion
    }
}
