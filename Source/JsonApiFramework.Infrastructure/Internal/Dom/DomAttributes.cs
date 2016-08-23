// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Internal.Dom
{
    internal class DomAttributes : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Attributes; } }

        public override string Name
        { get { return "Attributes"; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomAttributes Create(params Node<DomNodeType>[] domNodes)
        {
            var domAttributes = new DomAttributes(domNodes);
            return domAttributes;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomAttributes(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion
    }
}
