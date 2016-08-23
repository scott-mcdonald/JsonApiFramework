// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework
{
    /// <summary>
    /// Abstracts the options to be used by a DocumentContext object.
    /// </summary>
    public interface IDocumentContextOptions
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>The type of DocumentContext this set of options is intended for.</summary>
        Type DocumentContextType { get; }

        /// <summary>Gets the collection of extensions that contain the configured options.</summary>
        IEnumerable<IDocumentContextExtension> Extensions { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds the given extension by extension type to this set of options.
        /// </summary>
        /// <typeparam name="TExtension">Type of extension to add.</typeparam>
        /// <param name="extension">The strongly typed extension to add.</param>
        void AddExtension<TExtension>(TExtension extension)
            where TExtension : class, IDocumentContextExtension;

        /// <summary>
        /// Gets the extension of the specified extension type. An exception
        /// is thrown if the specified extension does not exist.
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get.</typeparam>
        /// <returns>
        /// The strongly typed extension requested by the generic parameter.
        /// </returns>
        /// <throws>
        /// An InternalErrorException if the type of extension does not exist.
        /// </throws>
        TExtension GetExtension<TExtension>()
            where TExtension : class, IDocumentContextExtension;

        /// <summary>
        /// Gets the extension of the specified extension type. Returns false
        /// if no extension of the specified type does not exist.
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get.</typeparam>
        /// <param name="extension">The strongly typed extension requested by the generic parameter.</param>
        /// <returns>True if the specified extension exists, false otherwise.</returns>
        bool TryGetExtension<TExtension>(out TExtension extension)
            where TExtension : class, IDocumentContextExtension;
        #endregion
    }
}
