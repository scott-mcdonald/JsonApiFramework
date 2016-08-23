// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write errors node in the DOM tree.
    /// </summary>
    internal class DomReadWriteErrors : NodesContainer<DomNodeType>, IDomErrors
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Errors; } }

        public override string Name
        { get { return "ReadWriteErrors"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return false; } }
        #endregion

        #region IDomErrors Implementation
        public IEnumerable<Error> Errors
        { get { return this.GetErrors(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteErrors Create(params Node<DomNodeType>[] domResources)
        {
            var domReadWriteErrors = new DomReadWriteErrors(domResources);
            return domReadWriteErrors;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DomReadOnlyError AddDomReadOnlyError(Error error)
        {
            Contract.Requires(error != null);

            var domReadOnlyError = this.CreateAndAddNode(() => DomReadOnlyError.Create(error));
            return domReadOnlyError;
        }

        internal IEnumerable<DomReadOnlyError> AddDomReadOnlyErrorCollection(IEnumerable<Error> errorCollection)
        {
            Contract.Requires(errorCollection != null);

            var domReadOnlyErrorCollection = errorCollection
                .Select(x =>
                    {
                        var error = x;
                        var domReadOnlyError = this.CreateAndAddNode(() => DomReadOnlyError.Create(error));
                        return domReadOnlyError;
                    })
                .ToList();
            return domReadOnlyErrorCollection;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteErrors(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IEnumerable<Error> GetErrors()
        {
            var errors = new List<Error>();

            var domErrorsNodes = this.Nodes();
            foreach (var domErrorsNode in domErrorsNodes)
            {
                var domErrorsNodeType = domErrorsNode.NodeType;
                switch (domErrorsNodeType)
                {
                    case DomNodeType.Error:
                        {
                            var domError = (IDomError)domErrorsNode;
                            var error = domError.Error;
                            errors.Add(error);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domErrorsNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return errors;
        }
        #endregion
    }
}
