// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class ResourceLinkageKey
        : IEquatable<ResourceLinkageKey>
        , IComparable<ResourceLinkageKey>
        , IComparable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ResourceIdentifier FromResourceIdentifier { get; private set; }
        public string FromRel { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            if (obj == null)
                return false;

            var objAsResourceLinkageKey = obj as ResourceLinkageKey;
            if (objAsResourceLinkageKey == null)
                return false;

            return this.FromResourceIdentifier == objAsResourceLinkageKey.FromResourceIdentifier &&
                   this.FromRel == objAsResourceLinkageKey.FromRel;
        }

        public override int GetHashCode()
        {
            return this.FromResourceIdentifier.GetHashCode() ^ this.FromRel.GetHashCode();
        }

        public override string ToString()
        {
            var fromResourceIdentifierAsString = this.FromResourceIdentifier.SafeToString();
            var fromRelAsString = this.FromRel.SafeToString();
            return String.Format("{0} [id={1} rel={2}]", TypeName, fromResourceIdentifierAsString, fromRelAsString);
        }
        #endregion

        #region IEquatable<ResourceLinkageKey> Implementation
        public bool Equals(ResourceLinkageKey other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return this.FromResourceIdentifier == other.FromResourceIdentifier && this.FromRel == other.FromRel;
        }
        #endregion

        #region IComparable<ResourceLinkageKey>
        public int CompareTo(ResourceLinkageKey other)
        {
            if (Object.ReferenceEquals(this, other))
                return 0;

            if (other == null)
                return 1;

            var resourceIdentifierCompare = this.FromResourceIdentifier.CompareTo(other.FromResourceIdentifier);
            return resourceIdentifierCompare != 0
                ? resourceIdentifierCompare
                : String.Compare(this.FromRel, other.FromRel, StringComparison.Ordinal);
        }
        #endregion

        #region IComparable
        int IComparable.CompareTo(object obj)
        {
            return this.CompareTo((ResourceLinkageKey)obj);
        }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Equality Operators
        public static bool operator ==(ResourceLinkageKey a, ResourceLinkageKey b)
        {
            if (Object.ReferenceEquals(a, b))
                return true;

            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
                return false;

            return (a.FromResourceIdentifier == b.FromResourceIdentifier && a.FromRel == b.FromRel);
        }

        public static bool operator !=(ResourceLinkageKey a, ResourceLinkageKey b)
        { return !(a == b); }
        #endregion

        #region Comparison Operators
        public static bool operator <(ResourceLinkageKey left, ResourceLinkageKey right)
        {
            if (left == null && right == null)
                return false;

            if (left == null)
                return true;

            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ResourceLinkageKey left, ResourceLinkageKey right)
        {
            if (left == null)
                return true;

            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ResourceLinkageKey left, ResourceLinkageKey right)
        {
            if (left == null)
                return false;

            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ResourceLinkageKey left, ResourceLinkageKey right)
        {
            if (left == null && right == null)
                return true;

            if (left == null)
                return false;

            return left.CompareTo(right) >= 0;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ResourceLinkageKey(ResourceIdentifier fromResourceIdentifier, string fromRel)
        {
            Contract.Requires(fromResourceIdentifier != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);

            this.FromResourceIdentifier = fromResourceIdentifier;
            this.FromRel = fromRel;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceLinkageKey).Name;
        #endregion
    }
}