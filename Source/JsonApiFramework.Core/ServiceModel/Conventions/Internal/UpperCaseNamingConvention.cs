// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using Humanizer;

namespace JsonApiFramework.ServiceModel.Conventions.Internal
{
    /// <summary>Naming convention that lowercases the name.</summary>
    internal class UpperCaseNamingConvention : INamingConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region INamingConvention Implementation
        public string Apply(string oldName)
        {
            if (String.IsNullOrWhiteSpace(oldName))
                return oldName;

            var newName = oldName.Transform(To.UpperCase);
            return newName;
        }
        #endregion
    }
}