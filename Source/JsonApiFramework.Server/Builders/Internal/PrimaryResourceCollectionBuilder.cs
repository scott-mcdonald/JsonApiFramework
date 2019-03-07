// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class PrimaryResourceCollectionBuilder : ResourceCollectionBuilder<IPrimaryResourceCollectionBuilder>, IPrimaryResourceCollectionBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPrimaryResourceBuilder Implementation
        public IIncludedBuilder ResourceCollectionEnd()
        {
            // Notify base class building is done.
            this.OnBuildEnd();

            // Return the parent builder.
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal PrimaryResourceCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, Type clrResourceType, IEnumerable<object> clrResourceCollection)
            : base(parentBuilder, domDocument.AddDataCollection(), clrResourceType, clrResourceCollection)
        {
            this.Builder = this;
        }
        #endregion
    }

    internal class PrimaryResourceCollectionBuilder<TResource> : ResourceCollectionBuilder<IPrimaryResourceCollectionBuilder<TResource>, TResource>, IPrimaryResourceCollectionBuilder<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPrimaryResourceBuilder<TResource> Implementation
        public IIncludedBuilder ResourceCollectionEnd()
        {
            // Notify base class building is done.
            this.OnBuildEnd();

            // Return the parent builder.
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal PrimaryResourceCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, Type clrResourceType, IEnumerable<TResource> clrResourceCollection)
            : base(parentBuilder, domDocument.AddDataCollection(), clrResourceType, clrResourceCollection)
        {
            this.Builder = this;
        }
        #endregion
    }
}
