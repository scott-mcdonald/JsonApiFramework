// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class PrimaryResourceBuilder<TResource> : ResourceBuilder<IPrimaryResourceBuilder<TResource>, TResource>, IPrimaryResourceBuilder<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPrimaryResourceBuilder<TResource> Implementation
        public IIncludedBuilder ResourceEnd()
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
        internal PrimaryResourceBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, TResource clrResource)
            : base(parentBuilder, domDocument.AddData(), clrResource)
        {
            this.Builder = this;
        }
        #endregion
    }
}
