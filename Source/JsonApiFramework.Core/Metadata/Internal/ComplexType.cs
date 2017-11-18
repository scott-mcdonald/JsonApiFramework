// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Internal
{
    internal class ComplexType<TObject> : TypeBase<TObject>, IComplexType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexType(IAttributesInfo attributesInfo)
            : base(attributesInfo)
        { }
        #endregion
    }
}