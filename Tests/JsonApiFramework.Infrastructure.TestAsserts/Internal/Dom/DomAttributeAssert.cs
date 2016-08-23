// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.TestAsserts.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomAttributeAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(JToken expected, DomNode actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }

            switch (expected.Type)
            {
                case JTokenType.None:
                case JTokenType.Null:
                    {
                        Assert.Null(actual);
                        return;
                    }
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Attribute, actual.NodeType);

            var actualDomAttribute = (DomAttribute)actual;
            var actualApiAttribute = actualDomAttribute.ApiAttribute;
            ObjectAssert.Equal(expected, actualApiAttribute);
        }
        #endregion
    }
}