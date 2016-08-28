// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using JsonApiFramework.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant error object.
    /// </summary>
    /// <see cref="http://jsonapi.org"/>
    [JsonConverter(typeof(ErrorConverter))]
    public class Error : JsonObject
        , IGetLinks
        , IGetMeta
        , ISetLinks
        , ISetMeta
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Id { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public JObject Source { get; set; }
        public Links Links { get; set; }
        public Meta Meta { get; set; }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static implicit operator Error(ErrorException errorException)
        {
            return errorException != null
                ? new Error
                    {
                        Id = errorException.Id,
                        Status = errorException.Status.HasValue ? errorException.Status.Value.ToString() : null,
                        Code = errorException.Code,
                        Title = errorException.Title,
                        Detail = errorException.Detail,
                        Source = !String.IsNullOrWhiteSpace(errorException.Source) ? JObject.Parse(errorException.Source) : null,
                        Links = errorException.Links,
                        Meta = errorException.Meta
                    }
                : new Error
                    {
                        Id = CreateNewId()
                    };
        }

        public static implicit operator Error(Exception exception)
        {
            return exception != null
                ? new Error
                    {
                        Id = CreateNewId(),
                        Status = HttpStatusCode.InternalServerError.ToString(),
                        Title = exception.GetType().Name,
                        Detail = exception.Message
                    }
                : new Error
                    {
                        Id = CreateNewId()
                    };
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return String.Format("{0} [id={1} status={2} code={3} title={4}]",
                TypeName,
                this.Id,
                this.Status,
                this.Code,
                this.Title);
        }
        #endregion

        #region Helper Methods
        public static string CreateId(string id)
        {
            return !String.IsNullOrWhiteSpace(id) ? id : CreateNewId();
        }

        public static string CreateNewId()
        {
            var idAsGuid = Guid.NewGuid().ToString();
            return idAsGuid;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly Error Empty = new Error();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Error).Name;
        #endregion
    }
}