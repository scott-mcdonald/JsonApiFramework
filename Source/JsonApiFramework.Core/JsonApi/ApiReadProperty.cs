// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Represents an immutable property intended to be read from JSON where the
/// property value type is not known at compile time and will remain a JToken.
/// </summary>
/// <remarks>
/// The property value will be kept as a JToken until later deserialization
/// when the target deserializaiton type is known.
/// </remarks>
public class ApiReadProperty : ApiProperty
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public ApiReadProperty(string name, JsonElement value)
        : base(name)
    {
        this.Value = value;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    public JsonElement Value { get; private set; }
    #endregion

    #region Conversion Methods
    public override object ToClrObject(Type clrObjectType)
    {
        var sourceElement = this.Value;

        var clrObject = sourceElement.Deserialize(clrObjectType);
        return clrObject;
    }

    public override TObject ToClrObject<TObject>()
    {
        var sourceElement = this.Value;

        var clrObject = sourceElement.Deserialize<TObject>();
        return clrObject;
    }
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region ApiProperty Overrides
    protected override string ValueAsString()
    { return this.Value.SafeToString(); }
    #endregion

    // INTERNAL METHODS /////////////////////////////////////////////////
    #region ApiProperty Overrides
    internal override object ValueAsObject()
    { return this.Value; }

    internal override void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);

        var name = this.Name;
        var value = this.Value;

        writer.WritePropertyName(name);
        value.WriteTo(writer);
    }
    #endregion
}