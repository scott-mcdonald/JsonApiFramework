// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;

namespace JsonApiFramework.Internal
{
    internal class DocumentContextImplementation : IDocumentContextImplementation
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContextImplementation(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            this.Options = options;

            this.LazyDomDocument = this.CreateLazyDomDocument();
            this.LazyDocumentReader = this.CreateLazyDocumentReader();
            this.LazyDocumentWriter = this.CreateLazyDocumentWriter();
        }

        public DocumentContextImplementation(IDocumentContextOptions options, Document apiDocument)
        {
            Contract.Requires(options != null);
            Contract.Requires(apiDocument != null);

            this.Options = options;

            this.LazyDomDocument = this.CreateLazyDomDocument(apiDocument);
            this.LazyDocumentReader = this.CreateLazyDocumentReader();
            this.LazyDocumentWriter = this.CreateLazyDocumentWriter();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDocumentContextImplementation Implementation
        public IDocumentContextOptions Options
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDisposable Implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IDocumentContextImplementation Implementation
        public void Configure(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            this.DoPreConfiguration(documentContextBase);

            this.ConfigureOptions(documentContextBase);
            this.ConfigureConventionSet(documentContextBase);
            this.ConfigureServiceModel(documentContextBase);

            this.DoPostConfiguration(documentContextBase);

            this.ValidateConfiguration();
        }

        public void CreateNewDocument()
        {
            this.DisposeDocumentReader();
            this.DisposeDocumentWriter();

            this.LazyDomDocument = this.CreateLazyDomDocument();
            this.LazyDocumentReader = this.CreateLazyDocumentReader();
            this.LazyDocumentWriter = this.CreateLazyDocumentWriter();
        }

        public DomDocument GetDomDocument()
        {
            var domDocument = this.LazyDomDocument.Value;
            return domDocument;
        }

        public IDocumentReader GetDocumentReader()
        {
            var documentReader = this.LazyDocumentReader.Value;
            return documentReader;
        }

        public IDocumentWriter GetDocumentWriter()
        {
            var documentWriter = this.LazyDocumentWriter.Value;
            return documentWriter;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }

        private Lazy<DomDocument> LazyDomDocument { get; set; }
        private Lazy<IDocumentReader> LazyDocumentReader { get; set; }
        private Lazy<IDocumentWriter> LazyDocumentWriter { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialization Methods
        private void ConfigureOptions(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            var options = this.Options;
            var optionsBuilder = new DocumentContextOptionsBuilder(options);
            documentContextBase.OnConfiguring(optionsBuilder);
        }

        private void ConfigureConventionSet(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            // If we already have the service model or service model conventions,
            // no need to create the service model convetions.
            if (this.GetServiceModel() != null || this.GetConventionSet() != null)
                return;

            var conventionSetBuilder = new ConventionSetBuilder();

            documentContextBase.OnConventionSetCreating(conventionSetBuilder);

            var conventionSet = conventionSetBuilder.Create();
            this.SetConventionSet(conventionSet);
        }

        private void ConfigureServiceModel(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            // If we already have the service model, no need to create the service model.
            if (this.GetServiceModel() != null)
                return;

            var serviceModelBuilder = new ServiceModelBuilder();

            documentContextBase.OnServiceModelCreating(serviceModelBuilder);

            var conventionSet = this.GetConventionSet();
            var serviceModel = serviceModelBuilder.Create(conventionSet);
            if (serviceModel.ResourceTypes.Any() == false)
            {
                // Do not accept an empty service model, this will cause an InternalException
                // to be thrown when validating the final configuration of the document context.
                return;
            }

            this.SetServiceModel(serviceModel);
        }

        private void DoPreConfiguration(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            var options = this.Options;
            documentContextBase.OnPreConfiguration(options);
        }

        private void DoPostConfiguration(DocumentContextBase documentContextBase)
        {
            Contract.Requires(documentContextBase != null);

            var options = this.Options;
            documentContextBase.OnPostConfiguration(options);
        }

        private void ValidateConfiguration()
        {
            var configurationErrorMessages = new List<string>();
            foreach (var extension in this.Options.Extensions)
            {
                extension.ValidateConfiguration(configurationErrorMessages);
            }

            if (configurationErrorMessages.Any() == false)
                return;

            // Errors in the configuration of this document context.
            var detail = configurationErrorMessages.Aggregate((current, next) =>
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(current);
                    stringBuilder.Append(' ');
                    stringBuilder.Append(next);
                    return stringBuilder.ToString();
                });
            throw new InternalErrorException(detail);
        }
        #endregion

        #region Factory Methods
        private Lazy<DomDocument> CreateLazyDomDocument()
        {
            var lazyDomDocument = new Lazy<DomDocument>(() =>
                {
                    var serviceModel = this.GetServiceModel();
                    var domDocument = DomDocument.Create(serviceModel);
                    return domDocument;
                });
            return lazyDomDocument;
        }

        private Lazy<DomDocument> CreateLazyDomDocument(Document apiDocument)
        {
            var lazyDomDocument = new Lazy<DomDocument>(() =>
                {
                    var serviceModel = this.GetServiceModel();
                    var domDocument = DomDocument.Parse(apiDocument, serviceModel);
                    return domDocument;
                });
            return lazyDomDocument;
        }

        private Lazy<IDocumentReader> CreateLazyDocumentReader()
        {
            var lazyDocumentReader = new Lazy<IDocumentReader>(() =>
                {
                    var domDocument = this.GetDomDocument();
                    var documentReader = new DocumentReader(domDocument);
                    return documentReader;
                });
            return lazyDocumentReader;
        }

        private Lazy<IDocumentWriter> CreateLazyDocumentWriter()
        {
            var lazyDocumentWriter = new Lazy<IDocumentWriter>(() =>
                {
                    var domDocument = this.GetDomDocument();
                    var documentWriter = new DocumentWriter(domDocument);
                    return documentWriter;
                });
            return lazyDocumentWriter;
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
        {
            this.DisposeDocumentReader();
            this.DisposeDocumentWriter();
        }

        private void DisposeDocumentReader()
        {
            if (this.LazyDocumentReader == null || !this.LazyDocumentReader.IsValueCreated)
                return;

            var documentReader = this.LazyDocumentReader.Value;
            var documentReaderAsDisposable = documentReader as IDisposable;
            if (documentReaderAsDisposable == null)
                return;

            documentReaderAsDisposable.Dispose();
        }

        private void DisposeDocumentWriter()
        {
            if (this.LazyDocumentWriter == null || !this.LazyDocumentWriter.IsValueCreated)
                return;

            var documentWriter = this.LazyDocumentWriter.Value;
            var documentWriterAsDisposable = documentWriter as IDisposable;
            if (documentWriterAsDisposable == null)
                return;

            documentWriterAsDisposable.Dispose();
        }

        private void DisposeUnmanagedResources()
        { }

        private void NullOutReferences()
        {
            this.Options = null;

            this.LazyDomDocument = null;
            this.LazyDocumentReader = null;
            this.LazyDocumentWriter = null;
        }
        #endregion
    }
}
