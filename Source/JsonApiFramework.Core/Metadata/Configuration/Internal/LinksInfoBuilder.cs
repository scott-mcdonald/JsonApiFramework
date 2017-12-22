// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class LinksInfoBuilder : ILinksInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksInfoBuilder(IClrPropertyBinding clrLinksPropertyBinding)
        {
            this.ClrLinksPropertyBinding = clrLinksPropertyBinding;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksInfoFactory Implementation
        public ILinksInfo Create(IMetadataConventions metadataConventions)
        {
            var clrLinksPropertyBinding = this.ClrLinksPropertyBinding;
            var linksInfo = new LinksInfo(clrLinksPropertyBinding);
            return linksInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IClrPropertyBinding ClrLinksPropertyBinding { get; }
        #endregion
    }
}
