// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia
{
    public interface IHypermediaAssembler
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, ILinkContext linkContext);
        Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, IEnumerable<object> clrResourceCollection, ILinkContext linkContext);
        Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, object clrResource, ILinkContext linkContext);

        Link CreateResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, ILinkContext linkContext);
        Relationship CreateResourceRelationship(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, IRelationshipContext relationshipContext);
        #endregion
    }

    public interface IHypermediaAssembler<in TResource> : IHypermediaAssembler
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TPath2, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TPath2 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrPath2Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TPath2, in TPath3, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrPath2Type { get; }
        Type ClrPath3Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TPath2, in TPath3, in TPath4, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrPath2Type { get; }
        Type ClrPath3Type { get; }
        Type ClrPath4Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TPath2, in TPath3, in TPath4, in TPath5, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TPath5 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrPath2Type { get; }
        Type ClrPath3Type { get; }
        Type ClrPath4Type { get; }
        Type ClrPath5Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }

    public interface IHypermediaAssembler<in TPath1, in TPath2, in TPath3, in TPath4, in TPath5, in TPath6, in TResource> : IHypermediaAssembler
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TPath5 : class
        where TPath6 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPath1Type { get; }
        Type ClrPath2Type { get; }
        Type ClrPath3Type { get; }
        Type ClrPath4Type { get; }
        Type ClrPath5Type { get; }
        Type ClrPath6Type { get; }
        Type ClrResourceType { get; }
        #endregion
    }
}
