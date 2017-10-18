// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Collections
{
    /// <summary>
    /// Represents an ordered read-only dictionary.
    /// </summary>
    /// <remarks>
    /// This read-only dictionary is ordered meaning it preserves the order in which the
    /// key/value pairs were passed upon construction.
    /// </remarks>
    /// <typeparam name="TKey">The type of the keys in the ordered read-only dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the ordered read-only dictionary.</typeparam>
    public class OrderedReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public OrderedReadOnlyDictionary()
        {
            this.ReadOnlyDictionary = new Dictionary<TKey, int>();
            this.ReadOnlyList = new List<KeyValuePair<TKey, TValue>>();
        }

        public OrderedReadOnlyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> source)
            : this(source, null)
        { }

        public OrderedReadOnlyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                this.ReadOnlyDictionary = new Dictionary<TKey, int>();
                this.ReadOnlyList = new List<KeyValuePair<TKey, TValue>>();
                return;
            }

            this.ReadOnlyList = new List<KeyValuePair<TKey, TValue>>(source);

            var count = this.ReadOnlyList.Count;
            var dictionary = new Dictionary<TKey, int>(count, comparer ?? EqualityComparer<TKey>.Default);
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
        #region IReadOnlyCollection<KeyValuePair<TKey, Value>> Implementation
        public int Count => this.ReadOnlyDictionary.Count;
        #endregion

        #region IReadOnlyDictionary<TKey, TValue> Implementation
        public IEnumerable<TKey> Keys
        { get { return this.ReadOnlyList.Select(x => x.Key); } }

        public IEnumerable<TValue> Values
        { get { return this.ReadOnlyList.Select(x => x.Value); } }

        public TValue this[TKey key]
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
        #region IReadOnlyDictionary<TKey, TValue> Implementation
        public bool ContainsKey(TKey key)
        { return this.ReadOnlyDictionary.ContainsKey(key); }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!this.ReadOnlyDictionary.TryGetValue(key, out var index))
            {
                value = default(TValue);
                return false;
            }

            value = this.ReadOnlyList[index].Value;
            return true;
        }
        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Implementation
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        { return this.ReadOnlyList.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Fields
        private IReadOnlyDictionary<TKey, int> ReadOnlyDictionary { get; }
        private IReadOnlyList<KeyValuePair<TKey, TValue>> ReadOnlyList { get; }
        #endregion
    }
}