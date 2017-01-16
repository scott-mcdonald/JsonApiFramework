// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents an indexed array item
    /// node contained by an array node in the DOM tree.
    /// </summary>
    public interface IDomItem : IDomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the index of this DOM item.</summary>
        int Index { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the DOM item value contained by this DOM item.</summary>
        IDomNode DomItemValue();
        #endregion
    }
}