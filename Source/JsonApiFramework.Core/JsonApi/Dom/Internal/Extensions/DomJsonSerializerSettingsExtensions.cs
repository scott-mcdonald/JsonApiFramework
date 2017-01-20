// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Extension methods for the <c>DomJsonSerializerSettings</c> class.</summary>
    internal static class DomJsonSerializerSettingsExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static NullValueHandling ResolveNullValueHandling(this DomJsonSerializerSettings domJsonSerializerSettings, JsonSerializer jsonSerializer, PropertyType apiPropertyType)
        {
            Contract.Requires(domJsonSerializerSettings != null);
            Contract.Requires(jsonSerializer != null);

            // Handle special cases for certain type of DOM nodes.
            switch (apiPropertyType)
            {
                case PropertyType.Data:
                    {
                        // Always include null resource or resource identifier DOM nodes.
                        return NullValueHandling.Include;
                    }
            }

            // Use a null value handling override if specified.
            var nullValueHandlingOverrides = domJsonSerializerSettings.NullValueHandlingOverrides;
            // ReSharper disable once InvertIf
            if (nullValueHandlingOverrides != null)
            {
                NullValueHandling nullValueHandling;
                if (nullValueHandlingOverrides.TryGetValue(apiPropertyType, out nullValueHandling))
                {
                    return nullValueHandling;
                }
            }

            // Default to the JSON.NET setting.
            return jsonSerializer.NullValueHandling;
        }
        #endregion
    }
}