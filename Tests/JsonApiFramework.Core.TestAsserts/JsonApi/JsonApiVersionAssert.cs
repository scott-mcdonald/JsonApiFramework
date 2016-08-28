// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class JsonApiVersionAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(JsonApiVersion expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            JsonApiVersionAssert.Equal(expected, actualJToken);
        }

        public static void Equal(JsonApiVersion expected, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ClrObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            Assert.Equal(JTokenType.Object, actualJTokenType);

            var actualJObject = (JObject)actualJToken;

            // Version
            Assert.Equal(expected.Version, (string)actualJObject.SelectToken(Keywords.Version));

            // Meta
            var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
            MetaAssert.Equal(expected.Meta, actualMetaJToken);
        }

        public static void Equal(JsonApiVersion expected, JsonApiVersion actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // Version
            Assert.Equal(expected.Version, actual.Version);

            // Meta
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }
        #endregion
    }
}