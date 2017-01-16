// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi.Internal;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api compliant resource identifier.</summary>
    public class ResourceIdentifier : JsonObject
        , IEquatable<ResourceIdentifier>
        , IComparable<ResourceIdentifier>
        , IComparable
        , IGetResourceIdentity
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifier()
            : this(null, null)
        { }

        public ResourceIdentifier(string type)
            : this(type, null)
        { }

        public ResourceIdentifier(string type, string id)
        {
            this.Type = type;
            this.Id = id;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Type { get; set; }
        public string Id { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override bool Equals(object obj)
        { return GetResourceIdentityExtensions.Equals(this, obj); }

        public override int GetHashCode()
        { return GetResourceIdentityExtensions.GetHashCode(this); }

        public override string ToString()
        {
            var type = this.Type ?? CoreStrings.NullText;
            var id = this.Id ?? CoreStrings.NullText;
            return $"{TypeName} [type={type} id={id}]";
        }
        #endregion

        #region IEquatable<T> Implementation
        public bool Equals(ResourceIdentifier other)
        { return GetResourceIdentityExtensions.Equals(this, other); }
        #endregion

        #region IComparable<T> Implementation
        public int CompareTo(ResourceIdentifier other)
        { return GetResourceIdentityExtensions.CompareTo(this, other); }
        #endregion

        #region IComparable Implementation
        public int CompareTo(object obj)
        { return GetResourceIdentityExtensions.CompareTo(this, obj); }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Equality Operators
        public static bool operator ==(ResourceIdentifier a, ResourceIdentifier b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return (a.Type == b.Type && a.Id == b.Id);
        }

        public static bool operator !=(ResourceIdentifier a, ResourceIdentifier b)
        { return !(a == b); }
        #endregion

        #region Comparison Operators
        public static bool operator <(ResourceIdentifier left, ResourceIdentifier right)
        {
            if (left == null && right == null)
                return false;

            if (left == null)
                return true;

            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ResourceIdentifier left, ResourceIdentifier right)
        {
            if (left == null)
                return true;

            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ResourceIdentifier left, ResourceIdentifier right)
        {
            if (left == null)
                return false;

            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ResourceIdentifier left, ResourceIdentifier right)
        {
            if (left == null && right == null)
                return true;

            if (left == null)
                return false;

            return left.CompareTo(right) >= 0;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly ResourceIdentifier Empty = new ResourceIdentifier();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceIdentifier).Name;
        #endregion
    }
}