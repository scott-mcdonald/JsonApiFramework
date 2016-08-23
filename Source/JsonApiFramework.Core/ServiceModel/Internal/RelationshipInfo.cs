// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class RelationshipInfo : InfoObject
        , IRelationshipInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipInfo(string rel, string apiRelPathSegment, Type toClrType, RelationshipCardinality toCardinality, RelationshipCanonicalRelPathMode toCanonicalRelPathMode)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiRelPathSegment) == false);
            Contract.Requires(toClrType != null);

            this.Rel = rel;
            this.ApiRelPathSegment = apiRelPathSegment;
            this.ToClrType = toClrType;
            this.ToCardinality = toCardinality;
            this.ToCanonicalRelPathMode = toCanonicalRelPathMode;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipInfo Implementation
        [JsonProperty] public string Rel { get; internal set; }
        [JsonProperty] public string ApiRelPathSegment { get; internal set; }
        [JsonProperty] public Type ToClrType { get; internal set; }
        [JsonProperty] public RelationshipCardinality ToCardinality { get; internal set; }
        [JsonProperty] public RelationshipCanonicalRelPathMode ToCanonicalRelPathMode { get; internal set; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipInfo()
        { }
        #endregion
    }
}