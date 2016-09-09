// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using Humanizer;

namespace JsonApiFramework.Conventions.Internal
{
    /// <summary>Naming convention that applies the json:api naming standard for members.</summary>
    internal class StandardMemberNamingConvention : INamingConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region INamingConvention Implementation
        public string Apply(string oldName)
        {
            if (String.IsNullOrWhiteSpace(oldName))
                return oldName;

            // Apply the JsonApi standard naming convention of member names:
            // 1. Names are lower case.
            // 2. Names use the hyphen minus '-' as word separators.
            var newName = oldName.Underscore()
                                 .Hyphenate();
            return newName;
        }
        #endregion
    }
}