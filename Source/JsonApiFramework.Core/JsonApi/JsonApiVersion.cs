// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant jsonapi object.
    /// </summary>
    /// <see cref="http://jsonapi.org"/>
    [JsonConverter(typeof(JsonApiVersionConverter))]    
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonApiVersion : JsonObject
        , IGetMeta
        , ISetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiVersion()
        { }

        public JsonApiVersion(string version, JObject meta = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(version) == false);

            this.Version = version;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        [JsonProperty(Keywords.Version)] public string Version { get; set; }

        [JsonProperty(Keywords.Meta)] public Meta Meta { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return String.Format("{0} [version={1}]", TypeName, this.Version); }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public const string Version10String = "1.0";
        public const string Version11String = "1.1";

        public static readonly JsonApiVersion Version10 = new JsonApiVersion(Version10String);
        public static readonly JsonApiVersion Version11 = new JsonApiVersion(Version11String);

        public static readonly JsonApiVersion Empty = new JsonApiVersion();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(JsonApiVersion).Name;
        #endregion
    }
}