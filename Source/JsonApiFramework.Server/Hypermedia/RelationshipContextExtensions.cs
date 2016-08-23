// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class RelationshipContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static bool HasLinks(this IRelationshipContext relationshipContext)
        {
            Contract.Requires(relationshipContext != null);

            return relationshipContext.LinkContexts != null && relationshipContext.LinkContexts.Any();
        }
        #endregion
    }
}
