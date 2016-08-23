// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.ServiceModel.Conventions.Internal
{
    /// <summary>
    /// Discovery and configuration of the hypermedia metadata of a CLR resource.
    /// </summary>
    internal class HypermediaDiscoveryConvention : IResourceTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceTypeConvention Implementation
        public IResourceTypeBuilder Apply(IResourceTypeBuilder resourceTypeConfiguration)
        {
            Contract.Requires(resourceTypeConfiguration != null);

            // Can leverage the default behavior of the HypermediaInfoConfiguration
            // constructor to use the CLR type name for the current hypermedia metadata configuration.
            resourceTypeConfiguration.Hypermedia();
            return resourceTypeConfiguration;
        }
        #endregion
    }
}