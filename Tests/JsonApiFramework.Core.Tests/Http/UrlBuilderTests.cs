// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Http
{
    public class UrlBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UrlBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestUrlBuilderWithEmptyConfiguration()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration();

            // Act

            // Assert
            Assert.Throws<UriFormatException>(() => UrlBuilder.Create(configuration).Build());
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHost()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPort()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndStringPathSegment()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles")
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/articles", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndStringPathSegments()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443
                };

            var pathSegments = new[]
                {
                    "articles",
                    "42"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path(pathSegments)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/articles/42", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndStringPathSegmentAndStringPathSegment()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles")
                                .Path("42")
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/articles/42", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndRootStringPathSegments()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443,
                    RootPathSegments = new[] {"api", "en-us"}
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/api/en-us", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndRootStringPathSegmentsAndStringPathSegment()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443,
                    RootPathSegments = new[] {"api", "en-us"}
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles")
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/api/en-us/articles", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndPortAndRootStringPathSegmentsAndStringPathSegmentAndStringPathSegment()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com",
                    Port = 443,
                    RootPathSegments = new[] {"api", "en-us"}
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles")
                                .Path("42")
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com:443/api/en-us/articles/42", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndOptionalStringPathSegmentFalse()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles", false)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndOptionalStringPathSegmentTrue()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path("articles", true)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com/articles", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndOptionalStringPathSegmentsFalse()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com"
                };

            var pathSegments = new[]
                {
                    "articles",
                    "42"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path(pathSegments, false)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com", url);
        }

        [Fact]
        public void TestUrlBuilderWithSchemeAndHostAndOptionalStringPathSegmentsTrue()
        {
            // Arrange
            var configuration = new UrlBuilderConfiguration
                {
                    Scheme = Uri.UriSchemeHttps,
                    Host = "api.example.com"
                };

            var pathSegments = new[]
                {
                    "articles",
                    "42"
                };

            // Act
            var url = UrlBuilder.Create(configuration)
                                .Path(pathSegments, true)
                                .Build();
            this.Output.WriteLine(url);

            // Assert
            Assert.Equal("https://api.example.com/articles/42", url);
        }

        [Theory]
        [MemberData("ObjectPathData")]
        public void TestUrlBuilderWithObjectPath(string name, IUrlBuilderConfiguration configuration, object path, bool includePath, string expectedUrl)
        {
            // Arrange

            // Act
            var expectedUrlOutputLine = "Expected URL: {0}".FormatWith(expectedUrl);
            this.Output.WriteLine(expectedUrlOutputLine);

            var actualUrl = UrlBuilder.Create(configuration)
                                      .Path(path, includePath)
                                      .Build();

            var actualUrlOutputLine = "Actual URL:   {0}".FormatWith(actualUrl);
            this.Output.WriteLine(actualUrlOutputLine);

            // Assert
            Assert.Equal(expectedUrl, actualUrl);
        }

        [Theory]
        [MemberData("PathObjectData")]
        public void TestUrlBuilderWithPathObject(string name, IUrlBuilderConfiguration configuration, IPath path, bool includePath, string expectedUrl)
        {
            // Arrange

            // Act
            var expectedUrlOutputLine = "Expected URL: {0}".FormatWith(expectedUrl);
            this.Output.WriteLine(expectedUrlOutputLine);

            var actualUrl = UrlBuilder.Create(configuration)
                                      .Path(path, includePath)
                                      .Build();

            var actualUrlOutputLine = "Actual URL:   {0}".FormatWith(actualUrl);
            this.Output.WriteLine(actualUrlOutputLine);

            // Assert
            Assert.Equal(expectedUrl, actualUrl);
        }

        [Theory]
        [MemberData("PathObjectCollectionData")]
        public void TestUrlBuilderWithPathObjectCollection(string name, IUrlBuilderConfiguration configuration, IEnumerable<IPath> pathCollection, bool includePathCollection, string expectedUrl)
        {
            // Arrange

            // Act
            var expectedUrlOutputLine = "Expected URL: {0}".FormatWith(expectedUrl);
            this.Output.WriteLine(expectedUrlOutputLine);

            var actualUrl = UrlBuilder.Create(configuration)
                                      .Path(pathCollection, includePathCollection)
                                      .Build();

            var actualUrlOutputLine = "Actual URL:   {0}".FormatWith(actualUrl);
            this.Output.WriteLine(actualUrlOutputLine);

            // Assert
            Assert.Equal(expectedUrl, actualUrl);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly UrlBuilderConfiguration TestConfiguration = new UrlBuilderConfiguration
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "api.example.com"
            };

        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 123, DateTimeKind.Utc);
        public static readonly string TestDateTimeString = TestDateTime.ToString("O");

        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
        public static readonly string TestDateTimeOffsetString = TestDateTimeOffset.ToString("O");

        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 0, 0, 0, 0);
        public static readonly string TestTimeSpanString = TestTimeSpan.ToString("c");

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);
        public static readonly byte[] TestGuidByteArray = TestGuid.ToByteArray();

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };
        public const string TestByteArrayString = "KhgwVAwVRFY=";

        public const int TestRedOrdinalValue0 = 0;
        public const int TestGreenOrdinalValue24 = 24;
        public const int TestBlueOrdinalValue42 = 42;

        public const string TestBlueString = "Blue";

        // ReSharper disable UnusedMember.Global
        public enum PrimaryColor
        {
            Red = TestRedOrdinalValue0,
            Green = TestGreenOrdinalValue24,
            Blue = TestBlueOrdinalValue42
        };
        // ReSharper restore UnusedMember.Global

        public const int TestEnumOrdinal = TestBlueOrdinalValue42;
        public const string TestEnumString = TestBlueString;
        public const PrimaryColor TestEnum = PrimaryColor.Blue;

        public static readonly Type TestType = typeof(UrlBuilderTests);
        public static readonly string TestTypeString = TestType.GetCompactQualifiedName();

        public static readonly IEnumerable<object[]> ObjectPathData = new[]
            {
                new object[] {"WithNullObjectPath", TestConfiguration, null, true, "https://api.example.com"},
                new object[] {"WithBoolObjectPath", TestConfiguration, true, true, "https://api.example.com/True"},
                new object[] {"WithByteObjectPath", TestConfiguration, (byte)42, true, "https://api.example.com/42"},
                new object[] {"WithByteArrayObjectPath", TestConfiguration, TestByteArray, true, "https://api.example.com" + "/" + TestByteArrayString},
                new object[] {"WithCharObjectPath", TestConfiguration, 'X', true, "https://api.example.com/X"},
                new object[] {"WithDateTimeObjectPath", TestConfiguration, TestDateTime, true, "https://api.example.com" + "/" + TestDateTimeString},
                new object[] {"WithDateTimeOffsetObjectPath", TestConfiguration, TestDateTimeOffset, true, "https://api.example.com" + "/" + TestDateTimeOffsetString},
                new object[] {"WithDecimalObjectPath", TestConfiguration, (decimal)42.1, true, "https://api.example.com/42.1"},
                new object[] {"WithDoubleObjectPath", TestConfiguration, (double)42.1, true, "https://api.example.com/42.1"},
                new object[] {"WithEnumObjectPath", TestConfiguration, TestEnum, true, "https://api.example.com" + "/" + TestEnumString},
                new object[] {"WithFloatObjectPath", TestConfiguration, (float)42.1, true, "https://api.example.com/42.1"},
                new object[] {"WithGuidObjectPath", TestConfiguration, TestGuid, true, "https://api.example.com" + "/" + TestGuidString},
                new object[] {"WithIntObjectPath", TestConfiguration, (int)42, true, "https://api.example.com/42"},
                new object[] {"WithLongObjectPath", TestConfiguration, (long)42, true, "https://api.example.com/42"},
                new object[] {"WithSByteObjectPath", TestConfiguration, (sbyte)42, true, "https://api.example.com/42"},
                new object[] {"WithShortObjectPath", TestConfiguration, (short)42, true, "https://api.example.com/42"},
                new object[] {"WithTimeSpanObjectPath", TestConfiguration, TestTimeSpan, true, "https://api.example.com" + "/" + TestTimeSpanString},
                new object[] {"WithUIntObjectPath", TestConfiguration, (uint)42, true, "https://api.example.com/42"},
                new object[] {"WithULongObjectPath", TestConfiguration, (ulong)42, true, "https://api.example.com/42"},
                new object[] {"WithUShortObjectPath", TestConfiguration, (ushort)42, true, "https://api.example.com/42"},
            };

        public static readonly IEnumerable<object[]> PathObjectData = new[]
            {
                new object[] {"WithNullPathObject", TestConfiguration, null, true, "https://api.example.com"},
                new object[] {"WithNullPathSegments", TestConfiguration, new NullPathSegments(), true, "https://api.example.com"},
                new object[] {"WithEmptyPathSegments", TestConfiguration, new EmptyPathSegments(), true, "https://api.example.com"},
                new object[] {"WithLeftRightPathSegments", TestConfiguration, new LeftRightPath("left", "right"), true, "https://api.example.com/left/right"},
                new object[] {"WithLeftRightPathSegmentsNotIncluded", TestConfiguration, new LeftRightPath("left", "right"), false, "https://api.example.com"},
                new object[] {"WithLeftOnlyPathSegments", TestConfiguration, new LeftRightPath("left", null), true, "https://api.example.com/left"},
                new object[] {"WithLeftOnlyPathSegmentsNotIncluded", TestConfiguration, new LeftRightPath("left", null), false, "https://api.example.com"},
                new object[] {"WithRightOnlyPathSegments", TestConfiguration, new LeftRightPath(null, "right"), true, "https://api.example.com/right"},
                new object[] {"WithRightOnlyPathSegmentsNotIncluded", TestConfiguration, new LeftRightPath(null, "right"), false, "https://api.example.com"},
            };

        public static readonly IEnumerable<object[]> PathObjectCollectionData = new[]
            {
                new object[] {"WithNullPathCollectionObject", TestConfiguration, null, true, "https://api.example.com"},
                new object[] {"WithEmptyPathCollectionObject", TestConfiguration, Enumerable.Empty<IPath>(), true, "https://api.example.com"},
                new object[] {"WithOneNullPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new NullPathSegments() }, true, "https://api.example.com"},
                new object[] {"WithOneEmptyPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new EmptyPathSegments() }, true, "https://api.example.com"},
                new object[] {"WithOneLeftRightPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath("left", "right") }, true, "https://api.example.com/left/right"},
                new object[] {"WithOneLeftRightPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath("left", "right") }, false, "https://api.example.com"},
                new object[] {"WithOneLeftOnlyPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath("left", null) }, true, "https://api.example.com/left"},
                new object[] {"WithOneLeftOnlyPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath("left", null) }, false, "https://api.example.com"},
                new object[] {"WithOneRightOnlyPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath(null, "right") }, true, "https://api.example.com/right"},
                new object[] {"WithOneRightOnlyPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath(null, "right") }, false, "https://api.example.com"},
                new object[] {"WithTwoLeftRightPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath("left1", "right1"), new LeftRightPath("left2", "right2") }, true, "https://api.example.com/left1/right1/left2/right2"},
                new object[] {"WithTwoLeftRightPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath("left1", "right1"), new LeftRightPath("left2", "right2") }, false, "https://api.example.com"},
                new object[] {"WithTwoLeftOnlyPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath("left1", null), new LeftRightPath("left2", null) }, true, "https://api.example.com/left1/left2"},
                new object[] {"WithTwoLeftOnlyPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath("left1", null), new LeftRightPath("left2", null) }, false, "https://api.example.com"},
                new object[] {"WithTwoRightOnlyPathSegmentsPathCollectionObject", TestConfiguration, new[]{ new LeftRightPath(null, "right1"), new LeftRightPath(null, "right2") }, true, "https://api.example.com/right1/right2"},
                new object[] {"WithTwoRightOnlyPathSegmentsPathCollectionObjectNotIncluded", TestConfiguration, new[]{ new LeftRightPath(null, "right1"), new LeftRightPath(null, "right2")  }, false, "https://api.example.com"},
            };
        #endregion

        #region Test Types
        private class NullPathSegments : IPath
        {
            public NullPathSegments()
            {
                this.PathSegments = null;
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }

        private class EmptyPathSegments : IPath
        {
            public EmptyPathSegments()
            {
                this.PathSegments = Enumerable.Empty<string>();
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }

        private class LeftRightPath : IPath
        {
            public LeftRightPath(string left, string right)
            {
                this.PathSegments = new[] { left, right };
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }
        #endregion
    }
}
