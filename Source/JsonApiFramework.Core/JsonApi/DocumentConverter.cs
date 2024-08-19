// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for the family of <c>Document</c> objects.
/// </summary>
public class DocumentConverter : Converter<Document>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Document ReadTypedObject(JsonElement documentJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var document = CreateDocumentOrDerivedDocument(documentJsonElement, options);

        ReadJsonApiVersion(documentJsonElement, options, document);
        ReadMeta(documentJsonElement, options, document);
        ReadLinks(documentJsonElement, options, document);

        return document;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        writer.WriteStartObject();

        WriteJsonApiVersion(writer, options, document);
        WriteMeta(writer, options, document);
        WriteLinks(writer, options, document);
        WriteData(writer, options, document);
        WriteErrors(writer, options, document);
        WriteIncluded(writer, options, document);

        writer.WriteEndObject();
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Read Methods
    private static Document CreateDocumentOrDerivedDocument(JsonElement documentJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

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
        bool dataExists = documentJsonElement.TryGetProperty(Keywords.Data, out var dataJsonElement);
        bool errorExists = documentJsonElement.TryGetProperty(Keywords.Errors, out var errorsJsonElement);

        if (dataExists && errorExists)
        {
            var detail = CoreErrorStrings.JsonApiDocumentCanNotContainBothMembersDetail.FormatWith(Keywords.Data, Keywords.Errors);
            throw new JsonApiException(CoreErrorStrings.JsonApiErrorTitle, detail);
        }

        if (dataExists)
        {
            var dataJsonElementValueKind = dataJsonElement.ValueKind;
            switch (dataJsonElementValueKind)
            {
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    {
                        var nullDocument = new NullDocument();
                        return nullDocument;
                    }

                case JsonValueKind.Object:
                    {
                        var data = ReadData(dataJsonElement, options);
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

                                ReadIncluded(documentJsonElement, options, resourceDocument);
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

                case JsonValueKind.Array:
                    {
                        var dataArray = dataJsonElement.EnumerateArray();
                        if (!dataArray.Any())
                        {
                            var emptyDocument = new EmptyDocument();
                            return emptyDocument;
                        }

                        var dataCollection = dataArray.Select(x => ReadData(x, options))
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

                                    ReadIncluded(documentJsonElement, options, resourceCollectionDocument);
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

        if (errorExists)
        {
            var errorsJsonElementValueKind = errorsJsonElement.ValueKind;
            switch (errorsJsonElementValueKind)
            {
                case JsonValueKind.Array:
                    {
                        var errors = errorsJsonElement.EnumerateArray().Select(x => x.Deserialize<Error>(options))
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

    private static Tuple<DataType, object> ReadData(JsonElement dataJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var dataType = dataJsonElement.GetDataType();
        switch (dataType)
        {
            case DataType.Resource:
                {
                    var resource = dataJsonElement.Deserialize<Resource>(options);
                    var data = new Tuple<DataType, object>(DataType.Resource, resource);
                    return data;
                }

            case DataType.ResourceIdentifier:
                {
                    var resourceIdentifier = dataJsonElement.Deserialize<ResourceIdentifier>(options);
                    var data = new Tuple<DataType, object>(DataType.ResourceIdentifier, resourceIdentifier);
                    return data;
                }

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void ReadJsonApiVersion(JsonElement documentJsonElement, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        try
        {
            var jsonApiJsonElement = documentJsonElement.GetProperty(Keywords.JsonApi);
            var jsonApiVersion = jsonApiJsonElement.Deserialize<JsonApiVersion>(options);
            document.JsonApiVersion = jsonApiVersion;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }

    private static void ReadIncluded(JsonElement documentJsonElement, JsonSerializerOptions options, ISetIncluded document)
    {
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        try
        {
            var includedJsonElement = documentJsonElement.GetProperty(Keywords.Included);
            var included = includedJsonElement.EnumerateArray().Select(x => x.Deserialize<Resource>(options)).ToList();
            document.Included = included;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }
    #endregion

    #region Write Methods
    private static void WriteJsonApiVersion(Utf8JsonWriter writer, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        if (document.JsonApiVersion == null)
            return;

        writer.WritePropertyName(Keywords.JsonApi);
        var jsonApiJsonElement = JsonSerializer.SerializeToElement(document.JsonApiVersion, options);
        jsonApiJsonElement.WriteTo(writer);
    }

    private static void WriteData(Utf8JsonWriter writer, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        // If document is not a data document, nothing to do.
        if (!document.IsDataDocument()) return;

        writer.WritePropertyName(Keywords.Data);

        var data = document.GetData();
        if (data != null)
        {
            var dataJsonElement = JsonSerializer.SerializeToElement(data, options);
            dataJsonElement.WriteTo(writer);
        }
        else
        {
            if (document.IsDataCollectionDocument())
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }
    }

    private static void WriteErrors(Utf8JsonWriter writer, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(document != null);

        // If document is not an errors document, nothing to do.
        if (!document.IsErrorsDocument())
            return;

        // Write errors member.
        writer.WritePropertyName(Keywords.Errors);

        var errors = document.GetErrors();
        var errorsJsonElement = JsonSerializer.SerializeToElement(errors, options);

        errorsJsonElement.WriteTo(writer);
    }

    private static void WriteIncluded(Utf8JsonWriter writer, JsonSerializerOptions options, Document document)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
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
        var includedJsonElement = JsonSerializer.SerializeToElement(included, options);

        includedJsonElement.WriteTo(writer);
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly object[] EmptyDataArray = Enumerable.Empty<object>()
                                                                .ToArray();
    #endregion
}