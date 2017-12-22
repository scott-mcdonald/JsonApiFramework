// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Metadata.Configuration.Internal;
using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.Properties;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents the commonality between resource type and complex type configuration.
    /// </summary>
    /// <typeparam name="TObject">The type of complex/resource object to build metadata about.</typeparam>
    public abstract class TypeBaseBuilder<TObject> : ITypeBaseBuilder<TObject>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ITypeBaseBuilder<TAttributesInfoBuilder> Implementation
        public IAttributesInfoBuilder<TObject> Attributes()
        {
            if (this.AttributesInfoBuilder != null)
            {
                var detail = CoreErrorStrings.MetadataExceptionAlreadyConfiguredDetail
                                             .FormatWith(this.Name, "attributes");
                throw new MetadataException(detail);
            }

            var attributesInfoBuilder = new AttributesInfoBuilder<TObject>();
            this.AttributesInfoBuilder = attributesInfoBuilder;
            return this.AttributesInfoBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected TypeBaseBuilder(string name)
        { this.Name = name; }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal string Name { get; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IAttributesInfo CreateAttributesInfo(IMetadataConventions metadataConventions)
        {
            Contract.Requires(metadataConventions != null);

            if (this.AttributesInfoBuilder == null)
                return new AttributesInfo(default(IEnumerable<IAttributeInfo>));

            var attributesInfoFactory = (IAttributesInfoFactory)this.AttributesInfoBuilder;
            var attributesInfo = attributesInfoFactory.Create(metadataConventions);
            return attributesInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IAttributesInfoBuilder<TObject> AttributesInfoBuilder { get; set; }
        #endregion
    }
}
