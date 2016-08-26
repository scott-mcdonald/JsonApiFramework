// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework
{
    public interface IDocumentContextExtension
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Abstracts the work to validate this extension object was configured
        /// correctly. If misconfigured, the extension object should add to the
        /// list of configuration error messages.
        /// </summary>
        /// <param name="configurationErrorMessages"></param>
        void ValidateConfiguration(ICollection<string> configurationErrorMessages);
        #endregion
    }
}
