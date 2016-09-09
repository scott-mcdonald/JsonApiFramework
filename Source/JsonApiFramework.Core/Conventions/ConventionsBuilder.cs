// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Conventions.Internal;

namespace JsonApiFramework.Conventions
{
    public class ConventionsBuilder : IConventionsBuilder, IConventionsFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IConventionsBuilder Implementation
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

        public IComplexTypeConventionsBuilder ComplexTypeConventions()
        {
            this.ComplexTypeConventionsBuilder = this.ComplexTypeConventionsBuilder ?? new ComplexTypeConventionsBuilder();
            return this.ComplexTypeConventionsBuilder;
        }

        public IResourceTypeConventionsBuilder ResourceTypeConventions()
        {
            this.ResourceTypeConventionsBuilder = this.ResourceTypeConventionsBuilder ?? new ResourceTypeConventionsBuilder();
            return this.ResourceTypeConventionsBuilder;
        }
        #endregion

        #region IConventionsFactory Implementation
        public IConventions Create()
        {
            var apiAttributeNamingConventions = this.ApiAttributeNamingConventionsBuilder != null
                ? this.ApiAttributeNamingConventionsBuilder.Build()
                : null;

            var apiTypeNamingConventions = this.ApiTypeNamingConventionsBuilder != null
                ? this.ApiTypeNamingConventionsBuilder.Build()
                : null;

            var complexTypeConventions = this.ComplexTypeConventionsBuilder != null
                ? this.ComplexTypeConventionsBuilder.Build()
                : null;

            var resourceTypeConventions = this.ResourceTypeConventionsBuilder != null
                ? this.ResourceTypeConventionsBuilder.Build()
                : null;

            var conventions = new JsonApiFramework.Conventions.Internal.Conventions
                {
                    ApiAttributeNamingConventions = apiAttributeNamingConventions,
                    ApiTypeNamingConventions = apiTypeNamingConventions,
                    ComplexTypeConventions = complexTypeConventions,
                    ResourceTypeConventions = resourceTypeConventions
                };
            return conventions;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private NamingConventionsBuilder ApiAttributeNamingConventionsBuilder { get; set; }
        private NamingConventionsBuilder ApiTypeNamingConventionsBuilder { get; set; }
        private ComplexTypeConventionsBuilder ComplexTypeConventionsBuilder { get; set; }
        private ResourceTypeConventionsBuilder ResourceTypeConventionsBuilder { get; set; }
        #endregion
    }
}