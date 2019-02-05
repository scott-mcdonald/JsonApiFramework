// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.ServiceModel.Configuration
{
    public interface IConfigurationCollection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Add<TComplex>(ComplexTypeBuilder<TComplex> resourceTypeBuilder);

        void Add<TResource>(ResourceTypeBuilder<TResource> resourceTypeBuilder)
            where TResource : class;
        #endregion
    }
}