// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Internal;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.TestData.ClrResources.ComplexTypes;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel.Configuration
{
    public class ComplexTypeBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexTypeBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("CreateComplexTypeTestData")]
        public void TestComplexTypeBuilderCreateComplexType(string name, IComplexTypeFactory complexTypeFactory, IConventions conventions, IComplexType expectedComplexType)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            var serializerSettings = new JsonSerializerSettings
                {
                    Converters = new[]
                        {
                            (JsonConverter)new StringEnumConverter()
                        },
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include
                };

            var expectedJson = expectedComplexType.ToJson(serializerSettings);
            this.Output.WriteLine("Expected ComplexType");
            this.Output.WriteLine(expectedJson);

            // Act
            var actualComplexType = complexTypeFactory.Create(conventions);

            this.Output.WriteLine(String.Empty);

            var actualJson = actualComplexType.ToJson(serializerSettings);
            this.Output.WriteLine("Actual ComplexType");
            this.Output.WriteLine(actualJson);

            // Assert
            ComplexTypeAssert.Equal(expectedComplexType, actualComplexType);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> CreateComplexTypeTestData = new[]
            {
                new object[] {"WithMailingAddressComplexTypeWithNullConventions", new TestConfigurations.MailingAddressConfigurationWithNullConventions(), null, ClrSampleData.MailingAddressComplexType},
                new object[] {"WithMailingAddressConfigurationIgnoreAllButAddressWithNullConventions", new TestConfigurations.MailingAddressConfigurationIgnoreAllButAddressWithNullConventions(), null, new ComplexType(ClrSampleData.MailingAddressClrType, new AttributesInfo(typeof(MailingAddress), new [] { ClrSampleData.MailingAddressAddressAttributeInfo }))},
                new object[] {"WithPhoneNumberComplexTypeWithNullConventions", new TestConfigurations.PhoneNumberConfigurationWithNullConventions(), null, ClrSampleData.PhoneNumberComplexType},

                new object[] {"WithMailingAddressComplexTypeWithConventions", new TestConfigurations.MailingAddressConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.MailingAddressComplexType},
                new object[] {"WithMailingAddressConfigurationIgnoreAllButAddressWithConventions", new TestConfigurations.MailingAddressConfigurationIgnoreAllButAddressWithConventions(), TestConfigurations.CreateConventions(), new ComplexType(ClrSampleData.MailingAddressClrType, new AttributesInfo(typeof(MailingAddress), new [] { ClrSampleData.MailingAddressAddressAttributeInfo }))},
                new object[] {"WithPhoneNumberComplexTypeWitConventions", new TestConfigurations.PhoneNumberConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PhoneNumberComplexType},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
