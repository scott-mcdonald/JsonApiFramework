// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi2
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
            this.Id = Error.CreateNewId();
            this.Status = default(HttpStatusCode?);
            this.Code = default(string);
            this.Title = default(string);
            this.Detail = default(string);
            this.SourceAsJson = default(string);
            //this.Links = default(Links);
            //this.Meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail)
            : base(detail)
        {
            this.Id = Error.CreateId(id);
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.SourceAsJson = default(string);
            //this.Links = default(Links);
            //this.Meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, Exception innerException)
            : base(detail, innerException)
        {
            this.Id = Error.CreateId(id);
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.SourceAsJson = default(string);
            //this.Links = default(Links);
            //this.Meta = default(Meta);
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, JObject source /*, Links links, Meta meta */)
            : base(detail)
        {
            this.Id = Error.CreateId(id);
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.SourceAsJson = CreateSourceAsJson(source);
            //this.Links = links;
            //this.Meta = meta;
        }

        public ErrorException(string id, HttpStatusCode? status, string code, string title, string detail, JObject source/*, Links links, Meta meta */, Exception innerException)
            : base(detail, innerException)
        {
            this.Id = Error.CreateId(id);
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.SourceAsJson = CreateSourceAsJson(source);
            //this.Links = links;
            //this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Id { get; }
        public HttpStatusCode? Status { get; }
        public string Code { get; }
        public string Title { get; }
        public string Detail { get; }
        //public Links Links { get; }
        //public Meta Meta { get; }
        #endregion

        #region Exception Overrides
        public override string Source { get { return this.SourceAsJson; } set { } }
        public override string Message => this.Detail;
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        private string SourceAsJson { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateSourceAsJson(JObject source)
        {
            var sourceAsJson = source?.ToString(Formatting.None);
            return sourceAsJson;
        }
        #endregion
    }
}