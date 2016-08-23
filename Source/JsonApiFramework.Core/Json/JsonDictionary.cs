// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
    /// <summary>
    /// Represents a specialized dictionary of string key to generic typed
    /// value pairs that can be serialized/deserialized into JSON.
    /// </summary>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [JsonDictionary]
    public class JsonDictionary<TValue> : JsonObject, IDictionary<string, TValue>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonDictionary()
        {
            _dictionary = new Dictionary<string, TValue>(StringComparer.OrdinalIgnoreCase);
        }

        public JsonDictionary(IEqualityComparer<string> comparer)
        {
            _dictionary = new Dictionary<string, TValue>(comparer ?? StringComparer.OrdinalIgnoreCase);
        }

        public JsonDictionary(IDictionary<string, TValue> dictionary)
        {
            Contract.Requires(dictionary != null);

            _dictionary = new Dictionary<string, TValue>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        public JsonDictionary(IDictionary<string, TValue> dictionary, IEqualityComparer<string> comparer)
        {
            Contract.Requires(dictionary != null);

            _dictionary = new Dictionary<string, TValue>(dictionary, comparer ?? StringComparer.OrdinalIgnoreCase);
        }

        public JsonDictionary(int capacity)
        {
            Contract.Requires(capacity > 0);

            _dictionary = new Dictionary<string, TValue>(capacity, StringComparer.OrdinalIgnoreCase);
        }

        public JsonDictionary(int capacity, IEqualityComparer<string> comparer)
        {
            Contract.Requires(capacity > 0);

            _dictionary = new Dictionary<string, TValue>(capacity, comparer ?? StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDictionary<string, TValue> Implementation
        public ICollection<string> Keys
        { get { return _dictionary.Keys; } }

        public ICollection<TValue> Values
        { get { return _dictionary.Values; } }

        public TValue this[string key]
        { get { return _dictionary[key]; } set { _dictionary[key] = value; } }
        #endregion

        #region ICollection<KeyValuePair<string, TValue>> Implementation
        public int Count
        { get { return _dictionary.Count; } }

        public bool IsReadOnly
        { get { return this.AsCollection().IsReadOnly; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDictionary<string, TValue> Implementation
        public void Add(string key, TValue value)
        { _dictionary.Add(key, value); }

        public bool ContainsKey(string key)
        { return _dictionary.ContainsKey(key); }

        public bool Remove(string key)
        { return _dictionary.Remove(key); }

        public bool TryGetValue(string key, out TValue value)
        { return _dictionary.TryGetValue(key, out value); }
        #endregion

        #region ICollection<KeyValuePair<string, TValue>> Implementation
        public void Add(KeyValuePair<string, TValue> item)
        { this.AsCollection().Add(item); }

        public void Clear()
        { _dictionary.Clear(); }

        public bool Contains(KeyValuePair<string, TValue> item)
        { return this.AsCollection().Contains(item); }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        { this.AsCollection().CopyTo(array, arrayIndex); }

        public bool Remove(KeyValuePair<string, TValue> item)
        { return this.AsCollection().Remove(item); }
        #endregion

        #region IEnumerable<KeyValuePair<string, TValue>> Implementation
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        { return _dictionary.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)_dictionary).GetEnumerator(); }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private ICollection<KeyValuePair<string, TValue>> AsCollection()
        { return _dictionary; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly Dictionary<string, TValue> _dictionary;
        #endregion
    }
}