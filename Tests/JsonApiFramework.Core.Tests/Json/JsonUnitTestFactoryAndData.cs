// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.XUnit;

namespace JsonApiFramework.Tests.Json
{
    public class JsonUnitTestFactoryAndData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonUnitTestFactoryAndData(
            Func<JsonUnitTestData, IUnitTest> parseFactory,
            Func<JsonUnitTestData, IUnitTest> toJsonFactory,
            JsonUnitTestData data)
        {
            this.ParseFactory = parseFactory;
            this.ToJsonFactory = toJsonFactory;
            this.Data = data;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region Properties
        public Func<JsonUnitTestData, IUnitTest> ParseFactory { get; private set; }
        public Func<JsonUnitTestData, IUnitTest> ToJsonFactory { get; private set; }
        public JsonUnitTestData Data { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return this.Data.Name; }
        #endregion
    }
}
