// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents an object node that
    /// contains DOM property nodes in the DOM tree.
    /// </summary>
    public interface IDomObject : IDomNode
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the DOM property node collection contained by this DOM object.</summary>
        IEnumerable<IDomProperty> DomProperties();
        #endregion
    }
}