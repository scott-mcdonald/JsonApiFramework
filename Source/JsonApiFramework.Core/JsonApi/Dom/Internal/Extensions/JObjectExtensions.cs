// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Properties;

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Extension methods for JSON.NET the JObject class.</summary>
    internal static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the json:api data type that the JSON.NET object represents.</summary>
        public static DataType GetApiDataType(this JObject jObject)
        {
            Contract.Requires(jObject != null);

            // Determine if JObject represents one of the following:
            // 1. None,
            // 2. Resource, or
            // 3. ResourceIdentifier
            //
            // Assume if the JObject only has 2 child properties named with
            // the json:api "type" and "id" keywords then if must be a ResourceIdentifier.
            var propertiesCount = jObject.Properties().Count();
            switch (propertiesCount)
            {
                case 0:
                    {
                        // JObject contains no properties, is NOT a resource or resource identifier.
                        return DataType.None;
                    }

                case 1:
                    {
                        // JObject contains 1 property.
                        //
                        // If it is "type" then it MUST be a Resource for POST purposes (Server generates the identifier)
                        // else is NOT a resource or resource identifier.
                        var property = jObject.Properties()
                                              .First();
                        var propertyNameIsType = property.Name == Keywords.Type;
                        return propertyNameIsType ? DataType.Resource : DataType.None;
                    }

                case 2:
                    {
                        // JObject contains 2 properties.
                        //
                        // If they are "type" and "id" it is MOST LIKELY a ResourceIdentifier
                        // else if one of the two properties is "type" then it MUST be a Resource for POST purposes (Server generates the identifier)
                        // else is NOT a resource or resource identifier.
                        var propertyNames = jObject.Properties()
                                                   .OrderByDescending(x => x.Name)
                                                   .Select(x => x.Name)
                                                   .ToArray();
                        var propertyNamesContainOnlyTypeAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Id;
                        if (propertyNamesContainOnlyTypeAndId)
                            return DataType.ResourceIdentifier;

                        var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type;
                        return propertyNamesContainType ? DataType.Resource : DataType.None;
                    }

                case 3:
                    {
                        // JObject contains 3 properties.
                        //
                        // If they are "type", "id", and "meta" it is MOST LIKELY a ResourceIdentifier
                        // else if one of the three properties is "type" then it MUST be a Resource for POST purposes (Server generates the identifier)
                        // else is NOT a resource or resource identifier.
                        var propertyNames = jObject.Properties()
                                                   .OrderByDescending(x => x.Name)
                                                   .Select(x => x.Name)
                                                   .ToArray();
                        var propertyNamesContainOnlyTypeAndMetaAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Meta && propertyNames[2] == Keywords.Id;
                        if (propertyNamesContainOnlyTypeAndMetaAndId)
                        {
                            return DataType.ResourceIdentifier;
                        }

                        var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type || propertyNames[2] == Keywords.Type;
                        return propertyNamesContainType ? DataType.Resource : DataType.None;
                    }

                default:
                    {
                        // JObject contains 4+ properties.
                        //
                        // If one of the properties is "type" then it MUST be a Resource
                        // else is NOT a resource or resource identifier.
                        var propertyNamesContainType = jObject.Properties()
                                                              .Any(x => x.Name == Keywords.Type);
                        return propertyNamesContainType ? DataType.Resource : DataType.None;
                    }
            }
        }

        /// <summary>Gets the json:api document type that the JSON.NET object represents.</summary>
        public static DocumentType GetApiDocumentType(this JObject jObject)
        {
            Contract.Requires(jObject != null);

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
                        return DocumentType.NullDocument;

                    case JTokenType.Object:
                        {
                            var dataJObject = (JObject)dataJToken;
                            var dataJObjectDataType = dataJObject.GetApiDataType();
                            switch (dataJObjectDataType)
                            {
                                case DataType.Resource:
                                    return DocumentType.ResourceDocument;

                                case DataType.ResourceIdentifier:
                                    return DocumentType.ResourceIdentifierDocument;

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
                                return DocumentType.EmptyDocument;
                            }

                            var dataJArrayFirstJObjectDataType = dataJArrayFirstJObject.GetApiDataType();
                            switch (dataJArrayFirstJObjectDataType)
                            {
                                case DataType.Resource:
                                    return DocumentType.ResourceCollectionDocument;

                                case DataType.ResourceIdentifier:
                                    return DocumentType.ResourceIdentifierCollectionDocument;

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
                        return DocumentType.ErrorsDocument;

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeAnArrayDetail
                                                         .FormatWith(Keywords.Errors);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            return DocumentType.Document;
        }

        /// <summary>Gets the json:api relationship type that the JSON.NET object represents.</summary>
        public static RelationshipType GetApiRelationshipType(this JObject jObject)
        {
            Contract.Requires(jObject != null);

            // Analyze "data" nodes to determine the relationship type.
            //
            // 1. If "data" is not present, then "Relationship".
            // 2. If "data" is present, then
            //    2.1 If "data" is null, then "ToOneRelationship".
            //    2.2 If "data" is an object, then "ToOneRelationship".
            //    2.3 If "data" is an array, then "ToManyRelationship".
            var dataJToken = jObject.SelectToken(Keywords.Data);
            if (dataJToken == null)
                return RelationshipType.Relationship;

            var dataJTokenType = dataJToken.Type;
            switch (dataJTokenType)
            {
                case JTokenType.Null:
                case JTokenType.Object:
                    return RelationshipType.ToOneRelationship;

                case JTokenType.Array:
                    return RelationshipType.ToManyRelationship;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        public static DocumentType GetApiDocumentType(JToken dataJToken, JToken errorsJToken)
        {
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
                        return DocumentType.NullDocument;

                    case JTokenType.Object:
                        {
                            var dataJObject = (JObject)dataJToken;
                            var dataJObjectDataType = dataJObject.GetApiDataType();
                            switch (dataJObjectDataType)
                            {
                                case DataType.Resource:
                                    return DocumentType.ResourceDocument;

                                case DataType.ResourceIdentifier:
                                    return DocumentType.ResourceIdentifierDocument;

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
                                return DocumentType.EmptyDocument;
                            }

                            var dataJArrayFirstJObjectDataType = dataJArrayFirstJObject.GetApiDataType();
                            switch (dataJArrayFirstJObjectDataType)
                            {
                                case DataType.Resource:
                                    return DocumentType.ResourceCollectionDocument;

                                case DataType.ResourceIdentifier:
                                    return DocumentType.ResourceIdentifierCollectionDocument;

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
                        return DocumentType.ErrorsDocument;

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberShouldBeAnArrayDetail
                                                         .FormatWith(Keywords.Errors);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            return DocumentType.Document;
        }
        #endregion
    }
}
