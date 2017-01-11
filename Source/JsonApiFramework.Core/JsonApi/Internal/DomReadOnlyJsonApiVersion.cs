// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class DomReadOnlyJsonApiVersion : DomReadOnlyNode
        , IDomJsonApiVersion
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyJsonApiVersion(JsonApiVersion jsonApiVersion)
            : base(DomNodeType.JsonApiVersion, "ReadOnlyJsonApiVersion")
        {
            Contract.Requires(jsonApiVersion != null);

            this.JsonApiVersion = jsonApiVersion;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IGetJsonApiVersion Implementation
        public JsonApiVersion GetJsonApiVersion()
        { return this.JsonApiVersion; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region IGetJsonApiVersion Implementation
        private JsonApiVersion JsonApiVersion { get; }
        #endregion
    }
}
