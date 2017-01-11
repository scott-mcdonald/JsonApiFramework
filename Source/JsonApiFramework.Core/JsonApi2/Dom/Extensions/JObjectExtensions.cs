// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>Extension methods for JSON.NET the JObject class.</summary>
    public static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the json:api data type that a JSON.NET object represents.</summary>
        public static ApiDataType GetApiDataType(this JObject jObject)
        {
            // Handle special case when JObject is null.
            if (jObject == null)
            {
                return ApiDataType.Unknown;
            }

            // Determine if JObject represents one of the following:
            // 1. Resource, or
            // 2. ResourceIdentifier
            // Assume if the JObject only has 2 child properties named with
            // the json:api "type" and "id" keywords then if must be a ResourceIdentifier.
            var propertiesCount = jObject.Properties().Count();
            switch (propertiesCount)
            {
                case 0:
                    {
                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 1:
                    {
                        var property = jObject.Properties()
                                              .First();
                        if (property.Name == Keywords.Type)
                            return ApiDataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 2:
                    {
                        var propertyNames = jObject.Properties()
                                                   .OrderByDescending(x => x.Name)
                                                   .Select(x => x.Name)
                                                   .ToArray();
                        var propertyNamesContainOnlyTypeAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Id;
                        if (propertyNamesContainOnlyTypeAndId)
                            return ApiDataType.ResourceIdentifier;

                        var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type;
                        if (propertyNamesContainType)
                            return ApiDataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 3:
                    {
                        var propertyNames = jObject.Properties()
                                                   .OrderByDescending(x => x.Name)
                                                   .Select(x => x.Name)
                                                   .ToArray();
                        var propertyNamesContainOnlyTypeAndMetaAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Meta && propertyNames[2] == Keywords.Id;
                        if (propertyNamesContainOnlyTypeAndMetaAndId)
                        {
                            // Note: A JSON object with "type", "id", and "meta" could be either a resource identifier or resource per the JSON API specification.
                            // Because there is no "attributes", "relationships", or "links" properties the most likely intention is the JSON object is a resource
                            // identifier although this is not 100% but my best guess.
                            return ApiDataType.ResourceIdentifier;
                        }

                        var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type || propertyNames[2] == Keywords.Type;
                        if (propertyNamesContainType)
                            return ApiDataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                default:
                    {
                        var typeProperty = jObject.Properties()
                                                  .SingleOrDefault(x => x.Name == Keywords.Type);
                        if (typeProperty != null)
                            return ApiDataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }
            }
        }

        /// <summary>Gets the json:api document type that a JSON.NET object represents.</summary>
        public static ApiDocumentType GetApiDocumentType(this JObject jObject)
        {
            // Handle special case when JObject is null.
            if (jObject == null)
            {
                return ApiDocumentType.Document;
            }

            // Analyze "data" and "errors" nodes to determine the concrete
            // document type.
            //
            // 1. If "data" is present, then
            //    1.1 If "data" is an object, then
            //        1.1.1 If "data" is null, then "NullDocument".
            //        1.1.2 If "data" is a resource, then "ResourceDocument".
            //        1.1.3 If "data" is a resource identifier, then "ResourceIdentifierDocument".
            //    1.2 If "data" is an array, then
            //        1.2.1 If "data" is an empty array, then "EmptyDocument".
            //        1.2.2 If "data" is an array of resources, then "ResourceCollectionDocument".
            //        1.2.3 If "data" is an array of resource identifiers, then "ResourceIdentifierCollectionDocument".
            // 2. If "errors" is present, then
            //    2.1 If "errors" is an array, then "ErrorsDocument".
            // 3. Else "Document".
            var dataJToken = jObject.SelectToken(Keywords.Data);
            var errorsJToken = jObject.SelectToken(Keywords.Errors);
            if (dataJToken != null && errorsJToken != null)
            {
                var detail = CoreErrorStrings.JsonApiDocumentContainsBothMembersDetail
                                             .FormatWith(Keywords.Data, Keywords.Errors);
                throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
            }

            if (dataJToken != null)
            {
                var dataJTokenType = dataJToken.Type;
                switch (dataJTokenType)
                {
                    case JTokenType.Null:
                        return ApiDocumentType.NullDocument;

                    case JTokenType.Object:
                        {
                            var dataJObject = (JObject)dataJToken;
                            var dataJObjectDataType = dataJObject.GetApiDataType();
                            switch (dataJObjectDataType)
                            {
                                case ApiDataType.Resource:
                                    return ApiDocumentType.ResourceDocument;

                                case ApiDataType.ResourceIdentifier:
                                    return ApiDocumentType.ResourceIdentifierDocument;

                                default:
                                    var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeResourceOrResourceIdentifierDetail
                                                                 .FormatWith(Keywords.Data);
                                    throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                            }
                        }

                    case JTokenType.Array:
                        {
                            var dataJArray = (JArray)dataJToken;
                            var dataJArrayFirstJObject = (JObject)dataJArray.FirstOrDefault();
                            if (dataJArrayFirstJObject == null)
                            {
                                return ApiDocumentType.EmptyDocument;
                            }

                            var dataJArrayFirstJObjectDataType = dataJArrayFirstJObject.GetApiDataType();
                            switch (dataJArrayFirstJObjectDataType)
                            {
                                case ApiDataType.Resource:
                                    return ApiDocumentType.ResourceCollectionDocument;

                                case ApiDataType.ResourceIdentifier:
                                    return ApiDocumentType.ResourceIdentifierCollectionDocument;

                                default:
                                    var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeResourceOrResourceIdentifierDetail
                                                                 .FormatWith(Keywords.Data);
                                    throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                            }

                        }

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeNullAnObjectOrAnArrayDetail
                                                         .FormatWith(Keywords.Data);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            // ReSharper disable once InvertIf
            if (errorsJToken != null)
            {
                var errorsJTokenType = errorsJToken.Type;
                switch (errorsJTokenType)
                {
                    case JTokenType.Array:
                        return ApiDocumentType.ErrorsDocument;

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeAnArrayDetail
                                                         .FormatWith(Keywords.Errors);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            return ApiDocumentType.Document;
        }
        #endregion
    }
}
