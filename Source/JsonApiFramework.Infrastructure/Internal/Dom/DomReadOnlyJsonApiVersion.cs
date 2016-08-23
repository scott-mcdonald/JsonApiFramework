// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only json:api version informational node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyJsonApiVersion : Node<DomNodeType>, IDomJsonApiVersion
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.JsonApiVersion; } }

        public override string Name
        { get { return "JsonApiVersion"; } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomJsonApi Implementation
        public JsonApiVersion JsonApiVersion
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyJsonApiVersion Create(JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(jsonApiVersion != null);

            var domReadOnlyJsonApiVersion = new DomReadOnlyJsonApiVersion(jsonApiVersion);
            return domReadOnlyJsonApiVersion;
        }

        public static DomReadOnlyJsonApiVersion Create(IGetJsonApiVersion getJsonApiVersion)
        {
            Contract.Requires(getJsonApiVersion != null && getJsonApiVersion.JsonApiVersion != null);

            var jsonApiVersion = getJsonApiVersion.JsonApiVersion;

            var domReadOnlyJsonApiVersion = new DomReadOnlyJsonApiVersion(jsonApiVersion);
            return domReadOnlyJsonApiVersion;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyJsonApiVersion(JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(jsonApiVersion != null);

            this.JsonApiVersion = jsonApiVersion;
        }
        #endregion
    }
}
