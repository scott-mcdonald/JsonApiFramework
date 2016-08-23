// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a CLR exception that semantically equilvalent to a
    /// json:api compliant Error object.
    /// </summary>
    /// <remarks>
    /// Intended primarily to be a base-class for more concrete exceptions.
    /// 
    /// Part of the base-class functionality is to provide services to easily
    /// create json:api Error objects from derived ErrorException objects
    /// when needed.
    /// 
    /// Will create an "Id" based on GUID's if no identifier is passed upon
    /// construction.
    /// 
    /// The "Source" property of Exception is the JSON representation of the
    /// "Source" from json:api Error objects as a JObject passed upon
    /// construction.
    /// </remarks>
    public class ErrorException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorException()
        {
            this._id = Error.CreateNewId();
            this._status = default(HttpStatusCode?);
            this._code = default(string);
            this._title = default(string);
            this._detail = default(string);
            this._sourceAsJson = default(string);
            this._links = default(Links);
            this._meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail)
            : base(detail)
        {
            this._id = Error.CreateId(id);
            this._status = status;
            this._code = code;
            this._title = title;
            this._detail = detail;
            this._sourceAsJson = default(string);
            this._links = default(Links);
            this._meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, Exception innerException)
            : base(detail, innerException)
        {
            this._id = Error.CreateId(id);
            this._status = status;
            this._code = code;
            this._title = title;
            this._detail = detail;
            this._sourceAsJson = default(string);
            this._links = default(Links);
            this._meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, JObject source, Links links, Meta meta)
            : base(detail)
        {
            this._id = Error.CreateId(id);
            this._status = status;
            this._code = code;
            this._title = title;
            this._detail = detail;
            this._sourceAsJson = CreateSourceAsJson(source);
            this._links = links;
            this._meta = meta;
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, JObject source, Links links, Meta meta, Exception innerException)
            : base(detail, innerException)
        {
            this._id = Error.CreateId(id);
            this._status = status;
            this._code = code;
            this._title = title;
            this._detail = detail;
            this._sourceAsJson = CreateSourceAsJson(source);
            this._links = links;
            this._meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        // ReSharper disable ConvertToAutoProperty
        public string Id { get { return this._id; } set { this._id = value; } }
        public HttpStatusCode? Status { get { return this._status; } set { this._status = value; } }
        public string Code { get { return this._code; } set { this._code = value; } }
        public string Title { get { return this._title; } set { this._title = value; } }
        public string Detail { get { return this._detail; } set { this._detail = value; } }
        public Links Links { get { return this._links; } set { this._links = value; } }
        public Meta Meta { get { return this._meta; } set { this._meta = value; } }
        // ReSharper restore ConvertToAutoProperty
        #endregion

        #region Exception Overrides
        public override string Source { get { return this._sourceAsJson; } set { this._sourceAsJson = value; } }
        public override string Message { get { return this._detail; } }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateSourceAsJson(JObject source)
        {
            if (source == null)
                return null;

            var sourceAsJson = source.ToString(Formatting.None);
            return sourceAsJson;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private string _id;
        private HttpStatusCode? _status;
        private string _code;
        private string _title;
        private string _detail;
        private string _sourceAsJson;
        private Links _links;
        private Meta _meta;
        #endregion
    }
}