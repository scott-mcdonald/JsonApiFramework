// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IErrorsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IErrorsBuilder AddError(Error error);

        IErrorsBuilder AddError(IEnumerable<Error> errorCollection);

        IDocumentWriter ErrorsEnd();
        #endregion
    }
}