// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>IGetResourceIdentity</c> interface.</summary>
    public static class GetResourceIdentityExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static int CompareTo(this IGetResourceIdentity getResourceIdentity, IGetResourceIdentity other)
        {
            if (Object.ReferenceEquals(getResourceIdentity, other))
                return 0;

            if (other == null)
                return 1;

            var typeCompare = String.Compare(getResourceIdentity.Type, other.Type, System.StringComparison.Ordinal);
            return typeCompare != 0
                ? typeCompare
                : String.Compare(getResourceIdentity.Id, other.Id, System.StringComparison.Ordinal);
        }

        public static int CompareTo(this IGetResourceIdentity getResourceIdentity, object obj)
        {
            return getResourceIdentity.CompareTo((IGetResourceIdentity)obj);
        }

        public static bool Equals(this IGetResourceIdentity getResourceIdentity, IGetResourceIdentity other)
        {
            if (Object.ReferenceEquals(getResourceIdentity, other))
                return true;

            if (other == null)
                return false;

            return getResourceIdentity.Type == other.Type && getResourceIdentity.Id == other.Id;
        }

        public static bool Equals(this IGetResourceIdentity getResourceIdentity, object obj)
        {
            if (Object.ReferenceEquals(getResourceIdentity, obj))
                return true;

            if (obj == null)
                return false;

            var objAsHasResourceIdentity = obj as IGetResourceIdentity;
            if (objAsHasResourceIdentity == null)
                return false;

            return getResourceIdentity.Type == objAsHasResourceIdentity.Type && getResourceIdentity.Id == objAsHasResourceIdentity.Id;
        }

        public static int GetHashCode(this IGetResourceIdentity getResourceIdentity)
        {
            Contract.Requires(getResourceIdentity != null);

            return getResourceIdentity.Type.GetHashCode() ^ getResourceIdentity.Id.GetHashCode();
        }

        public static bool IsDefined(this IGetResourceIdentity getResourceIdentity)
        {
            Contract.Requires(getResourceIdentity != null);

            return !getResourceIdentity.IsUndefined();
        }

        public static bool IsUndefined(this IGetResourceIdentity getResourceIdentity)
        {
            Contract.Requires(getResourceIdentity != null);

            return getResourceIdentity == null ||
                String.IsNullOrWhiteSpace(getResourceIdentity.Type) ||
                String.IsNullOrWhiteSpace(getResourceIdentity.Id);
        }
        #endregion
    }
}
