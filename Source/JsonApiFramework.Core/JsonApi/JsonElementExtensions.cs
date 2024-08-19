// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

public static class JsonElementExtensions
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    public static DataType GetDataType(this JsonElement jsonElement)
    {
        // Determine if jsonElement represents one of the following:
        // 1. Resource, or
        // 2. ResourceIdentifier
        // Assume if the jsonElement only has 2 child properties named with
        // the json:api "type" and "id" keywords then if must be a ResourceIdentifier.
        var properties = jsonElement.EnumerateObject();
        var propertiesCount = properties.Count();
        switch (propertiesCount)
        {
            case 0:
                {
                    // Invalid JSON
                    var jsonElementJson = jsonElement.ToString();
                    var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                    var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jsonElementJson);
                    throw new JsonApiException(title, detail);
                }

            case 1:
                {
                    var property = properties.First();
                    if (property.Name == Keywords.Type)
                        return DataType.Resource;

                    // Invalid JSON
                    var jsonElementJson = jsonElement.ToString();
                    var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                    var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jsonElementJson);
                    throw new JsonApiException(title, detail);
                }

            case 2:
                {
                    var propertyNames = properties
                                               .OrderByDescending(x => x.Name)
                                               .Select(x => x.Name)
                                               .ToArray();
                    var propertyNamesContainOnlyTypeAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Id;
                    if (propertyNamesContainOnlyTypeAndId)
                        return DataType.ResourceIdentifier;

                    var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type;
                    if (propertyNamesContainType)
                        return DataType.Resource;

                    // Invalid JSON
                    var jsonElementJson = jsonElement.ToString();
                    var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                    var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jsonElementJson);
                    throw new JsonApiException(title, detail);
                }

            case 3:
                {
                    var propertyNames = properties
                                               .OrderByDescending(x => x.Name)
                                               .Select(x => x.Name)
                                               .ToArray();
                    var propertyNamesContainOnlyTypeAndMetaAndId = propertyNames[0] == Keywords.Type && propertyNames[1] == Keywords.Meta && propertyNames[2] == Keywords.Id;
                    if (propertyNamesContainOnlyTypeAndMetaAndId)
                    {
                        // Note: A JSON object with "type", "id", and "meta" could be either a resource identifier or resource per the JSON API specification.
                        // Because there is no "attributes", "relationships", or "links" properties the most likely intention is the JSON object is a resource
                        // identifier although this is not 100% but my best guess.
                        return DataType.ResourceIdentifier;
                    }

                    var propertyNamesContainType = propertyNames[0] == Keywords.Type || propertyNames[1] == Keywords.Type || propertyNames[2] == Keywords.Type;
                    if (propertyNamesContainType)
                        return DataType.Resource;

                    // Invalid JSON
                    var jsonElementJson = jsonElement.ToString();
                    var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                    var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jsonElementJson);
                    throw new JsonApiException(title, detail);
                }

            default:
                {
                    var typeProperty = properties.SingleOrDefault(x => x.Name == Keywords.Type);

                    // Invalid JSON
                    var jsonElementJson = jsonElement.ToString();
                    var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                    var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jsonElementJson);
                    throw new JsonApiException(title, detail);
                }
        }
    }
    #endregion
}
