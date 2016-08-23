// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static DataType GetDataType(this JObject jObject)
        {
            Contract.Requires(jObject != null);

            // Handle special case when JObject is null.
            if (jObject == null)
                return DataType.Unknown;

            // Determine if JObject represents one of the following:
            // 1. Resource, or
            // 2. ResourceIdentifier
            // Assume if the JObject only has 2 child properties named with
            // the json:api "type" and "id" keywords then if must be a ResourceIdentifier.
            var propertiesCount = jObject.Properties().Count();
            switch (propertiesCount)
            {
                case 0:
                    {
                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 1:
                    {
                        var property = jObject.Properties()
                                              .First();
                        if (property.Name == Keywords.Type)
                            return DataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 2:
                    {
                        var propertyNames = jObject.Properties()
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
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                case 3:
                    {
                        var propertyNames = jObject.Properties()
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
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }

                default:
                    {
                        var typeProperty = jObject.Properties()
                                                  .SingleOrDefault(x => x.Name == Keywords.Type);
                        if (typeProperty != null)
                            return DataType.Resource;

                        // Invalid JSON
                        var jObjectJson = jObject.ToString();
                        var title = CoreErrorStrings.JsonTextContainsInvalidJsonTitle;
                        var detail = CoreErrorStrings.JsonTextContainsInvalidJsonForResourceOrResourceIdentifierDetail.FormatWith(jObjectJson);
                        throw new JsonApiException(title, detail);
                    }
            }
        }
        #endregion
    }
}
