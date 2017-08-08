// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;
using System.Reflection;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonObjectDeserializeUnitTest<T> : UnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonObjectDeserializeUnitTest(JsonObjectSerializationUnitTestData data)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.SourceJson = data.ExpectedJson;
            this.ExpectedObject = (T)data.ExpectedObject;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            var expectedObjectTypeName = typeof(T).Name;
            var expectedObjectAsString = this.ExpectedObject != null
                ? "{0} ({1})".FormatWith(this.ExpectedObject, expectedObjectTypeName)
                : "null ({0})".FormatWith(expectedObjectTypeName);

            this.WriteLine("Source JSON");
            this.WriteLine(this.SourceJson);
            this.WriteLine();
            this.WriteLine("Expected Object");
            this.WriteLine(expectedObjectAsString);
        }

        protected override void Act()
        {
            var actualObject = JsonObject.Parse<T>(this.SourceJson, this.Settings);
            this.ActualObject = actualObject;

            var actualObjectTypeName = typeof(T).Name;
            var actualObjectAsString = this.ActualObject != null
                ? "{0} ({1})".FormatWith(this.ActualObject, actualObjectTypeName)
                : "null ({0})".FormatWith(actualObjectTypeName);

            this.WriteLine();
            this.WriteLine("Actual Object");
            this.WriteLine(actualObjectAsString);
        }

        protected override void Assert()
        {
            // Use the FluentAssertion ShouldBeEquivalentTo method to compare
            // the expected and actual object graphs.
            var type = typeof(T);
            var typeHasAnyPublicInstanceProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any();
            if (typeHasAnyPublicInstanceProperties)
            {
                this.ActualObject.ShouldBeEquivalentTo(this.ExpectedObject);
                return;
            }

            // Handle special case where the type has no members for the
            // FluentAssertion ShouldBeEquivalentTo method to work with.
            this.ActualObject.Should().BeOfType<T>();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private T ActualObject { get; set; }
        #endregion

        #region User Supplied Properties
        private JsonSerializerSettings Settings { get; }
        private string SourceJson { get; }
        private T ExpectedObject { get; }
        #endregion
    }
}