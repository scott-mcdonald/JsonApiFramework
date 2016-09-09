// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Conventions.Internal
{
    internal class Conventions : IConventions
    {
        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region IConventions Implementation
        public IEnumerable<INamingConvention> ApiAttributeNamingConventions { get; internal set; }
        public IEnumerable<INamingConvention> ApiTypeNamingConventions { get; internal set; }
        public IEnumerable<IComplexTypeConvention> ComplexTypeConventions { get; internal set; }
        public IEnumerable<IResourceTypeConvention> ResourceTypeConventions { get; internal set; }
        #endregion
    }
}