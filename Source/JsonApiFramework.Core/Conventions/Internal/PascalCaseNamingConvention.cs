// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Humanizer;

namespace JsonApiFramework.Conventions.Internal;

/// <summary>Naming convention that applies PascalCasing to members.</summary>
internal class PascalCaseNamingConvention : INamingConvention
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region INamingConvention Implementation
    public string Apply(string oldName)
    {
        return string.IsNullOrWhiteSpace(oldName) ? oldName : oldName.Pascalize();
    }
    #endregion
}
