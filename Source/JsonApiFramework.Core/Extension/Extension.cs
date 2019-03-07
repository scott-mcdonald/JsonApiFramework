// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Extension
{
    /// <summary>Base class for any class wants to implement <see cref="IExtension{T}"/> but not provide any implementation for the attach and detach methods.</summary>
    public abstract class Extension<T> : IExtension<T>
        where T : IExtensibleObject<T>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IExtension<T> Implementation
        /// <summary>NOOP implementation of the attach method.</summary>
        /// <param name="owner">The extensible object that aggregates the extension.</param>
        public virtual void Attach(T owner)
        {
        }

        /// <summary>NOOP implementation of the detach method.</summary>
        /// <param name="owner">The extensible object that aggregates the extension.</param>
        public virtual void Detach(T owner)
        {
        }
        #endregion
    }
}
