// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for the <c>Document</c> class.</summary>
    public static class DocumentExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Document Extension Methods

        /// <summary>Returns if this document contains a data member.</summary>
        public static bool IsDataDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsDataDocument();
        }

        /// <summary>Returns if this document data member is a collection of resources or resource identifiers.</summary>
        public static bool IsDataCollectionDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsDataCollectionDocument();
        }

        /// <summary>Returns if this document primary data is null or empty.</summary>
        public static bool IsDataNullOrEmpty(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            switch (documentType)
            {
                case DocumentType.ResourceCollectionDocument:
                    return document.GetResourceCollection().EmptyIfNull().Any() == false;

                case DocumentType.ResourceDocument:
                    return document.GetResource() == null;

                case DocumentType.ResourceIdentifierCollectionDocument:
                    return document.GetResourceIdentifierCollection().EmptyIfNull().Any() == false;

                case DocumentType.ResourceIdentifierDocument:
                    return document.GetResourceIdentifier() == null;

                default:
                    return true;
            }
        }

        /// <summary>Returns if this document contains an empty data array member.</summary>
        public static bool IsEmptyDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsEmptyDocument();
        }

        /// <summary>Returns if this document is an errors document.</summary>
        public static bool IsErrorsDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsErrorsDocument();
        }

        /// <summary>Returns if this document included resources is null or empty.</summary>
        public static bool IsIncludedNullOrEmpty(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            switch (documentType)
            {
                case DocumentType.ResourceCollectionDocument:
                case DocumentType.ResourceDocument:
                    return document.GetIncludedResources().EmptyIfNull().Any() == false;

                default:
                    return true;
            }
        }

        /// <summary>Returns if this document only contains a meta member, no data or errors members.</summary>
        public static bool IsMetaOnlyDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            var documentMeta = document.Meta;
            return documentType.IsMetaOnlyDocument(documentMeta);
        }

        /// <summary>Returns if this document is a null document.</summary>
        public static bool IsNullDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsNullDocument();
        }

        /// <summary>Returns if this document is a resource document.</summary>
        public static bool IsResourceDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsResourceDocument();
        }

        /// <summary>Returns if this document is a resource identifier document.</summary>
        public static bool IsResourceIdentifierDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsResourceIdentifierDocument();
        }

        /// <summary>Returns if this document is a resource collection document.</summary>
        public static bool IsResourceCollectionDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsResourceCollectionDocument();
        }

        /// <summary>Returns if this document is a resource identifier collection document.</summary>
        public static bool IsResourceIdentifierCollectionDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            return documentType.IsResourceIdentifierCollectionDocument();
        }

        /// <summary>
        /// Returns if this document has at least either a data member, an errors member, or a meta member.
        /// </summary>
        /// <remarks>
        /// A valid json:api document can not contain both a data an errors member.
        /// </remarks>
        public static bool IsValidDocument(this Document document)
        {
            Contract.Requires(document != null);

            var documentType = document.GetDocumentType();
            var documentMeta = document.Meta;
            return documentType.IsValidDocument(documentMeta);
        }
        #endregion
    }
}