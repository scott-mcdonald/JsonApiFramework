// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi.Dom
{
    public class DomReadOnlyJsonApiVersion : DomNode
        , IDomJsonApiVersion
    {
        // PUBLIC CONSTRUCTOR ///////////////////////////////////////////////
        #region Constructor
        public DomReadOnlyJsonApiVersion(JsonApiVersion jsonApiVersion)
            : base(DomNodeType.JsonApiVersion, "JsonApiVersion")
        {
            Contract.Requires(jsonApiVersion != null);

            this.JsonApiVersion = jsonApiVersion;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomJsonApiVersion Implementation
        public JsonApiVersion JsonApiVersion { get; }
        #endregion
    }
}
