// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Configuration.Internal;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public class ClrTypeBuilder<T> : IClrTypeBuilder<T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IClrTypeBuilder Implementation
        public Type ClrType { get { return typeof(T); } }
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

            attributeInfoConfiguration = new AttributeInfoBuilder(this.ClrType, clrPropertyName, clrPropertyType);
            this.AttributeInfoBuilderDictionary.Add(clrPropertyName, attributeInfoConfiguration);
            this.AttributeInfoBuilderOrder.Add(clrPropertyName);

            return attributeInfoConfiguration;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ClrTypeBuilder()
        { }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal IAttributesInfo CreateAttributesInfo(IConventions conventions)
        {
            var attributesInfoBuilder = new AttributesInfoBuilder(this.ClrType);
            var attributeInfoCollection = this.AttributeInfoBuilderOrder
                                              .EmptyIfNull()
                                              .Select(x =>
                                                  {
                                                      var clrPropertyName = x;
                                                      var attributeInfoConfiguration = this.AttributeInfoBuilderDictionary.Single(y => y.Key == clrPropertyName).Value;
                                                      var attributeInfo = attributeInfoConfiguration.CreateAttributeInfo(conventions);
                                                      return attributeInfo;
                                                  })
                                              .Where(x => x != null)
                                              .ToList();
            var attributesInfo = attributesInfoBuilder.CreateAttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<string, AttributeInfoBuilder> AttributeInfoBuilderDictionary { get; set; }
        private IList<string> AttributeInfoBuilderOrder { get; set; }
        #endregion
    }
}