// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only resource identifier node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyResourceIdentifier : Node<DomNodeType>, IDomResourceIdentifier
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.ResourceIdentifier; } }

        public override string Name
        {
            get
            {
                var type = this.ApiResourceType ?? "null";
                var id = this.ApiResourceId ?? "null";
                return "ReadOnlyResourceIdentifier [type={0} id={1}]".FormatWith(type, id);
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

        public Type ClrResourceType
        { get; private set; }
        #endregion

        #region IDomResourceIdentifier Implementation
        public ResourceIdentifier ApiResourceIdentifier
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyResourceIdentifier Create(ResourceIdentifier apiResourceIdentifier, Type clrResourceType)
        {
            Contract.Requires(apiResourceIdentifier != null);
            Contract.Requires(clrResourceType != null);

            var domReadOnlyResourceIdentifier = new DomReadOnlyResourceIdentifier(apiResourceIdentifier, clrResourceType);
            return domReadOnlyResourceIdentifier;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyResourceIdentifier(ResourceIdentifier apiResourceIdentifier, Type clrResourceType)
        {
            Contract.Requires(apiResourceIdentifier != null);
            Contract.Requires(clrResourceType != null);

            var apiResourceType = apiResourceIdentifier.Type;
            var apiResourceId = apiResourceIdentifier.Id;
            var apiResourceMeta = apiResourceIdentifier.Meta;

            this.ApiResourceIdentifier = apiResourceIdentifier;
            this.ApiResourceMeta = apiResourceMeta;
            this.ApiResourceType = apiResourceType;
            this.ApiResourceId = apiResourceId;

            this.ClrResourceType = clrResourceType;
        }
        #endregion
    }
}
