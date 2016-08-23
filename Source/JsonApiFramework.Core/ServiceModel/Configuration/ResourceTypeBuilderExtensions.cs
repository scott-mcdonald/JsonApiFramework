// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public static class ResourceTypeBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IResourceIdentityInfoBuilder ResourceIdentity<TResource, TProperty>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, TProperty>> clrPropertySelector)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyType = typeof(TProperty);
            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return resourceTypeBuilder.ResourceIdentity(clrPropertyName, clrPropertyType);
        }

        public static IAttributeInfoBuilder Attribute<TResource, TProperty>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, TProperty>> clrPropertySelector)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyType = typeof(TProperty);
            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return resourceTypeBuilder.Attribute(clrPropertyName, clrPropertyType);
        }

        public static IRelationshipsInfoBuilder Relationships<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Relationships>> clrPropertySelector)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);

            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return resourceTypeBuilder.Relationships(clrPropertyName);
        }

        public static ILinksInfoBuilder Links<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Links>> clrPropertySelector)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);

            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return resourceTypeBuilder.Links(clrPropertyName);
        }

        public static IMetaInfoBuilder Meta<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Meta>> clrPropertySelector)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);

            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return resourceTypeBuilder.Meta(clrPropertyName);
        }
        #endregion
    }
}
