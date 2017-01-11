// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>Extension methods for any object that implements the <c>IDomNode</c> interface.</summary>
    public static class DomJsonSerializerSettingsExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static NullValueHandling ResolveNullValueHandling(this DomJsonSerializerSettings domJsonSerializerSettings, JsonSerializer jsonSerializer, ApiPropertyType apiPropertyType)
        {
            Contract.Requires(domJsonSerializerSettings != null);
            Contract.Requires(jsonSerializer != null);

            // Handle special cases for certain type of DOM nodes.
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (apiPropertyType)
            {
                case ApiPropertyType.Meta:
                    {
                        return domJsonSerializerSettings.MetaNullValueHandling ?? jsonSerializer.NullValueHandling;
                    }

                case ApiPropertyType.Data:
                    {
                        // Always include null data resource or resource DOM nodes.
                        return NullValueHandling.Include;
                    }

                default:
                    {
                        // Default to the JSON.NET setting.
                        return jsonSerializer.NullValueHandling;
                    }
            }
        }
        #endregion
    }
}