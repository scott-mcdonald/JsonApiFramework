// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.XUnit
{
    /// <summary>
    /// Abstracts an individual unit test executed inside an XUnitTests object.
    /// </summary>
    public interface IUnitTest
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Execute(XUnitTests xUnitTests);
        #endregion
    }
}