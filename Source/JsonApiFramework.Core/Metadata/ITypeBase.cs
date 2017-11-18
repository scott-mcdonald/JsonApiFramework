// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the common metadata between complex/resource types.</summary>
    public interface ITypeBase
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the CLR type of the complex/resource type.</summary>
        Type ClrType { get; }

        /// <summary>Gets the 'attributes' metadata of the complex/resource type.</summary>
        IAttributesInfo AttributesInfo { get; }
        #endregion
    }
}