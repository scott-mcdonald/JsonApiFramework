// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework
{
    public class DocumentContextOptionsBuilder : IDocumentContextOptionsBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContextOptionsBuilder(IDocumentContextOptions options)
        {
            Contract.Requires(options != null);

            this.Options = options;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDocumentContextOptionsBuilder Implementation
        public IDocumentContextOptions Options { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentContextOptionsBuilder Implementation
        public void ModifyExtension<TExtension>(Action<TExtension> modifyExtensionAction)
            where TExtension : class, IDocumentContextExtension, new()
        {
            Contract.Requires(modifyExtensionAction != null);

            this.Options.ModifyExtension(modifyExtensionAction);
        }
        #endregion
    }
}
