// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Properties;

namespace JsonApiFramework.Internal
{
    /// <summary>Represents an implementation detail for creating standard framework error messages.</summary>
    internal static class ErrorMessageFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JSON Error Messages
        public static ErrorMessage CreateJsonArrayReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON array. Expected JSON null or JSON array.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonArrayItemReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON array item. Expected JSON null, JSON boolean, JSON number, JSON string, or JSON object.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonObjectReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for a standard JSON object. Expected JSON null or JSON object.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonObjectMemberReadErrorMessage(string apiPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected '{apiPropertyName}' member for a standard JSON object. Expected JSON null, JSON boolean, JSON number, JSON string, JSON object, or JSON array.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonValueReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for a standard JSON value. Expected JSON null, JSON boolean, JSON number, or JSON string.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }
        #endregion

        #region JSON Api Error Messages
        public static ErrorMessage CreateJsonApiArrayItemReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = "Read unexpected JSON for json:api array item. Expected JSON object. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiDataReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api data. Expected JSON null, JSON object, or JSON array. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiDocumentContainsDataAndErrorsReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api document. Document contains both data and errors members. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiErrorsReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api errors. Expected JSON array. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiIncludedReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api included. Expected JSON array. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiLinkReadErrorMessage()
        {
            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api link. Expected JSON null, JSON string, or JSON object. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiObjectReadErrorMessage(string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {apiObjectType}. Expected JSON null or JSON object. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiObjectMemberReadErrorMessage(string apiPropertyName, string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected '{apiPropertyName}' member for json:api {apiObjectType}. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }

        public static ErrorMessage CreateJsonApiStringReadErrorMessage(string apiObjectType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiObjectType) == false);

            var title = CoreErrorStrings.JsonReadErrorTitle;
            var detail = $"Read unexpected JSON for json:api {apiObjectType}. Expected JSON null or JSON string. Violates the JSON API specification.";
            var errorMessage = new ErrorMessage(title, detail);
            return errorMessage;
        }
        #endregion
    }
}