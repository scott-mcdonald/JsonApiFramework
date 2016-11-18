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
            this.ReadOnlyDictionary = new Dictionary<string, int>();
            this.ReadOnlyList = new List<KeyValuePair<string, TValue>>();
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
                this.ReadOnlyDictionary = new Dictionary<string, int>();
                this.ReadOnlyList = new List<KeyValuePair<string, TValue>>();
            }

            this.ReadOnlyList = new List<KeyValuePair<string, TValue>>(source);

            var count = this.ReadOnlyList.Count;
            var dictionary = new Dictionary<string, int>(count, comparer ?? StringComparer.OrdinalIgnoreCase);
            for (var index = 0; index < count; ++index)
            {
                var keyValuePair = this.ReadOnlyList[index];
                var key = keyValuePair.Key;
                dictionary.Add(key, index);
            }

            this.ReadOnlyDictionary = dictionary;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IReadOnlyCollection<KeyValuePair<string, Value>> Implementation
        public int Count => this.ReadOnlyDictionary.Count;
        #endregion

        #region IReadOnlyDictionary<string, TValue> Implementation
        public IEnumerable<string> Keys
        { get { return this.ReadOnlyList.Select(x => x.Key); } }

        public IEnumerable<TValue> Values
        { get { return this.ReadOnlyList.Select(x => x.Value); } }

        public TValue this[string key]
        {
            get
            {
                var index = this.ReadOnlyDictionary[key];
                var value = this.ReadOnlyList[index].Value;
                return value;
            }
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IReadOnlyDictionary<string, TValue> Implementation
        public bool ContainsKey(string key)
        { return this.ReadOnlyDictionary.ContainsKey(key); }

        public bool TryGetValue(string key, out TValue value)
        {
            int index;
            if (!this.ReadOnlyDictionary.TryGetValue(key, out index))
            {
                value = default(TValue);
                return false;
            }

            value = this.ReadOnlyList[index].Value;
            return true;
        }
        #endregion

        #region IEnumerable<KeyValuePair<string, TValue>> Implementation
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        { return this.ReadOnlyList.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Fields
        private IReadOnlyDictionary<string, int> ReadOnlyDictionary { get; }
        private IReadOnlyList<KeyValuePair<string, TValue>> ReadOnlyList { get; }
        #endregion
    }
}