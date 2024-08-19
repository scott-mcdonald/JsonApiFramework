// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for the family of <c>Relationship</c> objects.
/// </summary>
public class RelationshipConverter : Converter<Relationship>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Relationship ReadTypedObject(JsonElement relationshipJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var relationship = CreateRelationshipAndReadData(relationshipJsonElement, options);
        ReadMeta(relationshipJsonElement, options, relationship);
        ReadLinks(relationshipJsonElement, options, relationship);

        return relationship;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Relationship relationship)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(relationship != null);

        writer.WriteStartObject();

        WriteLinks(writer, options, relationship);
        WriteData(writer, options, relationship);
        WriteMeta(writer, options, relationship);

        writer.WriteEndObject();
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Read Methods
    private static Relationship CreateRelationshipAndReadData(JsonElement relationshipJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        // Data
        Relationship relationship;

        // Analyze data to determine the concrete type of Relationship
        // object to create:
        // 1. If "data" is not present, then create a "Relationship" object.
        // 2. If "data" is present, then
        //    2.1 If "data" is an object, then create a "ToOneRelationship" object.
        //    2.2 If "data" is an array, then create a "ToManyRelationship" object.
        JsonElement dataJsonElement;
        try
        {
            dataJsonElement = relationshipJsonElement.GetProperty(Keywords.Data);
        }
        catch (KeyNotFoundException)
        {
            return new();
        }

        var dataJsonElementValueKind = dataJsonElement.ValueKind;
        switch (dataJsonElementValueKind)
        {
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                {
                    relationship = new ToOneRelationship
                        {
                            Data = null
                        };
                }
                break;

            case JsonValueKind.Object:
                {
                    var data = dataJsonElement.Deserialize<ResourceIdentifier>(options);
                    relationship = new ToOneRelationship
                        {
                            Data = data
                        };
                }
                break;

            case JsonValueKind.Array:
                {
                    var data = dataJsonElement.EnumerateArray()
                                            .Select(x => x.Deserialize<ResourceIdentifier>(options))
                                            .ToList();
                    relationship = new ToManyRelationship
                        {
                            Data = data
                        };
                }
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return relationship;
    }
    #endregion

    #region Write Methods
    private static void WriteData(Utf8JsonWriter writer, JsonSerializerOptions options, Relationship relationship)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(relationship != null);

        var relationshipType = relationship.GetRelationshipType();
        switch (relationshipType)
        {
            case RelationshipType.Relationship:
                {
                    // NOOP
                }
                break;

            case RelationshipType.ToOneRelationship:
                {
                    writer.WritePropertyName(Keywords.Data);

                    var toOneResourceLinkage = relationship.GetToOneResourceLinkage();
                    if (toOneResourceLinkage != null)
                    {
                        var dataJsonElement = JsonSerializer.SerializeToElement(toOneResourceLinkage, options);

                        dataJsonElement.WriteTo(writer);
                    }
                    else
                    {
                        writer.WriteStartObject();
                        writer.WriteEndObject();
                    }
                }
                break;

            case RelationshipType.ToManyRelationship:
                {
                    writer.WritePropertyName(Keywords.Data);

                    var toManyResourceLinkage = relationship.GetToManyResourceLinkage() ?? EmptyDataArray;

                    var dataJsonElement = JsonSerializer.SerializeToElement(toManyResourceLinkage, options);

                    dataJsonElement.WriteTo(writer);
                }
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly ResourceIdentifier[] EmptyDataArray = Enumerable.Empty<ResourceIdentifier>().ToArray();
    #endregion
}