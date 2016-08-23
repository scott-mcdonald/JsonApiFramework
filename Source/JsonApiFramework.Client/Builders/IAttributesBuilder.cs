// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Client
{
    public interface IAttributesBuilder<out TParentBuilder, out TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IAttributesBuilder<TParentBuilder, TResource> AddAttribute<TProperty>(IAttributeProxy<TProperty> attributeProxy);

        TParentBuilder AttributesEnd();
        #endregion
    }
}