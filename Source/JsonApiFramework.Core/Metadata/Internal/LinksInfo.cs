// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Internal
{
    internal class LinksInfo : ILinksInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksInfo(IClrPropertyBinding clrLinksPropertyBinding)
        {
            this.ClrLinksPropertyBinding = clrLinksPropertyBinding;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ILinksInfo Implementation
        public IClrPropertyBinding ClrLinksPropertyBinding { get; }
        #endregion
    }
}