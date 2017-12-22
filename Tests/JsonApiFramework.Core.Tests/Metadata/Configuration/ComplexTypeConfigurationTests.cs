// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Metadata;
using JsonApiFramework.Metadata.Configuration;
using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable ExpressionIsAlwaysNull
namespace JsonApiFramework.Tests.Metadata.Configuration
{
    public class ComplexTypeConfigurationTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexTypeConfigurationTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateComplexTypeTestData))]
        public void TestCreateComplexType(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IComplexType MailingAddressComplexTypeAndDefaultOptions = new ComplexType<MailingAddress>(
            Factory.CreateAttributesInfo(
                Factory<MailingAddress>.CreateAttributeInfo("StreetAddress", x => x.StreetAddress),
                Factory<MailingAddress>.CreateAttributeInfo("City", x => x.City),
                Factory<MailingAddress>.CreateAttributeInfo("State", x => x.State),
                Factory<MailingAddress>.CreateAttributeInfo("ZipCode", x => x.ZipCode),
                Factory<MailingAddress>.CreateAttributeInfo("Country", x => x.Country))
        );

        private static readonly IComplexType MailingAddressComplexTypeAndOptionsSettingApiAttributeNames = new ComplexType<MailingAddress>(
            Factory.CreateAttributesInfo(
                Factory<MailingAddress>.CreateAttributeInfo("street-address", x => x.StreetAddress),
                Factory<MailingAddress>.CreateAttributeInfo("city", x => x.City),
                Factory<MailingAddress>.CreateAttributeInfo("state", x => x.State),
                Factory<MailingAddress>.CreateAttributeInfo("zip-code", x => x.ZipCode),
                Factory<MailingAddress>.CreateAttributeInfo("country", x => x.Country))
        );

        private static readonly IComplexType MailingAddressComplexTypeAndOptionsSettingApiAttributeNamesAndExcludingOneAttribute = new ComplexType<MailingAddress>(
            Factory.CreateAttributesInfo(
                Factory<MailingAddress>.CreateAttributeInfo("street-address", x => x.StreetAddress),
                Factory<MailingAddress>.CreateAttributeInfo("city", x => x.City),
                Factory<MailingAddress>.CreateAttributeInfo("state", x => x.State),
                Factory<MailingAddress>.CreateAttributeInfo("zip-code", x => x.ZipCode))
        );

        private static readonly IComplexType MailingAddressComplexTypeAndOptionsSettingApiAttributeNamesAndExcludingManyAttributes = new ComplexType<MailingAddress>(
            Factory.CreateAttributesInfo(
                Factory<MailingAddress>.CreateAttributeInfo("zip-code", x => x.ZipCode))
        );

        public static readonly IEnumerable<object[]> CreateComplexTypeTestData = new[]
        {
            new object[]
            {
                new CreateComplexTypeUnitTest<MailingAddress>(
                    "WithNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var complexTypeConfiguration = new ComplexTypeConfiguration<MailingAddress>();
                        complexTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.StreetAddress)
                                .Attribute(x => x.City)
                                .Attribute(x => x.State)
                                .Attribute(x => x.ZipCode)
                                .Attribute(x => x.Country);
                        return complexTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    MailingAddressComplexTypeAndDefaultOptions)
            },

            new object[]
            {
                new CreateComplexTypeUnitTest<MailingAddress>(
                    "WithNullConventionsAndOptionsSettingApiAttributeNames",
                    () =>
                    {
                        var complexTypeConfiguration = new ComplexTypeConfiguration<MailingAddress>();
                        complexTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.StreetAddress, config => config.SetApiAttributeName("street-address"))
                                .Attribute(x => x.City, config => config.SetApiAttributeName("city"))
                                .Attribute(x => x.State, config => config.SetApiAttributeName("state"))
                                .Attribute(x => x.ZipCode, config => config.SetApiAttributeName("zip-code"))
                                .Attribute(x => x.Country, config => config.SetApiAttributeName("country"));
                        return complexTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    MailingAddressComplexTypeAndOptionsSettingApiAttributeNames)
            },

            new object[]
            {
                new CreateComplexTypeUnitTest<MailingAddress>(
                    "WithNullConventionsAndOptionsSettingApiAttributeNamesAndExcludingOneAttribute",
                    () =>
                    {
                        var complexTypeConfiguration = new ComplexTypeConfiguration<MailingAddress>();
                        complexTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.StreetAddress, config => config.SetApiAttributeName("street-address"))
                                .Attribute(x => x.City, config => config.SetApiAttributeName("city"))
                                .Attribute(x => x.State, config => config.SetApiAttributeName("state"))
                                .Attribute(x => x.ZipCode, config => config.SetApiAttributeName("zip-code"))
                                .Attribute(x => x.Country, config => config.Exclude());
                        return complexTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    MailingAddressComplexTypeAndOptionsSettingApiAttributeNamesAndExcludingOneAttribute)
            },

            new object[]
            {
                new CreateComplexTypeUnitTest<MailingAddress>(
                    "WithNullConventionsAndOptionsSettingApiAttributeNamesAndExcludingManyAttributes",
                    () =>
                    {
                        var complexTypeConfiguration = new ComplexTypeConfiguration<MailingAddress>();
                        complexTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.StreetAddress, config => config.Exclude())
                                .Attribute(x => x.City, config => config.Exclude())
                                .Attribute(x => x.State, config => config.Exclude())
                                .Attribute(x => x.ZipCode, config => config.SetApiAttributeName("zip-code"))
                                .Attribute(x => x.Country, config => config.Exclude());
                        return complexTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    MailingAddressComplexTypeAndOptionsSettingApiAttributeNamesAndExcludingManyAttributes)
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region ComplexType Types
        // ReSharper disable once ClassNeverInstantiated.Local
        private class MailingAddress
        {
            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Country { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region Unit Tests
        private class CreateComplexTypeUnitTest<T> : UnitTest
        {
            #region Constructors
            public CreateComplexTypeUnitTest(string name, Func<ComplexTypeConfiguration<T>> factoryMethod, IMetadataConventions metadataConventions, IComplexType expectedComplexType)
                : base(name)
            {
                this.FactoryMethod = factoryMethod;
                this.MetadataConventions = metadataConventions;
                this.ExpectedComplexType = expectedComplexType;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                var complexTypeConfiguration = this.FactoryMethod();
                this.ComplexTypeConfiguration = complexTypeConfiguration;

                var clrTypeName = typeof(T).Name;
                this.WriteLine("CLR Type : {0}", clrTypeName);
                this.WriteLine();

                this.WriteLine("Expected ComplexType");
                this.WriteLine(this.ExpectedComplexType.ToJson());
                this.WriteLine();
            }

            protected override void Act()
            {
                var metadataConventions = this.MetadataConventions;
                var compexTypeConfiguration = this.ComplexTypeConfiguration;
                var actualComplexType = compexTypeConfiguration.Create(metadataConventions);
                this.ActualComplexType = actualComplexType;

                this.WriteLine("Actual ComplexType");
                this.WriteLine(this.ActualComplexType.ToJson());
            }

            protected override void Assert()
            {
                this.ActualComplexType.ShouldBeEquivalentTo(this.ExpectedComplexType);
            }
            #endregion

            #region User Supplied Properties
            private Func<ComplexTypeConfiguration<T>> FactoryMethod { get; }
            private IMetadataConventions MetadataConventions { get; }
            private IComplexType ExpectedComplexType { get; }
            #endregion

            #region Calculated Properties
            private ComplexTypeConfiguration<T> ComplexTypeConfiguration { get; set; }
            private IComplexType ActualComplexType { get; set; }
            #endregion
        }
        #endregion
    }
}
