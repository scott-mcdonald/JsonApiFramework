// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Extension;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class RelationshipInfo : JsonObject, IRelationshipInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipInfo(string rel, string apiRelPathSegment, Type toClrType, RelationshipCardinality toCardinality, RelationshipCanonicalRelPathMode toCanonicalRelPathMode)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiRelPathSegment) == false);
            Contract.Requires(toClrType != null);

            this.ExtensionDictionary = new ExtensionDictionary<IRelationshipInfo>(this);

            this.Rel                    = rel;
            this.ApiRelPathSegment      = apiRelPathSegment;
            this.ToClrType              = toClrType;
            this.ToCardinality          = toCardinality;
            this.ToCanonicalRelPathMode = toCanonicalRelPathMode;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipInfo Implementation
        [JsonProperty] public string                           Rel                    { get; internal set; }
        [JsonProperty] public string                           ApiRelPathSegment      { get; internal set; }
        [JsonProperty] public Type                             ToClrType              { get; internal set; }
        [JsonProperty] public RelationshipCardinality          ToCardinality          { get; internal set; }
        [JsonProperty] public RelationshipCanonicalRelPathMode ToCanonicalRelPathMode { get; internal set; }
        #endregion

        #region IExtensibleObject<T> Implementation
        public IEnumerable<IExtension<IRelationshipInfo>> Extensions => this.ExtensionDictionary.Extensions;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public void AddExtension(IExtension<IRelationshipInfo> extension)
        {
            Contract.Requires(extension != null);

            this.ExtensionDictionary.AddExtension(extension);
        }

        public void RemoveExtension(Type extensionType)
        {
            Contract.Requires(extensionType != null);

            this.ExtensionDictionary.RemoveExtension(extensionType);
        }

        public bool TryGetExtension(Type extensionType, out IExtension<IRelationshipInfo> extension)
        {
            Contract.Requires(extensionType != null);

            return this.ExtensionDictionary.TryGetExtension(extensionType, out extension);
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipInfo()
        {
            this.ExtensionDictionary = new ExtensionDictionary<IRelationshipInfo>(this);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ExtensionDictionary<IRelationshipInfo> ExtensionDictionary { get; }
        #endregion
    }
}