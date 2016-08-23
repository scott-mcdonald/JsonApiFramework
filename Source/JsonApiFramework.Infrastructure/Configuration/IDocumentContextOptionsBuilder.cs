// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework
{
    public interface IDocumentContextOptionsBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>The document context options being built.</summary>
        IDocumentContextOptions Options { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Modify an extension object contained in a document context
        /// options object. Extension object is created if it does not exist.
        /// </summary>
        /// <typeparam name="TExtension">Type of extension to add.</typeparam>
        /// <param name="modifyExtensionAction">
        /// Action object that modifies the target extension object.
        /// </param>
        /// <remarks>
        /// This method is intended for use by extension methods to configure
        /// specific extension objects.
        /// </remarks>
        void ModifyExtension<TExtension>(Action<TExtension> modifyExtensionAction)
            where TExtension : class, IDocumentContextExtension, new();
        #endregion
    }
}
