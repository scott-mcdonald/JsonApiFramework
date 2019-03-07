// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Extension
{
    /// <summary>Represents an object of extension properties, methods, events, etc.</summary>
    public interface IExtension<T>
        where T : IExtensibleObject<T>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Enables an extension object to be notified when it has been aggregated. Called when the extension is added to the <see cref="IExtensibleObject{T}.Extensions"/> property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates the extension.</param>
        void Attach(T owner);

        /// <summary>
        /// Enables an extension object to be notified when it is no longer aggregated. Called when an extension is removed from the <see cref="IExtensibleObject{T}.Extensions"/> property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates the extension.</param>
        void Detach(T owner);
        #endregion
    }
}
