// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Conventions.Internal
{
    internal class ComplexTypeConventionsBuilder : IComplexTypeConventionsBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexTypeConventionsBuilder()
        { this.ComplexTypeConventions = new List<IComplexTypeConvention>(); }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IComplexTypeConventionsBuilder Implementation
        public IComplexTypeConventionsBuilder AddPropertyDiscoveryConvention()
        {
            this.ComplexTypeConventions.Add(new PropertyDiscoveryComplexTypeConvention());
            return this;
        }
        #endregion

        #region Factory Methods
        public IEnumerable<IComplexTypeConvention> Build()
        { return this.ComplexTypeConventions; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IList<IComplexTypeConvention> ComplexTypeConventions { get; set; }
        #endregion
    }
}