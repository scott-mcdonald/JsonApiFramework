// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
    public static class DocumentReaderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentReader Extensions
        public static bool IsDataDocument(this IDocumentReader documentReader)
        {
            Contract.Requires(documentReader != null);

            var documentType = documentReader.GetDocumentType();
            return documentType.IsDataDocument();
        }

        public static bool IsErrorsDocument(this IDocumentReader documentReader)
        {
            Contract.Requires(documentReader != null);

            var documentType = documentReader.GetDocumentType();
            return documentType.IsErrorsDocument();
        }

        public static bool IsMetaOnlyDocument(this IDocumentReader documentReader)
        {
            Contract.Requires(documentReader != null);

            var documentType = documentReader.GetDocumentType();
            var documentMeta = documentReader.GetDocumentMeta();
            return documentType.IsMetaOnlyDocument(documentMeta);
        }

        public static bool IsValidDocument(this IDocumentReader documentReader)
        {
            Contract.Requires(documentReader != null);

            var documentType = documentReader.GetDocumentType();
            var documentMeta = documentReader.GetDocumentMeta();
            return documentType.IsValidDocument(documentMeta);
        }
        #endregion
    }
}