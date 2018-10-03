// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.ServiceModel.Configuration
{
    public interface IServiceModelBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IConfigurationCollection Configurations { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IComplexTypeBuilder Complex<TComplex>();

        IResourceTypeBuilder<TResource> Resource<TResource>()
            where TResource : class, IResource;

        void HomeResource<TResource>()
            where TResource : class, IResource;
        #endregion
    }
}