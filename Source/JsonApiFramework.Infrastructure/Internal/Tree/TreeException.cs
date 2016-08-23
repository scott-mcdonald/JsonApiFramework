// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Represents an exception that is thrown for an invalid operation in an
    /// object tree.
    /// </summary>
    internal class TreeException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeException()
        { }

        public TreeException(string message)
            : base(message)
        { }

        public TreeException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}