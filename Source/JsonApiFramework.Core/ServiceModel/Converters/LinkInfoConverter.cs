// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Converters;

public class LinkInfoConverter : JsonConverter<ILinkInfo>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region CustomCreationConverter Overrides
    public override bool CanConvert(Type typeToConvert) => true;
    
    public override ILinkInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => new LinkInfo();

    public override void Write(Utf8JsonWriter writer, ILinkInfo value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value, options);
    #endregion
}