// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable json:api meta object.
    /// </summary>
    /// <remarks>
    /// The <c>Meta</c> object is a thin wrapper around a <c>IDomObject</c>
    /// that represents the json:api meta object as a DOM tree. 
    /// </remarks>
    public class Meta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Meta(IDomObject domObject)
        {
            this.DomObject = domObject;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var domObject = (DomObject)this.DomObject ?? Dom.Internal.DomObject.Empty;
            var domObjectTreeString = domObject.ToTreeString();
            return domObjectTreeString;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region JSON Properties
        private IDomObject DomObject { get; }
        #endregion
    }
}