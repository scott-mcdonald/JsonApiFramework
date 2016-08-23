// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;
    using DomNodesContainer = NodesContainer<DomNodeType>;

    internal static class DomAttributesAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(JObject expected, DomNode actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Attributes, actual.NodeType);

            var actualDomNodesContainer = (DomNodesContainer)actual;
            var actualDomAttributes = actualDomNodesContainer.Nodes()
                                                             .Cast<DomAttribute>()
                                                             .ToList();

            var actualDomAttributesCount = actualDomAttributes.Count;
            Assert.Equal(expected.Count, actualDomAttributesCount);

            foreach (var actualDomAttribute in actualDomAttributes)
            {
                var apiPropertyName = actualDomAttribute.ApiPropertyName;

                JToken expectedApiAttributeJToken;
                Assert.True(expected.TryGetValue(apiPropertyName, out expectedApiAttributeJToken));

                // ReSharper disable once ExpressionIsAlwaysNull
                DomAttributeAssert.Equal(expectedApiAttributeJToken, actualDomAttribute);
            }
        }
        #endregion
    }
}