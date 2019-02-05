// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class RelationshipInfoBuilder<TResource> : IRelationshipInfoBuilder<TResource>
        where TResource : class
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipInfoBuilder(string rel, Type toClrType, RelationshipCardinality toCardinality)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(toClrType != null);

            var relationshipInfoFactory = CreateRelationshipInfoFactory(rel, toClrType, toCardinality);
            this.RelationshipInfoFactory = relationshipInfoFactory;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipInfoBuilder<TResource> Implementation
        public IRelationshipInfoBuilder<TResource> SetApiRelPathSegment(string apiRelPathSegment)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRelPathSegment) == false);

            this.RelationshipInfoModifierCollection = this.RelationshipInfoModifierCollection ?? new List<Action<RelationshipInfo>>();
            this.RelationshipInfoModifierCollection.Add(x => { x.ApiRelPathSegment = apiRelPathSegment; });
            return this;
        }

        public IRelationshipInfoBuilder<TResource> SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode toCanonicalRelPathMode)
        {
            this.RelationshipInfoModifierCollection = this.RelationshipInfoModifierCollection ?? new List<Action<RelationshipInfo>>();
            this.RelationshipInfoModifierCollection.Add(x => { x.ToCanonicalRelPathMode = toCanonicalRelPathMode; });
            return this;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IRelationshipInfo CreateRelationshipInfo()
        {
            var relationshipInfo = this.RelationshipInfoFactory();

            if (this.RelationshipInfoModifierCollection == null)
                return relationshipInfo;

            foreach (var relationshipInfoModifier in this.RelationshipInfoModifierCollection)
            {
                relationshipInfoModifier(relationshipInfo);
            }

            return relationshipInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<RelationshipInfo> RelationshipInfoFactory { get; set; }
        private IList<Action<RelationshipInfo>> RelationshipInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<RelationshipInfo> CreateRelationshipInfoFactory(string rel, Type toClrType, RelationshipCardinality toCardinality)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(toClrType != null);

            return () => new RelationshipInfo(rel, rel, toClrType, toCardinality, default(RelationshipCanonicalRelPathMode));
        }
        #endregion
    }
}