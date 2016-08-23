// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>IGetRelationships</c> interface.</summary>
    public static class GetRelationshipsExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static bool HasRelationships(this IGetRelationships getRelationships)
        {
            Contract.Requires(getRelationships != null);

            return getRelationships.Relationships != null;
        }

        public static Relationship GetRelationship(this IGetRelationships getRelationships, string rel)
        {
            Contract.Requires(getRelationships != null);

            var relationships = getRelationships.Relationships;
            if (relationships != null)
                return relationships.GetRelationship(rel);

            throw new RelationshipNotFoundException(rel);
        }

        public static bool TryGetRelationship(this IGetRelationships getRelationships, string rel, out Relationship relationship)
        {
            Contract.Requires(getRelationships != null);

            var relationships = getRelationships.Relationships;
            if (relationships != null)
                return relationships.TryGetRelationship(rel, out relationship);

            relationship = null;
            return false;
        }
        #endregion
    }
}
