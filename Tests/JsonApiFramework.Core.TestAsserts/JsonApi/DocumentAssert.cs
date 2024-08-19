// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Reflection;
using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class DocumentAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Document expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        DocumentAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Document expected, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Object, actualJsonValueKind);

        // Meta
        var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
        MetaAssert.Equal(expected.Meta, actualMetaJsonElement);

        // Links
        var actualLinksJsonElement = actualJsonElement.GetProperty(Keywords.Links);
        LinksAssert.Equal(expected.Links, actualLinksJsonElement);

        // Data

        // Document types can be the following:
        // 1. Document
        // 2. EmptyDocument
        // 3. ErrorsDocument
        // 4. NullDocument
        // 5. ResourceDocument
        // 6. ResourceIdentifierDocument
        // 7. ResourceCollectionDocument
        // 8. ResourceIdentifierCollectionDocument
        var expectedType = expected.GetType();
        bool actualDataJsonElementExists = actualJsonElement.TryGetProperty(Keywords.Data, out var actualDataJsonElement);
        bool actualErrorsJsonElementExists = actualJsonElement.TryGetProperty(Keywords.Errors, out var actualErrorsJsonElement);

        if (actualDataJsonElementExists && actualErrorsJsonElementExists)
        {
            var message = "Document can not contain both \"{0}\" and \"{1}\" members.".FormatWith(Keywords.Data, Keywords.Errors);
            Assert.True(false, message);
            return;
        }

        if (actualDataJsonElementExists)
        {
            var actualDataJsonValueKind = actualDataJsonElement.ValueKind;
            switch (actualDataJsonValueKind)
            {
                // NullDocument, ResourceDocument, or ResourceIdentifierDocument (data is null)
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    {
                        // For this scenario, the expected type is either a NullDocument, ResourceDocument, or a ResourceIdentifierDocument.
                        Assert.True(expected.IsDataNullOrEmpty());
                    }
                    break;

                // ResourceDocument or ResourceIdentifierDocument (one resource or resource identifier)
                case JsonValueKind.Object:
                    {
                        var dataType = actualDataJsonElement.GetDataType();
                        switch (dataType)
                        {
                            case DataType.Resource:
                                {
                                    Assert.Equal(ResourceDocumentTypeInfo, expectedType);

                                    var expectedResourceDocument = (ResourceDocument)expected;
                                    var expectedResource = expectedResourceDocument.Data;

                                    ResourceAssert.Equal(expectedResource, actualDataJsonElement);

                                    // Included
                                    var expectedIncluded = expected.GetIncludedResources();
                                    var actualIncludedJsonElement = actualJsonElement.GetProperty(Keywords.Included);
                                    ResourceAssert.Equal(expectedIncluded, actualIncludedJsonElement);
                                }
                                break;

                            case DataType.ResourceIdentifier:
                                {
                                    Assert.Equal(ResourceIdentifierDocumentTypeInfo, expectedType);

                                    var expectedResourceIdentifierDocument = (ResourceIdentifierDocument)expected;
                                    var expectedResourceIdentifier = expectedResourceIdentifierDocument.Data;

                                    ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualDataJsonElement);
                                }
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;

                // ResourceCollectionDocument or ResourceIdentifierCollectionDocument (many resources or resource identifiers)
                case JsonValueKind.Array:
                    {
                        var count = actualDataJsonElement.EnumerateArray().Count();
                        if (count == 0)
                        {
                            // For this scenario, the expected type is either an EmptyDocument, ResourceCollectionDocument, or a ResourceIdentifierCollectionDocument.
                            Assert.True(expected.IsDataNullOrEmpty());
                        }
                        else
                        {
                            var dataType = actualDataJsonElement[0].GetDataType();
                            switch (dataType)
                            {
                                case DataType.Resource:
                                    {
                                        Assert.Equal(ResourceCollectionDocumentTypeInfo, expectedType);

                                        var expectedResourceCollectionDocument = (ResourceCollectionDocument)expected;
                                        var expectedResourceCollection = expectedResourceCollectionDocument.Data;

                                        ResourceAssert.Equal(expectedResourceCollection, actualDataJsonElement);

                                        // Included
                                        var expectedIncluded = expected.GetIncludedResources();
                                        var actualIncludedJsonElement = actualJsonElement.GetProperty(Keywords.Included);
                                        ResourceAssert.Equal(expectedIncluded, actualIncludedJsonElement);
                                    }
                                    break;

                                case DataType.ResourceIdentifier:
                                    {
                                        Assert.Equal(ResourceIdentifierCollectionDocumentTypeInfo, expectedType);

                                        var expectedResourceIdentifierCollectionDocument = (ResourceIdentifierCollectionDocument)expected;
                                        var expectedResourceIdentifierCollection = expectedResourceIdentifierCollectionDocument.Data;

                                        ResourceIdentifierAssert.Equal(expectedResourceIdentifierCollection, actualDataJsonElement);
                                    }
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                    break;

                default:
                    Assert.True(false, string.Format("Invalid JsonElement [type={0}] for document.", actualJsonValueKind));
                    break;
            }
        }
        else if (actualErrorsJsonElementExists)
        {
            // Errors
            var expectedErrors = expected.GetErrors();
            ErrorAssert.Equal(expectedErrors, actualErrorsJsonElement);
        }
        else
        {
            Assert.Equal(DocumentTypeInfo, expectedType);
        }
    }

    public static void Equal(Document expected, Document actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        // JsonApiVersion
        JsonApiVersionAssert.Equal(expected.JsonApiVersion, actual.JsonApiVersion);

        // Meta
        MetaAssert.Equal(expected.Meta, actual.Meta);

        // Links
        LinksAssert.Equal(expected.Links, actual.Links);

        // Data

        // Document types can be the following:
        // 1. Document
        // 2. EmptyDocument
        // 3. ErrorsDocument
        // 4. NullDocument
        // 5. ResourceDocument
        // 6. ResourceIdentifierDocument
        // 7. ResourceCollectionDocument
        // 8. ResourceIdentifierCollectionDocument
        var expectedDocumentTypeInfo = expected.GetType().GetTypeInfo();
        if (expectedDocumentTypeInfo == DocumentTypeInfo)
        {
            // NOOP
        }
        else if (expectedDocumentTypeInfo == EmptyDocumentTypeInfo)
        {
            var expectedData = expected.GetData();
            var actualData = actual.GetData();

            ClrObjectAssert.Equal(expectedData, actualData);
        }
        else if (expectedDocumentTypeInfo == ErrorsDocumentTypeInfo)
        {
            var expectedErrors = expected.GetErrors();
            var actualErrors = actual.GetErrors();

            ErrorAssert.Equal(expectedErrors, actualErrors);
        }
        else if (expectedDocumentTypeInfo == NullDocumentTypeInfo)
        {
            var expectedData = expected.GetData();
            var actualData = actual.GetData();

            ClrObjectAssert.Equal(expectedData, actualData);
        }
        else if (expectedDocumentTypeInfo == ResourceDocumentTypeInfo)
        {
            var expectedResourceDocument = (ResourceDocument)expected;
            var expectedResource = expectedResourceDocument.Data;
            if (expectedResource == null)
            {
                Assert.True(actual.IsNullDocument() || (actual.IsResourceDocument() && actual.IsDataNullOrEmpty()));
                return;
            }

            var actualResourceDocument = (ResourceDocument)actual;
            var actualResource = actualResourceDocument.Data;

            ResourceAssert.Equal(expectedResource, actualResource);

            // Included
            var expectedIncluded = expected.GetIncludedResources();
            var actualIncluded = actual.GetIncludedResources();
            ResourceAssert.Equal(expectedIncluded, actualIncluded);
        }
        else if (expectedDocumentTypeInfo == ResourceIdentifierDocumentTypeInfo)
        {
            var expectedResourceIdentifierDocument = (ResourceIdentifierDocument)expected;
            var expectedResourceIdentifier = expectedResourceIdentifierDocument.Data;
            if (expectedResourceIdentifier == null)
            {
                Assert.True(actual.IsNullDocument() || (actual.IsResourceIdentifierDocument() && actual.IsDataNullOrEmpty()));
                return;
            }

            var actualResourceIdentifierDocument = (ResourceIdentifierDocument)actual;
            var actualResourceIdentifier = actualResourceIdentifierDocument.Data;

            ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifier);
        }
        else if (expectedDocumentTypeInfo == ResourceCollectionDocumentTypeInfo)
        {
            var expectedResourceCollectionDocument = (ResourceCollectionDocument)expected;
            var expectedResourceCollection = expectedResourceCollectionDocument.Data;
            Assert.NotNull(expectedResourceCollection);

            if (!expectedResourceCollection.Any())
            {
                Assert.True(actual.IsEmptyDocument() || (actual.IsResourceCollectionDocument() && actual.IsDataNullOrEmpty()));
                return;
            }

            var actualResourceCollectionDocument = (ResourceCollectionDocument)actual;
            var actualResourceCollection = actualResourceCollectionDocument.Data;

            ResourceAssert.Equal(expectedResourceCollection, actualResourceCollection);

            // Included
            var expectedIncludedResources = expected.GetIncludedResources();
            var actualIncludedResources = actual.GetIncludedResources();
            ResourceAssert.Equal(expectedIncludedResources, actualIncludedResources);
        }
        else if (expectedDocumentTypeInfo == ResourceIdentifierCollectionDocumentTypeInfo)
        {
            var expectedResourceIdentifierCollectionDocument = (ResourceIdentifierCollectionDocument)expected;
            var expectedResourceIdentifierCollection = expectedResourceIdentifierCollectionDocument.Data;
            Assert.NotNull(expectedResourceIdentifierCollection);

            if (!expectedResourceIdentifierCollection.Any())
            {
                Assert.True(actual.IsEmptyDocument() || (actual.IsResourceIdentifierCollectionDocument() && actual.IsDataNullOrEmpty()));
                return;
            }

            var actualResourceIdentifierCollectionDocument = (ResourceIdentifierCollectionDocument)actual;
            var actualResourceIdentifierCollection = actualResourceIdentifierCollectionDocument.Data;

            ResourceIdentifierAssert.Equal(expectedResourceIdentifierCollection, actualResourceIdentifierCollection);
        }
        else
        {
            Assert.True(false, string.Format("Unknown document type={0}", expectedDocumentTypeInfo));
        }
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly TypeInfo DocumentTypeInfo = typeof(Document).GetTypeInfo();
    private static readonly TypeInfo EmptyDocumentTypeInfo = typeof(EmptyDocument).GetTypeInfo();
    private static readonly TypeInfo ErrorsDocumentTypeInfo = typeof(ErrorsDocument).GetTypeInfo();
    private static readonly TypeInfo NullDocumentTypeInfo = typeof(NullDocument).GetTypeInfo();
    private static readonly TypeInfo ResourceDocumentTypeInfo = typeof(ResourceDocument).GetTypeInfo();
    private static readonly TypeInfo ResourceIdentifierDocumentTypeInfo = typeof(ResourceIdentifierDocument).GetTypeInfo();
    private static readonly TypeInfo ResourceCollectionDocumentTypeInfo = typeof(ResourceCollectionDocument).GetTypeInfo();
    private static readonly TypeInfo ResourceIdentifierCollectionDocumentTypeInfo = typeof(ResourceIdentifierCollectionDocument).GetTypeInfo();
    #endregion
}