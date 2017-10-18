// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the exception that is thrown from a <c>Relationships</c> object
    /// when an error occurs, such as when a requested <c>Relationship</c> is not found.
    /// </summary>
    public class RelationshipsException : Exception
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static RelationshipsException CreateNotFoundException(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var message = $"Could not find relationship for the rel={rel}.";
            throw new RelationshipsException(message);
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private RelationshipsException(string message)
            : base(message)
        { }
        #endregion
    }
}