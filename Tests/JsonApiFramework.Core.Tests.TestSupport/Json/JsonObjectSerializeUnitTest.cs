// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonObjectSerializeUnitTest<T> : UnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonObjectSerializeUnitTest(JsonObjectSerializationUnitTestData data)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.SourceObject = (T)data.ExpectedObject;
            this.ExpectedJson = data.ExpectedJson;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            var sourceObjectTypeName = typeof(T).Name;
            var sourceObjectAsString = this.SourceObject != null
                ? "{0} ({1})".FormatWith(this.SourceObject, sourceObjectTypeName)
                : "null ({0})".FormatWith(sourceObjectTypeName);

            this.WriteLine("Source Object");
            this.WriteLine(sourceObjectAsString);
            this.WriteLine();
            this.WriteLine("Expected JSON");
            this.WriteLine(this.ExpectedJson);
        }

        protected override void Act()
        {
            this.ActualJson = JsonObject.ToJson(this.SourceObject, this.Settings);

            this.WriteLine();
            this.WriteLine("Actual JSON");
            this.WriteLine(this.ActualJson);
        }

        protected override void Assert()
        {
            if (String.IsNullOrWhiteSpace(this.ExpectedJson))
            {
                this.ActualJson.Should().BeNullOrWhiteSpace();
                return;
            }

            var expectedJsonNormalized = this.ExpectedJson.RemoveWhitespace();
            var actualJsonNormalized = this.ActualJson.RemoveWhitespace();

            actualJsonNormalized.Should().Be(expectedJsonNormalized);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private string ActualJson { get; set; }
        #endregion

        #region User Supplied Properties
        private JsonSerializerSettings Settings { get; }
        private T SourceObject { get; }
        private string ExpectedJson { get; }
        #endregion
    }
}