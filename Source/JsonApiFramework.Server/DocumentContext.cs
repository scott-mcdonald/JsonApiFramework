// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    public class DocumentContext : DocumentContextBase, IDocumentFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContext(IDocumentContextOptions options)
            : base(options)
        { }

        public DocumentContext(IDocumentContextOptions options, Document apiDocument)
            : base(options, apiDocument)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentFactory Implementation
        public IDocumentBuilder NewDocument(string currentRequestUrl)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(currentRequestUrl) == false);

            return this.NewDocument(new Uri(currentRequestUrl));
        }

        public IDocumentBuilder NewDocument(Uri currentRequestUrl)
        {
            Contract.Requires(currentRequestUrl != null);

            var documentBuilder = this.CreateDocumentBuilder(currentRequestUrl);
            return documentBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DocumentContext()
            : base(new DocumentContextOptions<DocumentContext>())
        { }

        protected DocumentContext(Document apiDocument)
            : base(new DocumentContextOptions<DocumentContext>(), apiDocument)
        { }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region DocumentContextBase Overrides
        protected override void Dispose(bool disposeManagedResources)
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

                // Explicitly call the base class Dispose implementation.
                base.Dispose(disposeManagedResources);
            }
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region DocumentContextBase Overrides
        internal override void OnPreConfiguration(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            base.OnPreConfiguration(options);

            options.CreateExtension<ServerDocumentContextExtension>();
        }

        internal override void OnPostConfiguration(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            base.OnPostConfiguration(options);

            var coreExtension = options.GetExtension<CoreDocumentContextExtension>();
            var serviceModel = coreExtension.ServiceModel;

            var serverExtension                        = options.GetExtension<ServerDocumentContextExtension>();
            var urlBuilderConfiguration                = serverExtension.UrlBuilderConfiguration;
            var urlBuilderConfigurationPerResourceType = serverExtension.UrlBuilderConfigurationPerResourceType;

            var hypermediaContext = new HypermediaContext(serviceModel, urlBuilderConfiguration, urlBuilderConfigurationPerResourceType);
            serverExtension.HypermediaContext = hypermediaContext;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IDocumentBuilder CreateDocumentBuilder(Uri currentRequestUrl)
        {
            Contract.Requires(currentRequestUrl != null);

            this.Implementation.CreateNewDocument();

            var domDocument = this.Implementation.GetDomDocument();
            var serviceModel = this.Implementation.GetServiceModel();
            var documentWriter = this.Implementation.GetDocumentWriter();
            var hypermediaAssemblerRegistry = this.Implementation.GetHypermediaAssemblerRegistry();
            var hypermediaContext = this.Implementation.GetHypermediaContext();
            var queryParameters = QueryParameters.Create(currentRequestUrl);
            var sparseFieldsetsEnabled = this.Implementation.IsSparseFieldsetsEnabled();

            var documentBuilderContext = new DocumentBuilderContext(currentRequestUrl, queryParameters, sparseFieldsetsEnabled);
            var documentBuilder = new DocumentBuilder(domDocument, serviceModel, documentWriter, hypermediaAssemblerRegistry, hypermediaContext, documentBuilderContext);
            return documentBuilder;
        }
        #endregion

        #region Dispose Methods
        private void DisposeManagedResources()
        { }

        private void DisposeUnmanagedResources()
        { }

        private void NullOutReferences()
        { }
        #endregion
    }
}
