// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Metadata.Internal
{
    internal class AttributesInfo : IAttributesInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributesInfo(IEnumerable<IAttributeInfo> attributeInfoCollection)
        {
            this.AttributeInfoCollection = attributeInfoCollection.SafeToReadOnlyCollection();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IAttributesInfo Implementation
        public IReadOnlyCollection<IAttributeInfo> AttributeInfoCollection { get; }
        #endregion
    }
}