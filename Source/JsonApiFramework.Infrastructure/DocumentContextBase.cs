﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Conventions;
using JsonApiFramework.Internal;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework;

public class DocumentContextBase : IDisposable
    , IDocumentReader
    , IDocumentWriter
    , IGetServiceModel
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

    public object GetRelatedResource(Type clrRelatedResourceType, Relationship relationship, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetRelatedResource(clrRelatedResourceType, relationship, enumerateIncludedResources); }

    public IEnumerable<object> GetRelatedResourceCollection(Type clrRelatedResourceType, Relationship relationship, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetRelatedResourceCollection(clrRelatedResourceType, relationship, enumerateIncludedResources); }

    public object GetResource(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResource(clrResourceType, enumerateIncludedResources); }

    public object GetResource<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResource<TResourceId>(clrResourceType, clrResourceId, enumerateIncludedResources); }

    public IEnumerable<object> GetResourceCollection(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceCollection(clrResourceType, enumerateIncludedResources); }

    public TResourceId GetResourceId<TResourceId>(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceId<TResourceId>(clrResourceType, enumerateIncludedResources); }

    public IEnumerable<TResourceId> GetResourceIdCollection<TResourceId>(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceIdCollection<TResourceId>(clrResourceType, enumerateIncludedResources); }

    public Meta GetResourceMeta(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceMeta(clrResourceType, enumerateIncludedResources); }

    public Meta GetResourceMeta(object clrResource, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceMeta(clrResource, enumerateIncludedResources); }

    public Meta GetResourceMeta<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceMeta(clrResourceType, clrResourceId, enumerateIncludedResources); }

    public IEnumerable<Meta> GetResourceMetaCollection(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceMetaCollection(clrResourceType, enumerateIncludedResources); }

    public Links GetResourceLinks(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceLinks(clrResourceType, enumerateIncludedResources); }

    public Links GetResourceLinks(object clrResource, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceLinks(clrResource, enumerateIncludedResources); }

    public Links GetResourceLinks<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceLinks(clrResourceType, clrResourceId, enumerateIncludedResources); }

    public IEnumerable<Links> GetResourceLinksCollection(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceLinksCollection(clrResourceType, enumerateIncludedResources); }

    public Relationships GetResourceRelationships(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceRelationships(clrResourceType, enumerateIncludedResources); }

    public Relationships GetResourceRelationships(object clrResource, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceRelationships(clrResource, enumerateIncludedResources); }

    public Relationships GetResourceRelationships<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceRelationships(clrResourceType, clrResourceId, enumerateIncludedResources); }

    public IEnumerable<Relationships> GetResourceRelationshipsCollection(Type clrResourceType, bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceRelationshipsCollection(clrResourceType, enumerateIncludedResources); }

    public IEnumerable<Type> GetResourceTypeCollection(bool enumerateIncludedResources = true)
    { return this.Implementation.GetDocumentReader().GetResourceTypeCollection(enumerateIncludedResources); }

    public IEnumerable<Error> GetErrorCollection()
    { return this.Implementation.GetDocumentReader().GetErrorCollection(); }
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

    protected internal virtual void OnConventionsCreating(IConventionsBuilder conventionsBuilder)
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
