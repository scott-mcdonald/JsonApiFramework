// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api version object.</summary>
    public class JsonApiVersion : IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiVersion(string version)
            : this(version, null)
        { }

        public JsonApiVersion(string version, Meta meta)
        {
            this.Version = version;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Version { get; }
        public Meta Meta { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var version = this.Version ?? String.Empty;
            return $"{TypeName} [version={version}]";
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly JsonApiVersion Version10 = new JsonApiVersion(Version10String);
        public static readonly JsonApiVersion Version11 = new JsonApiVersion(Version11String);
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private const string Version10String = "1.0";
        private const string Version11String = "1.1";
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(JsonApiVersion).Name;
        #endregion
    }
}