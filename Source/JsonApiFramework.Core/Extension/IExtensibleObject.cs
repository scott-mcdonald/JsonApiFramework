// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.Extension
{
    /// <summary>Represents an object that can be extended by aggregating "extensions". Allowed one extension per extension CLR type.</summary>
    public interface IExtensibleObject<T>
        where T : IExtensibleObject<T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the collection of aggregated extension objects for the extensible object.</summary>
        IEnumerable<IExtension<T>> Extensions { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds an extension by extension CLR type to the extensible object. Throws an exception if an extension of the same CLR type has already been added.
        /// </summary>
        /// <param name="extension">The extension to add to the extensible object.</param>
        void AddExtension(IExtension<T> extension);

        /// <summary>
        /// Removes an extension by extension CLR type from the extensible object. Does nothing if an extension with the CLR type has not been added.
        /// </summary>
        /// <param name="extensionType">The CLR extension type to remove from the extensible object.</param>
        void RemoveExtension(Type extensionType);

        /// <summary>
        /// Try and get the extension by extension CLR type from the extensible object. Returns true if the extension exists, false otherwise.
        /// </summary>
        /// <param name="extensionType">The CLR extension type to get from extensible object.</param>
        /// <param name="extension">Output parameter to capture the extension if it exists in the extensible object.</param>
        /// <returns>True if the extension exists in the extensible object, false otherwise.</returns>
        bool TryGetExtension(Type extensionType, out IExtension<T> extension);
        #endregion
    }
}
