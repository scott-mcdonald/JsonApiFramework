// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomDocumentAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Document expected, DomDocument actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // JsonApiVersion
            var expectedJsonApiVersion = expected.JsonApiVersion;
            var domJsonApiVersion = actual.GetNode(DomNodeType.JsonApiVersion);
            DomJsonApiVersionAssert.Equal(expectedJsonApiVersion, domJsonApiVersion);

            // Meta
            var expectedMeta = expected.Meta;
            var domMeta = actual.GetNode(DomNodeType.Meta);
            DomMetaAssert.Equal(expectedMeta, domMeta);

            // Links
            var expectedLinks = expected.Links;
            var domLinks = actual.GetNode(DomNodeType.Links);
            DomLinksAssert.Equal(expectedLinks, domLinks);

            // Data/Included
            if (expected.IsDataDocument())
            {
                var expectedDocumentType = expected.GetDocumentType();
                switch (expectedDocumentType)
                {
                    case DocumentType.EmptyDocument:
                        {
                            var domDataCollection = actual.GetNode(DomNodeType.DataCollection);

                            DomDataCollectionAssert.Empty(domDataCollection);
                        }
                        break;

                    case DocumentType.NullDocument:
                        {
                            var domData = actual.GetNode(DomNodeType.Data);

                            DomDataAssert.Null(domData);
                        }
                        break;

                    case DocumentType.ResourceDocument:
                        {
                            var expectedResource = expected.GetResource();
                            var domData = actual.GetNode(DomNodeType.Data);

                            DomDataAssert.Equal(expectedResource, domData);

                            // Included
                            var expectedIncluded = expected.GetIncludedResources();
                            var domIncluded = actual.GetNode(DomNodeType.Included);
                            DomIncludedAssert.Equal(expectedIncluded, domIncluded);
                        }
                        break;

                    case DocumentType.ResourceCollectionDocument:
                        {
                            var expectedResourceCollection = expected.GetResourceCollection();
                            var domDataCollection = actual.GetNode(DomNodeType.DataCollection);

                            DomDataCollectionAssert.Equal(expectedResourceCollection, domDataCollection);

                            // Included
                            var expectedIncluded = expected.GetIncludedResources();
                            var domIncluded = actual.GetNode(DomNodeType.Included);
                            DomIncludedAssert.Equal(expectedIncluded, domIncluded);
                        }
                        break;

                    case DocumentType.ResourceIdentifierDocument:
                        {
                            var expectedResourceIdentifier = expected.GetResourceIdentifier();
                            var domData = actual.GetNode(DomNodeType.Data);

                            DomDataAssert.Equal(expectedResourceIdentifier, domData);
                        }
                        break;

                    case DocumentType.ResourceIdentifierCollectionDocument:
                        {
                            var expectedResourceIdentifierCollection = expected.GetResourceIdentifierCollection();
                            var domDataCollection = actual.GetNode(DomNodeType.DataCollection);

                            DomDataCollectionAssert.Equal(expectedResourceIdentifierCollection, domDataCollection);
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // If this is a data document then the document errors node
                // should not be present in the DOM tree.
                var domErrors = actual.GetNode(DomNodeType.Errors);
                Assert.Null(domErrors);
            }
            else if (expected.IsErrorsDocument())
            {
                // Errors
                var expectedErrors = expected.GetErrors();
                var domErrors = actual.GetNode(DomNodeType.Errors);
                DomErrorsAssert.Equal(expectedErrors, domErrors);

                // If this is an errors document then the document data, data collection,
                // or included nodes should not be present in the DOM tree.
                var domData = actual.GetNode(DomNodeType.Data);
                Assert.Null(domData);

                var domDataCollection = actual.GetNode(DomNodeType.DataCollection);
                Assert.Null(domDataCollection);

                var domIncluded = actual.GetNode(DomNodeType.Included);
                Assert.Null(domIncluded);
            }
            else
            {
                // If this is a base document then the document data, data collection, included,
                // or errors nodes should not be present in the DOM tree.
                var domData = actual.GetNode(DomNodeType.Data);
                Assert.Null(domData);

                var domDataCollection = actual.GetNode(DomNodeType.DataCollection);
                Assert.Null(domDataCollection);

                var domIncluded = actual.GetNode(DomNodeType.Included);
                Assert.Null(domIncluded);

                var domErrors = actual.GetNode(DomNodeType.Errors);
                Assert.Null(domErrors);
            }
        }
        #endregion
    }
}