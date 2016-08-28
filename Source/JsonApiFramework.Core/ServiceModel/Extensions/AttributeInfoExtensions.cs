// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel
{
    public static class AttributeInfoExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static ApiProperty GetApiAttribute(this IAttributeInfo attributeInfo, IGetAttributes apiResource)
        {
            Contract.Requires(attributeInfo != null);

            if (apiResource == null || apiResource.Attributes == null)
                return null;

            var apiAttributeName = attributeInfo.ApiPropertyName;

            ApiProperty apiAttribute;
            return apiResource.Attributes.TryGetApiProperty(apiAttributeName, out apiAttribute)
                ? apiAttribute
                : null;
        }
        #endregion
    }
}
