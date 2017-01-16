// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

namespace JsonApiFramework.JsonApi2
{
    /// <summary>Represents an immutable json:api version object.</summary>
    public class JsonApiVersion : JsonObject
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiVersion(string version = null)
        {
            this.Version = version;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Version { get; }
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
    }
}