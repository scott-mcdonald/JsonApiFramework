// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework
{
    /// <summary>Extension methods for the JSON.NET JProperty class.</summary>
    public static class JPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Standard predicate if a JProperty name and the parameter name are equal independent of case.</summary>
        public static bool AreNamesEqual(this JProperty jProperty, string name)
        {
            Contract.Requires(jProperty != null);

            var areNamesEqual = String.Compare(name, jProperty.Name, StringComparison.OrdinalIgnoreCase) == 0;
            return areNamesEqual;
        }

        /// <summary>
        /// Reads the property value as a string representation of a CLR type from this JProperty.
        /// If the property value is not a string or the string cannot be converted into a CLR type, null is returned.
        /// </summary>
        public static TEnum? ReadEnumPropertyValue<TEnum>(this JProperty jProperty)
            where TEnum : struct
        {
            Contract.Requires(jProperty != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(TEnum?);

            var jValue = (JValue)jToken;
            var stringValue = (string)jValue;
            var enumValue = stringValue.ParseEnum<TEnum>();
            return enumValue;
        }

        /// <summary>Reads the property value as a string from this JProperty. If the property value is not a string, null is returned.</summary>
        public static string ReadStringPropertyValue(this JProperty jProperty)
        {
            Contract.Requires(jProperty != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(string);

            var jValue = (JValue)jToken;
            var stringValue = (string)jValue;
            return stringValue;
        }

        /// <summary>
        /// Reads the property value as a string representation of a CLR type from this JProperty.
        /// If the property value is not a string or the string cannot be converted into a CLR type, null is returned.
        /// </summary>
        public static Type ReadTypePropertyValue(this JProperty jProperty)
        {
            Contract.Requires(jProperty != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(Type);

            var jValue = (JValue)jToken;
            var stringValue = (string)jValue;
            var typeValue = Type.GetType(stringValue);
            return typeValue;
        }
        #endregion
    }
}
