// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Dom;
using JsonApiFramework.Dom.Internal;

using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Extension methods for the JSON.NET JObject class.</summary>
    internal static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the json:api document type that the JSON.NET object represents.</summary>
        public static ApiDocumentType GetApiDocumentType(this JObject jObject, DomDeserializationContext domDeserializationContext)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(domDeserializationContext != null);

            // Analyze "data" and "errors" nodes to determine the concrete
            // document type.
            //
            // 1. If "data" is present, then
            //    1.1 If "data" is a null or an object, then "DataDocument".
            //    1.2 If "data" is an array, then "DataCollectionDocument".
            // 2. If "errors" is present, then
            //    2.1 If "errors" is an array, then "ErrorsDocument".
            // 3. Else "Document".
            var dataJToken = jObject.SelectToken(Keywords.Data);
            var errorsJToken = jObject.SelectToken(Keywords.Errors);
            if (dataJToken != null && errorsJToken != null)
            {
                var jsonPointer = jObject.GetJsonPointer();
                domDeserializationContext.AddJsonApiDocumentContainsDataAndErrorsError(jsonPointer);

                return ApiDocumentType.Document;
            }

            if (dataJToken != null)
            {
                var dataJTokenType = dataJToken.Type;
                switch (dataJTokenType)
                {
                    case JTokenType.Null:
                    case JTokenType.Object:
                        return ApiDocumentType.DataDocument;

                    case JTokenType.Array:
                        return ApiDocumentType.DataCollectionDocument;

                    default:
                        return ApiDocumentType.Document;
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
                        return ApiDocumentType.Document;
                }
            }

            return ApiDocumentType.Document;
        }

        /// <summary>Gets the json:api relationship type that the JSON.NET object represents.</summary>
        public static RelationshipType GetApiRelationshipType(this JObject jObject)
        {
            Contract.Requires(jObject != null);

            // Analyze "data" nodes to determine the relationship type.
            //
            // 1. If "data" is not present, then "Relationship".
            // 2. If "data" is present, then
            //    2.1 If "data" is null or an object, then "ToOneRelationship".
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
                    return RelationshipType.Relationship;
            }
        }
        #endregion
    }
}
