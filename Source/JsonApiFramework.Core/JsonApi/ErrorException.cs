// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a CLR exception that is semantically equilvalent to a
    /// json:api compliant Error object.
    /// </summary>
    /// <remarks>
    /// Intended primarily to be a base-class for more concrete CLR exceptions.
    /// 
    /// Part of the base-class functionality is to provide services to easily
    /// create json:api Error objects from derived ErrorException objects
    /// when needed.
    /// 
    /// Will create an "Id" based on GUID's if no identifier is passed upon
    /// construction.
    /// </remarks>
    public class ErrorException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorException(string id,
                              Links links,
                              HttpStatusCode status,
                              string code,
                              string title,
                              string detail,
                              ErrorSource source,
                              Meta meta)
            : this(id, links, status, code, title, detail, source, meta, null)
        { }

        public ErrorException(string id,
                              Links links,
                              HttpStatusCode status,
                              string code,
                              string title,
                              string detail,
                              ErrorSource source,
                              Meta meta,
                              Exception innerException)
            : base(detail, innerException)
        {
            this.Id = Error.GetOrCreateId(id);
            this.Links = links;
            this.Status = status;
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.ErrorSource = source;
            this.Meta = meta;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Id { get; }
        public Links Links { get; }
        public HttpStatusCode Status { get; }
        public string Code { get; }
        public string Title { get; }
        public string Detail { get; }
        public ErrorSource ErrorSource { get; }
        public Meta Meta { get; }
        #endregion

        #region Exception Overrides
        public override string Source { get { return this.ErrorSource.SafeToString(); } set { } }
        public override string Message => this.Detail;
        #endregion
    }
}