// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Dom;
using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomJsonSerializeUnitTest<T> : UnitTest
        where T : IDomNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomJsonSerializeUnitTest(DomJsonSerializationUnitTestData data, params Action<string>[] additionalAsserts)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.SourceDomTree = (T)data.ExpectedSerializeDomTree;
            this.ExpectedJson = data.ExpectedJson;
            this.AdditionalAsserts = additionalAsserts;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            var sourceDomTreeAsString = this.SourceDomTree != null
                ? this.SourceDomTree.ToTreeString()
                : "null";

            this.WriteLine("Source DOM Tree");
            this.WriteLine(sourceDomTreeAsString);
            this.WriteLine();
            this.WriteLine("Expected JSON");
            this.WriteLine(this.ExpectedJson);
        }

        protected override void Act()
        {
            this.ActualJson = JsonObject.ToJson(this.SourceDomTree, this.Settings);

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

            if (this.AdditionalAsserts == null)
                return;

            foreach (var additionalAssert in this.AdditionalAsserts)
            {
                additionalAssert(this.ActualJson);
            }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private string ActualJson { get; set; }
        #endregion

        #region User Supplied Properties
        private JsonSerializerSettings Settings { get; }
        private T SourceDomTree { get; }
        private string ExpectedJson { get; }

        private IEnumerable<Action<string>> AdditionalAsserts { get; }
        #endregion
    }
}