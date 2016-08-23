// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel.Conventions.Internal;

namespace JsonApiFramework.ServiceModel.Conventions
{
    public class ConventionSetBuilder : IConventionSetBuilder, IConventionSetFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IConventionSetBuilder Implementation
        public INamingConventionsBuilder ApiAttributeNamingConventions()
        {
            this.ApiAttributeNamingConventionsBuilder = this.ApiAttributeNamingConventionsBuilder ?? new NamingConventionsBuilder();
            return this.ApiAttributeNamingConventionsBuilder;
        }

        public INamingConventionsBuilder ApiTypeNamingConventions()
        {
            this.ApiTypeNamingConventionsBuilder = this.ApiTypeNamingConventionsBuilder ?? new NamingConventionsBuilder();
            return this.ApiTypeNamingConventionsBuilder;
        }

        public IResourceTypeConventionsBuilder ResourceTypeConventions()
        {
            this.ResourceTypeConventionsBuilder = this.ResourceTypeConventionsBuilder ?? new ResourceTypeConventionsBuilder();
            return this.ResourceTypeConventionsBuilder;
        }
        #endregion

        #region IConventionSetFactory Implementation
        public ConventionSet Create()
        {
            var apiAttributeNamingConventions = this.ApiAttributeNamingConventionsBuilder != null
                ? this.ApiAttributeNamingConventionsBuilder.Build()
                : null;

            var apiTypeNamingConventions = this.ApiTypeNamingConventionsBuilder != null
                ? this.ApiTypeNamingConventionsBuilder.Build()
                : null;

            var resourceTypeConventions = this.ResourceTypeConventionsBuilder != null
                ? this.ResourceTypeConventionsBuilder.Build()
                : null;

            var conventionSet = new ConventionSet
                {
                    ApiAttributeNamingConventions = apiAttributeNamingConventions,
                    ApiTypeNamingConventions = apiTypeNamingConventions,
                    ResourceTypeConventions = resourceTypeConventions
                };
            return conventionSet;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private NamingConventionsBuilder ApiAttributeNamingConventionsBuilder { get; set; }
        private NamingConventionsBuilder ApiTypeNamingConventionsBuilder { get; set; }
        private ResourceTypeConventionsBuilder ResourceTypeConventionsBuilder { get; set; }
        #endregion
    }
}