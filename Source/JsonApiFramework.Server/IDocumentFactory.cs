// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Server
{
    public interface IDocumentFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentBuilder NewDocument(string currentRequestUrl);

        IDocumentBuilder NewDocument(Uri currentRequestUrl);
        #endregion
    }
}
