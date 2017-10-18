// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonReadOnlyDictionarySerializeUnitTest<T> : UnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonReadOnlyDictionarySerializeUnitTest(JsonObjectSerializationUnitTestData data)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.Source = (JsonReadOnlyDictionary<T>)data.ExpectedSerializeObject;
            this.ExpectedJson = data.ExpectedJson;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            this.WriteLine("Source Dictionary");
            foreach (var expectedKeyAndValuePair in this.Source)
            {
                this.WriteLine("  {0} {1}", expectedKeyAndValuePair.Key, expectedKeyAndValuePair.Value);
            }
            this.WriteLine();
            this.WriteLine("Expected JSON");
            this.WriteLine(this.ExpectedJson);
        }

        protected override void Act()
        {
            var settings = this.Settings;
            this.ActualJson = this.Source.ToJson(settings);
            this.WriteLine();
            this.WriteLine("Actual JSON");
            this.WriteLine(this.ActualJson);
        }

        protected override void Assert()
        {
            this.ActualJson.Should().Be(this.ExpectedJson);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private string ActualJson { get; set; }
        #endregion

        #region User Supplied Properties
        private JsonReadOnlyDictionary<T> Source { get; }
        private JsonSerializerSettings Settings { get; }
        private string ExpectedJson { get; }
        #endregion
    }
}