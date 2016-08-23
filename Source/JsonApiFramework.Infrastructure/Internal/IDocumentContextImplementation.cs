// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Internal
{
    internal interface IDocumentContextImplementation : IDisposable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IDocumentContextOptions Options { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Configure(DocumentContextBase documentContextBase);
        void CreateNewDocument();

        DomDocument GetDomDocument();
        IDocumentReader GetDocumentReader();
        IDocumentWriter GetDocumentWriter();
        #endregion
    }
}