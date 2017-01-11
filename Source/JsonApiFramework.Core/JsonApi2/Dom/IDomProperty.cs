// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a named property node
    /// contained by an object node in the DOM tree.
    /// </summary>
    public interface IDomProperty : IDomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api property type this DOM property represents.</summary>
        ApiPropertyType ApiPropertyType { get; }

        /// <summary>Gets the json:api property name this DOM property represents.</summary>
        string ApiPropertyName { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the DOM property value contained by this DOM property.</summary>
        IDomNode DomPropertyValue();
        #endregion
    }
}