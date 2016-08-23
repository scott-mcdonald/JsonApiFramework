// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a resource or resource identifier collection node in the DOM tree.
    /// </summary>
    internal class DomDataCollection : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.DataCollection; } }

        public override string Name
        { get { return "Data[]"; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomDataCollection Create(params Node<DomNodeType>[] domResourcesOrResourceIdentifiers)
        {
            var domDataCollection = new DomDataCollection(domResourcesOrResourceIdentifiers);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResources(params DomReadOnlyResource[] domReadOnlyResources)
        {
            var domDataCollection = new DomDataCollection(domReadOnlyResources);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResources(params DomReadWriteResource[] domReadWriteResources)
        {
            var domDataCollection = new DomDataCollection(domReadWriteResources);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResources(IEnumerable<DomReadOnlyResource> domReadOnlyResources)
        {
            var domDataCollection = new DomDataCollection(domReadOnlyResources);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResources(IEnumerable<DomReadWriteResource> domReadWriteResources)
        {
            var domDataCollection = new DomDataCollection(domReadWriteResources);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResourceIdentifiers(params DomReadOnlyResourceIdentifier[] domReadOnlyResourceIdentifiers)
        {
            var domDataCollection = new DomDataCollection(domReadOnlyResourceIdentifiers);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResourceIdentifiers(params DomReadWriteResourceIdentifier[] domReadWriteResourceIdentifiers)
        {
            var domDataCollection = new DomDataCollection(domReadWriteResourceIdentifiers);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResourceIdentifiers(IEnumerable<DomReadOnlyResourceIdentifier> domReadOnlyResourceIdentifiers)
        {
            var domDataCollection = new DomDataCollection(domReadOnlyResourceIdentifiers);
            return domDataCollection;
        }

        public static DomDataCollection CreateFromResourceIdentifiers(IEnumerable<DomReadWriteResourceIdentifier> domReadWriteResourceIdentifiers)
        {
            var domDataCollection = new DomDataCollection(domReadWriteResourceIdentifiers);
            return domDataCollection;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomDataCollection(IEnumerable<Node<DomNodeType>> domResourcesOrResourceIdentifiers)
            : base(domResourcesOrResourceIdentifiers)
        { }
        #endregion
    }
}
