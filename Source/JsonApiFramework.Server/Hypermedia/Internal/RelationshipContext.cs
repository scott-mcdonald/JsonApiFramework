// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class RelationshipContext : IRelationshipContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipContext(string rel, IEnumerable<ILinkContext> linkContexts, Meta meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Rel = rel;
            this.LinkContexts = linkContexts.EmptyIfNull();
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipContext Implementation
        public string Rel { get; private set; }
        public IEnumerable<ILinkContext> LinkContexts { get; private set; }
        public Meta Meta { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipContext Implementation
        public virtual RelationshipType GetRelationshipType()
        { return RelationshipType.Relationship; }

        public virtual ResourceIdentifier GetToOneResourceLinkage()
        {
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildIncorrectResourceLinkage
                                           .FormatWith("to-one", this.Rel);
            throw new DocumentBuildException(detail);
        }

        public virtual IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        {
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildIncorrectResourceLinkage
                                           .FormatWith("to-many", this.Rel);
            throw new DocumentBuildException(detail);
        }
        #endregion
    }
}
