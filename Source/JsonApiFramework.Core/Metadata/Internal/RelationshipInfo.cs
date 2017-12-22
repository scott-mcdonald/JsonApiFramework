// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Metadata.Internal
{
    internal class RelationshipInfo : IRelationshipInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipInfo(string apiRel, RelationshipCardinality apiCardinality, Type clrRelatedResourceType, IClrPropertyBinding clrRelatedResourcePropertyBinding)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);
            Contract.Requires(clrRelatedResourceType != null);

            this.ApiRel = apiRel;
            this.ApiCardinality = apiCardinality;
            this.ClrRelatedResourceType = clrRelatedResourceType;
            this.ClrRelatedResourcePropertyBinding = clrRelatedResourcePropertyBinding;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipInfo Implementation
        public string ApiRel { get; }
        public RelationshipCardinality ApiCardinality { get; }
        public Type ClrRelatedResourceType { get; }
        public IClrPropertyBinding ClrRelatedResourcePropertyBinding { get; }
        #endregion
    }
}