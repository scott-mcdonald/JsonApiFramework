// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi2.Dom
{
    public interface IDomResource : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IDomProperty DomMeta { get; }
        IDomProperty DomType { get; }
        IDomProperty DomId { get; }
        IDomProperty DomAttributes { get; }
        IDomProperty DomRelationships { get; }
        IDomProperty DomLinks { get; }
        #endregion
    }
}