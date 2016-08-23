// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only resource node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyResource : Node<DomNodeType>, IDomResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Resource; } }

        public override string Name
        {
            get
            {
                var type = this.ApiResourceType ?? "null";
                var id = this.ApiResourceId ?? "null";
                return "ReadOnlyResource [type={0} id={1}]".FormatWith(type, id);
            }
        }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomResourceIdentity Implementation
        public Meta ApiResourceMeta
        { get; private set; }

        public string ApiResourceType
        { get; private set; }

        public string ApiResourceId
        { get; private set; }

        public Links ApiResourceLinks
        { get; private set; }

        public Relationships ApiResourceRelationships
        { get; private set; }

        public Type ClrResourceType
        { get; private set; }
        #endregion

        #region IDomResource Implementation
        public Resource ApiResource
        { get; private set; }

        public object ClrResource
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyResource Create(Resource apiResource, object clrResource)
        {
            Contract.Requires(apiResource != null);
            Contract.Requires(clrResource != null);

            var domReadOnlyResource = new DomReadOnlyResource(apiResource, clrResource);
            return domReadOnlyResource;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyResource(Resource apiResource, object clrResource)
        {
            Contract.Requires(apiResource != null);
            Contract.Requires(clrResource != null);

            // JsonApi Resource
            var apiResourceMeta = apiResource.Meta;
            var apiResourceType = apiResource.Type;
            var apiResourceId = apiResource.Id;
            var apiResourceRelationships = apiResource.Relationships;
            var apiResourceLinks = apiResource.Links;

            this.ApiResource = apiResource;
            this.ApiResourceMeta = apiResourceMeta;
            this.ApiResourceType = apiResourceType;
            this.ApiResourceId = apiResourceId;
            this.ApiResourceRelationships = apiResourceRelationships;
            this.ApiResourceLinks = apiResourceLinks;

            // CLR Resource
            var clrResourceType = clrResource.GetType();

            this.ClrResource = clrResource;
            this.ClrResourceType = clrResourceType;
        }
        #endregion
    }
}
