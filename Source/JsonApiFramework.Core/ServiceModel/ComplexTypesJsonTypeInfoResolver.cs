// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace JsonApiFramework.ServiceModel;

public class ComplexTypesJsonTypeInfoResolver: DefaultJsonTypeInfoResolver
{
    // PUBLIC CONSRUCTORS ///////////////////////////////////////////////
    #region Constructors
    public ComplexTypesJsonTypeInfoResolver(IServiceModel serviceModel)
    {
        Contract.Requires(serviceModel != null);

        this.ServiceModel = serviceModel;
    }
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region DefaultContractResolver Overrides
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        if (!this.ServiceModel.TryGetComplexType(type, out IComplexType complexType))
            return typeInfo;

        var complexTypeProperties = typeInfo
            .Properties
            .Select(property =>
                {
                    var clrPropertyName = property.Name;

                    IAttributeInfo attributeInfo;
                    if (complexType.TryGetClrAttributeInfo(clrPropertyName, out attributeInfo))
                    {
                        var apiPropertyName = attributeInfo.ApiPropertyName;
                        property.Name = apiPropertyName;
                    }

                    return property;
                });
        typeInfo.Properties.Clear();
        foreach (JsonPropertyInfo propertyInfo in complexTypeProperties)
        {
            typeInfo.Properties.Add(propertyInfo);
        }
        return typeInfo;
    }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private IServiceModel ServiceModel { get; set; }
    #endregion
}