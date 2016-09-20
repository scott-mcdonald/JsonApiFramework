// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public class ComplexTypeBuilder<TComplex> : ClrTypeBuilder<TComplex>
        , IComplexTypeBuilder<TComplex>
        , IComplexTypeFactory
    {
        #region IComplexTypeFactory Implementation
        public IComplexType Create(IConventions conventions)
        {
            // Apply all ComplexType level conventions if any.
            this.ApplyComplexTypeConventions(conventions);

            // Create all the ComplexType parameters needed to construct a ComplexType metadata object.
            var clrComplexType = this.ClrType;
            var attributesInfo = this.CreateAttributesInfo(conventions);

            var complexType = new ComplexType(clrComplexType, attributesInfo);
            return complexType;
        }
        #endregion

        // PROTECTED/INTERNAL CONSTRUCTORS //////////////////////////////////
        #region Constructors
        protected internal ComplexTypeBuilder()
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyComplexTypeConventions(IConventions conventions)
        {
            if (conventions == null)
                return;

            var complexTypeConventions = conventions.ComplexTypeConventions ?? Enumerable.Empty<IComplexTypeConvention>();
            foreach (var resourceTypeConvention in complexTypeConventions)
            {
                resourceTypeConvention.Apply(this);
            }
        }
        #endregion
    }
}