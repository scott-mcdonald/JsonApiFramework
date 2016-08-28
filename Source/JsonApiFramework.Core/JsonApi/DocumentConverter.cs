// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for the family of <c>Document</c> objects.
    /// </summary>
    public class DocumentConverter : Converter<Document>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Document ReadTypedObject(JObject documentJObject, JsonSerializer serializer)
        {
            Contract.Requires(documentJObject != null);
            Contract.Requires(serializer != null);

            var document = CreateDocumentOrDerivedDocument(documentJObject, serializer);

            ReadJsonApiVersion(documentJObject, serializer, document);
            ReadMeta(documentJObject, serializer, document);
            ReadLinks(documentJObject, serializer, document);

            return document;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, Document document)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            writer.WriteStartObject();

            WriteJsonApiVersion(writer, serializer, document);
            WriteMeta(writer, serializer, document);
            WriteLinks(writer, serializer, document);
            WriteData(writer, serializer, document);
            WriteErrors(writer, serializer, document);
            WriteIncluded(writer, serializer, document);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        private static Document CreateDocumentOrDerivedDocument(JToken documentJToken, JsonSerializer serializer)
        {
            Contract.Requires(documentJToken != null);
            Contract.Requires(serializer != null);

            // Data

            // Analyze "data" and "errors" JSON to determine the concrete type
            // of Document object to create:
            //
            // 1. If "data" is present, then
            //    1.1 If "data" is an object, then
            //        1.1.1 If "data" is a null, then create a "NullDocument" object.
            //        1.1.2 If "data" is a resource, then create a "ResourceDocument" object.
            //        1.1.3 If "data" is a resource identifier, then create a "ResourceIdentifierDocument" object.
            //    1.2 If "data" is an array, then
            //        1.2.1 If "data" is an empty array, then create an "EmptyDocument" object.
            //        1.2.2 If "data" is an array of resources, then create a "ResourceCollectionDocument" object.
            //        1.2.3 If "data" is an array of resource identifiers, then create a "ResourceIdentifierCollectionDocument" object.
            // 2. If "errors" is present, then create an "ErrorsDocument" object.
            // 3. Else create a "Document" object.
            var dataJToken = documentJToken.SelectToken(Keywords.Data);
            var errorsJToken = documentJToken.SelectToken(Keywords.Errors);
            if (dataJToken != null && errorsJToken != null)
            {
                var detail = CoreErrorStrings.JsonApiDocumentCanNotContainBothMembersDetail.FormatWith(Keywords.Data, Keywords.Errors);
                throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
            }

            if (dataJToken != null)
            {
                var dataJTokenType = dataJToken.Type;
                switch (dataJTokenType)
                {
                    case JTokenType.None:
                    case JTokenType.Null:
                        {
                            var nullDocument = new NullDocument();
                            return nullDocument;
                        }

                    case JTokenType.Object:
                        {
                            var data = ReadData(dataJToken, serializer);
                            var dataType = data.Item1;
                            var dataObject = data.Item2;
                            switch (dataType)
                            {
                                case DataType.Resource:
                                {
                                    var resource = (Resource)dataObject;
                                    var resourceDocument = new ResourceDocument
                                        {
                                            Data = resource
                                        };

                                    ReadIncluded(documentJToken, serializer, resourceDocument);
                                    return resourceDocument;
                                }

                                case DataType.ResourceIdentifier:
                                {
                                    var resourceIdentifier = (ResourceIdentifier)dataObject;
                                    var resourceDocument = new ResourceIdentifierDocument()
                                        {
                                            Data = resourceIdentifier
                                        };

                                    return resourceDocument;
                                }

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }

                    case JTokenType.Array:
                        {
                            var dataJArray = (JArray)dataJToken;
                            if (dataJArray.Count == 0)
                            {
                                var emptyDocument = new EmptyDocument();
                                return emptyDocument;
                            }

                            var dataCollection = dataJArray.Select(x => ReadData(x, serializer))
                                                           .ToList();
                            var dataType = dataCollection.First()
                                                         .Item1;
                            if (dataCollection.Any(x => x.Item1 != dataType))
                            {
                                // Mismatch of resource and resource identifier data types.
                                throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, CoreErrorStrings.JsonApiDocumentMustContainHetergenousCollectionOrResourcesOrResourceIdentifiersDetail);
                            }

                            switch (dataType)
                            {
                                case DataType.Resource:
                                    {
                                        var resourceCollection = dataCollection.Select(x => x.Item2)
                                                                               .Cast<Resource>()
                                                                               .ToList();
                                        var resourceCollectionDocument = new ResourceCollectionDocument
                                            {
                                                Data = resourceCollection
                                            };

                                        ReadIncluded(documentJToken, serializer, resourceCollectionDocument);
                                        return resourceCollectionDocument;
                                    }

                                case DataType.ResourceIdentifier:
                                    {
                                        var resourceIdentifierCollection = dataCollection.Select(x => x.Item2)
                                                                                         .Cast<ResourceIdentifier>()
                                                                                         .ToList();
                                        var resourceCollectionDocument = new ResourceIdentifierCollectionDocument
                                            {
                                                Data = resourceIdentifierCollection
                                            };

                                        return resourceCollectionDocument;
                                    }

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberMemberShouldBeNullAnObjectOrAnArrayDetail.FormatWith(Keywords.Data);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            if (errorsJToken != null)
            {
                var errorsJTokenType = errorsJToken.Type;
                switch (errorsJTokenType)
                {
                    case JTokenType.Array:
                        {
                            var errors = errorsJToken.Select(x => x.ToObject<Error>(serializer))
                                                     .ToList();
                            var errorsDocument = new ErrorsDocument
                                {
                                    Errors = errors
                                };
                            return errorsDocument;
                        }

                    default:
                        {
                            var detail = CoreErrorStrings.JsonApiDocumentContainsIllegalMemberMemberShouldBeAnArrayDetail.FormatWith(Keywords.Errors);
                            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
                        }
                }
            }

            var document = new Document();
            return document;
        }

        private static Tuple<DataType, object> ReadData(JToken dataJToken, JsonSerializer serializer)
        {
            Contract.Requires(dataJToken != null);
            Contract.Requires(serializer != null);

            var dataJObject = (JObject)dataJToken;
            var dataType = dataJObject.GetDataType();
            switch (dataType)
            {
                case DataType.Resource:
                    {
                        var resource = dataJObject.ToObject<Resource>(serializer);
                        var data = new Tuple<DataType, object>(DataType.Resource, resource);
                        return data;
                    }

                case DataType.ResourceIdentifier:
                    {
                        var resourceIdentifier = dataJObject.ToObject<ResourceIdentifier>(serializer);
                        var data = new Tuple<DataType, object>(DataType.ResourceIdentifier, resourceIdentifier);
                        return data;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ReadJsonApiVersion(JToken documentJToken, JsonSerializer serializer, Document document)
        {
            Contract.Requires(documentJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            var jsonApiJToken = documentJToken.SelectToken(Keywords.JsonApi);
            if (jsonApiJToken == null)
                return;

            var jsonApiVersion = jsonApiJToken.ToObject<JsonApiVersion>(serializer);
            document.JsonApiVersion = jsonApiVersion;
        }

        private static void ReadIncluded(JToken documentJToken, JsonSerializer serializer, ISetIncluded document)
        {
            Contract.Requires(documentJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            var includedJToken = documentJToken.SelectToken(Keywords.Included);
            if (includedJToken == null)
                return;

            var included = includedJToken.Select(x => x.ToObject<Resource>(serializer))
                                         .ToList();
            document.Included = included;
        }
        #endregion

        #region Write Methods
        private static void WriteJsonApiVersion(JsonWriter writer, JsonSerializer serializer, Document document)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            if (document.JsonApiVersion == null)
                return;

            writer.WritePropertyName(Keywords.JsonApi);
            var jsonApiJToken = JToken.FromObject(document.JsonApiVersion, serializer);
            jsonApiJToken.WriteTo(writer);
        }

        private static void WriteData(JsonWriter writer, JsonSerializer serializer, Document document)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            // If document is not a data document, nothing to do.
            if (!document.IsDataDocument())
                return;

            if (!document.IsDataCollectionDocument())
            {
                // Write data as an object.
                writer.WritePropertyName(Keywords.Data);

                var data = document.GetData();
                if (data != null)
                {
                    var dataJToken = JToken.FromObject(data, serializer);
                    var dataJObject = (JObject)dataJToken;

                    dataJObject.WriteTo(writer);
                }
                else
                {
                    writer.WriteValue(NullData);
                }
            }
            else
            {
                // Write data as an array.
                writer.WritePropertyName(Keywords.Data);

                var data = document.GetData();
                if (data != null)
                {
                    var dataJToken = JToken.FromObject(data, serializer);
                    var dataJArray = (JArray)dataJToken;

                    dataJArray.WriteTo(writer);
                }
                else
                {
                    writer.WriteValue(EmptyDataArray);
                }
            }
        }

        private static void WriteErrors(JsonWriter writer, JsonSerializer serializer, Document document)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            // If document is not an errors document, nothing to do.
            if (!document.IsErrorsDocument())
                return;

            // Write errors member.
            writer.WritePropertyName(Keywords.Errors);

            var errors = document.GetErrors();
            var errorsJToken = JToken.FromObject(errors, serializer);
            var errorsJArray = (JArray)errorsJToken;

            errorsJArray.WriteTo(writer);
        }

        private static void WriteIncluded(JsonWriter writer, JsonSerializer serializer, Document document)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(document != null);

            // If document is not a resource document or a resource collection
            // document, nothing to do.
            if (!document.IsResourceDocument() && !document.IsResourceCollectionDocument())
                return;

            // If document does not contain any data, then do not write an
            // included member as included can not be present per specification.
            if (document.IsDataNullOrEmpty())
                return;

            // If document does not contain any included resources, then
            // nothing to write.
            if (document.IsIncludedNullOrEmpty())
                return;

            // Write included member.
            writer.WritePropertyName(Keywords.Included);

            var included = document.GetIncludedResources();
            var includedJToken = JToken.FromObject(included, serializer);
            var includedJArray = (JArray)includedJToken;

            includedJArray.WriteTo(writer);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly object NullData = default(object);

        private static readonly object[] EmptyDataArray = Enumerable.Empty<object>()
                                                                    .ToArray();
        #endregion
    }
}