// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents the configuration to build and create a complex type in a service model.
    /// </summary>
    /// <typeparam name="TComplex">The type of complex object to build and create metadata about.</typeparam>
    public class ComplexTypeConfiguration<TComplex> : TypeBaseBuilder<TComplex>
        , IComplexTypeBuilder<TComplex>
        , IComplexTypeFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexTypeConfiguration()
            : base(ComplexTypeConfigurationName)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IComplexTypeFactory Implementation
        public IComplexType Create(IMetadataConventions metadataConventions)
        {
            // Apply all ComplexType level conventions if any.
            this.ApplyComplexTypeConventions(metadataConventions);

            // Create all the ComplexType parameters needed to construct a ComplexType metadata object.
            var attributesInfo = this.CreateAttributesInfo(metadataConventions);
            var complexType = new ComplexType<TComplex>(attributesInfo);
            return complexType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static readonly string ComplexTypeConfigurationName = "ComplexTypeConfiguration [clrType={0}]".FormatWith(typeof(TComplex).Name);
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyComplexTypeConventions(IMetadataConventions metadataConventions)
        {
            if (metadataConventions == null)
                return;

            //var complexTypeConventions = metadataConventions.ComplexTypeConventions ?? Enumerable.Empty<IComplexTypeConvention>();
            //foreach (var complexTypeConvention in complexTypeConventions)
            //{
            //    complexTypeConvention.Apply(this);
            //}
        }
        #endregion
    }
}
