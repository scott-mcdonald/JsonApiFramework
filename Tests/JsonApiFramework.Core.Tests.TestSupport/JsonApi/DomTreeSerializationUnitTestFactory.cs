// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.XUnit;

namespace JsonApiFramework.Tests.JsonApi
{
    public class DomTreeSerializationUnitTestFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomTreeSerializationUnitTestFactory(
            Func<DomTreeSerializationUnitTestData, IUnitTest> domTreeSerializeUnitTestFactory,
            Func<DomTreeSerializationUnitTestData, IUnitTest> domTreeDeserializeUnitTestFactory,
            DomTreeSerializationUnitTestData data)
        {
            this.DomTreeSerializeUnitTestFactory = domTreeSerializeUnitTestFactory;
            this.DomTreeDeserializeUnitTestFactory = domTreeDeserializeUnitTestFactory;
            this.Data = data;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region Properties
        public Func<DomTreeSerializationUnitTestData, IUnitTest> DomTreeSerializeUnitTestFactory { get; }
        public Func<DomTreeSerializationUnitTestData, IUnitTest> DomTreeDeserializeUnitTestFactory { get; }
        public DomTreeSerializationUnitTestData Data { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return this.Data.Name; }
        #endregion
    }
}
