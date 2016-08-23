// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.JsonApi
{
    public static class DocumentTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region DocumentType Extensions
        public static bool IsDataDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.EmptyDocument:
                case DocumentType.NullDocument:
                case DocumentType.ResourceCollectionDocument:
                case DocumentType.ResourceDocument:
                case DocumentType.ResourceIdentifierCollectionDocument:
                case DocumentType.ResourceIdentifierDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataCollectionDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.EmptyDocument:
                case DocumentType.ResourceCollectionDocument:
                case DocumentType.ResourceIdentifierCollectionDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsEmptyDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.EmptyDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsErrorsDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ErrorsDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsMetaOnlyDocument(this DocumentType documentType, Meta documentMeta)
        {
            switch (documentType)
            {
                case DocumentType.Document:
                    {
                        return documentMeta != null;
                    }

                default:
                    return false;
            }
        }

        public static bool IsNullDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.NullDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsResourceDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ResourceDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsResourceIdentifierDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ResourceIdentifierDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsResourceCollectionDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ResourceCollectionDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsResourceIdentifierCollectionDocument(this DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ResourceIdentifierCollectionDocument:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsValidDocument(this DocumentType documentType, Meta documentMeta)
        {
            switch (documentType)
            {
                case DocumentType.Document:
                    {
                        return documentMeta != null;
                    }

                case DocumentType.EmptyDocument:
                case DocumentType.ErrorsDocument:
                case DocumentType.NullDocument:
                case DocumentType.ResourceCollectionDocument:
                case DocumentType.ResourceDocument:
                case DocumentType.ResourceIdentifierCollectionDocument:
                case DocumentType.ResourceIdentifierDocument:
                    return true;

                default:
                    return false;
            }
        }
        #endregion
    }
}