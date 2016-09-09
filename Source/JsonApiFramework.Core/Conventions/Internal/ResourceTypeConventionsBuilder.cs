// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Conventions.Internal
{
    internal class ResourceTypeConventionsBuilder : IResourceTypeConventionsBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceTypeConventionsBuilder()
        { this.ResourceTypeConventions = new List<IResourceTypeConvention>(); }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceTypeConventionsBuilder Implementation
        public IResourceTypeConventionsBuilder AddPropertyDiscoveryConvention()
        {
            this.ResourceTypeConventions.Add(new PropertyDiscoveryResourceTypeConvention());
            return this;
        }
        #endregion

        #region Factory Methods
        public IEnumerable<IResourceTypeConvention> Build()
        { return this.ResourceTypeConventions; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IList<IResourceTypeConvention> ResourceTypeConventions { get; set; }
        #endregion
    }
}