// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant errors document.
    /// </summary>
    public class ErrorsDocument : Document
        , IGetErrors
        , ISetErrors
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorsDocument()
        { this.Errors = new List<Error>(); }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public List<Error> Errors { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override void AddError(Error error)
        {
            Contract.Requires(error != null);

            this.Errors = this.Errors ?? new List<Error>();
            this.Errors.Add(error);
        }

        public override void AddErrors(IEnumerable<Error> errorCollection)
        {
            Contract.Requires(errorCollection != null && errorCollection.All(x => x != null));

            this.Errors = this.Errors ?? new List<Error>();
            this.Errors.AddRange(errorCollection);
        }

        public override DocumentType GetDocumentType()
        { return DocumentType.ErrorsDocument; }

        public override IEnumerable<Error> GetErrors()
        { return this.Errors; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ErrorsDocument Empty = new ErrorsDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ErrorsDocument).Name;
        #endregion
    }
}