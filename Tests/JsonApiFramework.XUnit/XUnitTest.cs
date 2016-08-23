// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Xunit.Abstractions;

namespace JsonApiFramework.XUnit
{
    /// <summary>
    /// Base class for any xUnit based tests that want to capture test output.
    /// </summary>
    public abstract class XUnitTest
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected XUnitTest(ITestOutputHelper output)
        {
            Contract.Requires(output != null);

            this.Output = output;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected ITestOutputHelper Output { get; private set; }
        #endregion
    }
}
