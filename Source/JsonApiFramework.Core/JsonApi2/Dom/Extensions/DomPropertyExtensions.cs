// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>Extension methods for any object that implements the <c>IDomProperty</c> interface.</summary>
    public static class DomPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static bool HasValue(this IDomProperty domProperty)
        {
            Contract.Requires(domProperty != null);

            var hasValue = domProperty.DomPropertyValue() != null;
            return hasValue;
        }
        #endregion
    }
}