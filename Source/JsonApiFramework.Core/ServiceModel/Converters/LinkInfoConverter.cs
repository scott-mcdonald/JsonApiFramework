// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.ServiceModel.Internal;

using Newtonsoft.Json.Converters;

namespace JsonApiFramework.ServiceModel.Converters
{
    public class LinkInfoConverter : CustomCreationConverter<ILinkInfo>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region CustomCreationConverter Overrides
        public override ILinkInfo Create(Type objectType)
        { return new LinkInfo(); }
        #endregion
    }
}