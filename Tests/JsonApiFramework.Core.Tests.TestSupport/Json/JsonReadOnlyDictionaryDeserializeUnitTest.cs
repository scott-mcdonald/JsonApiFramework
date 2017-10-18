// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonReadOnlyDictionaryDeserializeUnitTest<T> : UnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonReadOnlyDictionaryDeserializeUnitTest(JsonObjectSerializationUnitTestData data)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.SourceJson = data.ExpectedJson;
            this.ExpectedDictionary = (JsonReadOnlyDictionary<T>)data.ExpectedDeserializeObject;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            this.WriteLine("Source JSON");
            this.WriteLine(this.SourceJson);
            this.WriteLine();
            this.WriteLine("Expected Dictionary");
            foreach (var expectedKeyAndValuePair in this.ExpectedDictionary)
            {
                this.WriteLine("  {0} {1}", expectedKeyAndValuePair.Key, expectedKeyAndValuePair.Value);
            }
        }

        protected override void Act()
        {
            var source = this.SourceJson;
            var settings = this.Settings;
            var actualDictionary = JsonObject.Parse<JsonReadOnlyDictionary<T>>(source, settings);

            this.ActualDictionary = actualDictionary;
            this.WriteLine();
            this.WriteLine("Actual Dictionary");
            foreach (var actualKeyAndValuePair in this.ActualDictionary)
            {
                this.WriteLine("  {0} {1}", actualKeyAndValuePair.Key, actualKeyAndValuePair.Value);
            }
        }

        protected override void Assert()
        {
            this.ActualDictionary.ShouldAllBeEquivalentTo(this.ExpectedDictionary);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private JsonReadOnlyDictionary<T> ActualDictionary { get; set; }
        #endregion

        #region User Supplied Properties
        private string SourceJson { get; set; }
        private JsonSerializerSettings Settings { get; set; }
        private JsonReadOnlyDictionary<T> ExpectedDictionary { get; set; }
        #endregion
    }
}