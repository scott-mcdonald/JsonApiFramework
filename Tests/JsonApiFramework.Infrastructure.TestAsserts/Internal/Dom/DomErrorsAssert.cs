// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomErrorsAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IEnumerable<Error> expected, DomNode actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualType = actual.GetType();

            if (actualType == typeof(DomReadOnlyErrors))
            {
                var actualDomReadOnlyErrors = (DomReadOnlyErrors)actual;
                DomReadOnlyErrorsAssert.Equal(expected, actualDomReadOnlyErrors);
            }
            else if (actualType == typeof(DomReadWriteErrors))
            {
                var actualDomReadWriteErrors = (DomReadWriteErrors)actual;
                DomReadWriteErrorsAssert.Equal(expected, actualDomReadWriteErrors);
            }
            else
            {
                Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
            }
        }
        #endregion
    }
}