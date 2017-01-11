// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi.Internal
{
    internal abstract class DomReadOnlyNode : DomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override bool IsReadOnly => true;
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomReadOnlyNode(DomNodeType type, string name)
            : base(type, name)
        { }

        protected DomReadOnlyNode(DomNodeType type, string name, IEnumerable<DomNode> domNodes)
            : base(type, name, domNodes)
        { }
        #endregion
    }
}
