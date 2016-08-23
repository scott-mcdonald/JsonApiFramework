// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework
{
    public static class DocumentContextOptionsExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static void CreateExtension<TExtension>(this IDocumentContextOptions documentContextOptions)
            where TExtension : class, IDocumentContextExtension, new()
        {
            Contract.Requires(documentContextOptions != null);

            // Install new extension if it does not already exist.
            TExtension extension;
            var extensionExists = documentContextOptions.TryGetExtension(out extension);
            if (extensionExists)
                return;

            extension = new TExtension();
            documentContextOptions.AddExtension(extension);
        }

        public static void ModifyExtension<TExtension>(this IDocumentContextOptions documentContextOptions, Action<TExtension> modifyExtensionAction)
            where TExtension : class, IDocumentContextExtension, new()
        {
            Contract.Requires(documentContextOptions != null);
            Contract.Requires(modifyExtensionAction != null);

            // Add an extension if it does not already exist.
            TExtension extension;
            var extensionExists = documentContextOptions.TryGetExtension(out extension);
            if (extensionExists == false)
            {
                extension = new TExtension();
                documentContextOptions.AddExtension(extension);
            }

            // Modify either the existing or new extension object.
            modifyExtensionAction(extension);
        }
        #endregion
    }
}
