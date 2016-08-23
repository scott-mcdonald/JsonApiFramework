// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant resource identifier.
    /// </summary>
    /// <see cref="http://jsonapi.org"/>
    [JsonConverter(typeof(ResourceIdentifierConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceIdentifier : JsonObject
        , IEquatable<ResourceIdentifier>
        , IComparable<ResourceIdentifier>
        , IComparable
        , IGetMeta
        , IGetResourceIdentity
        , ISetMeta
        , ISetResourceIdentity
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifier()
        { }

        public ResourceIdentifier(string type)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(type) == false);

            this.Type = type;
        }

        public ResourceIdentifier(string type, string id)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(type) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(id) == false);

            this.Type = type;
            this.Id = id;
        }

        public ResourceIdentifier(string type, string id, Meta meta)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(type) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(id) == false);
            Contract.Requires(meta != null);

            this.Type = type;
            this.Id = id;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        [JsonProperty(Keywords.Type)] public string Type { get; set; }
        [JsonProperty(Keywords.Id)] public string Id { get; set; }
        [JsonProperty(Keywords.Meta)] public Meta Meta { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override bool Equals(object obj)
        { return GetResourceIdentityExtensions.Equals(this, obj); }

        public override int GetHashCode()
        { return GetResourceIdentityExtensions.GetHashCode(this); }

        public override string ToString()
        {
            var type = this.Type ?? JsonConstants.Null;
            var id = this.Id ?? JsonConstants.Null;
            return String.Format("{0} [type={1} id={2}]", TypeName, type, id);
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
            if (Object.ReferenceEquals(a, b))
                return true;

            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
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