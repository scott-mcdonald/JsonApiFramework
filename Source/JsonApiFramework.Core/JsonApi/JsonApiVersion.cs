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
            : this(null, version)
        { }

        public JsonApiVersion(Meta meta, string version)
        {
            this.Meta = meta;
            this.Version = version;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public Meta Meta { get; }
        public string Version { get; }
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