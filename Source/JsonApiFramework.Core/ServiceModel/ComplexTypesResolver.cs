// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonApiFramework.ServiceModel
{
    public class ComplexTypesContractResolver: DefaultContractResolver
    {
        // PUBLIC CONSRUCTORS ///////////////////////////////////////////////
        #region Constructors
        public ComplexTypesContractResolver(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            this.ServiceModel = serviceModel;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region DefaultContractResolver Overrides
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            IComplexType complexType;
            if (!this.ServiceModel.TryGetComplexType(type, out complexType))
                return properties;

            var complexTypeProperties = properties
                .Select(property =>
                    {
                        var clrPropertyName = property.PropertyName;

                        IAttributeInfo attributeInfo;
                        if (complexType.TryGetClrAttributeInfo(clrPropertyName, out attributeInfo))
                        {
                            var apiPropertyName = attributeInfo.ApiPropertyName;
                            property.PropertyName = apiPropertyName;
                        }

                        return property;
                    })
                .ToList();
            return complexTypeProperties;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IServiceModel ServiceModel { get; set; }
        #endregion
    }
}