// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.Client.Internal;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.Client
{
    public static class AttributesBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IAttributesBuilder<TParentBuilder, TResource> AddAttribute<TParentBuilder, TResource, TProperty>(this IAttributesBuilder<TParentBuilder, TResource> attributesBuilder, string propertyName, TProperty propertyValue)
            where TParentBuilder : class
            where TResource : class
        {
            Contract.Requires(attributesBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var attribute = new AttributeProxy<TProperty>(propertyName, propertyValue);
            return attributesBuilder.AddAttribute(attribute);
        }

        public static IAttributesBuilder<TParentBuilder, TResource> AddAttribute<TParentBuilder, TResource, TProperty>(this IAttributesBuilder<TParentBuilder, TResource> attributesBuilder, Expression<Func<TResource, object>> propertySelector, TProperty propertyValue)
            where TParentBuilder : class
            where TResource : class
        {
            Contract.Requires(attributesBuilder != null);
            Contract.Requires(propertySelector != null);

            var propertyName = StaticReflection.GetMemberName(propertySelector);
            var attribute = new AttributeProxy<TProperty>(propertyName, propertyValue);
            return attributesBuilder.AddAttribute(attribute);
        }
        #endregion
    }
}