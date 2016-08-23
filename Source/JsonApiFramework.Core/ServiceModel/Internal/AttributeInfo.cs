// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class AttributeInfo : PropertyInfo
        , IAttributeInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributeInfo(string clrPropertyName, Type clrResourceType, string apiPropertyName)
            : base(clrPropertyName, clrResourceType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            this.ApiPropertyName = apiPropertyName;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IAttributeInfo Implementation
        [JsonProperty] public string ApiPropertyName { get; internal set; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributeInfo()
        { }
        #endregion
    }
}