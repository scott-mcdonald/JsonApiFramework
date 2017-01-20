// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.XUnit;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomJsonSerializationUnitTestFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomJsonSerializationUnitTestFactory(
            Func<DomJsonSerializationUnitTestData, IUnitTest> domJsonSerializeUnitTestFactory,
            Func<DomJsonSerializationUnitTestData, IUnitTest> domJsonDeserializeUnitTestFactory,
            DomJsonSerializationUnitTestData data)
        {
            this.DomJsonSerializeUnitTestFactory = domJsonSerializeUnitTestFactory;
            this.DomJsonDeserializeUnitTestFactory = domJsonDeserializeUnitTestFactory;
            this.Data = data;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region Properties
        public Func<DomJsonSerializationUnitTestData, IUnitTest> DomJsonSerializeUnitTestFactory { get; }
        public Func<DomJsonSerializationUnitTestData, IUnitTest> DomJsonDeserializeUnitTestFactory { get; }
        public DomJsonSerializationUnitTestData Data { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return this.Data.Name; }
        #endregion
    }
}
