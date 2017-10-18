// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Internal
{
    /// <summary>Represents an immutable error message object.</summary>
    public class ErrorMessage
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorMessage(string title, string detail)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(title));
            Contract.Requires(String.IsNullOrWhiteSpace(detail));

            this.Title = title;
            this.Detail = detail;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Title { get; }
        public string Detail { get; }
        #endregion
    }
}