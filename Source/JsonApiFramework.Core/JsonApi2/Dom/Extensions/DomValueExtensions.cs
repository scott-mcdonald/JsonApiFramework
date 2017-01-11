// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>Extension methods for any object that implements the <c>IDomValue</c> interface.</summary>
    public static class DomValueExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static TTarget ClrValue<TTarget>(this IDomValue domValue)
        {
            Contract.Requires(domValue != null);

            var clrValue = domValue.ClrValue<TTarget>(null, null);
            return clrValue;
        }

        public static TTarget ClrValue<TTarget>(this IDomValue domValue, ITypeConverter typeConverter)
        {
            Contract.Requires(domValue != null);

            var clrValue = domValue.ClrValue<TTarget>(typeConverter, null);
            return clrValue;
        }
        #endregion
    }
}