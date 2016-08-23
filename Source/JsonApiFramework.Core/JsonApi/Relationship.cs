// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant relationship.
    /// </summary>
    [JsonConverter(typeof(RelationshipConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public class Relationship : JsonObject
        , IGetLinks
        , IGetMeta
        , ISetLinks
        , ISetMeta
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        [JsonProperty(Keywords.Links)] public Links Links { get; set; }
        [JsonProperty(Keywords.Meta)] public Meta Meta { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var self = this.Links != null
                ? this.Links.Self ?? Link.Empty
                : Link.Empty;

            var related = this.Links != null
                ? this.Links.Related ?? Link.Empty
                : Link.Empty;

            return String.Format("{0} [self={1} related={2}]", TypeName, self, related);
        }
        #endregion

        #region Relationship Overrides
        /// <summary>Adds to the "to-many" resource linkage on this relationship.</summary>
        public virtual void AddToManyResourceLinkage(Resource resource)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToManyRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail); }

        /// <summary>Adds to the "to-many" resource linkage on this relationship.</summary>
        public virtual void AddToManyResourceLinkageRange(IEnumerable<Resource> resourceCollection)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToManyRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail); }

        /// <summary>Adds to the "to-many" resource linkage on this relationship.</summary>
        public virtual void AddToManyResourceLinkage(ResourceIdentifier resourceIdentifier)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToManyRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail); }

        /// <summary>Adds to the "to-many" resource linkage on this relationship.</summary>
        public virtual void AddToManyResourceLinkageRange(IEnumerable<ResourceIdentifier> resourceIdentifierCollection)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToManyRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierCollectionDetail); }

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

        /// <summary>Sets the "to-one" resource linkage on this relationship.</summary>
        public virtual void SetToOneResourceLinkage(Resource resource)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToOneRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierDetail); }

        /// <summary>Sets the "to-one" resource linkage on this relationship.</summary>
        public virtual void SetToOneResourceLinkage(ResourceIdentifier resourceIdentifier)
        { throw new JsonApiException(CoreErrorStrings.RelationshipNotToOneRelatioshipTitle, CoreErrorStrings.RelationshipDoesNotContainDataMemberAsResourceIdentifierDetail); }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly Relationship Empty = new Relationship();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Relationship).Name;
        #endregion
    }
}