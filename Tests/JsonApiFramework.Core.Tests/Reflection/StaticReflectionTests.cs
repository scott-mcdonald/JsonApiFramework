// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
    #pragma warning disable 649
    public class StaticReflectionTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public StaticReflectionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestStaticReflectionGetFieldName()
        {
            // Arrange
            var widget = new Widget();

            // Act
            var actualByStaticField = StaticReflection.GetMemberName<Widget>(x => x.Field);
            var actualByExtensionField = widget.GetMemberName(x => x.Field);

            // Assert
            Assert.Equal("Field", actualByStaticField);
            Assert.Equal("Field", actualByExtensionField);
        }

        [Fact]
        public void TestStaticReflectionGetMethodName()
        {
            // Arrange
            var widget = new Widget();

            // Act
            var actualByStaticMethod = StaticReflection.GetMemberName<Widget>(x => x.Method());
            var actualByExtensionMethod = widget.GetMemberName(x => x.Method());

            // Assert
            Assert.Equal("Method", actualByStaticMethod);
            Assert.Equal("Method", actualByExtensionMethod);
        }

        [Fact]
        public void TestStaticReflectionGetPropertyName()
        {
            // Arrange
            var widget = new Widget();

            // Act
            var actualByStaticMethod = StaticReflection.GetMemberName<Widget>(x => x.Property);
            var actualByExtensionMethod = widget.GetMemberName(x => x.Property);

            // Assert
            Assert.Equal("Property", actualByStaticMethod);
            Assert.Equal("Property", actualByExtensionMethod);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private class Widget
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Property { get; set; }
            public string Method() { return String.Empty; }

            // ReSharper disable once UnusedField.Compiler
            // ReSharper disable once UnassignedField.Compiler
            public string Field;
        }
        #endregion
    }
    #pragma warning restore 649
}
