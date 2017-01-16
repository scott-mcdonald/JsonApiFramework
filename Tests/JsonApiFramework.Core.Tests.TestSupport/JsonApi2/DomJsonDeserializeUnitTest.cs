// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi2;
using JsonApiFramework.JsonApi2.Dom;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.JsonApi2
{
    public class DomJsonDeserializeUnitTest<T> : UnitTest
        where T : IDomNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomJsonDeserializeUnitTest(DomJsonSerializationUnitTestData data, params Action<T> [] additionalAsserts)
            : base(data.Name)
        {
            this.Settings = data.Settings;
            this.SourceJson = data.ExpectedJson;
            this.ExpectedDomTree = (T)data.ExpectedDeserializeDomTree;
            this.AdditionalAsserts = additionalAsserts;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected override void Arrange()
        {
            var expectedDomTreeAsString = this.ExpectedDomTree != null
                ? this.ExpectedDomTree.ToTreeString()
                : "null";

            this.WriteLine("Source JSON");
            this.WriteLine(this.SourceJson);
            this.WriteLine();
            this.WriteLine("Expected DOM Tree");
            this.WriteLine(expectedDomTreeAsString);
        }

        protected override void Act()
        {
            var actualDomTree = JsonObject.Parse<T>(this.SourceJson, this.Settings);
            this.ActualDomTree = actualDomTree;

            var actualDomTreeAsString = this.ActualDomTree != null
                ? this.ActualDomTree.ToTreeString()
                : "null";

            this.WriteLine();
            this.WriteLine("Actual DOM Tree");
            this.WriteLine(actualDomTreeAsString);
        }

        protected override void Assert()
        {
            // Use the FluentAssertion ShouldBeEquivalentTo method to compare
            // the expected and actual object graphs.
            this.ActualDomTree.ShouldBeEquivalentTo(this.ExpectedDomTree,
                config => config.AllowingInfiniteRecursion()
                                .IncludingAllRuntimeProperties()
                                .IgnoringCyclicReferences());

            if (this.ActualDomTree == null)
                return;

            if (this.AdditionalAsserts == null)
                return;

            foreach (var additionalAssert in this.AdditionalAsserts)
            {
                additionalAssert(this.ActualDomTree);
            }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////
        #region Calculated Properties
        private T ActualDomTree { get; set; }
        #endregion

        #region User Supplied Properties
        private JsonSerializerSettings Settings { get; }
        private string SourceJson { get; }
        private T ExpectedDomTree { get; }
        private IEnumerable<Action<T>> AdditionalAsserts { get; }
        #endregion
    }
}