// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Extension
{
    /// <summary>
    /// Represents a specialized dictionary for extensions indexed by extension type. Calls attach and detach extension notifications when extensions are added and removed from the dictionary.
    /// </summary>
    /// <remarks>
    /// Designed to be used by <see cref="IExtensibleObject{T}"/> implementations as an object to do the boilerplate extension management required by the <see cref="IExtensibleObject{T}"/> abstraction.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ExtensionDictionary<T> : IDictionary<Type, IExtension<T>>, IExtensibleObject<T>
        where T : IExtensibleObject<T>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ExtensionDictionary(T owner)
        {
            Contract.Requires(owner != null);

            this.Owner = owner;

            this.Dictionary = new Dictionary<Type, IExtension<T>>();
        }

        public ExtensionDictionary(T owner, Func<IDictionary<Type, IExtension<T>>> dictionaryFactory)
        {
            Contract.Requires(owner != null);
            Contract.Requires(dictionaryFactory != null);

            this.Owner = owner;

            this.Dictionary = dictionaryFactory();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public IEnumerable<IExtension<T>> Extensions => this.Dictionary.Values;
        #endregion

        #region IDictionary<TKey, TValue> Implementation
        public ICollection<Type> Keys => this.Dictionary.Keys;

        public ICollection<IExtension<T>> Values => this.Dictionary.Values;
        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Implementation
        public int Count => this.Dictionary.Count;

        public bool IsReadOnly => this.Dictionary.IsReadOnly;
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region IDictionary<TKey, TValue> Implementation
        public IExtension<T> this[Type key]
        {
            get => this.Dictionary[key];
            set
            {
                this.Remove(key);
                this.Add(key, value);
            }
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public void AddExtension(IExtension<T> extension)
        {
            Contract.Requires(extension != null);

            var extensionType = extension.GetType();
            this.Add(extensionType, extension);
        }

        public void RemoveExtension(Type extensionType)
        {
            Contract.Requires(extensionType != null);

            this.Remove(extensionType);
        }

        public bool TryGetExtension(Type extensionType, out IExtension<T> extension)
        {
            Contract.Requires(extensionType != null);

            return this.Dictionary.TryGetValue(extensionType, out extension);
        }
        #endregion

        #region IDictionary<TKey, TValue> Implementation
        public void Add(Type key, IExtension<T> value)
        {
            Contract.Requires(key != null);
            Contract.Requires(value != null);

            this.Dictionary.Add(key, value);

            // Notify extension it has been attached to an extensible object.
            value.Attach(this.Owner);
        }

        public bool ContainsKey(Type key)
        {
            Contract.Requires(key != null);

            return this.Dictionary.ContainsKey(key);
        }

        public bool Remove(Type key)
        {
            Contract.Requires(key != null);

            if (!this.Dictionary.TryGetValue(key, out var value))
            {
                return false;
            }

            // Notify extension it has been detached from an extensible object.
            value.Detach(this.Owner);
            return true;
        }

        public bool TryGetValue(Type key, out IExtension<T> value)
        {
            Contract.Requires(key != null);

            return this.Dictionary.TryGetValue(key, out value);
        }
        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Implementation
        public void Add(KeyValuePair<Type, IExtension<T>> kvp)
        {
            Contract.Requires(kvp.Key != null);
            Contract.Requires(kvp.Value != null);

            this.Dictionary.Add(kvp);

            // Notify extension it has been attached to an extensible object.
            var value = kvp.Value;
            value.Attach(this.Owner);
        }

        public void Clear()
        {
            var values = this.Dictionary.Values.ToList();

            this.Dictionary.Clear();

            foreach (var value in values)
            {
                // Notify extension it has been detached from an extensible object.
                value.Detach(this.Owner);
            }
        }

        public bool Contains(KeyValuePair<Type, IExtension<T>> kvp)
        {
            return this.Dictionary.Contains(kvp);
        }

        public void CopyTo(KeyValuePair<Type, IExtension<T>>[] array, int arrayIndex)
        {
            this.Dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Type, IExtension<T>> kvp)
        {
            Contract.Requires(kvp.Key != null);
            Contract.Requires(kvp.Value != null);

            if (!this.Dictionary.Remove(kvp))
            {
                return false;
            }

            // Notify extension it has been detached from an extensible object.
            var value = kvp.Value;
            value.Detach(this.Owner);
            return true;
        }
        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Implementation
        public IEnumerator<KeyValuePair<Type, IExtension<T>>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private T Owner { get; }

        private IDictionary<Type, IExtension<T>> Dictionary { get; }
        #endregion
    }
}
