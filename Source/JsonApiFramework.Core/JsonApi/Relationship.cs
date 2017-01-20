// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api relationship object.</summary>
    public class Relationship
        : IGetLinks
        , IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Relationship(Links links)
            : this(null, links)
        { }

        public Relationship(Meta meta)
            : this(meta, null)
        { }

        public Relationship(Meta meta, Links links)
        {
            this.Meta = meta;
            this.Links = links;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public Meta Meta { get; }
        public Links Links { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var links = this.Links ?? Links.Empty;
            return $"{TypeName} [links={links}]";
        }
        #endregion

        #region Relationship Overrides
        /// <summary>Gets the relationship type of this relationship.</summary>
        public virtual RelationshipType GetRelationshipType()
        { return RelationshipType.Relationship; }

        /// <summary>Gets the "to-one" resource linkage from this relationship.</summary>
        public virtual ResourceIdentifier GetToOneResourceLinkage()
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToOneRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierDetail); }

        /// <summary>Gets the "to-many" resource linkage from this relationship.</summary>
        public virtual IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToOneRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail); }

        /// <summary>Returns if this relationship resource linkage is null or empty.</summary>
        public virtual bool IsResourceLinkageNullOrEmpty()
        { return true; }

        /// <summary>Returns if this relationship is a "to-one" relationship.</summary>
        public virtual bool IsToOneRelationship()
        { return false; }

        /// <summary>Returns if this relationship is a "to-many" relationship.</summary>
        public virtual bool IsToManyRelationship()
        { return false; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Relationship).Name;
        #endregion
    }
}