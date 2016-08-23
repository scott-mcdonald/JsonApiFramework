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
    public class DomReadOnlyErrorsTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyErrorsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyErrorsTestData")]
        public void TestDomReadOnlyErrorCreate(string name, Error[] expectedErrors)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyErrors.Create(expectedErrors);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyErrorsAssert.Equal(expectedErrors, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyErrorsTestData = new[]
            {
                new object[] {"WithEmptyObject", new Error[]
                    {
                        
                    }
                },
                new object[] {"WithOneErrorObject", new []
                    {
                        ApiSampleData.Error
                    }
                },
                new object[] {"WithManyErrorObjects", new []
                    {
                        ApiSampleData.Error1,
                        ApiSampleData.Error2
                    }
                }
            };
        #endregion
    }
}
