// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Dom.Internal;
using JsonApiFramework.Properties;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Extension methods for the <c>DomDeserializationContext</c> class.</summary>
    internal static class DomDeserializationContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static void AddJsonApiArrayItemError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for json:api array item. Expected JSON object. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiDataError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {Keywords.Data}. Expected JSON null, JSON object, or JSON array. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiDocumentContainsDataAndErrorsError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {Keywords.Document}. Document contains both {Keywords.Data} and {Keywords.Errors} members. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiErrorsError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {Keywords.Errors}. Expected JSON array. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiIncludedError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {Keywords.Included}. Expected JSON array. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiLinkError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {Keywords.Link}. Expected JSON null, JSON string, or JSON object. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiObjectError(this DomDeserializationContext domDeserializationContext, string jsonPointer, string apiObjectType)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {apiObjectType}. Expected JSON null or JSON object. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiObjectMemberError(this DomDeserializationContext domDeserializationContext, string jsonPointer, string apiPropertyName, string apiObjectType)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected '{apiPropertyName}' member for json:api {apiObjectType}. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonApiStringError(this DomDeserializationContext domDeserializationContext, string jsonPointer, string apiObjectType)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {apiObjectType}. Expected JSON null or JSON string. Violates the JSON API specification.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonArrayError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON array. Expected JSON null or JSON array.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonArrayItemError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON array item. Expected JSON null, JSON boolean, JSON number, JSON string, or JSON object.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonObjectError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for a standard JSON object. Expected JSON null or JSON object.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonObjectMemberError(this DomDeserializationContext domDeserializationContext, string jsonPointer, string apiPropertyName)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected '{apiPropertyName}' member for a standard JSON object. Expected JSON null, JSON boolean, JSON number, JSON string, JSON object, or JSON array.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }

        public static void AddJsonValueError(this DomDeserializationContext domDeserializationContext, string jsonPointer)
        {
            Contract.Requires(domDeserializationContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON value. Expected JSON null, JSON boolean, JSON number, or JSON string.";
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
            domDeserializationContext.AddError(error);
        }
        #endregion
    }
}