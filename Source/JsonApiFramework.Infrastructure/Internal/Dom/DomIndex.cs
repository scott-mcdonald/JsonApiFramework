// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents an index in a collection in the DOM tree.
    /// </summary>
    internal class DomIndex : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Index; } }

        public override string Name
        { get { return "[{0}]".FormatWith(this.Index); } }
        #endregion

        #region Properties
        public string Index { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomIndex Create<TIndex>(TIndex index)
        {
            var indexAsString = TypeConverter.Convert<string>(index);
            var domIndex = new DomIndex(indexAsString);
            return domIndex;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomIndex(string index)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(index) == false);

            this.Index = index;
        }
        #endregion
    }
}
