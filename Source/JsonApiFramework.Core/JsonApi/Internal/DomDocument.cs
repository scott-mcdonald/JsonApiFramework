// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class DomDocument : DomNode
        , IDomDocument
        , ISetJsonApiVersion
        , ISetMeta
        , ISetLinks
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocument(IServiceModel serviceModel, params DomNode[] domNodes)
            : base(DomNodeType.Document, "Document", domNodes)
        {
            Contract.Requires(serviceModel != null);

            this.ServiceModel = serviceModel;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public IServiceModel ServiceModel { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomDocument Implementation
        public DocumentType GetDocumentType()
        { throw new NotImplementedException(); }
        #endregion

        #region IGetJsonApiVersion Implementation
        public JsonApiVersion GetJsonApiVersion()
        {
            var domNode = this.DomNodes()
                              .SingleOrDefault(x => x.Type == DomNodeType.JsonApiVersion);
            var domJsonApiVersion = (IDomJsonApiVersion)domNode;
            var jsonApiVersion = domJsonApiVersion?.GetJsonApiVersion();
            return jsonApiVersion;
        }
        #endregion

        #region IGetMeta Implementation
        public Meta GetMeta()
        {
            var domNode = this.DomNodes()
                              .SingleOrDefault(x => x.Type == DomNodeType.Meta);
            var domMeta = (IDomMeta)domNode;
            var meta = domMeta?.GetMeta();
            return meta;
        }
        #endregion

        #region IGetLinks Implementation
        public Links GetLinks()
        {
            var domNode = this.DomNodes()
                              .SingleOrDefault(x => x.Type == DomNodeType.Links);
            var domLinks = (IDomLinks)domNode;
            var links = domLinks?.GetLinks();
            return links;
        }
        #endregion

        #region ISetJsonApiVersion Implementation
        public void SetJsonApiVersion(JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(jsonApiVersion != null);

            var domJsonApiVersion = new DomReadOnlyJsonApiVersion(jsonApiVersion);
            this.AddOrUpdateDomNode(DomNodeType.JsonApiVersion, domJsonApiVersion);
        }
        #endregion

        #region ISetMeta Implementation
        public void SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            var domMeta = new DomReadOnlyMeta(meta);
            this.AddOrUpdateDomNode(DomNodeType.Meta, domMeta);
        }
        #endregion

        #region ISetLinks Implementation
        public void SetLinks(Links links)
        {
            Contract.Requires(links != null);

            var domLinks = new DomReadOnlyLinks(links);
            this.AddOrUpdateDomNode(DomNodeType.Links, domLinks);
        }
        #endregion
    }
}
