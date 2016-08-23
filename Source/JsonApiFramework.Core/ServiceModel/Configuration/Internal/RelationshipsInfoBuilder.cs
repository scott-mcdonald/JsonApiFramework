// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class RelationshipsInfoBuilder : IRelationshipsInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsInfoBuilder(string clrPropertyName = null)
        {
            var relationshipsInfoFactory = CreateRelationshipsInfoFactory(clrPropertyName);
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
        private static Func<IEnumerable<IRelationshipInfo>, RelationshipsInfo> CreateRelationshipsInfoFactory(string clrPropertyName)
        {
            Func<IEnumerable<IRelationshipInfo>, RelationshipsInfo> relationshipsInfoFactory = (collection) =>
                {
                    var relationshipsInfo = new RelationshipsInfo
                        {
                            // PropertyInfo Properties
                            ClrPropertyName = clrPropertyName,
                            ClrPropertyType = String.IsNullOrWhiteSpace(clrPropertyName) == false
                                ? typeof(Relationships)
                                : null,

                            // RelationshipsInfo Properties
                            Collection = collection.SafeToList(),
                        };
                    return relationshipsInfo;
                };
            return relationshipsInfoFactory;
        }
        #endregion
    }
}