// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.JsonApi
{
    public class DomTreeSerializationUnitTestData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomTreeSerializationUnitTestData(string name, JsonSerializerSettings settings, IDomNode expectedDomTree, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedDomTree = expectedDomTree;
            this.ExpectedJson = expectedJson;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region User Supplied Properties
        public string Name { get; private set; }
        public JsonSerializerSettings Settings { get; private set; }
        public IDomNode ExpectedDomTree { get; private set; }
        public string ExpectedJson { get; private set; }
        #endregion
    }
}
