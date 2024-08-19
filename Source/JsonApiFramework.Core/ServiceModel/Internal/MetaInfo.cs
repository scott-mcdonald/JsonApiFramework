// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel.Internal;

internal class MetaInfo : PropertyInfo
    , IMetaInfo
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public MetaInfo(Type clrDeclaringType, string clrPropertyName)
        : base(clrDeclaringType, clrPropertyName, typeof(Meta))
    { }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal MetaInfo()
    { }
    #endregion
}