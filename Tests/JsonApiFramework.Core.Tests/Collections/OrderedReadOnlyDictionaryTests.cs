// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using FluentAssertions;

using JsonApiFramework.Collections;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Collections
{
    public class OrderedReadOnlyDictionaryTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public OrderedReadOnlyDictionaryTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(OrderedReadOnlyDictionaryTestData))]
        public void TestOrderedReadOnlyDictionaryCreation(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static IEnumerable<KeyValuePair<Guid, Guid>> CreateGuidToGuidCollection(int count)
        {
            var collection = new List<KeyValuePair<Guid, Guid>>(count);
            for (var i = 0; i < count; ++i)
            {
                var keyValuePair = new KeyValuePair<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid());
                collection.Add(keyValuePair);
            }
            return collection;
        }

        public static readonly IEnumerable<object[]> OrderedReadOnlyDictionaryTestData = new[]
            {
                new object[] { new OrderedReadOnlyDictionaryUnitTest<Guid, Guid>("With 10 KeyValuePair<Guid, Guid> Objects", CreateGuidToGuidCollection(10)) },
                new object[] { new OrderedReadOnlyDictionaryUnitTest<Guid, Guid>("With 20 KeyValuePair<Guid, Guid> Objects", CreateGuidToGuidCollection(20)) },
                new object[] { new OrderedReadOnlyDictionaryUnitTest<Guid, Guid>("With 30 KeyValuePair<Guid, Guid> Objects", CreateGuidToGuidCollection(30)) },
                new object[] { new OrderedReadOnlyDictionaryUnitTest<Guid, Guid>("With 40 KeyValuePair<Guid, Guid> Objects", CreateGuidToGuidCollection(40)) },
                new object[] { new OrderedReadOnlyDictionaryUnitTest<Guid, Guid>("With 50 KeyValuePair<Guid, Guid> Objects", CreateGuidToGuidCollection(50)) },
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        public class OrderedReadOnlyDictionaryUnitTest<TKey, TValue> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public OrderedReadOnlyDictionaryUnitTest(string name, IEnumerable<KeyValuePair<TKey, TValue>> expected)
                : base(name)
            {
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.WriteOrderedReadOnlyDictionary("Expected", this.Expected);

                this.WriteLine();
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = new OrderedReadOnlyDictionary<TKey, TValue>(this.Expected);

                this.WriteOrderedReadOnlyDictionary("Actual", this.Actual);
            }

            private void WriteOrderedReadOnlyDictionary(string actualOrExpectedText, IEnumerable<KeyValuePair<TKey, TValue>> orderedReadOnlyDictionary)
            {
                Contract.Requires(String.IsNullOrWhiteSpace(actualOrExpectedText) == false);
                Contract.Requires(orderedReadOnlyDictionary != null);

                this.WriteLine("{0} OrderedReadOnlyDictionary<{1}, {2}> Ordering", actualOrExpectedText, typeof(TKey).Name, typeof(TValue).Name);
                this.WriteLine();
                this.WriteLine("{0,-50} {1,-50}", "Key", "Value");
                this.WriteLine();
                foreach (var keyValuePair in orderedReadOnlyDictionary)
                {
                    var key = keyValuePair.Key;
                    var value = keyValuePair.Value;
                    this.WriteLine("{0,-50} {1,-50}", key, value);
                }
            }

            protected override void Assert()
            {
                this.Actual.Should().ContainInOrder(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private OrderedReadOnlyDictionary<TKey, TValue> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private IEnumerable<KeyValuePair<TKey, TValue>> Expected { get; }
            #endregion
        }
        #endregion
    }
}
