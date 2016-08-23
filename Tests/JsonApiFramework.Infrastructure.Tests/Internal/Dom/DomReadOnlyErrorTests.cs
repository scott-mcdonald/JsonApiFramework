// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadOnlyErrorTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyErrorTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyErrorTestData")]
        public void TestDomReadOnlyErrorCreate(string name, Error expectedError)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyError.Create(expectedError);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyErrorAssert.Equal(expectedError, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyErrorTestData = new[]
            {
                new object[] {"WithEmptyObject", Error.Empty},
                new object[] {"WithCompleteObject", ApiSampleData.Error},
                new object[] {"WithCompleteObject1", ApiSampleData.Error1},
                new object[] {"WithCompleteObject2", ApiSampleData.Error2}
            };
        #endregion
    }
}
