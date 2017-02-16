// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.XUnit
{
    public class AggregateUnitTest : XUnitSerializable
        , IUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AggregateUnitTest(string name, params IUnitTest [] unitTestCollection)
            : this(name, unitTestCollection.AsEnumerable())
        { }

        public AggregateUnitTest(string name, IEnumerable<IUnitTest> unitTestCollection)
            : base(name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.UnitTestCollection = unitTestCollection;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IUnitTest Implementation
        public void Execute(XUnitTests xUnitTests)
        {
            xUnitTests.WriteLine("Test Name: {0}", this.Name);
            xUnitTests.WriteDoubleDashedLine();
            xUnitTests.WriteLine();

            foreach (var unitTest in this.UnitTestCollection)
            {
                unitTest.Execute(xUnitTests);
                xUnitTests.WriteDashedLine();
                xUnitTests.WriteLine();
            }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IEnumerable<IUnitTest> UnitTestCollection { get; }
        #endregion
    }
}