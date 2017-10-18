// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.XUnit
{
    /// <summary>
    /// Base class to capture boilerplate code for an individual unit test
    /// executed inside an xUnitTests object.
    /// </summary>
    public abstract class UnitTest : XUnitSerializable
        , IUnitTest
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IUnitTest Implementation
        public void Execute(XUnitTests xUnitTests)
        {
            this.XUnitTests = xUnitTests;

            this.WriteLine("Test Name: {0}", this.Name);
            this.WriteLine();

            this.Arrange();
            this.Act();
            this.Assert();
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected UnitTest(string name)
            : base(name)
        { }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected XUnitTests XUnitTests { get; private set; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region UnitTest Overrides
        protected virtual void Arrange()
        { }

        protected virtual void Act()
        { }

        protected virtual void Assert()
        { }
        #endregion

        #region Write Methods
        protected void WriteLine()
        { this.XUnitTests.WriteLine(); }

        protected void WriteLine(string message)
        { this.XUnitTests.WriteLine(message); }

        protected void WriteLine(string format, params object[] args)
        { this.XUnitTests.WriteLine(format, args); }

        protected void WriteDashedLine()
        { this.XUnitTests.WriteDashedLine(); }
        #endregion
    }
}