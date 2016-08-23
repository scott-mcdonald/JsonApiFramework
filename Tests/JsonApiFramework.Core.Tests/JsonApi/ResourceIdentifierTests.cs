// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    // Disable: warning CS1718: Comparison made to same variable; did you mean to compare something else?
    #pragma warning disable 1718
    public class ResourceIdentifierTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ResourceIdentifierTestData")]
        public void TestResourceIdentifierToJson(string name, ResourceIdentifier expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            ResourceIdentifierAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceIdentifierTestData")]
        public void TestResourceIdentifierParse(string name, ResourceIdentifier expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<ResourceIdentifier>(json);

            // Assert
            ResourceIdentifierAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestResourceIdentifierEqualsMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.False(resourceIdentifier1.Equals(resourceIdentifier0));
            Assert.True(resourceIdentifier1.Equals(resourceIdentifier1));
            Assert.False(resourceIdentifier1.Equals(resourceIdentifier2));
            Assert.False(resourceIdentifier1.Equals(resourceIdentifier3));
            Assert.False(resourceIdentifier1.Equals(resourceIdentifier4));
            Assert.False(resourceIdentifier1.Equals(resourceIdentifier5));

            Assert.False(resourceIdentifier2.Equals(resourceIdentifier0));
            Assert.False(resourceIdentifier2.Equals(resourceIdentifier1));
            Assert.True(resourceIdentifier2.Equals(resourceIdentifier2));
            Assert.False(resourceIdentifier2.Equals(resourceIdentifier3));
            Assert.True(resourceIdentifier2.Equals(resourceIdentifier4));
            Assert.False(resourceIdentifier2.Equals(resourceIdentifier5));

            Assert.False(resourceIdentifier3.Equals(resourceIdentifier0));
            Assert.False(resourceIdentifier3.Equals(resourceIdentifier1));
            Assert.False(resourceIdentifier3.Equals(resourceIdentifier2));
            Assert.True(resourceIdentifier3.Equals(resourceIdentifier3));
            Assert.False(resourceIdentifier3.Equals(resourceIdentifier4));
            Assert.False(resourceIdentifier3.Equals(resourceIdentifier5));

            Assert.False(resourceIdentifier4.Equals(resourceIdentifier0));
            Assert.False(resourceIdentifier4.Equals(resourceIdentifier1));
            Assert.True(resourceIdentifier4.Equals(resourceIdentifier2));
            Assert.False(resourceIdentifier4.Equals(resourceIdentifier3));
            Assert.True(resourceIdentifier4.Equals(resourceIdentifier4));
            Assert.False(resourceIdentifier4.Equals(resourceIdentifier5));

            Assert.False(resourceIdentifier5.Equals(resourceIdentifier0));
            Assert.False(resourceIdentifier5.Equals(resourceIdentifier1));
            Assert.False(resourceIdentifier5.Equals(resourceIdentifier2));
            Assert.False(resourceIdentifier5.Equals(resourceIdentifier3));
            Assert.False(resourceIdentifier5.Equals(resourceIdentifier4));
            Assert.True(resourceIdentifier5.Equals(resourceIdentifier5));
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.False(resourceIdentifier1 == resourceIdentifier0);
            Assert.True(resourceIdentifier1 == resourceIdentifier1);
            Assert.False(resourceIdentifier1 == resourceIdentifier2);
            Assert.False(resourceIdentifier1 == resourceIdentifier3);
            Assert.False(resourceIdentifier1 == resourceIdentifier4);
            Assert.False(resourceIdentifier1 == resourceIdentifier5);

            Assert.False(resourceIdentifier2 == resourceIdentifier0);
            Assert.False(resourceIdentifier2 == resourceIdentifier1);
            Assert.True(resourceIdentifier2 == resourceIdentifier2);
            Assert.False(resourceIdentifier2 == resourceIdentifier3);
            Assert.True(resourceIdentifier2 == resourceIdentifier4);
            Assert.False(resourceIdentifier2 == resourceIdentifier5);

            Assert.False(resourceIdentifier3 == resourceIdentifier0);
            Assert.False(resourceIdentifier3 == resourceIdentifier1);
            Assert.False(resourceIdentifier3 == resourceIdentifier2);
            Assert.True(resourceIdentifier3 == resourceIdentifier3);
            Assert.False(resourceIdentifier3 == resourceIdentifier4);
            Assert.False(resourceIdentifier3 == resourceIdentifier5);

            Assert.False(resourceIdentifier4 == resourceIdentifier0);
            Assert.False(resourceIdentifier4 == resourceIdentifier1);
            Assert.True(resourceIdentifier4 == resourceIdentifier2);
            Assert.False(resourceIdentifier4 == resourceIdentifier3);
            Assert.True(resourceIdentifier4 == resourceIdentifier4);
            Assert.False(resourceIdentifier4 == resourceIdentifier5);

            Assert.False(resourceIdentifier5 == resourceIdentifier0);
            Assert.False(resourceIdentifier5 == resourceIdentifier1);
            Assert.False(resourceIdentifier5 == resourceIdentifier2);
            Assert.False(resourceIdentifier5 == resourceIdentifier3);
            Assert.False(resourceIdentifier5 == resourceIdentifier4);
            Assert.True(resourceIdentifier5 == resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierNotEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.True(resourceIdentifier1 != resourceIdentifier0);
            Assert.False(resourceIdentifier1 != resourceIdentifier1);
            Assert.True(resourceIdentifier1 != resourceIdentifier2);
            Assert.True(resourceIdentifier1 != resourceIdentifier3);
            Assert.True(resourceIdentifier1 != resourceIdentifier4);
            Assert.True(resourceIdentifier1 != resourceIdentifier5);

            Assert.True(resourceIdentifier2 != resourceIdentifier0);
            Assert.True(resourceIdentifier2 != resourceIdentifier1);
            Assert.False(resourceIdentifier2 != resourceIdentifier2);
            Assert.True(resourceIdentifier2 != resourceIdentifier3);
            Assert.False(resourceIdentifier2 != resourceIdentifier4);
            Assert.True(resourceIdentifier2 != resourceIdentifier5);

            Assert.True(resourceIdentifier3 != resourceIdentifier0);
            Assert.True(resourceIdentifier3 != resourceIdentifier1);
            Assert.True(resourceIdentifier3 != resourceIdentifier2);
            Assert.False(resourceIdentifier3 != resourceIdentifier3);
            Assert.True(resourceIdentifier3 != resourceIdentifier4);
            Assert.True(resourceIdentifier3 != resourceIdentifier5);

            Assert.True(resourceIdentifier4 != resourceIdentifier0);
            Assert.True(resourceIdentifier4 != resourceIdentifier1);
            Assert.False(resourceIdentifier4 != resourceIdentifier2);
            Assert.True(resourceIdentifier4 != resourceIdentifier3);
            Assert.False(resourceIdentifier4 != resourceIdentifier4);
            Assert.True(resourceIdentifier4 != resourceIdentifier5);

            Assert.True(resourceIdentifier5 != resourceIdentifier0);
            Assert.True(resourceIdentifier5 != resourceIdentifier1);
            Assert.True(resourceIdentifier5 != resourceIdentifier2);
            Assert.True(resourceIdentifier5 != resourceIdentifier3);
            Assert.True(resourceIdentifier5 != resourceIdentifier4);
            Assert.False(resourceIdentifier5 != resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierCompareToMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier0) > 0);
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier1) == 0);
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier2) < 0);
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier3) < 0);
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier4) < 0);
            Assert.True(resourceIdentifier1.CompareTo(resourceIdentifier5) < 0);

            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier0) > 0);
            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier1) > 0);
            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier2) == 0);
            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier3) < 0);
            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier4) == 0);
            Assert.True(resourceIdentifier2.CompareTo(resourceIdentifier5) > 0);

            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier0) > 0);
            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier1) > 0);
            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier2) > 0);
            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier3) == 0);
            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier4) > 0);
            Assert.True(resourceIdentifier3.CompareTo(resourceIdentifier5) > 0);

            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier0) > 0);
            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier1) > 0);
            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier2) == 0);
            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier3) < 0);
            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier4) == 0);
            Assert.True(resourceIdentifier4.CompareTo(resourceIdentifier5) > 0);

            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier0) > 0);
            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier1) > 0);
            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier2) < 0);
            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier3) < 0);
            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier4) < 0);
            Assert.True(resourceIdentifier5.CompareTo(resourceIdentifier5) == 0);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.False(resourceIdentifier1 < resourceIdentifier0);
            Assert.False(resourceIdentifier1 < resourceIdentifier1);
            Assert.True(resourceIdentifier1 < resourceIdentifier2);
            Assert.True(resourceIdentifier1 < resourceIdentifier3);
            Assert.True(resourceIdentifier1 < resourceIdentifier4);
            Assert.True(resourceIdentifier1 < resourceIdentifier5);

            Assert.False(resourceIdentifier2 < resourceIdentifier0);
            Assert.False(resourceIdentifier2 < resourceIdentifier1);
            Assert.False(resourceIdentifier2 < resourceIdentifier2);
            Assert.True(resourceIdentifier2 < resourceIdentifier3);
            Assert.False(resourceIdentifier2 < resourceIdentifier4);
            Assert.False(resourceIdentifier2 < resourceIdentifier5);

            Assert.False(resourceIdentifier3 < resourceIdentifier0);
            Assert.False(resourceIdentifier3 < resourceIdentifier1);
            Assert.False(resourceIdentifier3 < resourceIdentifier2);
            Assert.False(resourceIdentifier3 < resourceIdentifier3);
            Assert.False(resourceIdentifier3 < resourceIdentifier4);
            Assert.False(resourceIdentifier3 < resourceIdentifier5);

            Assert.False(resourceIdentifier4 < resourceIdentifier0);
            Assert.False(resourceIdentifier4 < resourceIdentifier1);
            Assert.False(resourceIdentifier4 < resourceIdentifier2);
            Assert.True(resourceIdentifier4 < resourceIdentifier3);
            Assert.False(resourceIdentifier4 < resourceIdentifier4);
            Assert.False(resourceIdentifier4 < resourceIdentifier5);

            Assert.False(resourceIdentifier5 < resourceIdentifier0);
            Assert.False(resourceIdentifier5 < resourceIdentifier1);
            Assert.True(resourceIdentifier5 < resourceIdentifier2);
            Assert.True(resourceIdentifier5 < resourceIdentifier3);
            Assert.True(resourceIdentifier5 < resourceIdentifier4);
            Assert.False(resourceIdentifier5 < resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.False(resourceIdentifier1 <= resourceIdentifier0);
            Assert.True(resourceIdentifier1 <= resourceIdentifier1);
            Assert.True(resourceIdentifier1 <= resourceIdentifier2);
            Assert.True(resourceIdentifier1 <= resourceIdentifier3);
            Assert.True(resourceIdentifier1 <= resourceIdentifier4);
            Assert.True(resourceIdentifier1 <= resourceIdentifier5);

            Assert.False(resourceIdentifier2 <= resourceIdentifier0);
            Assert.False(resourceIdentifier2 <= resourceIdentifier1);
            Assert.True(resourceIdentifier2 <= resourceIdentifier2);
            Assert.True(resourceIdentifier2 <= resourceIdentifier3);
            Assert.True(resourceIdentifier2 <= resourceIdentifier4);
            Assert.False(resourceIdentifier2 <= resourceIdentifier5);

            Assert.False(resourceIdentifier3 <= resourceIdentifier0);
            Assert.False(resourceIdentifier3 <= resourceIdentifier1);
            Assert.False(resourceIdentifier3 <= resourceIdentifier2);
            Assert.True(resourceIdentifier3 <= resourceIdentifier3);
            Assert.False(resourceIdentifier3 <= resourceIdentifier4);
            Assert.False(resourceIdentifier3 <= resourceIdentifier5);

            Assert.False(resourceIdentifier4 <= resourceIdentifier0);
            Assert.False(resourceIdentifier4 <= resourceIdentifier1);
            Assert.True(resourceIdentifier4 <= resourceIdentifier2);
            Assert.True(resourceIdentifier4 <= resourceIdentifier3);
            Assert.True(resourceIdentifier4 <= resourceIdentifier4);
            Assert.False(resourceIdentifier4 <= resourceIdentifier5);

            Assert.False(resourceIdentifier5 <= resourceIdentifier0);
            Assert.False(resourceIdentifier5 <= resourceIdentifier1);
            Assert.True(resourceIdentifier5 <= resourceIdentifier2);
            Assert.True(resourceIdentifier5 <= resourceIdentifier3);
            Assert.True(resourceIdentifier5 <= resourceIdentifier4);
            Assert.True(resourceIdentifier5 <= resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.True(resourceIdentifier1 > resourceIdentifier0);
            Assert.False(resourceIdentifier1 > resourceIdentifier1);
            Assert.False(resourceIdentifier1 > resourceIdentifier2);
            Assert.False(resourceIdentifier1 > resourceIdentifier3);
            Assert.False(resourceIdentifier1 > resourceIdentifier4);
            Assert.False(resourceIdentifier1 > resourceIdentifier5);

            Assert.True(resourceIdentifier2 > resourceIdentifier0);
            Assert.True(resourceIdentifier2 > resourceIdentifier1);
            Assert.False(resourceIdentifier2 > resourceIdentifier2);
            Assert.False(resourceIdentifier2 > resourceIdentifier3);
            Assert.False(resourceIdentifier2 > resourceIdentifier4);
            Assert.True(resourceIdentifier2 > resourceIdentifier5);

            Assert.True(resourceIdentifier3 > resourceIdentifier0);
            Assert.True(resourceIdentifier3 > resourceIdentifier1);
            Assert.True(resourceIdentifier3 > resourceIdentifier2);
            Assert.False(resourceIdentifier3 > resourceIdentifier3);
            Assert.True(resourceIdentifier3 > resourceIdentifier4);
            Assert.True(resourceIdentifier3 > resourceIdentifier5);

            Assert.True(resourceIdentifier4 > resourceIdentifier0);
            Assert.True(resourceIdentifier4 > resourceIdentifier1);
            Assert.False(resourceIdentifier4 > resourceIdentifier2);
            Assert.False(resourceIdentifier4 > resourceIdentifier3);
            Assert.False(resourceIdentifier4 > resourceIdentifier4);
            Assert.True(resourceIdentifier4 > resourceIdentifier5);

            Assert.True(resourceIdentifier5 > resourceIdentifier0);
            Assert.True(resourceIdentifier5 > resourceIdentifier1);
            Assert.False(resourceIdentifier5 > resourceIdentifier2);
            Assert.False(resourceIdentifier5 > resourceIdentifier3);
            Assert.False(resourceIdentifier5 > resourceIdentifier4);
            Assert.False(resourceIdentifier5 > resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier();
            var resourceIdentifier2 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier3 = SampleData.PersonResourceIdentifier2;
            var resourceIdentifier4 = SampleData.PersonResourceIdentifier1;
            var resourceIdentifier5 = SampleData.CommentResourceIdentifier1;

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            // ReSharper disable HeuristicUnreachableCode
            Assert.True(resourceIdentifier1 >= resourceIdentifier0);
            Assert.True(resourceIdentifier1 >= resourceIdentifier1);
            Assert.False(resourceIdentifier1 >= resourceIdentifier2);
            Assert.False(resourceIdentifier1 >= resourceIdentifier3);
            Assert.False(resourceIdentifier1 >= resourceIdentifier4);
            Assert.False(resourceIdentifier1 >= resourceIdentifier5);

            Assert.True(resourceIdentifier2 >= resourceIdentifier0);
            Assert.True(resourceIdentifier2 >= resourceIdentifier1);
            Assert.True(resourceIdentifier2 >= resourceIdentifier2);
            Assert.False(resourceIdentifier2 >= resourceIdentifier3);
            Assert.True(resourceIdentifier2 >= resourceIdentifier4);
            Assert.True(resourceIdentifier2 >= resourceIdentifier5);

            Assert.True(resourceIdentifier3 >= resourceIdentifier0);
            Assert.True(resourceIdentifier3 >= resourceIdentifier1);
            Assert.True(resourceIdentifier3 >= resourceIdentifier2);
            Assert.True(resourceIdentifier3 >= resourceIdentifier3);
            Assert.True(resourceIdentifier3 >= resourceIdentifier4);
            Assert.True(resourceIdentifier3 >= resourceIdentifier5);

            Assert.True(resourceIdentifier4 >= resourceIdentifier0);
            Assert.True(resourceIdentifier4 >= resourceIdentifier1);
            Assert.True(resourceIdentifier4 >= resourceIdentifier2);
            Assert.False(resourceIdentifier4 >= resourceIdentifier3);
            Assert.True(resourceIdentifier4 >= resourceIdentifier4);
            Assert.True(resourceIdentifier4 >= resourceIdentifier5);

            Assert.True(resourceIdentifier5 >= resourceIdentifier0);
            Assert.True(resourceIdentifier5 >= resourceIdentifier1);
            Assert.False(resourceIdentifier5 >= resourceIdentifier2);
            Assert.False(resourceIdentifier5 >= resourceIdentifier3);
            Assert.False(resourceIdentifier5 >= resourceIdentifier4);
            Assert.True(resourceIdentifier5 >= resourceIdentifier5);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> ResourceIdentifierTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceIdentifier.Empty},
                new object[] {"WithNonEmptyObject", SampleData.ArticleResourceIdentifier},
                new object[] {"WithNonEmptyObjectAndMeta", SampleData.ArticleResourceIdentifierWithMeta}
            };
        #endregion
    }
    #pragma warning restore 1718
}
