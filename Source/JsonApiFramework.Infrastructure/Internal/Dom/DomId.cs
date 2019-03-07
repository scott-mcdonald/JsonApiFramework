// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    internal class DomId : Node<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Id; } }

        public override string Name
        { get { return "Id ({0})".FormatWith(this.ApiId); } }
        #endregion

        #region Properties
        public string ApiId { get; private set; }

        public object ClrId { get; private set; }
        public string ClrPropertyName { get; private set; }
        public Type ClrPropertyType { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomId CreateFromApiResourceIdentity(IResourceType resourceType, IGetResourceIdentity apiResourceIdentity)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(apiResourceIdentity != null);

            var apiId = apiResourceIdentity.Id;

            var resourceIdentity = resourceType.ResourceIdentityInfo;
            var clrId = resourceIdentity.ToClrId(apiId);
            var clrPropertyName = resourceIdentity.GetClrIdPropertyName();
            var clrPropertyType = resourceIdentity.GetClrIdPropertyType();

            var domId = new DomId(apiId, clrId, clrPropertyName, clrPropertyType);
            return domId;
        }

        public static DomId CreateFromClrResource(IResourceType resourceType, object clrResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);

            var resourceIdentity = resourceType.ResourceIdentityInfo;

            var apiId = resourceIdentity.GetApiId(clrResource);

            var clrId = resourceIdentity.GetClrId(clrResource);
            var clrPropertyName = resourceIdentity.GetClrIdPropertyName();
            var clrPropertyType = resourceIdentity.GetClrIdPropertyType();

            var domId = new DomId(apiId, clrId, clrPropertyName, clrPropertyType);
            return domId;
        }

        public static DomId CreateFromClrResourceId<TResourceId>(IResourceType resourceType, TResourceId clrResourceId)
        {
            Contract.Requires(resourceType != null);

            var resourceIdentity = resourceType.ResourceIdentityInfo;

            var apiId = resourceIdentity.ToApiId(clrResourceId);

            var clrId = resourceIdentity.ToClrId(clrResourceId);
            var clrPropertyName = resourceIdentity.GetClrIdPropertyName();
            var clrPropertyType = resourceIdentity.GetClrIdPropertyType();

            var domId = new DomId(apiId, clrId, clrPropertyName, clrPropertyType);
            return domId;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomId(string apiId, object clrId, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);
            Contract.Requires(clrId != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            this.ApiId = apiId;

            this.ClrId = clrId;
            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
        }
        #endregion
    }
}
