// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>ApiObject</c>.
/// </summary>
public class ApiObjectConverter : Converter<ApiObject>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override ApiObject ReadTypedObject(JsonElement element, JsonSerializerOptions options) 
        => ReadApiObject(element, options);

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, ApiObject attributes)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(attributes != null);

        WriteApiObject(writer, options, attributes);
    }
    #endregion
}