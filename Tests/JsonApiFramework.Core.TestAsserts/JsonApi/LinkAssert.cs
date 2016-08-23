// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class LinkAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Link expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            LinkAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Link expected, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            var expectedHRef = expected.HRef;
            var expectedMeta = expected.Meta;

            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            switch (actualJTokenType)
            {
                case JTokenType.String:
                    {
                        var actualHRef = (string)actualJToken;
                        Assert.Equal(expectedHRef, actualHRef);

                        Assert.Null(expectedMeta);
                    }
                    break;

                case JTokenType.Object:
                    {
                        var actualJObject = (JObject)actualJToken;

                        // HRef String
                        var actualHRefJToken = actualJObject.SelectToken(Keywords.HRef);
                        if (expectedHRef == null)
                        {
                            Assert.Null(actualHRefJToken);
                        }
                        else
                        {
                            Assert.Equal(JTokenType.String, actualHRefJToken.Type);

                            var actualHRef = (string)actualHRefJToken;
                            Assert.Equal(expectedHRef, actualHRef);
                        }

                        // Meta Object
                        var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
                        ObjectAssert.Equal(expectedMeta, actualMetaJToken);
                    }
                    break;

                case JTokenType.None:
                case JTokenType.Null:
                    {
                        Assert.Null(expectedHRef);
                        Assert.Null(expectedMeta);
                    }
                    break;

                default:
                    Assert.True(false, String.Format("Invalid JToken [type={0}] for link.", actualJTokenType));
                    return;
            }
        }

        public static void Equal(Link expected, Link actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // HRef
            Assert.Equal(expected.HRef, actual.HRef);

            // Meta
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IEnumerable<Link> expected, IEnumerable<Link> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToReadOnlyList();
            var actualCollection = actual.SafeToReadOnlyList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedLink = expectedCollection[index];
                var actualLink = actualCollection[index];

                LinkAssert.Equal(expectedLink, actualLink);
            }
        }
        #endregion
    }
}