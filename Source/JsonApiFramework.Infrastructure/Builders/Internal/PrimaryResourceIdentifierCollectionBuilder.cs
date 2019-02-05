// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal class PrimaryResourceIdentifierCollectionBuilder<TResource> : ResourceIdentifierCollectionBuilder<IPrimaryResourceIdentifierCollectionBuilder<TResource>, TResource>, IPrimaryResourceIdentifierCollectionBuilder<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPrimaryResourceIdentifierCollectionBuilder Implementation
        public IDocumentWriter ResourceIdentifierCollectionEnd()
        {
            // Return the parent builder.
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal PrimaryResourceIdentifierCollectionBuilder(IDocumentWriter parentBuilder, IServiceModel serviceModel, DomDocument domDocument)
            : base(serviceModel, domDocument.AddDataCollection())
        {
            Contract.Requires(parentBuilder != null);

            this.Builder = this;
            this.ParentBuilder = parentBuilder;
        }

        internal PrimaryResourceIdentifierCollectionBuilder(IDocumentWriter parentBuilder, IServiceModel serviceModel, DomDocument domDocument, IEnumerable<TResource> clrResourceCollection)
            : base(serviceModel, domDocument.AddDataCollection(), clrResourceCollection)
        {
            Contract.Requires(parentBuilder != null);

            this.Builder = this;
            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDocumentWriter ParentBuilder { get; set; }
        #endregion
    }
}
