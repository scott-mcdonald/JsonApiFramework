// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class RelationshipsInfo : PropertyInfo
        , IRelationshipsInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsInfo(IEnumerable<IRelationshipInfo> collection)
        {
            Contract.Requires(collection != null);

            this.Collection = collection.SafeToList();
        }

        public RelationshipsInfo(Type clrDeclaringType, string clrRelationshipsPropertyName, IEnumerable<IRelationshipInfo> collection)
            : base(clrDeclaringType, clrRelationshipsPropertyName, typeof(Relationships))
        {
            Contract.Requires(collection != null);

            this.Collection = collection.SafeToList();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipsInfo Implementation
        [JsonProperty] public IEnumerable<IRelationshipInfo> Collection { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsInfo Implementation
        public IRelationshipInfo GetRelationshipInfo(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            IRelationshipInfo relationship;
            if (this.TryGetRelationshipInfo(rel, out relationship))
                return relationship;

            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), this.ClrDeclaringType.Name);
            var relationshipDescription = "{0} [rel={1}]".FormatWith(typeof(Relationship).Name, rel);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(resourceTypeDescription, relationshipDescription);
            throw new ServiceModelException(detail);
        }

        public bool TryGetRelationshipInfo(string rel, out IRelationshipInfo relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            relationship = this.Collection.SingleOrDefault(x => x.Rel == rel);
            return relationship != null;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsInfo()
        { }
        #endregion
    }
}