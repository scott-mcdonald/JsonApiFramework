// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

// ReSharper disable CheckNamespace
namespace JsonApiFramework.Tests
{
    public class UriTests : XUnitTests
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
        public void TestUriGetPathSegments(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetPathSegmentsTestData = new[]
            {
                new object[] { new GetPathSegmentsUnitTest("WithNullUri", null, Enumerable.Empty<string>()) },

                new object[] { new GetPathSegmentsUnitTest("WithAbsoluteUriAnd0Path(s)", new Uri("http://api.example.com"), Enumerable.Empty<string>()) },
                new object[] { new GetPathSegmentsUnitTest("WithAbsoluteUriAnd1Path(s)", new Uri("http://api.example.com/path1"), new []{ "path1"}) },
                new object[] { new GetPathSegmentsUnitTest("WithAbsoluteUriAnd2Path(s)", new Uri("http://api.example.com/path1/path2"), new []{ "path1", "path2"}) },
                new object[] { new GetPathSegmentsUnitTest("WithAbsoluteUriAnd3Path(s)", new Uri("http://api.example.com/path1/path2/path3"), new []{ "path1", "path2", "path3"}) },
                new object[] { new GetPathSegmentsUnitTest("WithAbsoluteUriAnd4Path(s)", new Uri("http://api.example.com/path1/path2/path3/path4"), new []{ "path1", "path2", "path3", "path4"}) },

                new object[] { new GetPathSegmentsUnitTest("WithRelativeUriAnd0Path(s)", new Uri("", UriKind.Relative), Enumerable.Empty<string>()) },
                new object[] { new GetPathSegmentsUnitTest("WithRelativeUriAnd1Path(s)", new Uri("path1", UriKind.Relative), new []{ "path1"}) },
                new object[] { new GetPathSegmentsUnitTest("WithRelativeUriAnd2Path(s)", new Uri("path1/path2", UriKind.Relative), new []{ "path1", "path2"}) },
                new object[] { new GetPathSegmentsUnitTest("WithRelativeUriAnd3Path(s)", new Uri("path1/path2/path3", UriKind.Relative), new []{ "path1", "path2", "path3"}) },
                new object[] { new GetPathSegmentsUnitTest("WithRelativeUriAnd4Path(s)", new Uri("path1/path2/path3/path4", UriKind.Relative), new []{ "path1", "path2", "path3", "path4"}) },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        public class GetPathSegmentsUnitTest : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetPathSegmentsUnitTest(string name, Uri uri, IEnumerable<string> expected)
                : base(name)
            {
                this.Uri = uri;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Uri      = {0}", this.Uri.SafeToString());
                this.WriteLine("Expected = {0} ", this.Expected.SafeToDelimitedString(", "));
            }

            protected override void Act()
            {
                this.Actual = this.Uri.GetPathSegments();
                this.WriteLine("Actual   = {0}", this.Actual.SafeToDelimitedString(", "));
            }

            protected override void Assert()
            {
                this.Actual.ShouldAllBeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IEnumerable<string> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            Uri Uri { get; set; }
            IEnumerable<string> Expected { get; set; }
            #endregion
        }
        #endregion
    }
}