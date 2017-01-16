// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class ResourceIdentifierTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestResourceIdentifierEqualsMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            resourceIdentifier1.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier1).Should().BeTrue();
            resourceIdentifier1.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier4).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier5).Should().BeFalse();

            resourceIdentifier2.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier2).Should().BeTrue();
            resourceIdentifier2.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier4).Should().BeTrue();
            resourceIdentifier2.Equals(resourceIdentifier5).Should().BeFalse();

            resourceIdentifier3.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier3).Should().BeTrue();
            resourceIdentifier3.Equals(resourceIdentifier4).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier5).Should().BeFalse();

            resourceIdentifier4.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier2).Should().BeTrue();
            resourceIdentifier4.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier4).Should().BeTrue();
            resourceIdentifier4.Equals(resourceIdentifier5).Should().BeFalse();

            resourceIdentifier5.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier5.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier5.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier5.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier5.Equals(resourceIdentifier4).Should().BeFalse();
            resourceIdentifier5.Equals(resourceIdentifier5).Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier1 == resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier1 == resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 == resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 == resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier1 == resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier2 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 == resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier2 == resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier2 == resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 == resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier2 == resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier3 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 == resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 == resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier3 == resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier3 == resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier3 == resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier4 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 == resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier4 == resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier5 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier5 == resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier5 == resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier5 == resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier5 == resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier5 == resourceIdentifier5).Should().BeTrue();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void TestResourceIdentifierNotEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier1 != resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier1 != resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 != resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 != resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier1 != resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier2 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 != resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier2 != resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier2 != resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 != resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier2 != resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier3 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 != resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 != resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier3 != resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier3 != resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier3 != resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier4 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 != resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier4 != resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier5 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier5 != resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier5 != resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier5 != resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier5 != resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier5 != resourceIdentifier5).Should().BeFalse();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void TestResourceIdentifierCompareToMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            (resourceIdentifier1.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier1) == 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier2) < 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier3) < 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier4) < 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier5) < 0).Should().BeTrue();

            (resourceIdentifier2.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier1) > 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier2) == 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier3) < 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier4) == 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier5) > 0).Should().BeTrue();

            (resourceIdentifier3.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier1) > 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier2) > 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier3) == 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier4) > 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier5) > 0).Should().BeTrue();

            (resourceIdentifier4.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier1) > 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier2) == 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier3) < 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier4) == 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier5) > 0).Should().BeTrue();

            (resourceIdentifier5.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier5.CompareTo(resourceIdentifier1) > 0).Should().BeTrue();
            (resourceIdentifier5.CompareTo(resourceIdentifier2) < 0).Should().BeTrue();
            (resourceIdentifier5.CompareTo(resourceIdentifier3) < 0).Should().BeTrue();
            (resourceIdentifier5.CompareTo(resourceIdentifier4) < 0).Should().BeTrue();
            (resourceIdentifier5.CompareTo(resourceIdentifier5) == 0).Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier1 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier1 < resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 < resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 < resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier1 < resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier2 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 < resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier3 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier4 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 < resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 < resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier4 < resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier4 < resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier5 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier5 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier5 < resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier5 < resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier5 < resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier5 < resourceIdentifier5).Should().BeFalse();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier1 <= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier2 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 <= resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier2 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier2 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 <= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier2 <= resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier3 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 <= resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 <= resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier3 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier3 <= resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier3 <= resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier4 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 <= resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier4 <= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier4 <= resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier5 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier5 <= resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier5 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier5 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier5 <= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier5 <= resourceIdentifier5).Should().BeTrue();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier1 > resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier2 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 > resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier2 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier2 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 > resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier2 > resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier3 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 > resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 > resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier3 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier3 > resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier3 > resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier4 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 > resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier4 > resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier4 > resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier5 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier5 > resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier5 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier5 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier5 > resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier5 > resourceIdentifier5).Should().BeFalse();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = new ResourceIdentifier("people", "24");
            var resourceIdentifier3 = new ResourceIdentifier("people", "42");
            var resourceIdentifier4 = new ResourceIdentifier("people", "24");
            var resourceIdentifier5 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier1 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier1 >= resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 >= resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 >= resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier1 >= resourceIdentifier5).Should().BeFalse();

            (resourceIdentifier2 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 >= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier3 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier4 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 >= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 >= resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier4 >= resourceIdentifier4).Should().BeTrue();
            (resourceIdentifier4 >= resourceIdentifier5).Should().BeTrue();

            (resourceIdentifier5 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier5 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier5 >= resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier5 >= resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier5 >= resourceIdentifier4).Should().BeFalse();
            (resourceIdentifier5 >= resourceIdentifier5).Should().BeTrue();
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }
        #endregion
    }
}
