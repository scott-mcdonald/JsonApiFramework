// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Metadata;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.TypeConversion;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Metadata
{
    public class ClrPropertyBindingTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertyBindingTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetClrPropertyTestData))]
        public void TestGetClrProperty(IUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(SetClrPropertyTestData))]
        public void TestSetClrProperty(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly long TestOrdinalBefore = 42;
        private static readonly long TestOrdinalAfter = 24;

        private static readonly string TestOrdinalAsStringBefore = Convert.ToString(TestOrdinalBefore);
        private static readonly string TestOrdinalAsStringAfter = Convert.ToString(TestOrdinalAfter);

        private static readonly Enumeration TestEnumerationBefore = Enumeration.FourtyTwo;
        private static readonly Enumeration TestEnumerationAfter = Enumeration.TwentyFour;

        private static readonly string TestEnumerationAsStringBefore = TestEnumerationBefore.ToString();
        private static readonly string TestEnumerationAsStringAfter = TestEnumerationAfter.ToString();

        private static readonly Guid TestGuidBefore = Guid.NewGuid();
        private static readonly Guid TestGuidAfter = Guid.NewGuid();

        private static readonly string TestGuidAsStringBefore = TestGuidBefore.ToString();
        private static readonly string TestGuidAsStringAfter = TestGuidAfter.ToString();

        private static readonly ComplexId TestComplexIdBefore = new ComplexId
        {
            IdLong1 = TestOrdinalBefore,
            IdLong2 = TestOrdinalAfter
        };

        private static readonly ComplexId TestComplexIdAfter = new ComplexId
        {
            IdLong1 = TestOrdinalBefore + 100,
            IdLong2 = TestOrdinalAfter + 100
        };

        private static readonly Func<Identifiers> TestIdentifiersFactory = () => new Identifiers
        {
            IdByte = (byte)TestOrdinalBefore,
            IdEnumeration = TestEnumerationBefore,
            IdGuid = TestGuidBefore,
            IdLong = TestOrdinalBefore,
            IdString = TestOrdinalAsStringBefore,
            IdComplexType = TestComplexIdBefore
        };

        private static readonly IClrPropertyBinding IdBytePropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdByte);
        private static readonly IClrPropertyBinding IdEnumerationPropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdEnumeration);
        private static readonly IClrPropertyBinding IdGuidPropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdGuid);
        private static readonly IClrPropertyBinding IdLongPropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdLong);
        private static readonly IClrPropertyBinding IdStringPropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdString);
        private static readonly IClrPropertyBinding IdComplexTypePropertyBinding = Factory<Identifiers>.CreateClrPropertyBinding(x => x.IdComplexType);

        public static readonly IEnumerable<object[]> GetClrPropertyTestData = new[]
        {
            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithByteToByte", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithByteToEnumeration", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithByteToGuid", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithByteToLong", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithByteToString", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithByteToComplexType", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithEnumerationToByte", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithEnumerationToEnumeration", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithEnumerationToGuid", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithEnumerationToLong", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithEnumerationToString", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationAsStringBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithEnumerationToComplexType", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithGuidToByte", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithGuidToEnumeration", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithGuidToGuid", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestGuidBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithGuidToLong", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithGuidToString", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestGuidAsStringBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithGuidToComplexType", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithLongToByte", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithLongToEnumeration", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithLongToGuid", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithLongToLong", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithLongToString", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithLongToComplexType", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithStringToByte", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithStringToEnumeration", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithStringToGuid", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithStringToLong", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithStringToString", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithStringToComplexType", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new GetClrPropertyUnitTest<Identifiers, byte>("WithComplexTypeToByte", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Enumeration>("WithComplexTypeToEnumeration", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, Guid>("WithComplexTypeToGuid", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, long>("WithComplexTypeToLong", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, string>("WithComplexTypeToString", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new GetClrPropertyUnitTest<Identifiers, ComplexId>("WithComplexTypeToComplexType", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestComplexIdBefore) },
        };

        public static readonly IEnumerable<object[]> SetClrPropertyTestData = new[]
        {
            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithByteToByte", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore, (byte)TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithByteToEnumeration", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore, TestEnumerationAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithByteToGuid", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithByteToLong", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore, TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithByteToString", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore, TestOrdinalAsStringAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithByteToComplexType", IdBytePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithEnumerationToByte", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore, (byte)TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithEnumerationToEnumeration", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore, TestEnumerationAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithEnumerationToGuid", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithEnumerationToLong", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore, TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithEnumerationToString", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationAsStringBefore, TestEnumerationAsStringAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithEnumerationToComplexType", IdEnumerationPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithGuidToByte", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithGuidToEnumeration", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithGuidToGuid", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestGuidBefore, TestGuidAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithGuidToLong", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithGuidToString", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestGuidAsStringBefore, TestGuidAsStringAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithGuidToComplexType", IdGuidPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithLongToByte", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore, (byte)TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithLongToEnumeration", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore, TestEnumerationAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithLongToGuid", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithLongToLong", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore, TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithLongToString", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore, TestOrdinalAsStringAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithLongToComplexType", IdLongPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithStringToByte", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, (byte)TestOrdinalBefore, (byte)TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithStringToEnumeration", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestEnumerationBefore, TestEnumerationAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithStringToGuid", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithStringToLong", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalBefore, TestOrdinalAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithStringToString", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestOrdinalAsStringBefore, TestOrdinalAsStringAfter) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithStringToComplexType", IdStringPropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },

            new object[] { new SetClrPropertyUnitTest<Identifiers, byte>("WithComplexTypeToByte", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Enumeration>("WithComplexTypeToEnumeration", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, Guid>("WithComplexTypeToGuid", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, long>("WithComplexTypeToLong", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, string>("WithComplexTypeToString", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, false) },
            new object[] { new SetClrPropertyUnitTest<Identifiers, ComplexId>("WithComplexTypeToComplexType", IdComplexTypePropertyBinding, TestIdentifiersFactory, TypeConverter.Default, true, TestComplexIdBefore, TestComplexIdAfter) },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private enum Enumeration
        {
            TwentyFour = 24,
            FourtyTwo = 42
        };

        private class ComplexId
        {
            public long IdLong1 { get; set; }
            public long IdLong2 { get; set; }

            public override string ToString()
            { return $"{typeof(ComplexId).Name} [{nameof(ComplexId.IdLong1)}={this.IdLong1} {nameof(ComplexId.IdLong2)}={this.IdLong2}]"; }
        }

        private class Identifiers
        {
            public byte IdByte { get; set; }
            public Enumeration IdEnumeration { get; set; }
            public Guid IdGuid { get; set; }
            public long IdLong { get; set; }
            public string IdString { get; set; }
            public ComplexId IdComplexType { get; set; }
        }
        #endregion

        #region Unit Tests
        private class GetClrPropertyUnitTest<TObject, TValue> : UnitTest
        {
            #region Constructors
            public GetClrPropertyUnitTest(string name, IClrPropertyBinding clrPropertyBinding, Func<TObject> clrObjectFactory, ITypeConverter typeConverter, bool expectedSuccess, TValue clrExpectedPropertyValue = default(TValue))
                : base(name)
            {
                this.ClrPropertyBinding = clrPropertyBinding;
                this.ClrObjectFactory = clrObjectFactory;
                this.TypeConverter = typeConverter;
                this.ExpectedSuccess = expectedSuccess;
                this.ClrExpectedPropertyValue = clrExpectedPropertyValue;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("CLR Property Binding");
                this.WriteLine("  CLR Object Type   : {0}", typeof(TObject).Name);
                this.WriteLine("  CLR Property Type : {0}", this.ClrPropertyBinding.ClrPropertyType.Name);
                this.WriteLine("  CLR Property Name : {0}", this.ClrPropertyBinding.ClrPropertyName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success : {0}", this.ExpectedSuccess);
                if (this.ExpectedSuccess)
                {
                    this.WriteLine("  Value   : {0}", this.ClrExpectedPropertyValue);
                }
                this.WriteLine();
            }

            protected override void Act()
            {
                var clrObject = this.ClrObjectFactory();
                this.ClrObject = clrObject;

                try
                {
                    var clrActualPropertyValue = this.ClrPropertyBinding.GetClrProperty<TObject, TValue>(this.ClrObject, this.TypeConverter, null);

                    this.ClrActualPropertyValue = clrActualPropertyValue;
                    this.ActualSuccess = true;
                }
                catch (TypeConverterException typeConverterException)
                {
                    this.ClrActualPropertyValue = default(TValue);
                    this.ActualSuccess = false;
                }

                this.WriteLine("Actual");
                this.WriteLine("  Success : {0}", this.ActualSuccess);
                if (this.ActualSuccess)
                {
                    this.WriteLine("  Value   : {0}", this.ClrActualPropertyValue);
                }
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);

                if (this.ActualSuccess)
                {
                    this.ClrActualPropertyValue.ShouldBeEquivalentTo(this.ClrExpectedPropertyValue);
                }
            }
            #endregion

            #region User Supplied Properties
            private IClrPropertyBinding ClrPropertyBinding { get; }
            private Func<TObject> ClrObjectFactory { get; }
            private ITypeConverter TypeConverter { get; }
            private bool ExpectedSuccess { get; }
            private TValue ClrExpectedPropertyValue { get; }
            #endregion

            #region Calculated Properties
            private TObject ClrObject { get; set; }
            private bool ActualSuccess { get; set; }
            private TValue ClrActualPropertyValue { get; set; }
            #endregion
        }

        private class SetClrPropertyUnitTest<TObject, TValue> : UnitTest
        {
            #region Constructors
            public SetClrPropertyUnitTest(string name, IClrPropertyBinding clrPropertyBinding, Func<TObject> clrObjectFactory, ITypeConverter typeConverter, bool expectedSuccess, TValue clrExpectedPropertyValueBefore = default(TValue), TValue clrExpectedPropertyValueAfter = default(TValue))
                : base(name)
            {
                this.ClrPropertyBinding = clrPropertyBinding;
                this.ClrObjectFactory = clrObjectFactory;
                this.TypeConverter = typeConverter;
                this.ExpectedSuccess = expectedSuccess;
                this.ClrExpectedPropertyValueBefore = clrExpectedPropertyValueBefore;
                this.ClrExpectedPropertyValueAfter = clrExpectedPropertyValueAfter;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("CLR Property Binding");
                this.WriteLine("  CLR Object Type   : {0}", typeof(TObject).Name);
                this.WriteLine("  CLR Property Type : {0}", this.ClrPropertyBinding.ClrPropertyType.Name);
                this.WriteLine("  CLR Property Name : {0}", this.ClrPropertyBinding.ClrPropertyName);
                this.WriteLine();

                this.WriteLine("Expected");
                this.WriteLine("  Success : {0}", this.ExpectedSuccess);
                if (this.ExpectedSuccess)
                {
                    this.WriteLine("  Value (Before) : {0}", this.ClrExpectedPropertyValueBefore);
                    this.WriteLine("  Value (After)  : {0}", this.ClrExpectedPropertyValueAfter);
                }
                this.WriteLine();
            }

            protected override void Act()
            {
                var clrObject = this.ClrObjectFactory();
                this.ClrObject = clrObject;

                try
                {
                    var clrActualPropertyValueBefore = this.ClrPropertyBinding.GetClrProperty<TObject, TValue>(this.ClrObject, this.TypeConverter, null);
                    this.ClrActualPropertyValueBefore = clrActualPropertyValueBefore;

                    this.ClrPropertyBinding.SetClrProperty(this.ClrObject, this.ClrExpectedPropertyValueAfter, this.TypeConverter, null);

                    var clrActualPropertyValueAfter = this.ClrPropertyBinding.GetClrProperty<TObject, TValue>(this.ClrObject, this.TypeConverter, null);
                    this.ClrActualPropertyValueAfter = clrActualPropertyValueAfter;

                    this.ActualSuccess = true;
                }
                catch (TypeConverterException typeConverterException)
                {
                    this.ClrActualPropertyValueBefore = default(TValue);
                    this.ClrActualPropertyValueAfter = default(TValue);
                    this.ActualSuccess = false;
                }

                this.WriteLine("Actual");
                this.WriteLine("  Success : {0}", this.ActualSuccess);
                // ReSharper disable once InvertIf
                if (this.ActualSuccess)
                {
                    this.WriteLine("  Value (Before) : {0}", this.ClrActualPropertyValueBefore);
                    this.WriteLine("  Value (After)  : {0}", this.ClrActualPropertyValueAfter);
                }
            }

            protected override void Assert()
            {
                this.ActualSuccess.ShouldBeEquivalentTo(this.ExpectedSuccess);

                if (this.ActualSuccess)
                {
                    this.ClrActualPropertyValueBefore.ShouldBeEquivalentTo(this.ClrExpectedPropertyValueBefore);
                    this.ClrActualPropertyValueAfter.ShouldBeEquivalentTo(this.ClrExpectedPropertyValueAfter);
                }
            }
            #endregion

            #region User Supplied Properties
            private IClrPropertyBinding ClrPropertyBinding { get; }
            private Func<TObject> ClrObjectFactory { get; }
            private ITypeConverter TypeConverter { get; }
            private bool ExpectedSuccess { get; }
            private TValue ClrExpectedPropertyValueBefore { get; }
            private TValue ClrExpectedPropertyValueAfter { get; }
            #endregion

            #region Calculated Properties
            private TObject ClrObject { get; set; }
            private bool ActualSuccess { get; set; }
            private TValue ClrActualPropertyValueBefore { get; set; }
            private TValue ClrActualPropertyValueAfter { get; set; }
            #endregion
        }
        #endregion
    }
}
