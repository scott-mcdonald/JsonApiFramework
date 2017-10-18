// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the exception that is thrown from a <c>Links</c> object
    /// when an error occurs, such as when a requested <c>Link</c> is not found.
    /// </summary>
    public class LinksException : Exception
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static LinksException CreateNotFoundException(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var message = $"Could not find link for the rel={rel}.";
            throw new LinksException(message);
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private LinksException(string message)
            : base(message)
        { }
        #endregion
    }
}