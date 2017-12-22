// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using JsonApiFramework.Expressions;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.Metadata.Internal
{
    internal static class Factory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IAttributeInfo CreateAttributeInfo(string apiAttributeName, IClrPropertyBinding clrPropertyBinding)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiAttributeName) == false);
            Contract.Requires(clrPropertyBinding != null);

            var attributeInfo = new AttributeInfo(apiAttributeName, clrPropertyBinding);
            return attributeInfo;
        }

        public static AttributesInfo CreateAttributesInfo(IEnumerable<IAttributeInfo> attributeInfoCollection)
        {
            Contract.Requires(attributeInfoCollection != null);

            var attributesInfo = new AttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }

        public static AttributesInfo CreateAttributesInfo(params IAttributeInfo[] attributeInfoCollection)
        {
            Contract.Requires(attributeInfoCollection != null);

            var attributesInfo = Factory.CreateAttributesInfo(attributeInfoCollection.AsEnumerable());
            return attributesInfo;
        }

        public static IClrPropertyBinding CreateClrPropertyBinding(string clrPropertyName, Type clrPropertyType, Type clrObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(clrObjectType != null);

            var clrPropertyGetter = Factory.CreatePropertyGetter(clrObjectType, clrPropertyType, clrPropertyName);
            var clrPropertySetter = Factory.CreatePropertySetter(clrObjectType, clrPropertyType, clrPropertyName);
            var clrPropertyBinding = new ClrPropertyBinding(clrPropertyName, clrPropertyType, clrPropertyGetter, clrPropertySetter);
            return clrPropertyBinding;
        }

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

        public static ILinksInfo CreateLinksInfo()
        {
            var linksInfo = new LinksInfo(null);
            return linksInfo;
        }

        public static ILinksInfo CreateLinksInfo(IClrPropertyBinding clrLinksPropertyBinding)
        {
            var linksInfo = new LinksInfo(clrLinksPropertyBinding);
            return linksInfo;
        }

        public static IClrPropertyGetter CreatePropertyGetter(Type clrObjectType, Type clrPropertyType, string clrPropertyName)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertyGetterClosedGenericType = PropertyGetterOpenGenericType.MakeGenericType(clrObjectType, clrPropertyType);
            var propertyGetterNewExpression = ExpressionBuilder.New<string, IClrPropertyGetter>(propertyGetterClosedGenericType);
            var propertyGetterNewLambda = propertyGetterNewExpression.Compile();
            var propertyGetter = propertyGetterNewLambda(clrPropertyName);
            return propertyGetter;
        }

        public static IClrPropertySetter CreatePropertySetter(Type clrObjectType, Type clrPropertyType, string clrPropertyName)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertySetterClosedGenericType = PropertySetterOpenGenericType.MakeGenericType(clrObjectType, clrPropertyType);
            var propertySetterNewExpression = ExpressionBuilder.New<string, IClrPropertySetter>(propertySetterClosedGenericType);
            var propertySetterNewLambda = propertySetterNewExpression.Compile();
            var propertySetter = propertySetterNewLambda(clrPropertyName);
            return propertySetter;
        }

        public static IRelationshipInfo CreateRelationshipInfo(string apiRel, RelationshipCardinality apiCardinality, Type clrToType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);
            Contract.Requires(clrToType != null);

            var relationshipInfo = new RelationshipInfo(apiRel, apiCardinality, clrToType, null);
            return relationshipInfo;
        }

        public static IRelationshipInfo CreateRelationshipInfo(string apiRel, RelationshipCardinality apiCardinality, Type clrToType, IClrPropertyBinding clrToPropertyBinding)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);
            Contract.Requires(clrToType != null);

            var relationshipInfo = new RelationshipInfo(apiRel, apiCardinality, clrToType, clrToPropertyBinding);
            return relationshipInfo;
        }

        public static IRelationshipInfo CreateRelationshipInfo<TRelatedResource>(string apiRel, RelationshipCardinality apiCardinality)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);

            var clrToType = typeof(TRelatedResource);
            var relationshipInfo = new RelationshipInfo(apiRel, apiCardinality, clrToType, null);
            return relationshipInfo;
        }

        public static IRelationshipsInfo CreateRelationshipsInfo(IEnumerable<IRelationshipInfo> relationshipInfoCollection)
        {
            Contract.Requires(relationshipInfoCollection != null);

            var relationshipsInfo = new RelationshipsInfo(null, relationshipInfoCollection);
            return relationshipsInfo;
        }

        public static IRelationshipsInfo CreateRelationshipsInfo(IClrPropertyBinding clrRelationshipsPropertyBinding, IEnumerable<IRelationshipInfo> relationshipInfoCollection)
        {
            Contract.Requires(relationshipInfoCollection != null);

            var relationshipsInfo = new RelationshipsInfo(clrRelationshipsPropertyBinding, relationshipInfoCollection);
            return relationshipsInfo;
        }

        public static IRelationshipsInfo CreateRelationshipsInfo(params IRelationshipInfo[] relationshipInfoCollection)
        {
            Contract.Requires(relationshipInfoCollection != null);

            return new RelationshipsInfo(null, relationshipInfoCollection.AsEnumerable());
        }

        public static IRelationshipsInfo CreateRelationshipsInfo(IClrPropertyBinding clrRelationshipsPropertyBinding, params IRelationshipInfo[] relationshipInfoCollection)
        {
            Contract.Requires(relationshipInfoCollection != null);

            return new RelationshipsInfo(clrRelationshipsPropertyBinding, relationshipInfoCollection.AsEnumerable());
        }

        public static IResourceIdentityInfo CreateResourceIdentityInfo(string apiType, IClrPropertyBinding clrIdPropertyBinding)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiType) == false);
            Contract.Requires(clrIdPropertyBinding != null);

            var resourceIdentityInfo = new ResourceIdentityInfo(apiType, clrIdPropertyBinding);
            return resourceIdentityInfo;
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

        public static IServiceModel CreateServiceModel(string name, IEnumerable<IComplexType> complexTypes, IEnumerable<IResourceType> resourceTypes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);
            Contract.Requires(complexTypes != null);
            Contract.Requires(resourceTypes != null);

            var serviceModel = new ServiceModel(name, complexTypes, resourceTypes);
            return serviceModel;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly Type ComplexTypeOpenGenericType = typeof(ComplexType<>);
        private static readonly Type PropertyGetterOpenGenericType = typeof(ClrPropertyGetter<,>);
        private static readonly Type PropertySetterOpenGenericType = typeof(ClrPropertySetter<,>);
        private static readonly Type ResourceTypeOpenGenericType = typeof(ResourceType<>);
        #endregion
    }

    internal static class Factory<T>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IAttributeInfo CreateAttributeInfo<TProperty>(string apiAttributeName, Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiAttributeName) == false);
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyBinding = CreateClrPropertyBinding(clrPropertySelector);
            var attributeInfo = new AttributeInfo(apiAttributeName, clrPropertyBinding);
            return attributeInfo;
        }

        public static IClrPropertyBinding CreateClrPropertyBinding<TProperty>(Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            var clrPropertyType = typeof(TProperty);
            var clrPropertyGetter = new ClrPropertyGetter<T, TProperty>(clrPropertyName);
            var clrPropertySetter = new ClrPropertySetter<T, TProperty>(clrPropertyName);
            var clrPropertyBinding = new ClrPropertyBinding(clrPropertyName, clrPropertyType, clrPropertyGetter, clrPropertySetter);
            return clrPropertyBinding;
        }

        public static ILinksInfo CreateLinksInfo(Expression<Func<T, Links>> clrLinksPropertySelector)
        {
            Contract.Requires(clrLinksPropertySelector != null);

            var clrPropertyBinding = CreateClrPropertyBinding(clrLinksPropertySelector);
            var linksInfo = new LinksInfo(clrPropertyBinding);
            return linksInfo;
        }

        public static IRelationshipInfo CreateRelationshipInfo<TProperty>(string apiRel, Expression<Func<T, TProperty>> clrToPropertySelector)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);
            Contract.Requires(clrToPropertySelector != null);

            var clrToPropertyType = typeof(TProperty);
            var isCollection = TypeReflection.IsEnumerableOfT(clrToPropertyType, out var clrToCollectionItemType);
            var apiCardinality = isCollection ? RelationshipCardinality.ToMany : RelationshipCardinality.ToOne;
            var clrToType = isCollection ? clrToCollectionItemType : clrToPropertyType;
            var clrToPropertyBinding = CreateClrPropertyBinding(clrToPropertySelector);

            var relationshipInfo = new RelationshipInfo(apiRel, apiCardinality, clrToType, clrToPropertyBinding);
            return relationshipInfo;
        }

        public static IRelationshipsInfo CreateRelationshipsInfo(Expression<Func<T, Relationships>> clrRelationshipsPropertySelector, params IRelationshipInfo[] relationshipInfoCollection)
        {
            Contract.Requires(clrRelationshipsPropertySelector != null);
            Contract.Requires(relationshipInfoCollection != null);

            var clrRelationshipsPropertyBinding = CreateClrPropertyBinding(clrRelationshipsPropertySelector);
            var relationshipsInfo = new RelationshipsInfo(clrRelationshipsPropertyBinding, relationshipInfoCollection.AsEnumerable());
            return relationshipsInfo;
        }

        public static IResourceIdentityInfo CreateResourceIdentityInfo<TIdProperty>(string apiType, Expression<Func<T, TIdProperty>> clrIdPropertySelector)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiType) == false);
            Contract.Requires(clrIdPropertySelector != null);

            var clrIdPropertyBinding = CreateClrPropertyBinding(clrIdPropertySelector);
            var resourceIdentityInfo = new ResourceIdentityInfo(apiType, clrIdPropertyBinding);
            return resourceIdentityInfo;
        }
        #endregion
    }
}