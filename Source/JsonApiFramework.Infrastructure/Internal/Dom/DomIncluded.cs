// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents an included resources node in the DOM tree.
    /// </summary>
    internal class DomIncluded : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Included; } }

        public override string Name
        { get { return "Included[]"; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomIncluded Create(params Node<DomNodeType>[] domResources)
        {
            var domIncluded = new DomIncluded(domResources);
            return domIncluded;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomIncluded(params Node<DomNodeType>[] domResources)
            : base(domResources)
        { }
        #endregion
    }
}
