// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Represents the runtime context of deserializing JSON into a DOM tree.</summary>
    internal class DomDeserializationContext
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ApiDocumentType ApiDocumentType { get; private set; }
        public List<Error> ErrorsCollection { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void SetApiDocumentType(ApiDocumentType apiDocumentType)
        {
            this.ApiDocumentType = apiDocumentType;
        }
        #endregion

        #region Error Methods
        public void AddError(Error error)
        {
            Contract.Requires(error != null);

            this.ErrorsCollection = this.ErrorsCollection ?? new List<Error>();
            this.ErrorsCollection.Add(error);
        }

        public bool AnyErrors()
        { return this.ErrorsCollection != null && this.ErrorsCollection.Any(); }
        #endregion
    }
}
