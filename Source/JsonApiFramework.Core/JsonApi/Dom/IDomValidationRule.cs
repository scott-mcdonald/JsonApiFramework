// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts a validation rule to be applied on DOM related workflows.
    /// </summary>
    public interface IDomValidationRule
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ContextType { get; }
        #endregion
    }

    /// <summary>
    /// Abstracts a validation rule to be applied on DOM related workflows.
    /// </summary>
    /// <remarks>
    /// Returns a null for a passed validation result, otherwise returns an
    /// error object for a failed validation result.
    /// </remarks>
    public interface IDomValidationRule<in TContext> : IDomValidationRule
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Error Validate(TContext context);
        #endregion
    }
}