// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    internal class DomType : Node<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Type; } }

        public override string Name
        { get { return "Type ({0})".FormatWith(this.ApiType); } }
        #endregion

        #region Properties
        public string ApiType { get; private set; }
        public Type ClrType { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomType CreateFromResourceType(IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            var apiResourceType = resourceType.ResourceIdentity.ApiType;
            var clrResourceType = resourceType.ClrResourceType;

            var domType = new DomType(apiResourceType, clrResourceType);
            return domType;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomType(string apiResourceType, Type clrResourceType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiResourceType) == false);
            Contract.Requires(clrResourceType != null);

            this.ApiType = apiResourceType;
            this.ClrType = clrResourceType;
        }
        #endregion
    }
}
