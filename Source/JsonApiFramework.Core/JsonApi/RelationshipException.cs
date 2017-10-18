// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the exception that is thrown from a <c>Relationship</c> object
    /// when an error occurs, such as when the wrong resource linkage was requested
    /// </summary>
    public class RelationshipException : Exception
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static RelationshipException CreateToOneResourceLinkageException()
        {
            var message = $"Unable to get to-one resource linkage on a non to-one relationship.";
            throw new RelationshipException(message);
        }

        public static RelationshipException CreateToManyResourceLinkageException()
        {
            var message = $"Unable to get to-many resource linkage on a non to-many relationship.";
            throw new RelationshipException(message);
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private RelationshipException(string message)
            : base(message)
        { }
        #endregion
    }
}