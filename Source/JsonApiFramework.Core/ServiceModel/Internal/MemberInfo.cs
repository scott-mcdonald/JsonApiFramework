// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal abstract class MemberInfo : JsonObject
        , IMemberInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IMemberInfo Implementation
        [JsonProperty] public Type ClrDeclaringType { get; private set; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected MemberInfo(Type clrDeclaringType)
        {
            this.ClrDeclaringType = clrDeclaringType;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal MemberInfo()
        { }
        #endregion
    }
}