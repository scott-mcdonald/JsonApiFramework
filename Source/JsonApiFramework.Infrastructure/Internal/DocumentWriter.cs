// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    /// <summary>
    /// Base class to capture commonality between client and server json:api
    /// document writers.
    /// </summary>
    internal class DocumentWriter : IDocumentWriter
        , IDisposable
        , IGetDomDocument
        , IGetServiceModel
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentWriter(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            this.ServiceModel = serviceModel;

            var domDocument = DomDocument.Create(serviceModel);
            this.DomDocument = domDocument;
        }

        public DocumentWriter(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            var serviceModel = domDocument.GetServiceModel();
            this.ServiceModel = serviceModel;

            this.DomDocument = domDocument;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetDomDocument Implementation
        public DomDocument DomDocument { get; private set; }
        #endregion

        #region IGetServiceModel Implementation
        public IServiceModel ServiceModel { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentWriter Implementation
        public virtual Document WriteDocument()
        {
            var documentType = GetDocumentType(this.DomDocument);
            var apiDocument = CreateApiDocument(documentType);
            foreach (var domDocumentNode in this.DomDocument.Nodes())
            {
                var domDocumentNodeType = domDocumentNode.NodeType;
                switch (domDocumentNodeType)
                {
                    case DomNodeType.JsonApiVersion:
                        {
                            var domJsonApiVersion = (IDomJsonApiVersion)domDocumentNode;
                            var jsonApiVersion = domJsonApiVersion.JsonApiVersion;
                            apiDocument.JsonApiVersion = jsonApiVersion;
                        }
                        break;

                    case DomNodeType.Meta:
                        {
                            var domMeta = (IDomMeta)domDocumentNode;
                            var meta = domMeta.Meta;
                            apiDocument.Meta = meta;
                        }
                        break;

                    case DomNodeType.Links:
                        {
                            var domLinks = (IDomLinks)domDocumentNode;
                            var links = domLinks.Links;
                            apiDocument.Links = links;
                        }
                        break;

                    case DomNodeType.Data:
                        {
                            var domData = (DomData)domDocumentNode;
                            switch (documentType)
                            {
                                case DocumentType.ResourceDocument:
                                    {
                                        var domPrimaryResource = (IDomResource)domData.Node;
                                        var apiPrimaryResource = domPrimaryResource != null
                                            ? domPrimaryResource.ApiResource
                                            : null;

                                        apiDocument.SetResource(apiPrimaryResource);
                                    }
                                    break;

                                case DocumentType.ResourceIdentifierDocument:
                                    {
                                        var domPrimaryResourceIdentifier = (IDomResourceIdentifier)domData.Node;
                                        var apiPrimaryResourceIdentifier = domPrimaryResourceIdentifier != null
                                            ? domPrimaryResourceIdentifier.ApiResourceIdentifier
                                            : null;

                                        apiDocument.SetResourceIdentifier(apiPrimaryResourceIdentifier);
                                    }
                                    break;
                            }
                        }
                        break;

                    case DomNodeType.DataCollection:
                        {
                            var domDataCollection = (DomDataCollection)domDocumentNode;
                            switch (documentType)
                            {
                                case DocumentType.ResourceCollectionDocument:
                                    {
                                        var apiPrimaryResources = domDataCollection
                                            .Nodes()
                                            .Select(domNode =>
                                                {
                                                    var domPrimaryResource = (IDomResource)domNode;
                                                    var apiPrimaryResource = domPrimaryResource.ApiResource;
                                                    return apiPrimaryResource;
                                                })
                                            .ToList();

                                        apiDocument.AddResourceRange(apiPrimaryResources);
                                    }
                                    break;

                                case DocumentType.ResourceIdentifierCollectionDocument:
                                    {
                                        var apiPrimaryResourceIdentifiers = domDataCollection
                                            .Nodes()
                                            .Select(domNode =>
                                                {
                                                    var domPrimaryResourceIdentifier = (IDomResourceIdentifier)domNode;
                                                    var apiPrimaryResourceIdentifier = domPrimaryResourceIdentifier.ApiResourceIdentifier;
                                                    return apiPrimaryResourceIdentifier;
                                                })
                                            .ToList();

                                        apiDocument.AddResourceIdentifierRange(apiPrimaryResourceIdentifiers);
                                    }
                                    break;
                            }
                        }
                        break;

                    case DomNodeType.Included:
                        {
                            var domIncluded = (DomIncluded)domDocumentNode;
                            var apiIncludedResources = domIncluded
                                .Nodes()
                                .Select(domNode =>
                                    {
                                        var domIncludedResource = (IDomResource)domNode;
                                        var apiIncludedResource = domIncludedResource.ApiResource;
                                        return apiIncludedResource;
                                    })
                                .ToList();

                            apiDocument.AddIncludedResources(apiIncludedResources);
                        }
                        break;

                    case DomNodeType.Errors:
                        {
                            var domErrors = (IDomErrors)domDocumentNode;
                            var errors = domErrors.Errors;
                            apiDocument.AddErrors(errors);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(DomNodeType.Document, domDocumentNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return apiDocument;
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        private static Document CreateApiDocument(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.Document: return new Document();
                case DocumentType.EmptyDocument: return new EmptyDocument();
                case DocumentType.ErrorsDocument: return new ErrorsDocument();
                case DocumentType.NullDocument: return new NullDocument();
                case DocumentType.ResourceDocument: return new ResourceDocument();
                case DocumentType.ResourceCollectionDocument: return new ResourceCollectionDocument();
                case DocumentType.ResourceIdentifierDocument: return new ResourceIdentifierDocument();
                case DocumentType.ResourceIdentifierCollectionDocument: return new ResourceIdentifierCollectionDocument();
                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(DocumentType).Name, documentType);
                        throw new InternalErrorException(detail);
                    }
            }
        }

        private static DocumentType GetDocumentType(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            var documentType = domDocument.GetDocumentType();
            return documentType;
        }
        #endregion

        #region Dispose Methods
        private void Dispose(bool disposeManagedResources)
        {
            try
            {
                // Don't dispose more than once.
                if (this.IsDisposed)
                    return;

                // Dispose of managed resources conditionally and unmanaged
                // resources unconditionally. Lastly null out reference handles
                // to remove references to the respective objects for the GC.
                if (disposeManagedResources)
                {
                    this.DisposeManagedResources();
                }
                this.DisposeUnmanagedResources();
                this.NullOutReferences();
            }
            finally
            {
                // Set disposed flag.
                this.IsDisposed = true;
            }
        }

        private void DisposeManagedResources()
        { }

        private void DisposeUnmanagedResources()
        { }

        private void NullOutReferences()
        {
            this.DomDocument = null;
            this.ServiceModel = null;
        }
        #endregion
    }
}
