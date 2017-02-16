// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.XUnit;

namespace JsonApiFramework.Tests.Json
{
    public class JsonObjectSerializationUnitTestFactory : XUnitSerializable
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonObjectSerializationUnitTestFactory(Func<JsonObjectSerializationUnitTestData, IUnitTest> jsonObjectSerializeUnitTestFactory,
                                                      Func<JsonObjectSerializationUnitTestData, IUnitTest> jsonObjectDeserializeUnitTestFactory,
                                                      JsonObjectSerializationUnitTestData data)
            : base(data.Name)
        {
            this.JsonObjectSerializeUnitTestFactory = jsonObjectSerializeUnitTestFactory;
            this.JsonObjectDeserializeUnitTestFactory = jsonObjectDeserializeUnitTestFactory;
            this.Data = data;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region Properties
        public Func<JsonObjectSerializationUnitTestData, IUnitTest> JsonObjectSerializeUnitTestFactory { get; }
        public Func<JsonObjectSerializationUnitTestData, IUnitTest> JsonObjectDeserializeUnitTestFactory { get; }
        public JsonObjectSerializationUnitTestData Data { get; }
        #endregion
    }
}
