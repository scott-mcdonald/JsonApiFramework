// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace JsonApiFramework
{
    /// <summary>
    /// Extensions that provide <i>type-safe</i> deep copy semantics of a
    /// source object or collection of objects that implement the
    /// <c>IDeepCloneable</c> interface.
    /// </summary>
    public static class DeepCloneableExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static T DeepCloneWithJson<T>(this T source)
            where T : class, IDeepCloneable
        {
            if (source == null)
                return default(T);

            var sourceType = source.GetType();
            var sourceJson = JsonConvert.SerializeObject(source, DeepCloneSerializerSettings);
            var sourceCloned = JsonConvert.DeserializeObject(sourceJson, sourceType, DeepCloneSerializerSettings);
            return (T)sourceCloned;
        }

        public static T DeepCopy<T>(this T source)
            where T : class, IDeepCloneable
        {
            if (source == null)
            {
                return null;
            }

            var clone = (T)source.DeepClone();
            return clone;
        }

        public static IEnumerable<T> DeepCopyRange<T>(this IEnumerable<T> sourceCollection)
            where T : class, IDeepCloneable
        {
            if (sourceCollection != null)
            {
                foreach (var item in sourceCollection)
                {
                    // Check for possible item null reference.
                    if (item != null)
                    {
                        yield return item.DeepCopy();
                    }
                    else
                    {

                        yield return null;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        static DeepCloneableExtensions()
        {
            DeepCloneSerializerSettings = CreateDeepCloneSerializerSettings();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static JsonSerializerSettings DeepCloneSerializerSettings { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static JsonSerializerSettings CreateDeepCloneSerializerSettings()
        {
            var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
            return settings;
        }
        #endregion
    }
}
