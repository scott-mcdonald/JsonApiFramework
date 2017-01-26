// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api error source object.</summary>
    public class ErrorSource
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorSource(IDomObject domObject)
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

        #region Factory Methods
        public static ErrorSource CreateParameter(string parameter)
        {
            var domObject = new DomObject(new DomProperty(Keywords.Parameter, new DomValue<string>(parameter)));
            var errorSource = new ErrorSource(domObject);
            return errorSource;
        }

        public static ErrorSource CreatePointer(string pointer)
        {
            var domObject = new DomObject(new DomProperty(Keywords.Pointer, new DomValue<string>(pointer)));
            var errorSource = new ErrorSource(domObject);
            return errorSource;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region JSON Properties
        private IDomObject DomObject { get; }
        #endregion
    }
}