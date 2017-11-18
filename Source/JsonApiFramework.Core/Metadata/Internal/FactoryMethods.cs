// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;

namespace JsonApiFramework.Metadata.Internal
{
    internal static class FactoryMethods
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static IComplexType CreateComplexType(Type clrType, IAttributesInfo attributesInfo)
        {
            Contract.Requires(clrType != null);
            Contract.Requires(attributesInfo != null);

            var complexTypeClosedGenericType = ComplexTypeOpenGenericType.MakeGenericType(clrType);
            var complexTypeNewExpression = ExpressionBuilder.New<IAttributesInfo, IComplexType>(complexTypeClosedGenericType);
            var complexTypeNewLambda = complexTypeNewExpression.Compile();
            var complexType = complexTypeNewLambda(attributesInfo);
            return complexType;
        }

        public static IPropertyGetter CreatePropertyGetter(Type clrDeclaringType, Type clrPropertyType, string clrPropertyName)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertyGetterClosedGenericType = PropertyGetterOpenGenericType.MakeGenericType(clrDeclaringType, clrPropertyType);
            var propertyGetterNewExpression = ExpressionBuilder.New<string, IPropertyGetter>(propertyGetterClosedGenericType);
            var propertyGetterNewLambda = propertyGetterNewExpression.Compile();
            var propertyGetter = propertyGetterNewLambda(clrPropertyName);
            return propertyGetter;
        }

        public static IPropertySetter CreatePropertySetter(Type clrDeclaringType, Type clrPropertyType, string clrPropertyName)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertySetterClosedGenericType = PropertySetterOpenGenericType.MakeGenericType(clrDeclaringType, clrPropertyType);
            var propertySetterNewExpression = ExpressionBuilder.New<string, IPropertySetter>(propertySetterClosedGenericType);
            var propertySetterNewLambda = propertySetterNewExpression.Compile();
            var propertySetter = propertySetterNewLambda(clrPropertyName);
            return propertySetter;
        }

        public static IResourceType CreateResourceType(Type clrType, IResourceIdentityInfo resourceIdentityInfo, IAttributesInfo attributesInfo, IRelationshipsInfo relationshipsInfo, ILinksInfo linksInfo)
        {
            Contract.Requires(clrType != null);
            Contract.Requires(resourceIdentityInfo != null);
            Contract.Requires(attributesInfo != null);
            Contract.Requires(relationshipsInfo != null);
            Contract.Requires(linksInfo != null);

            var resourceTypeClosedGenericType = ResourceTypeOpenGenericType.MakeGenericType(clrType);
            var resourceTypeNewExpression = ExpressionBuilder.New<IResourceIdentityInfo, IAttributesInfo, IRelationshipsInfo, ILinksInfo, IResourceType>(resourceTypeClosedGenericType);
            var resourceTypeNewLambda = resourceTypeNewExpression.Compile();
            var resourceType = resourceTypeNewLambda(resourceIdentityInfo, attributesInfo, relationshipsInfo, linksInfo);
            return resourceType;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly Type ComplexTypeOpenGenericType = typeof(ComplexType<>);
        private static readonly Type PropertyGetterOpenGenericType = typeof(PropertyGetter<,>);
        private static readonly Type PropertySetterOpenGenericType = typeof(PropertySetter<,>);
        private static readonly Type ResourceTypeOpenGenericType = typeof(ResourceType<>);
        #endregion
    }
}