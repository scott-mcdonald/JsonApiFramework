// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a resource or resource identifier node in the DOM tree.
    /// </summary>
    internal class DomData : NodeContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Data; } }

        public override string Name
        { get { return "Data"; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomData Create(Node<DomNodeType> domResourceOrResourceIdentifier = null)
        {
            var domData = new DomData(domResourceOrResourceIdentifier);
            return domData;
        }

        public static DomData CreateFromResource(DomReadOnlyResource domReadOnlyResource)
        {
            var domData = new DomData(domReadOnlyResource);
            return domData;
        }

        public static DomData CreateFromResource(DomReadWriteResource domReadWriteResource)
        {
            var domData = new DomData(domReadWriteResource);
            return domData;
        }

        public static DomData CreateFromResourceIdentifier(DomReadOnlyResourceIdentifier domReadOnlyResourceIdentifier)
        {
            var domData = new DomData(domReadOnlyResourceIdentifier);
            return domData;
        }

        public static DomData CreateFromResourceIdentifier(DomReadWriteResourceIdentifier domReadWriteResourceIdentifier)
        {
            var domData = new DomData(domReadWriteResourceIdentifier);
            return domData;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomData(Node<DomNodeType> domResourceOrResourceIdentifier = null)
            : base(domResourceOrResourceIdentifier)
        { }
        #endregion
    }
}
