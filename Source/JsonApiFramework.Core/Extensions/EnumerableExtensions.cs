// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
namespace JsonApiFramework
{
    /// <summary>
    /// Extension methods for the .NET IEnumerable{T} class.
    /// </summary>
    public static class EnumerableExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>
        /// For a collection variable which may represent an empty collection
        /// as a null reference, this function will guarantee that an empty
        /// collection will be provided if the collection variable is storing
        /// a null reference.  If the reference is not null, the original
        /// collection will be returned.
        /// </summary>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        { return collection ?? Enumerable.Empty<T>(); }

        /// <summary>
        /// Indicates whether the specified collection is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the collection.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>true if the collection is null or an empty collection;
        /// otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        { return collection == null || collection.Any() == false; }

        /// <summary>
        /// Converts the elements of an System.Collections.IEnumerable to the
        /// specified type only if the collection is not null.
        /// 
        /// If null, returns an empty collection of specified type.
        /// </summary>
        public static IEnumerable<T> SafeCast<T>(this IEnumerable source)
        {
            source = source ?? Enumerable.Empty<T>();
            return source.Cast<T>();
        }

        /// <summary>
        /// Returns an array based on a collection even if the collection is
        /// null.
        /// 
        /// If the source collection is null, returns an empty array.
        /// </summary>
        public static T[] SafeToArray<T>(this IEnumerable<T> source)
        {
            source = source ?? Enumerable.Empty<T>();
            var array = source as T[] ?? source.ToArray();
            return array;
        }

        /// <summary>
        /// Returns a list based on a collection even if the collection is
        /// null.
        /// 
        /// If the source collection is null, returns an empty list.
        /// </summary>
        public static List<T> SafeToList<T>(this IEnumerable<T> source)
        {
            source = source ?? Enumerable.Empty<T>();
            var list = source as List<T> ?? source.ToList();
            return list;
        }

        /// <summary>
        /// Returns a read-only collection based on a collection even if the
        /// collection is null.
        /// 
        /// If the source collection is null, returns an empty read-only collection.
        /// </summary>
        public static IReadOnlyCollection<T> SafeToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            source = source ?? Enumerable.Empty<T>();
            var readOnlyCollection = source as IReadOnlyCollection<T> ?? source.ToList();
            return readOnlyCollection;
        }

        /// <summary>
        /// Returns a read-only list based on a collection even if the
        /// collection is null.
        /// 
        /// If the source collection is null, returns an empty read-only list.
        /// </summary>
        public static IReadOnlyList<T> SafeToReadOnlyList<T>(this IEnumerable<T> source)
        {
            source = source ?? Enumerable.Empty<T>();
            var readOnlyList = source as IReadOnlyList<T> ?? source.ToList();
            return readOnlyList;
        }

        /// <summary> 
        /// Aggregates all the collection items representations as a string (via ToString)
        /// into a single delimited list represented as a single string.
        /// </summary>
        /// <returns></returns>
        public static string SafeToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
        {
            if (source == null)
                return String.Empty;

            var sourceAsList = source.ToList();
            if (sourceAsList.Count == 0)
                return String.Empty;
            if (sourceAsList.Count == 1)
                return sourceAsList.First().SafeToString();

            var delimitedString = sourceAsList
                .EmptyIfNull()
                .Select(x => x.SafeToString())
                .Aggregate((current, next) =>
                    {
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append(current);
                        stringBuilder.Append(delimiter);
                        stringBuilder.Append(next);
                        return stringBuilder.ToString();
                    });
            return delimitedString;
        }
        #endregion
    }
}
