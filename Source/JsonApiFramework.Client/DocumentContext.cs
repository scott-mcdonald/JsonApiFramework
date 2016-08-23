// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Client.Internal;
using JsonApiFramework.Internal;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Client
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
        public IDocumentBuilder NewDocument()
        {
            var documentBuilder = this.CreateDocumentBuilder();
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

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IDocumentBuilder CreateDocumentBuilder()
        {
            this.Implementation.CreateNewDocument();

            var domDocument = this.Implementation.GetDomDocument();
            var serviceModel = this.Implementation.GetServiceModel();
            var documentWriter = this.Implementation.GetDocumentWriter();
            var documentBuilder = new DocumentBuilder(domDocument, serviceModel, documentWriter);
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
