// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Properties;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable json:api compliant resource identifier.
    /// </summary>
    /// <remarks>
    /// Resource identity per specification is the "type" and "id" string values paired together
    /// like a compound primary key to uniquely identify a single resource for the ecosystem of resources for a particular system.
    /// </remarks>
    public class ResourceIdentifier : IEquatable<ResourceIdentifier>
        , IComparable<ResourceIdentifier>
        , IComparable
        , IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifier(string type, string id)
            : this(type, id, null)
        { }

        public ResourceIdentifier(string type, string id, Meta meta)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(type) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(id) == false);

            this.Type = type;
            this.Id = id;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Type { get; }
        public string Id { get; }
        public Meta Meta { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var objAsResourceIdentifier = obj as ResourceIdentifier;
            if (objAsResourceIdentifier == null)
                return false;

            var objType = objAsResourceIdentifier.Type;
            var objId = objAsResourceIdentifier.Id;

            return this.Type == objType && this.Id == objId;
        }

        public override int GetHashCode()
        { return this.Type.GetHashCode() ^ this.Id.GetHashCode(); }

        public override string ToString()
        {
            var type = this.Type ?? CoreStrings.NullText;
            var id = this.Id ?? CoreStrings.NullText;
            return $"{TypeName} [type={type} id={id}]";
        }
        #endregion

        #region IEquatable<T> Implementation
        public bool Equals(ResourceIdentifier other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return this.Type == other.Type && this.Id == other.Id;
        }
        #endregion

        #region IComparable<T> Implementation
        public int CompareTo(ResourceIdentifier other)
        {
            if (ReferenceEquals(this, other))
                return 0;

            if (other == null)
                return 1;

            var typeCompare = String.Compare(this.Type, other.Type, StringComparison.Ordinal);
            return typeCompare != 0
                ? typeCompare
                : String.Compare(this.Id, other.Id, StringComparison.Ordinal);
        }
        #endregion

        #region IComparable Implementation
        public int CompareTo(object obj)
        { return this.CompareTo((ResourceIdentifier)obj); }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Equality Operators
        public static bool operator ==(ResourceIdentifier a, ResourceIdentifier b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Type == b.Type && a.Id == b.Id;
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

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceIdentifier).Name;
        #endregion
    }
}