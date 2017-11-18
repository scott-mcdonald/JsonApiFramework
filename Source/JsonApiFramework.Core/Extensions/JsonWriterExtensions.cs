// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Reflection;

using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework
{
    /// <summary>Extension methods for the JSON.NET JsonWriter class.</summary>
    public static class JsonWriterExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Writes a CLR bool as JSON property honoring the null value handling enumeration of the JSON serializer.</summary>
        public static void WriteBoolProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, bool? propertyValue)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (propertyValue.HasValue == false)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteValue(propertyValue.Value);
        }

        /// <summary>Writes a CLR enumeration as JSON property honoring the null value handling enumeration of the JSON serializer.</summary>
        public static void WriteEnumProperty<TEnum>(this JsonWriter writer, JsonSerializer serializer, string propertyName, TEnum? propertyValue)
            where TEnum : struct
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (propertyValue.HasValue == false)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);

            var propertyValueAsString = Convert.ToString(propertyValue.Value);
            writer.WriteValue(propertyValueAsString);
        }

        /// <summary>Writes 'null' as a JSON property honoring the null value handling enumeration of the JSON serializer.</summary>
        public static void WriteNullProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            switch (serializer.NullValueHandling)
            {
                case NullValueHandling.Include:
                    writer.WritePropertyName(propertyName);
                    writer.WriteToken(JsonToken.Null);
                    return;

                case NullValueHandling.Ignore:
                    // Ignore a null property.
                    return;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>Writes a CLR string as JSON property honoring the null value handling enumeration of the JSON serializer.</summary>
        public static void WriteStringProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, string propertyValue)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (propertyValue == null)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteValue(propertyValue);
        }

        /// <summary>Writes a CLR type object as JSON property honoring the null value handling enumeration of the JSON serializer.</summary>
        public static void WriteTypeProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, Type propertyValue)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (propertyValue == null)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);

            var propertyValueAsString = TypeReflection.GetCompactQualifiedName(propertyValue);
            writer.WriteValue(propertyValueAsString);
        }
        #endregion
    }
}
