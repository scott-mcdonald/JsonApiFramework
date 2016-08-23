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
    /// Represents a specialized read-only dictionary of string key to
    /// generic typed value pairs that can be serialized/deserialized into JSON.
    /// </summary>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [JsonDictionary]
    public class JsonReadOnlyDictionary<TValue> : JsonObject, IReadOnlyDictionary<string, TValue>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonReadOnlyDictionary(IDictionary<string, TValue> dictionary)
        {
            Contract.Requires(dictionary != null);

            _dictionary = new Dictionary<string, TValue>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        public JsonReadOnlyDictionary(IDictionary<string, TValue> dictionary, IEqualityComparer<string> comparer)
        {
            Contract.Requires(dictionary != null);

            _dictionary = new Dictionary<string, TValue>(dictionary, comparer ?? StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDictionary<string, object> Implementation
        public IEnumerable<string> Keys
        { get { return _dictionary.Keys; } }

        public IEnumerable<TValue> Values
        { get { return _dictionary.Values; } }

        public TValue this[string key]
        { get { return _dictionary[key]; } }
        #endregion

        #region ICollection<KeyValuePair<string, TValue>> Implementation
        public int Count
        { get { return _dictionary.Count; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDictionary<string, TValue> Implementation
        public bool ContainsKey(string key)
        { return _dictionary.ContainsKey(key); }

        public bool TryGetValue(string key, out TValue value)
        { return _dictionary.TryGetValue(key, out value); }
        #endregion

        #region IEnumerable<KeyValuePair<string, TValue>> Implementation
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        { return _dictionary.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)_dictionary).GetEnumerator(); }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly IReadOnlyDictionary<string, TValue> _dictionary;
        #endregion
    }
}