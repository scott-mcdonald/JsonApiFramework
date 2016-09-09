// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class RelationshipsInfoBuilder : IRelationshipsInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsInfoBuilder(Type clrDeclaringType, string clrPropertyName = null)
        {
            Contract.Requires(clrDeclaringType != null);

            var relationshipsInfoFactory = CreateRelationshipsInfoFactory(clrDeclaringType, clrPropertyName);
            this.RelationshipsInfoFactory = relationshipsInfoFactory;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IRelationshipsInfo CreateRelationshipsInfo(IEnumerable<IRelationshipInfo> collection)
        {
            var relationshipsInfo = this.RelationshipsInfoFactory(collection);

            if (this.RelationshipsInfoModifierCollection == null)
                return relationshipsInfo;

            foreach (var relationshipsInfoModifier in this.RelationshipsInfoModifierCollection)
            {
                relationshipsInfoModifier(relationshipsInfo);
            }

            return relationshipsInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IEnumerable<IRelationshipInfo>, RelationshipsInfo> RelationshipsInfoFactory { get; set; }
        private IList<Action<RelationshipsInfo>> RelationshipsInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IEnumerable<IRelationshipInfo>, RelationshipsInfo> CreateRelationshipsInfoFactory(Type clrDeclaringType, string clrPropertyName)
        {
            Contract.Requires(clrDeclaringType != null);

            Func<IEnumerable<IRelationshipInfo>, RelationshipsInfo> relationshipsInfoFactory = (collection) =>
                {
                    var collectionAsList = collection.SafeToList();

                    var isNotPartOfResource = String.IsNullOrWhiteSpace(clrPropertyName);
                    var relationshipsInfo = isNotPartOfResource
                        ? new RelationshipsInfo(collectionAsList)
                        : new RelationshipsInfo(clrDeclaringType, clrPropertyName, collectionAsList);
                    return relationshipsInfo;
                };
            return relationshipsInfoFactory;
        }
        #endregion
    }
}