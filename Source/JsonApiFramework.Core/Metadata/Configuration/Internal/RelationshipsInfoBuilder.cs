// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class RelationshipsInfoBuilder<TResource> : IRelationshipsInfoBuilder<TResource>, IRelationshipsInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsInfoBuilder(IResourceTypeBuilder<TResource> resourceTypeBuilder, IClrPropertyBinding clrRelationshipsPropertyBinding)
        {
            Contract.Requires(resourceTypeBuilder != null);

            this.ResourceTypeBuilder = resourceTypeBuilder;
            this.ClrRelationshipsPropertyBinding = clrRelationshipsPropertyBinding;
            this.RelationshipInfoBuilderList = new List<IRelationshipInfoBuilder<TResource>>();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsInfoBuilder<TResource> Implementation
        public IRelationshipsInfoBuilder<TResource> Relationship<TRelatedResource>(Expression<Func<TResource, TRelatedResource>> clrRelationResourcePropertySelector)
        { return this.Relationship(clrRelationResourcePropertySelector, null); }

        public IRelationshipsInfoBuilder<TResource> Relationship<TRelatedResource>(Expression<Func<TResource, TRelatedResource>> clrRelatedResourcePropertySelector, Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration)
        {
            Contract.Requires(clrRelatedResourcePropertySelector != null);

            var apiCardinality = RelationshipCardinality.ToOne;
            var clrRelatedResourceType = typeof(TRelatedResource);
            if (TypeReflection.IsEnumerableOfT(clrRelatedResourceType, out var enumerableType))
            {
                apiCardinality = RelationshipCardinality.ToMany;
                clrRelatedResourceType = enumerableType;
            }
            var clrRelatedResourcePropertyBinding = Factory<TResource>.CreateClrPropertyBinding(clrRelatedResourcePropertySelector);
            var apiRel = clrRelatedResourcePropertyBinding.ClrPropertyName;

            this.ToRelationship(apiRel, apiCardinality, clrRelatedResourceType, clrRelatedResourcePropertyBinding, configuration);
            return this;
        }

        public IRelationshipsInfoBuilder<TResource> ToManyRelationship<TRelatedResource>()
        {
            this.ToManyRelationship<TRelatedResource>(null);
            return this;
        }

        public IRelationshipsInfoBuilder<TResource> ToManyRelationship<TRelatedResource>(Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration)
        {
            var clrRelatedResourceType = typeof(TRelatedResource);
            this.ToRelationship(null, RelationshipCardinality.ToMany, clrRelatedResourceType, null, configuration);
            return this;
        }

        public IRelationshipsInfoBuilder<TResource> ToOneRelationship<TRelatedResource>()
        {
            this.ToOneRelationship<TRelatedResource>(null);
            return this;
        }

        public IRelationshipsInfoBuilder<TResource> ToOneRelationship<TRelatedResource>(Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration)
        {
            var clrRelatedResourceType = typeof(TRelatedResource);
            this.ToRelationship(null, RelationshipCardinality.ToOne, clrRelatedResourceType, null, configuration);
            return this;
        }
        #endregion

        #region IRelationshipsInfoFactory Implementation
        public IRelationshipsInfo Create(IMetadataConventions metadataConventions)
        {
            var relationshipsInfoCollection = this
                .RelationshipInfoBuilderList
                .Select(x =>
                {
                    var relationshipInfoBuilder = x;
                    var relationshipInfoFactory = (IRelationshipInfoFactory)relationshipInfoBuilder;
                    var relationshipInfo = relationshipInfoFactory.Create(metadataConventions);
                    return relationshipInfo;
                })
                .ToList();
            relationshipsInfoCollection = relationshipsInfoCollection.Distinct(DistinctEqualityComprarer)
                                                                     .ToList();

            var clrRelationshipsPropertyBinding = this.ClrRelationshipsPropertyBinding;
            var relationshipsInfo = new RelationshipsInfo(clrRelationshipsPropertyBinding, relationshipsInfoCollection);
            return relationshipsInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IResourceTypeBuilder<TResource> ResourceTypeBuilder { get; }
        private IClrPropertyBinding ClrRelationshipsPropertyBinding { get; }
        private IList<IRelationshipInfoBuilder<TResource>> RelationshipInfoBuilderList { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ToRelationship(string apiRel, RelationshipCardinality apiCardinality, Type clrRelatedResourceType, IClrPropertyBinding clrRelatedResourcePropertyBinding, Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration)
        {
            Contract.Requires(clrRelatedResourceType != null);

            var relationshipInfoBuilder = new RelationshipInfoBuilder<TResource>(apiRel, apiCardinality, clrRelatedResourceType, clrRelatedResourcePropertyBinding);
            this.RelationshipInfoBuilderList.Add(relationshipInfoBuilder);

            configuration?.Invoke(relationshipInfoBuilder);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly IEqualityComparer<IRelationshipInfo> DistinctEqualityComprarer = new RelationshipInfoEqualityComprarer();
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class RelationshipInfoEqualityComprarer : IEqualityComparer<IRelationshipInfo>
        {
            #region IEqualityComparer<T> Implementation
            public bool Equals(IRelationshipInfo x, IRelationshipInfo y)
            {
                Contract.Requires(x != null);
                Contract.Requires(y != null);

                return String.Compare(x.ApiRel, y.ApiRel, StringComparison.OrdinalIgnoreCase) == 0;
            }

            public int GetHashCode(IRelationshipInfo obj)
            {
                Contract.Requires(obj != null);

                return obj.ApiRel.GetHashCode();
            }
            #endregion
        }
        #endregion
    }
}
