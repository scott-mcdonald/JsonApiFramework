// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents an array node in the DOM tree.
    /// </summary>
    public interface IDomArray : IDomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the count of this DOM array.</summary>
        int Count { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets an indexed DOM node contained by this DOM array.</summary>
        //IDomNode DomItem(int index);

        /// <summary>Gets the DOM node collection contained by this DOM array.</summary>
        IEnumerable<IDomItem> DomItems();
        #endregion
    }
}