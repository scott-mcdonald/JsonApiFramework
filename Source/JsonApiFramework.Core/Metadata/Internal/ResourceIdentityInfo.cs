﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ResourceIdentityInfo : IResourceIdentityInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentityInfo(string apiType, string clrIdPropertyName, Type clrIdPropertyType, IClrPropertyBinding clrIdPropertyBinding)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiType) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(clrIdPropertyName) == false);
            Contract.Requires(clrIdPropertyType != null);
            Contract.Requires(clrIdPropertyBinding != null);

            this.ApiType = apiType;
            this.ClrIdPropertyName = clrIdPropertyName;
            this.ClrIdPropertyType = clrIdPropertyType;
            this.ClrIdPropertyBinding = clrIdPropertyBinding;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IResourceIdentity Implementation
        public string ApiType { get; }

        public string ClrIdPropertyName { get; }

        public Type ClrIdPropertyType { get; }

        public IClrPropertyBinding ClrIdPropertyBinding { get; }
        #endregion
    }
}