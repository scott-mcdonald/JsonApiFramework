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
        public RelationshipInfo(string rel, RelationshipCardinality toCardinality, Type toClrType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(toClrType != null);

            this.Rel = rel;
            this.ToCardinality = toCardinality;
            this.ToClrType = toClrType;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IProperty Implementation
        public string Rel { get; }
        public RelationshipCardinality ToCardinality { get; }
        public Type ToClrType { get; }
        #endregion
    }
}