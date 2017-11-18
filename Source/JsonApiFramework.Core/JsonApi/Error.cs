// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api error object.</summary>
    [JsonConverter(typeof(ErrorConverter))]
    public class Error : JsonObject
        , IGetLinks
        , IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Error(string id,
                     Links links,
                     HttpStatusCode? status,
                     string code,
                     string title,
                     string detail,
                     ErrorSource source,
                     Meta meta)
        {
            this.Id = GetOrCreateId(id);
            this.Links = links;
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.Source = source;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Id { get; }
        public Links Links { get; }
        public HttpStatusCode? Status { get; }
        public string Code { get; }
        public string Title { get; }
        public string Detail { get; }
        public ErrorSource Source { get; }
        public Meta Meta { get; }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static explicit operator Error(Exception exception)
        {
            Contract.Requires(exception != null);

            var error = Create(exception);
            return error;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var json = this.ToJson();
            return json;
        }
        #endregion

        #region Helper Methods
        public static string CreateId()
        {
            var idAsGuid = Guid.NewGuid();
            var isAsGuidString = idAsGuid.ToString();
            return isAsGuidString;
        }

        public static string GetOrCreateId(string id)
        {
            return String.IsNullOrWhiteSpace(id) ? CreateId() : id;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal static Error Create(Exception exception, string title = null)
        {
            Contract.Requires(exception != null);

            var id = CreateId();
            const HttpStatusCode status = HttpStatusCode.InternalServerError;
            title = title ?? exception.GetType().Name;
            var detail = exception.Message;

            var error = new Error(id, null, status, null, title, detail, null, null);
            return error;
        }
        #endregion
    }
}