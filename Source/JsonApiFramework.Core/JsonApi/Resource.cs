// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts a json:api compliant resource.
    /// </summary>
    /// <see cref="http://jsonapi.org"/>
    [JsonConverter(typeof(ResourceConverter))]
    public class Resource : JsonObject
        , IComparable<Resource>
        , IComparable
        , IEquatable<Resource>
        , IGetAttributes
        , IGetLinks
        , IGetMeta
        , IGetRelationships
        , IGetResourceIdentity
        , ISetAttributes
        , ISetLinks
        , ISetMeta
        , ISetRelationships
        , ISetResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Type { get; set; }
        public string Id { get; set; }
        public ApiObject Attributes { get; set; }
        public Relationships Relationships { get; set; }
        public Links Links { get; set; }
        public Meta Meta { get; set; }
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
            return String.Format("{0} [type={1} id={2}]", TypeName, type, id);
        }
        #endregion

        #region IEquatable<T> Implementation
        public bool Equals(Resource other)
        { return GetResourceIdentityExtensions.Equals(this, other); }
        #endregion

        #region IComparable<T> Implementation
        public int CompareTo(Resource other)
        { return GetResourceIdentityExtensions.CompareTo(this, other); }
        #endregion

        #region IComparable Implementation
        public int CompareTo(object obj)
        { return GetResourceIdentityExtensions.CompareTo(this, obj); }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static explicit operator ResourceIdentifier(Resource resource)
        {
            if (resource == null)
                return null;

            return new ResourceIdentifier
                {
                    Type = resource.Type,
                    Id = resource.Id
                };
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly Resource Empty = new Resource();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Resource).Name;
        #endregion
    }
}