// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Extensions
{
    public class UriTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UriTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetPathSegmentsTestData))]
        public void TestGetPathSegments(string name, Uri uri, IReadOnlyCollection<string> expectedPathSegments)
        {
            // Arrange
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("URI: {0}", uri.ToString());
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Expected Path Segments:");
            foreach (var expectedPathSegment in expectedPathSegments)
            {
                this.Output.WriteLine(expectedPathSegment);
            }
            this.Output.WriteLine(String.Empty);

            // Act
            var actualPathSegments = uri.GetPathSegments()
                                        .ToList();

            this.Output.WriteLine("Actual Path Segments:");
            foreach (var actualPathSegment in actualPathSegments)
            {
                this.Output.WriteLine(actualPathSegment);
            }
            this.Output.WriteLine(String.Empty);

            // Assert
            Assert.Equal(expectedPathSegments, actualPathSegments);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> GetPathSegmentsTestData = new[]
            {
                //new object[] {"AbsoluteUriWithEmptyPathSegments", "http://api.example.com", new List<string>() },
                //new object[] {"AbsoluteUriWithEmptyPathSegmentsEndingWithSlash", "http://api.example.com/", new List<string>() },

                //new object[] {"AbsoluteUriWithOnePathSegments", "http://api.example.com/path", new List<string> { "path"} },
                //new object[] {"AbsoluteUriWithOnePathSegmentsEndingWithSlash", "http://api.example.com/path/", new List<string> { "path"} },

                //new object[] {"AbsoluteUriWithTwoPathSegments", "http://api.example.com/path1/path2", new List<string> { "path1", "path2" } },
                //new object[] {"AbsoluteUriWithTwoPathSegmentsEndingWithSlash", "http://api.example.com/path1/path2/", new List<string> { "path1", "path2" } },

                new object[] {"AbsoluteUriWithThreePathSegments", new Uri("http://api.example.com/path1/path2/path3"), new List<string> { "path1", "path2", "path3" } },
                new object[] {"AbsoluteUriWithThreePathSegmentsEndingWithSlash", new Uri("http://api.example.com/path1/path2/path3/"), new List<string> { "path1", "path2", "path3" } },

                new object[] {"RelativeUriWithThreePathSegments", new Uri("path1/path2/path3", UriKind.Relative), new List<string> { "path1", "path2", "path3" } },
                new object[] {"RelativeUriWithThreePathSegmentsEndingInSlash", new Uri("path1/path2/path3/", UriKind.Relative), new List<string> { "path1", "path2", "path3" } },

                new object[] {"RelativeUriWithThreePathSegmentsBeginningWithSlash", new Uri("/path1/path2/path3", UriKind.Relative), new List<string> { "path1", "path2", "path3" } },
                new object[] {"RelativeUriWithThreePathSegmentsBeginningWithSlashAndEndingWithSlash", new Uri("/path1/path2/path3/", UriKind.Relative), new List<string> { "path1", "path2", "path3" } },
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
