// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api error object.</summary>
    public class Error
        : IGetLinks
        , IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Error(string id,
                     Links links,
                     string status,
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
        public string Status { get; }
        public string Code { get; }
        public string Title { get; }
        public string Detail { get; }
        public ErrorSource Source { get; }
        public Meta Meta { get; }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static implicit operator Error(ErrorException errorException)
        {
            Contract.Requires(errorException != null);

            var id = errorException.Id;
            var links = errorException.Links;
            var status = errorException.Status.ToString();
            var code = errorException.Code;
            var title = errorException.Title;
            var detail = errorException.Detail;
            var source = errorException.ErrorSource;
            var meta = errorException.Meta;

            var error = new Error(id, links, status, code, title, detail, source, meta);
            return error;
        }

        public static implicit operator Error(Exception exception)
        {
            Contract.Requires(exception != null);

            var id = CreateId();
            var status = HttpStatusCode.InternalServerError.ToString();
            var title = exception.GetType().Name;
            var detail = exception.Message;

            var error = new Error(id, null, status, null, title, detail, null, null);
            return error;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return "{0} [id={1} status={2} code={3} title={4}]".FormatWith(
                TypeName,
                this.Id,
                this.Status,
                this.Code,
                this.Title);
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
            return !String.IsNullOrWhiteSpace(id) ? id : CreateId();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Error).Name;
        #endregion
    }
}