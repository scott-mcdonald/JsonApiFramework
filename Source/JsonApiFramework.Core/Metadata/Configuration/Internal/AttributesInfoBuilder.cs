// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class AttributesInfoBuilder<TObject> : IAttributesInfoBuilder<TObject>, IAttributesInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributesInfoBuilder()
        {
            this.AttributeInfoBuilderList = new List<IAttributeInfoBuilder<TObject>>();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributesInfoBuilder<TObject, TAttributesInfoBuilder> Implementation
        public IAttributesInfoBuilder<TObject> Attribute<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            return this.Attribute(clrPropertySelector, null);
        }

        public IAttributesInfoBuilder<TObject> Attribute<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector, Func<IAttributeInfoBuilder<TObject>, IAttributeInfoBuilder<TObject>> configuration)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyBinding = Factory<TObject>.CreateClrPropertyBinding(clrPropertySelector);
            var attributeInfoBuilder = new AttributeInfoBuilder<TObject>(clrPropertyBinding);
            this.AttributeInfoBuilderList.Add(attributeInfoBuilder);

            configuration?.Invoke(attributeInfoBuilder);
            return this;
        }
        #endregion

        #region IAttributesInfoFactory Implementation
        public IAttributesInfo Create(IMetadataConventions metadataConventions)
        {
            var attributeInfoCollection = this
                .AttributeInfoBuilderList
                .Select(x =>
                {
                    var attributeInfoBuilder = x;
                    var attributeInfoFactory = (IAttributeInfoFactory)attributeInfoBuilder;
                    var attributeInfo = attributeInfoFactory.Create(metadataConventions);
                    return attributeInfo;
                })
                .Where(x => x != null)
                .ToList();
            attributeInfoCollection = attributeInfoCollection.Distinct(DistinctEqualityComprarer)
                                                             .ToList();

            var attributesInfo = new AttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IList<IAttributeInfoBuilder<TObject>> AttributeInfoBuilderList { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly IEqualityComparer<IAttributeInfo> DistinctEqualityComprarer = new AttributeInfoEqualityComprarer();
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class AttributeInfoEqualityComprarer : IEqualityComparer<IAttributeInfo>
        {
            #region IEqualityComparer<T> Implementation
            public bool Equals(IAttributeInfo x, IAttributeInfo y)
            {
                Contract.Requires(x != null);
                Contract.Requires(y != null);

                return String.Compare(x.ApiAttributeName, y.ApiAttributeName, StringComparison.OrdinalIgnoreCase) == 0;
            }

            public int GetHashCode(IAttributeInfo obj)
            {
                Contract.Requires(obj != null);

                return obj.ApiAttributeName.GetHashCode();
            }
            #endregion
        }
        #endregion
    }
}
