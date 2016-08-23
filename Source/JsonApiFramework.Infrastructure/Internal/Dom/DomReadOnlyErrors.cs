// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only errors node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyErrors : Node<DomNodeType>, IDomErrors
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Errors; } }

        public override string Name
        { get { return "ReadOnlyErrors"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomErrors Implementation
        public IEnumerable<Error> Errors
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyErrors Create(IEnumerable<Error> errors)
        {
            Contract.Requires(errors != null);

            var domReadOnlyErrors = new DomReadOnlyErrors(errors);
            return domReadOnlyErrors;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyErrors(IEnumerable<Error> errors)
        {
            Contract.Requires(errors != null);

            this.Errors = errors;
        }
        #endregion
    }
}
