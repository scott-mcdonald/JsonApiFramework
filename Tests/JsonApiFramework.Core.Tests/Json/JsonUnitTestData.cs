// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonUnitTestData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonUnitTestData(string name, JsonSerializerSettings settings, IJsonObject expectedObject, string expectedJson)
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
        public IJsonObject ExpectedObject { get; private set; }
        public string ExpectedJson { get; private set; }
        #endregion
    }
}
