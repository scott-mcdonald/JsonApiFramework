// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

using JsonApiFramework.Collections;

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
    /// <summary>
    /// Represents a specialized read-only ordered dictionary of string key
    /// to generic typed value pairs that can be serialized and deserialized
    /// into and from JSON.
    /// </summary>
    /// <remarks>
    /// This dictionary is ordered meaning it preserves the order in which the
    /// key/value pairs were passed to this read-only dictionary upon construction.
    /// </remarks>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [JsonDictionary]
    public class JsonReadOnlyDictionary<TValue> : JsonObject, IReadOnlyDictionary<string, TValue>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonReadOnlyDictionary()
            : this(null, null)
        { }

        public JsonReadOnlyDictionary(IEnumerable<KeyValuePair<string, TValue>> source)
            : this(source, StringComparer.OrdinalIgnoreCase)
        { }

        public JsonReadOnlyDictionary(IEnumerable<KeyValuePair<string, TValue>> source, IEqualityComparer<string> comparer)
        {
            this.OrderedReadOnlyDictionary = new OrderedReadOnlyDictionary<string, TValue>(source, comparer);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IReadOnlyCollection<KeyValuePair<string, Value>> Implementation
        public int Count => this.OrderedReadOnlyDictionary.Count;
        #endregion

        #region IReadOnlyDictionary<string, TValue> Implementation
        public IEnumerable<string> Keys => this.OrderedReadOnlyDictionary.Keys;
        public IEnumerable<TValue> Values => this.OrderedReadOnlyDictionary.Values;
        public TValue this[string key] => this.OrderedReadOnlyDictionary[key];
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IReadOnlyDictionary<string, TValue> Implementation
        public bool ContainsKey(string key)
        { return this.OrderedReadOnlyDictionary.ContainsKey(key); }

        public bool TryGetValue(string key, out TValue value)
        { return this.OrderedReadOnlyDictionary.TryGetValue(key, out value); }
        #endregion

        #region IEnumerable<KeyValuePair<string, TValue>> Implementation
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        { return this.OrderedReadOnlyDictionary.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private OrderedReadOnlyDictionary<string, TValue> OrderedReadOnlyDictionary { get; }
        #endregion
    }
}