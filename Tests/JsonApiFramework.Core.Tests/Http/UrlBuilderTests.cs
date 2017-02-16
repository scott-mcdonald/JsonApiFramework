// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FluentAssertions;

using JsonApiFramework.Http;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Http
{
    public class UrlBuilderTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UrlBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(BuildTestData))]
        public void TestHttpBuilderBuild(IUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IUrlBuilderConfiguration TestConfiguration = new UrlBuilderConfiguration("https", "api.example.com");
        public static readonly IUrlBuilderConfiguration TestConfigurationWithPort = new UrlBuilderConfiguration("https", "api.example.com", 443);
        public static readonly IUrlBuilderConfiguration TestConfigurationWithPortAndRootPathSegments = new UrlBuilderConfiguration("https", "api.example.com", 443, new[]{"api", "en-us"});

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };
        public const string TestByteArrayString = "KhgwVAwVRFY=";

        public const string DefaultDateTimeFormat = "O";

        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 0, DateTimeKind.Utc);
        public static readonly string TestDateTimeString = TestDateTime.ToString(DefaultDateTimeFormat);

        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 0, TimeSpan.Zero);
        public static readonly string TestDateTimeOffsetString = TestDateTimeOffset.ToString(DefaultDateTimeFormat);

        public enum PrimaryColor
        {
            Red,
            Green,
            Blue
        };

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);

        public const string DefaultTimeSpanFormat = "c";

        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 12, 24, 36, 123);
        public static readonly string TestTimeSpanString = TestTimeSpan.ToString(DefaultTimeSpanFormat);

        public static readonly Type TestType = typeof(UrlBuilderTests);
        public static readonly string TestTypeString = TypeReflection.GetCompactQualifiedName(TestType);

        public static readonly IEnumerable<object[]> BuildTestData = new[]
            {
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHost", TestConfiguration, "https://api.example.com") },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPort", TestConfigurationWithPort, "https://api.example.com:443") },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegment", TestConfigurationWithPort, "https://api.example.com:443/articles", x => x.Path("articles")) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAndRemoveLastSegment", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path("articles"), x => x.RemoveLastPathSegment()) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAndRemoveLastSegmentWithRemoveFalse", TestConfigurationWithPort, "https://api.example.com:443/articles", x => x.Path("articles"), x => x.RemoveLastPathSegment(false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAsEmptyString", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(String.Empty)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAsNullString", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(default(string))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentCollection", TestConfigurationWithPort, "https://api.example.com:443/articles/42", x => x.Path((new List<string>{"articles", "42"}) as IEnumerable<string>)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAndStringPathSegment", TestConfigurationWithPort, "https://api.example.com:443/articles/42", x => x.Path("articles"), x => x.Path("42")) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentAndStringPathSegmentAndRemoveLastSegment", TestConfigurationWithPort, "https://api.example.com:443/articles", x => x.Path("articles"), x => x.Path("42"), x => x.RemoveLastPathSegment()) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndRootPathSegments", TestConfigurationWithPortAndRootPathSegments, "https://api.example.com:443/api/en-us") },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndRootPathSegmentsAndPathSegment", TestConfigurationWithPortAndRootPathSegments, "https://api.example.com:443/api/en-us/articles", x => x.Path("articles")) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndRootPathSegmentsAndPathSegmentAndPathSegment", TestConfigurationWithPortAndRootPathSegments, "https://api.example.com:443/api/en-us/articles/42", x => x.Path("articles"), x => x.Path("42")) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path("articles", false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndStringPathSegmentCollectionWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(((new List<string>{"articles", "42"}) as IEnumerable<string>), false)) },

                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsNull", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(default(IPath))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsNullPathSegments", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new NullPathSegments())) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsEmptyPathSegments", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new EmptyPathSegments())) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsLeftAndRightPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left/right", x => x.Path(new LeftRightPath("left", "right"))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsLeftAndRightPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new LeftRightPath("left", "right"), false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsLeftOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left", x => x.Path(new LeftRightPath("left", null))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsLeftOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new LeftRightPath("left", null), false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsRightOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/right", x => x.Path(new LeftRightPath(null, "right"))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathAsRightOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new LeftRightPath(null, "right"), false)) },

                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAsNull", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(default(IEnumerable<IPath>))) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAsEmpty", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(Enumerable.Empty<IPath>())) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1NullPathSegments", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new NullPathSegments() })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1EmptyPathSegments", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new EmptyPathSegments() })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1LeftAndRightPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left/right", x => x.Path(new []{ new LeftRightPath("left", "right") })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1LeftAndRightPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath("left", "right") }, false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1LeftOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left", x => x.Path(new []{ new LeftRightPath("left", null) })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1LeftOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath("left", null) }, false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1RightOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/right", x => x.Path(new []{ new LeftRightPath(null, "right") })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs1RightOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath(null, "right") }, false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2LeftAndRightPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left1/right1/left2/right2", x => x.Path(new []{ new LeftRightPath("left1", "right1"), new LeftRightPath("left2", "right2") })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2LeftAndRightPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath("left1", "right1"), new LeftRightPath("left2", "right2") }, false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2LeftOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/left1/left2", x => x.Path(new []{ new LeftRightPath("left1", null), new LeftRightPath("left2", null) })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2LeftOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath("left1", null), new LeftRightPath("left2", null) }, false)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2RightOnlyPathSegments", TestConfigurationWithPort, "https://api.example.com:443/right1/right2", x => x.Path(new []{ new LeftRightPath(null, "right1"), new LeftRightPath(null, "right2") })) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndIPathCollectionAs2RightOnlyPathSegmentsWithIncludePathFalse", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path(new []{ new LeftRightPath(null, "right1"), new LeftRightPath(null, "right2") }, false)) },

                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericBoolPathSegment", TestConfigurationWithPort, "https://api.example.com:443/True", x => x.Path<bool>(true)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericBytePathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<byte>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericByteArrayPathSegment", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestByteArrayString, x => x.Path<byte[]>(TestByteArray)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericCharPathSegment", TestConfigurationWithPort, "https://api.example.com:443/*", x => x.Path<char>('*')) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericDateTimePathSegment", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestDateTimeString, x => x.Path<DateTime>(TestDateTime)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericDateTimeOffsetPathSegment", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestDateTimeOffsetString, x => x.Path<DateTimeOffset>(TestDateTimeOffset)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericDecimalPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42.1", x => x.Path<decimal>((decimal)42.1)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericDoublePathSegment", TestConfigurationWithPort, "https://api.example.com:443/42.2", x => x.Path<double>(42.2)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericEnumPathSegment", TestConfigurationWithPort, "https://api.example.com:443/Blue", x => x.Path<PrimaryColor>(PrimaryColor.Blue)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericFloatPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42.3", x => x.Path<float>((float)42.3)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericGuidPathSegment", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestGuidString, x => x.Path<Guid>(TestGuid)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericIntPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<int>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericLongPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<long>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericSBytePathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<sbyte>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericShortPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<short>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericTimeSpanPathSegment", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestTimeSpanString, x => x.Path<TimeSpan>(TestTimeSpan)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericUIntPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<uint>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericULongPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<ulong>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericUShortPathSegment", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<ushort>(42)) },

                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Bool>PathSegmentWithNull", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path<bool?>(null)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Bool>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/True", x => x.Path<bool?>(true)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Byte>PathSegmentWithNull", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path<byte?>(null)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Byte>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<byte?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Char>PathSegmentWithNull", TestConfigurationWithPort, "https://api.example.com:443", x => x.Path<char?>(null)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Char>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/*", x => x.Path<char?>('*')) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<DateTime>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestDateTimeString, x => x.Path<DateTime?>(TestDateTime)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<DateTimeOffset>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestDateTimeOffsetString, x => x.Path<DateTimeOffset?>(TestDateTimeOffset)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Decimal>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42.1", x => x.Path<decimal?>((decimal)42.1)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Double>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42.2", x => x.Path<double?>(42.2)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Enum>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/Blue", x => x.Path<PrimaryColor?>(PrimaryColor.Blue)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Float>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42.3", x => x.Path<float?>((float)42.3)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Guid>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestGuidString, x => x.Path<Guid?>(TestGuid)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Int>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<int?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Long>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<long?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<SByte>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<sbyte?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<Short>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<short?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<TimeSpan>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443" + "/" + TestTimeSpanString, x => x.Path<TimeSpan?>(TestTimeSpan)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<UInt>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<uint?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<ULong>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<ulong?>(42)) },
                new object[] { new UrlBuilderUnitTest("WithSchemeAndHostAndPortAndGenericNullable<UShort>PathSegmentWithValue", TestConfigurationWithPort, "https://api.example.com:443/42", x => x.Path<ushort?>(42)) },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        public class NullPathSegments : IPath
        {
            public NullPathSegments()
            {
                this.PathSegments = null;
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }

        public class EmptyPathSegments : IPath
        {
            public EmptyPathSegments()
            {
                this.PathSegments = Enumerable.Empty<string>();
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }

        public class LeftRightPath : IPath
        {
            public LeftRightPath(string left, string right)
            {
                this.PathSegments = new[] { left, right };
            }

            public IEnumerable<string> PathSegments
            { get; private set; }
        }
        #endregion

        #region UnitTest Types
        public class UrlBuilderUnitTest : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public UrlBuilderUnitTest(string name, IUrlBuilderConfiguration configuration, string expectedUrl, params Action<UrlBuilder>[] buildActions)
                : base(name)
            {
                this.Configuration = configuration;
                this.BuildActions = buildActions;
                this.ExpectedUrl = expectedUrl;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Configuration");
                this.WriteLine("  Scheme     = {0}", this.Configuration.Scheme);
                this.WriteLine("  Host       = {0}", this.Configuration.Host);
                this.WriteLine("  Port       = {0}", this.Configuration.Port.HasValue ? this.Configuration.Port.Value.ToString(CultureInfo.InvariantCulture) : "null" );
                this.WriteLine();
                this.WriteLine("Expected URL = {0}", this.ExpectedUrl);
            }

            protected override void Act()
            {
                var urlBuilder = UrlBuilder.Create(this.Configuration);
                if (this.BuildActions != null)
                {
                    foreach (var buildAction in this.BuildActions)
                    {
                        buildAction(urlBuilder);
                    }
                }
                this.ActualUrl = urlBuilder.Build();
                this.WriteLine("Actual URL   = {0}", this.ActualUrl);
            }

            protected override void Assert()
            {
                this.ActualUrl.Should().Be(this.ExpectedUrl);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private string ActualUrl { get; set; }
            #endregion

            #region User Supplied Properties
            private IUrlBuilderConfiguration Configuration { get; set; }
            private IEnumerable<Action<UrlBuilder>> BuildActions { get; set; }
            private string ExpectedUrl { get; set; }
            #endregion
        }
        #endregion
    }
}
