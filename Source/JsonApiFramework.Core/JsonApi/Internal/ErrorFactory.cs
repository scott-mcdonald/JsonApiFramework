// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Internal;
using JsonApiFramework.Properties;

namespace JsonApiFramework.JsonApi.Internal
{
    internal static class ErrorFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JSON Error Factory Methods
        public static Error CreateJsonArrayReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonArrayReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonArrayItemReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonArrayItemReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonObjectReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonObjectReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonObjectMemberReadError(string jsonPointer, string apiPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonObjectMemberReadErrorMessage(apiPropertyName);
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonValueReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonValueReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }
        #endregion

        #region JSON API Error Factory Methods
        public static Error CreateJsonApiArrayItemReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiArrayItemReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiDataReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiDataReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiDocumentContainsDataAndErrorsReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiDocumentContainsDataAndErrorsReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiErrorsReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiErrorsReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiIncludedReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiIncludedReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiLinkReadError(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiLinkReadErrorMessage();
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiObjectReadError(string jsonPointer, string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiObjectReadErrorMessage(apiObjectType);
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiObjectMemberReadError(string jsonPointer, string apiPropertyName, string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiObjectMemberReadErrorMessage(apiPropertyName, apiObjectType);
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }

        public static Error CreateJsonApiStringReadError(string jsonPointer, string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var errorMessage = ErrorMessageFactory.CreateJsonApiStringReadErrorMessage(apiObjectType);
            var source = ErrorSource.CreateFromJsonPointer(jsonPointer);
            var error = new Error(null, null, HttpStatusCode.BadRequest, null, errorMessage.Title, errorMessage.Detail, source, null);
            return error;
        }
        #endregion
    }
}