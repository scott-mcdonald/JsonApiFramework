// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Internal.Dom
{
    internal class DomHRef : Node<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.HRef; } }

        public override string Name
        { get { return "HRef ({0})".FormatWith(this.HRef); } }
        #endregion

        #region Properties
        public string HRef { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomHRef Create(string hRef)
        {
            var domHRef = new DomHRef(hRef);
            return domHRef;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomHRef(string hRef)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(hRef) == false);

            this.HRef = hRef;
        }
        #endregion
    }
}
