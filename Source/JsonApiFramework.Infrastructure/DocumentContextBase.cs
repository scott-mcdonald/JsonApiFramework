// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;

namespace JsonApiFramework
{
    public class DocumentContextBase : IDocumentReader, IDocumentWriter, IGetServiceModel
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetServiceModel Implementation
        public IServiceModel ServiceModel
        {
            get
            {
                var serviceModel = this.Implementation.GetServiceModel();
                return serviceModel;
            }
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentReader Implementation
        public JsonApiVersion GetJsonApiVersion()
        { return this.Implementation.GetDocumentReader().GetJsonApiVersion(); }

        public DocumentType GetDocumentType()
        { return this.Implementation.GetDocumentReader().GetDocumentType(); }

        public Meta GetDocumentMeta()
        { return this.Implementation.GetDocumentReader().GetDocumentMeta(); }

        public Links GetDocumentLinks()
        { return this.Implementation.GetDocumentReader().GetDocumentLinks(); }

        public TResource GetRelatedToOneResource<TResource>(Relationship relationship)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetRelatedToOneResource<TResource>(relationship); }

        public IEnumerable<TResource> GetRelatedToManyResourceCollection<TResource>(Relationship relationship)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetRelatedToManyResourceCollection<TResource>(relationship); }

        public TResource GetResource<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResource<TResource>(); }

        public TResource GetResource<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResource<TResource, TResourceId>(clrResourceId); }

        public IEnumerable<TResource> GetResourceCollection<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceCollection<TResource>(); }

        public TResourceId GetResourceId<TResource, TResourceId>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceId<TResource, TResourceId>(); }

        public IEnumerable<TResourceId> GetResourceIdCollection<TResource, TResourceId>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceIdCollection<TResource, TResourceId>(); }

        public Meta GetResourceMeta<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceMeta<TResource>(); }

        public Meta GetResourceMeta<TResource>(TResource clrResource)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceMeta(clrResource); }

        public Meta GetResourceMeta<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceMeta<TResource, TResourceId>(clrResourceId); }

        public IEnumerable<Meta> GetResourceMetaCollection<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceMetaCollection<TResource>(); }

        public Links GetResourceLinks<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceLinks<TResource>(); }

        public Links GetResourceLinks<TResource>(TResource clrResource)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceLinks(clrResource); }

        public Links GetResourceLinks<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceLinks<TResource, TResourceId>(clrResourceId); }

        public IEnumerable<Links> GetResourceLinksCollection<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceLinksCollection<TResource>(); }

        public Relationships GetResourceRelationships<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceRelationships<TResource>(); }

        public Relationships GetResourceRelationships<TResource>(TResource clrResource)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceRelationships(clrResource); }

        public Relationships GetResourceRelationships<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceRelationships<TResource, TResourceId>(clrResourceId); }

        public IEnumerable<Relationships> GetResourceRelationshipsCollection<TResource>()
            where TResource : class, IResource
        { return this.Implementation.GetDocumentReader().GetResourceRelationshipsCollection<TResource>(); }

        public IEnumerable<Type> GetResourceTypes()
        { return this.Implementation.GetDocumentReader().GetResourceTypes(); }

        public IEnumerable<Error> GetErrors()
        { return this.Implementation.GetDocumentReader().GetErrors(); }
        #endregion

        #region IDocumentWriter Implementation
        public Document WriteDocument()
        { return this.Implementation.GetDocumentWriter().WriteDocument(); }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        {
            var domTreeString = this.ToDomTreeString();
            return domTreeString;
        }
        #endregion

        #region Methods
        public string ToDomTreeString()
        {
            var domDocument = this.Implementation.GetDomDocument();
            var domDocumentTreeString = domDocument != null ? domDocument.ToTreeString() : "Document";
            return domDocumentTreeString;
        }
        #endregion

        // PROTECTED-INTERNAL METHODS ///////////////////////////////////////
        #region DocumentContextBase Overrides
        protected internal virtual void OnConfiguring(IDocumentContextOptionsBuilder optionsBuilder)
        { }

        protected internal virtual void OnConventionSetCreating(IConventionSetBuilder conventionSetBuilder)
        { }

        protected internal virtual void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
        { }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region DocumentContextBase Overrides
        protected virtual void Dispose(bool disposeManagedResources)
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
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal DocumentContextBase(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            var lazyImplementation = new Lazy<IDocumentContextImplementation>(() =>
                {
                    var implementation = new DocumentContextImplementation(options);
                    implementation.Configure(this);
                    return implementation;
                });
            this.LazyImplementation = lazyImplementation;
        }

        internal DocumentContextBase(IDocumentContextOptions options, Document apiDocument)
        {
            Contract.Requires(options != null);
            Contract.Requires(apiDocument != null);

            var lazyImplementation = new Lazy<IDocumentContextImplementation>(() =>
                {
                    var implementation = new DocumentContextImplementation(options, apiDocument);
                    implementation.Configure(this);
                    return implementation;
                });
            this.LazyImplementation = lazyImplementation;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal IDocumentContextImplementation Implementation
        {
            get
            {
                var isDisposed = this.IsDisposed;
                if (isDisposed)
                {
                    var objectDisposedException = this.CreateObjectDisposedException();
                    throw objectDisposedException;
                }

                var implementation = this.LazyImplementation.Value;
                return implementation;
            }
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region DocumentContextBase Overrides
        internal virtual void OnPreConfiguration(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            options.CreateExtension<CoreDocumentContextExtension>();
        }

        internal virtual void OnPostConfiguration(IDocumentContextOptions options)
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }

        private Lazy<IDocumentContextImplementation> LazyImplementation { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Dispose Methods
        private ObjectDisposedException CreateObjectDisposedException()
        {
            var typeName = this.GetType().Name;
            var detail = InfrastructureErrorStrings.ObjectDisposedExceptionDetailDocumentContext;
            var objectDisposedException = new ObjectDisposedException(typeName, detail);
            return objectDisposedException;
        }

        private void DisposeManagedResources()
        {
            this.DisposeLazyImplementation();
        }

        private void DisposeLazyImplementation()
        {
            if (this.LazyImplementation == null || !this.LazyImplementation.IsValueCreated)
                return;

            var implementation = this.LazyImplementation.Value;
            implementation.Dispose();
        }

        private void DisposeUnmanagedResources()
        { }

        private void NullOutReferences()
        {
            this.LazyImplementation = null;
        }
        #endregion
    }
}
