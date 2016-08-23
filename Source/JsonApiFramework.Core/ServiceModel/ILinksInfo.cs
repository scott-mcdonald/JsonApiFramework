// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.ServiceModel
{
    public interface ILinksInfo : IPropertyInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<ILinkInfo> Collection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        ILinkInfo GetLink(string rel);
        bool TryGetLink(string rel, out ILinkInfo link);
        #endregion
    }
}