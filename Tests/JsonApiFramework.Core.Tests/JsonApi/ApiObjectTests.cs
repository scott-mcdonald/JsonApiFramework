// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.XUnit;

using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class ApiObjectTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiObjectTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ApiObjectTestData")]
        public void TestObjectToJson(string name, ApiObject expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            ApiObjectAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ApiObjectTestData")]
        public void TestObjectParse(string name, ApiObject expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Object>(json);

            // Assert
            ApiObjectAssert.Equal(expected, (JToken)actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 123, DateTimeKind.Utc);
        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 0, 0, 0, 0);

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);

        public const string TestUriString = "https://api.example.com:8002/api/en-us/articles/42";
        public static readonly Uri TestUri = new Uri(TestUriString);

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };

        public const int TestRedOrdinalValue0 = 0;
        public const int TestGreenOrdinalValue24 = 24;
        public const int TestBlueOrdinalValue42 = 42;

        public const string TestBlueString = "Blue";

        // ReSharper disable UnusedMember.Global
        public enum RedGreenOrBlue
        {
            Red = TestRedOrdinalValue0,
            Green = TestGreenOrdinalValue24,
            Blue = TestBlueOrdinalValue42
        };
        // ReSharper restore UnusedMember.Global

        public const RedGreenOrBlue TestEnum = RedGreenOrBlue.Blue;

        public const string TestString = "The quick brown fox jumps over the lazy dog";

        public static readonly Type TestType = typeof(ApiObjectTests);

        public struct Point
        {
            public int XCoordinate { get; set; }
            public int YCoordinate { get; set; }
        }

        public interface IShape
        {
            bool Enabled { get; set; }
            string Name { get; set; }
            string Description { get; set; }
            Point Center { get; set; }
        }

        public class Shape : IShape
        {
            public bool Enabled { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Point Center { get; set; }
        }

        public class Circle : Shape
        {
            public uint Radius { get; set; }
        }

        public static readonly Circle TestCircle = new Circle
            {
                Enabled = true,
                Name = "My Circle",
                Description = "This is my circle of trust...",
                Center = new Point
                    {
                        XCoordinate = 42,
                        YCoordinate = 24
                    },
                Radius = 123
            };

        public static readonly IEnumerable<object[]> ApiObjectTestData = new[]
            {
                // Simple
                new object[] {"WithBool", new ApiObject(ApiProperty.Create("Bool", true))},
                new object[] {"WithByte", new ApiObject(ApiProperty.Create("Byte", (byte)42))},
                new object[] {"WithByteArray", new ApiObject(ApiProperty.Create("ByteArray", TestByteArray))},
                new object[] {"WithChar", new ApiObject(ApiProperty.Create("Char", '*'))},
                new object[] {"WithDateTime", new ApiObject(ApiProperty.Create("DateTime", TestDateTime))},
                new object[] {"WithDateTimeOffset", new ApiObject(ApiProperty.Create("DateTimeOffset", TestDateTimeOffset))},
                new object[] {"WithDecimal", new ApiObject(ApiProperty.Create("Decimal", (decimal)42.1))},
                new object[] {"WithDouble", new ApiObject(ApiProperty.Create("Double", (double)42.2))},
                new object[] {"WithEnum", new ApiObject(ApiProperty.Create("Enum", TestEnum))},
                new object[] {"WithFloat", new ApiObject(ApiProperty.Create("Float", (float)42.3))},
                new object[] {"WithGuid", new ApiObject(ApiProperty.Create("Guid", TestGuid))},
                new object[] {"WithInt", new ApiObject(ApiProperty.Create("Int", (int)42))},
                new object[] {"WithLong", new ApiObject(ApiProperty.Create("Long", (long)42))},
                new object[] {"WithNull", new ApiObject(ApiProperty.Create("Null", default(object)))},
                // ReSharper disable ConvertNullableToShortForm
                new object[] {"WithNullableBool", new ApiObject(ApiProperty.Create("NullableBool", new Nullable<bool>()))},
                new object[] {"WithNullableByte", new ApiObject(ApiProperty.Create("NullableByte", new Nullable<byte>()))},
                new object[] {"WithNullableChar", new ApiObject(ApiProperty.Create("NullableChar", new Nullable<byte>()))},
                new object[] {"WithNullableDateTime", new ApiObject(ApiProperty.Create("NullableDateTime", new Nullable<byte>()))},
                new object[] {"WithNullableDateTimeOffset", new ApiObject(ApiProperty.Create("NullableDateTimeOffset", new Nullable<byte>()))},
                new object[] {"WithNullableDecimal", new ApiObject(ApiProperty.Create("NullableDecimal", new Nullable<byte>()))},
                new object[] {"WithNullableDouble", new ApiObject(ApiProperty.Create("NullableDouble", new Nullable<byte>()))},
                new object[] {"WithNullableEnum", new ApiObject(ApiProperty.Create("NullableEnum", new Nullable<byte>()))},
                new object[] {"WithNullableFloat", new ApiObject(ApiProperty.Create("NullableFloat", new Nullable<byte>()))},
                new object[] {"WithNullableGuid", new ApiObject(ApiProperty.Create("NullableGuid", new Nullable<byte>()))},
                new object[] {"WithNullableInt", new ApiObject(ApiProperty.Create("NullableInt", new Nullable<byte>()))},
                new object[] {"WithNullableLong", new ApiObject(ApiProperty.Create("NullableLong", new Nullable<byte>()))},
                new object[] {"WithNullableSByte", new ApiObject(ApiProperty.Create("NullableSByte", new Nullable<byte>()))},
                new object[] {"WithNullableShort", new ApiObject(ApiProperty.Create("NullableShort", new Nullable<byte>()))},
                new object[] {"WithNullableTimeSpan", new ApiObject(ApiProperty.Create("NullableTimeSpan", new Nullable<byte>()))},
                new object[] {"WithNullableUInt", new ApiObject(ApiProperty.Create("NullableUInt", new Nullable<byte>()))},
                new object[] {"WithNullableULong", new ApiObject(ApiProperty.Create("NullableULong", new Nullable<byte>()))},
                new object[] {"WithNullableUShort", new ApiObject(ApiProperty.Create("NullableUShort", new Nullable<byte>()))},
                // ReSharper restore ConvertNullableToShortForm
                new object[] {"WithSByte", new ApiObject(ApiProperty.Create("SByte", (sbyte)42))},
                new object[] {"WithShort", new ApiObject(ApiProperty.Create("Short", (short)42))},
                new object[] {"WithString", new ApiObject(ApiProperty.Create("String", TestString))},
                new object[] {"WithTimeSpan", new ApiObject(ApiProperty.Create("TimeSpan", TestTimeSpan))},
                new object[] {"WithType", new ApiObject(ApiProperty.Create("Type", TestType))},
                new object[] {"WithUInt", new ApiObject(ApiProperty.Create("UInt", (uint)42))},
                new object[] {"WithULong", new ApiObject(ApiProperty.Create("ULong", (ulong)42))},
                new object[] {"WithUri", new ApiObject(ApiProperty.Create("Uri", TestUri))},
                new object[] {"WithUShort", new ApiObject(ApiProperty.Create("UShort", (ushort)42))},

                // Complex
                new object[] {"WithCircleRepresentedByClrObject", new ApiObject(ApiProperty.Create("Circle", TestCircle))},
                new object[] {"WithCircleRepresentedByApiObjectComposition",
                    new ApiObject(ApiProperty.Create("circle", new ApiObject(
                        ApiProperty.Create("enabled", TestCircle.Enabled),
                        ApiProperty.Create("name", TestCircle.Name),
                        ApiProperty.Create("description", TestCircle.Description),
                        ApiProperty.Create("center", new ApiObject(
                            ApiProperty.Create("x-coordinate", TestCircle.Center.XCoordinate),
                            ApiProperty.Create("y-coordinate", TestCircle.Center.YCoordinate))),
                        ApiProperty.Create("radius", TestCircle.Radius))))
                },
                new object[] {"WithAllTestData", new ApiObject(
                    ApiProperty.Create("Bool", true),
                    ApiProperty.Create("Byte", (byte)42),
                    ApiProperty.Create("ByteArray", TestByteArray),
                    ApiProperty.Create("Char", '*'),
                    ApiProperty.Create("DateTime", TestDateTime),
                    ApiProperty.Create("DateTimeOffset", TestDateTimeOffset),
                    ApiProperty.Create("Decimal", (decimal)42.1),
                    ApiProperty.Create("Double", (double)42.2),
                    ApiProperty.Create("Enum", TestEnum),
                    ApiProperty.Create("Float", (float)42.3),
                    ApiProperty.Create("Guid", TestGuid),
                    ApiProperty.Create("Int", (int)42),
                    ApiProperty.Create("Long", (long)42),
                    ApiProperty.Create("Null", default(object)),
                    // ReSharper disable ConvertNullableToShortForm
                    ApiProperty.Create("NullableBool", new Nullable<bool>()),
                    ApiProperty.Create("NullableByte", new Nullable<byte>()),
                    ApiProperty.Create("NullableChar", new Nullable<byte>()),
                    ApiProperty.Create("NullableDateTime", new Nullable<byte>()),
                    ApiProperty.Create("NullableDateTimeOffset", new Nullable<byte>()),
                    ApiProperty.Create("NullableDecimal", new Nullable<byte>()),
                    ApiProperty.Create("NullableDouble", new Nullable<byte>()),
                    ApiProperty.Create("NullableEnum", new Nullable<byte>()),
                    ApiProperty.Create("NullableFloat", new Nullable<byte>()),
                    ApiProperty.Create("NullableGuid", new Nullable<byte>()),
                    ApiProperty.Create("NullableInt", new Nullable<byte>()),
                    ApiProperty.Create("NullableLong", new Nullable<byte>()),
                    ApiProperty.Create("NullableSByte", new Nullable<byte>()),
                    ApiProperty.Create("NullableShort", new Nullable<byte>()),
                    ApiProperty.Create("NullableTimeSpan", new Nullable<byte>()),
                    ApiProperty.Create("NullableUInt", new Nullable<byte>()),
                    ApiProperty.Create("NullableULong", new Nullable<byte>()),
                    ApiProperty.Create("NullableUShort", new Nullable<byte>()),
                    // ReSharper restore ConvertNullableToShortForm
                    ApiProperty.Create("SByte", (sbyte)42),
                    ApiProperty.Create("Short", (short)42),
                    ApiProperty.Create("String", TestString),
                    ApiProperty.Create("TimeSpan", TestTimeSpan),
                    ApiProperty.Create("Type", TestType),
                    ApiProperty.Create("UInt", (uint)42),
                    ApiProperty.Create("ULong", (ulong)42),
                    ApiProperty.Create("Uri", TestUri),
                    ApiProperty.Create("UShort", (ushort)42),
                    ApiProperty.Create("Circle", TestCircle),
                    ApiProperty.Create("circle", new ApiObject(
                        ApiProperty.Create("enabled", TestCircle.Enabled),
                        ApiProperty.Create("name", TestCircle.Name),
                        ApiProperty.Create("description", TestCircle.Description),
                        ApiProperty.Create("center", new ApiObject(
                            ApiProperty.Create("x-coordinate", TestCircle.Center.XCoordinate),
                            ApiProperty.Create("y-coordinate", TestCircle.Center.YCoordinate))),
                        ApiProperty.Create("radius", TestCircle.Radius))))
                },
            };
        #endregion
    }
}
