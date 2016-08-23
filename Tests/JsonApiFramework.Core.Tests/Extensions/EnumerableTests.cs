// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Extensions
{
    public class EnumerableTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public EnumerableTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestEnumerableEmptyIfNullWithNullEnumerable()
        {
            // Act
            var nullCollection = default(IEnumerable<string>);

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nullCollection.EmptyIfNull();

            // Assert
            Assert.Null(nullCollection);
            Assert.NotNull(collection);
            Assert.Equal(0, collection.Count());
        }

        [Fact]
        public void TestEnumerableEmptyIfNullWithValidEnumerable()
        {
            // Act
            var nonEmptyCollection = new[]
                {
                    "String 1",
                    "String 2",
                    "String 3"
                };

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nonEmptyCollection.EmptyIfNull();

            // Assert
            Assert.NotNull(nonEmptyCollection);
            Assert.NotNull(collection);
            Assert.Equal(3, collection.Count());
        }

        [Fact]
        public void TestEnumerableIsNullOrEmptyWithNullEnumerable()
        {
            // Act
            var nullCollection = default(IEnumerable<string>);

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var actual = nullCollection.IsNullOrEmpty();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TestEnumerableIsNullOrEmptyWithEmptyEnumerable()
        {
            // Act
            var emptyCollection = Enumerable.Empty<string>();

            // Arrange
            var actual = emptyCollection.IsNullOrEmpty();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void TestEnumerableIsNullOrEmptyWithNonEmptyEnumerable()
        {
            // Act
            var nonEmptyCollection = new[]
                {
                    "String 1",
                    "String 2",
                    "String 3"
                };

            // Arrange
            var actual = nonEmptyCollection.IsNullOrEmpty();

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void TestEnumerableSafeCastWithNullEnumerable()
        {
            // Act
            var nullCollection = default(IEnumerable<object>);

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nullCollection.SafeCast<string>();

            // Assert
            Assert.Null(nullCollection);
            Assert.NotNull(collection);
            Assert.Equal(0, collection.Count());
        }

        [Fact]
        public void TestEnumerableSafeCastWithValidEnumerable()
        {
            // Act
            var nonEmptyCollection = new object[]
                {
                    "String 1",
                    "String 2",
                    "String 3"
                };

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nonEmptyCollection.SafeCast<string>();

            // Assert
            Assert.NotNull(nonEmptyCollection);
            Assert.NotNull(collection);
            Assert.Equal(3, collection.Count());
            Assert.IsAssignableFrom<IEnumerable<string>>(collection);
        }

        [Fact]
        public void TestEnumerableSafeToArrayWithNullEnumerable()
        {
            // Act
            var nullCollection = default(IEnumerable<string>);

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nullCollection.SafeToArray();

            // Assert
            Assert.Null(nullCollection);
            Assert.NotNull(collection);
            Assert.Equal(0, collection.Count());
            Assert.IsType<string[]>(collection);
        }

        [Fact]
        public void TestEnumerableSafeToArrayWithValidEnumerable()
        {
            // Act
            var nonEmptyCollection = new[]
                {
                    "String 1",
                    "String 2",
                    "String 3"
                };

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nonEmptyCollection.SafeToArray();

            // Assert
            Assert.NotNull(nonEmptyCollection);
            Assert.NotNull(collection);
            Assert.Equal(3, collection.Count());
            Assert.IsType<string[]>(collection);
        }

        [Fact]
        public void TestEnumerableSafeToListWithNullEnumerable()
        {
            // Act
            var nullCollection = default(IEnumerable<string>);

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nullCollection.SafeToList();

            // Assert
            Assert.Null(nullCollection);
            Assert.NotNull(collection);
            Assert.Equal(0, collection.Count());
            Assert.IsType<List<string>>(collection);
        }

        [Fact]
        public void TestEnumerableSafeToListWithValidEnumerable()
        {
            // Act
            var nonEmptyCollection = new[]
                {
                    "String 1",
                    "String 2",
                    "String 3"
                };

            // Arrange
            // ReSharper disable once ExpressionIsAlwaysNull
            var collection = nonEmptyCollection.SafeToList();

            // Assert
            Assert.NotNull(nonEmptyCollection);
            Assert.NotNull(collection);
            Assert.Equal(3, collection.Count());
            Assert.IsType<List<string>>(collection);
        }
        #endregion
    }
}