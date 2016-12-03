// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonObjectSerializationUnitTestData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonObjectSerializationUnitTestData(string name, JsonSerializerSettings settings, object expectedObject, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedObject = expectedObject;
            this.ExpectedJson = expectedJson;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region User Supplied Properties
        public string Name { get; private set; }
        public JsonSerializerSettings Settings { get; private set; }
        public object ExpectedObject { get; private set; }
        public string ExpectedJson { get; private set; }
        #endregion
    }
}
