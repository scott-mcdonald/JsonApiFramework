// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Extension
{
    /// <summary>Extension methods for the <see cref="IExtensibleObject{T}"/> class.</summary>
    public static class ExtensibleObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>
        /// Adds a strongly typed extension by extension CLR type to the extensible object.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        /// <param name="extension">The strongly typed extension to add to the extensible object.</param>
        public static void AddExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject, TExtension extension)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>
        {
            Contract.Requires(extensibleObject != null);

            extensibleObject.AddExtension(extension);
        }

        /// <summary>
        /// Removes a strongly typed extension by extension CLR type from the extensible object. Does nothing if an extension with the CLR type has not been added.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        public static void RemoveExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>
        {
            Contract.Requires(extensibleObject != null);

            var extensionType = typeof(TExtension);
            extensibleObject.RemoveExtension(extensionType);
        }

        /// <summary>
        /// Gets an extension by extension CLR type from the extensible object. An exception is thrown if the extension does not exist.
        /// </summary>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        /// <param name="extensionType">The CLR extension type to get from the extensible object.</param>
        /// <returns>Extension object contained in the extensible object.</returns>
        public static IExtension<T> GetExtension<T>(this IExtensibleObject<T> extensibleObject, Type extensionType)
            where T : IExtensibleObject<T>
        {
            Contract.Requires(extensibleObject != null);
            Contract.Requires(extensionType != null);

            if (extensibleObject.TryGetExtension(extensionType, out var extension))
                return extension;

            var message = $"{extensibleObject.GetType().Name} has missing extension [type={extensionType.Name}]. Ensure extensible object contains extension [type={extensionType.Name}] or use the TryGetExtension method if the extension is optional.";
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Gets a strongly typed extension by extension CLR type from the extensible object. An exception is thrown if the extension does not exist.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        /// <returns>Extension object contained in the extensible object.</returns>
        public static TExtension GetExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>
        {
            Contract.Requires(extensibleObject != null);

            var extensionType = typeof(TExtension);
            var extension = (TExtension)extensibleObject.GetExtension(extensionType);
            return extension;
        }

        /// <summary>
        /// Try and get the strongly typed extension by extension CLR type from the extensible object. Returns true if the extension exists, false otherwise.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        /// <param name="extension">Output parameter to capture the strongly typed extension if it exists in the extensible object.</param>
        /// <returns>True if the extension exists in the extensible object, false otherwise.</returns>
        public static bool TryGetExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject, out TExtension extension)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>
        {
            Contract.Requires(extensibleObject != null);

            var extensionType = typeof(TExtension);
            if (extensibleObject.TryGetExtension(extensionType, out var extensionAsInterface))
            {
                extension = (TExtension)extensionAsInterface;
                return true;
            }

            extension = default(TExtension);
            return false;
        }

        /// <summary>
        /// Creates a strongly typed extension and adds it to the extensible object if it doesn't already exist. Does nothing if it already exists.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        public static void CreateExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>, new()
        {
            Contract.Requires(extensibleObject != null);

            // Install new extension if it does not already exist.
            var extensionType = typeof(TExtension);
            if (extensibleObject.TryGetExtension(extensionType, out var extension))
                return;

            extension = new TExtension();
            extensibleObject.AddExtension(extension);
        }

        /// <summary>
        /// Modifies a strongly typed extension contained in the extensible object. Will create the strongly typed extension if it doesn't already exist.
        /// </summary>
        /// <typeparam name="T">Type of extensible object</typeparam>
        /// <typeparam name="TExtension">Type of extension</typeparam>
        /// <param name="extensibleObject">The CLR extensible object to execute the extension method on.</param>
        /// <param name="modifyExtensionAction">Modification to perform on the strongly typed extension object.</param>
        public static void ModifyExtension<T, TExtension>(this IExtensibleObject<T> extensibleObject, Action<TExtension> modifyExtensionAction)
            where T : IExtensibleObject<T>
            where TExtension : IExtension<T>, new()
        {
            Contract.Requires(extensibleObject != null);
            Contract.Requires(modifyExtensionAction != null);

            // Add extension if it does not already exist.
            TExtension extension;

            var extensionType = typeof(TExtension);
            if (extensibleObject.TryGetExtension(extensionType, out var extensionAsInterface))
            {
                extension = (TExtension)extensionAsInterface;
            }
            else
            {
                extension = new TExtension();
                extensibleObject.AddExtension(extension);
            }

            // Modify either the existing or new extension object.
            modifyExtensionAction(extension);
        }
        #endregion
    }
}
