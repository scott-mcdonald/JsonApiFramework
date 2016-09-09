// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Configuration.Internal;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public class ComplexTypeBuilder<TComplex>
        : IComplexTypeBuilder<TComplex>
        , IComplexTypeFactory
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IComplexTypeBuilder Implementation
        public Type ClrComplexType { get { return typeof(TComplex); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IComplexTypeBuilder Implementation
        public IAttributeInfoBuilder Attribute(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            this.AttributeInfoBuilderDictionary = this.AttributeInfoBuilderDictionary ?? new Dictionary<string, AttributeInfoBuilder>();
            this.AttributeInfoBuilderOrder = this.AttributeInfoBuilderOrder ?? new List<string>();

            AttributeInfoBuilder attributeInfoConfiguration;
            if (this.AttributeInfoBuilderDictionary.TryGetValue(clrPropertyName, out attributeInfoConfiguration))
            {
                return attributeInfoConfiguration;
            }

            attributeInfoConfiguration = new AttributeInfoBuilder(this.ClrComplexType, clrPropertyName, clrPropertyType);
            this.AttributeInfoBuilderDictionary.Add(clrPropertyName, attributeInfoConfiguration);
            this.AttributeInfoBuilderOrder.Add(clrPropertyName);

            return attributeInfoConfiguration;
        }

        #endregion

        #region IComplexTypeFactory Implementation
        public IComplexType Create(IConventions conventions)
        {
            // Apply all ComplexType level conventions if any.
            this.ApplyComplexTypeConventions(conventions);

            // Create all the ComplexType parameters needed to construct a ComplexType metadata object.
            var clrComplexType = this.ClrComplexType;
            var attributesInfo = this.CreateAttributesInfo(conventions);

            var complexType = new ComplexType(clrComplexType, attributesInfo);
            return complexType;
        }
        #endregion

        // PROTECTED/INTERNAL CONSTRUCTORS //////////////////////////////////
        #region Constructors
        protected internal ComplexTypeBuilder()
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private AttributesInfoBuilder AttributesInfoBuilder { get; set; }

        private IDictionary<string, AttributeInfoBuilder> AttributeInfoBuilderDictionary { get; set; }
        private IList<string> AttributeInfoBuilderOrder { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyComplexTypeConventions(IConventions conventions)
        {
            if (conventions == null)
                return;

            var complexTypeConventions = conventions.ComplexTypeConventions ?? Enumerable.Empty<IComplexTypeConvention>();
            foreach (var resourceTypeConvention in complexTypeConventions)
            {
                resourceTypeConvention.Apply(this);
            }
        }

        private IAttributesInfo CreateAttributesInfo(IConventions conventions)
        {
            this.AttributesInfoBuilder = this.AttributesInfoBuilder ?? new AttributesInfoBuilder(this.ClrComplexType);

            var attributeInfoCollection = this.AttributeInfoBuilderOrder
                                              .EmptyIfNull()
                                              .Select(x =>
                                                  {
                                                      var clrPropertyName = x;
                                                      var attributeInfoConfiguration = this.AttributeInfoBuilderDictionary.Single(y => y.Key == clrPropertyName).Value;
                                                      var attributeInfo = attributeInfoConfiguration.CreateAttributeInfo(conventions);
                                                      return attributeInfo;
                                                  })
                                              .ToList();
            var attributesInfo = this.AttributesInfoBuilder.CreateAttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }
        #endregion
    }
}