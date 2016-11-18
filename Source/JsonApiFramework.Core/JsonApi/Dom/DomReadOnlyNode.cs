// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Dom
{
    public abstract class DomReadOnlyNode : DomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetIsReadOnly Implementation
        public override bool IsReadOnly => true;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Add/Remove/Replace Overrides
        public override void AddNode(Node newNode)
        {
            base.AddNode(newNode);
        }
        #endregion

        // PROTECTED CONSTRUCTOR ////////////////////////////////////////////
        #region Constructor
        protected DomReadOnlyNode(DomNodeType type, string name)
            : base(type, name)
        { }
        #endregion
    }
}
