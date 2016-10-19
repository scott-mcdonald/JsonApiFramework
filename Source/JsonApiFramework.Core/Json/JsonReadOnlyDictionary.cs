// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        {
            _readOnlyDictionary = new Dictionary<string, int>();
            _readOnlyList = new List<KeyValuePair<string, TValue>>();
        }

        public JsonReadOnlyDictionary(IEnumerable<KeyValuePair<string, TValue>> source)
            // ReSharper disable IntroduceOptionalParameters.Global
            : this(source, null)
            // ReSharper restore IntroduceOptionalParameters.Global
        { }

        public JsonReadOnlyDictionary(IEnumerable<KeyValuePair<string, TValue>> source, IEqualityComparer<string> comparer)
        {
            if (source == null)
            {
                _readOnlyDictionary = new Dictionary<string, int>();
                _readOnlyList = new List<KeyValuePair<string, TValue>>();
            }

            _readOnlyList = new List<KeyValuePair<string, TValue>>(source);

            var count = _readOnlyList.Count;
            var dictionary = new Dictionary<string, int>(count, comparer ?? StringComparer.OrdinalIgnoreCase);
            for (var index = 0; index < count; ++index)
            {
                var keyValuePair = _readOnlyList[index];
                var key = keyValuePair.Key;
                dictionary.Add(key, index);
            }

            _readOnlyDictionary = dictionary;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IReadOnlyCollection<KeyValuePair<string, Value>> Implementation
        public int Count
        { get { return _readOnlyDictionary.Count; } }
        #endregion

        #region IReadOnlyDictionary<string, TValue> Implementation
        public IEnumerable<string> Keys
        { get { return _readOnlyList.Select(x => x.Key); } }

        public IEnumerable<TValue> Values
        { get { return _readOnlyList.Select(x => x.Value); } }

        public TValue this[string key]
        {
            get
            {
                var index = _readOnlyDictionary[key];
                var value = _readOnlyList[index].Value;
                return value;
            }
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IReadOnlyDictionary<string, TValue> Implementation
        public bool ContainsKey(string key)
        { return _readOnlyDictionary.ContainsKey(key); }

        public bool TryGetValue(string key, out TValue value)
        {
            int index;
            if (!_readOnlyDictionary.TryGetValue(key, out index))
            {
                value = default(TValue);
                return false;
            }

            value = _readOnlyList[index].Value;
            return true;
        }
        #endregion

        #region IEnumerable<KeyValuePair<string, TValue>> Implementation
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        { return _readOnlyList.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly IReadOnlyDictionary<string, int> _readOnlyDictionary;
        private readonly IReadOnlyList<KeyValuePair<string, TValue>> _readOnlyList;
        #endregion
    }
}