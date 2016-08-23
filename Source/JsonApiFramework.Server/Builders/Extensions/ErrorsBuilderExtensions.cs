// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class ErrorsBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IErrorsBuilder AddError(this IErrorsBuilder errorsBuilder, params Error[] errorCollection)
        {
            Contract.Requires(errorsBuilder != null);
            Contract.Requires(errorCollection != null);

            return errorsBuilder.AddError(errorCollection.AsEnumerable());
        }
        #endregion
    }
}