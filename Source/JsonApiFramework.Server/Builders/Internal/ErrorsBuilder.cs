// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class ErrorsBuilder : IErrorsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IErrorsBuilder Implementation
        public IErrorsBuilder AddError(Error error)
        {
            Contract.Requires(error != null);

            this.DomReadWriteErrors.AddDomReadOnlyError(error);
            return this;
        }

        public IErrorsBuilder AddError(IEnumerable<Error> errorCollection)
        {
            Contract.Requires(errorCollection != null);

            this.DomReadWriteErrors.AddDomReadOnlyErrorCollection(errorCollection);
            return this;
        }

        public IDocumentWriter ErrorsEnd()
        {
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ErrorsBuilder(DocumentBuilder parentBuilder, DomDocument domDocument)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domDocument != null);

            this.ParentBuilder = parentBuilder;

            var domReadWriteErrors = domDocument.GetOrAddErrors();
            this.DomReadWriteErrors = domReadWriteErrors;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private DocumentBuilder    ParentBuilder      { get; set; }
        private DomReadWriteErrors DomReadWriteErrors { get; set; }
        #endregion
    }
}
