// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only error node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyError : Node<DomNodeType>, IDomError
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Error; } }

        public override string Name
        { get { return "ReadOnlyError"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomError Implementation
        public Error Error
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyError Create(Error error)
        {
            Contract.Requires(error != null);

            var domReadOnlyError = new DomReadOnlyError(error);
            return domReadOnlyError;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyError(Error error)
        {
            Contract.Requires(error != null);

            this.Error = error;
        }
        #endregion
    }
}
