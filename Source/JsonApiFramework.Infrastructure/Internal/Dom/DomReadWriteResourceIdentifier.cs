// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write resource identifier node in the DOM tree.
    /// </summary>
    internal class DomReadWriteResourceIdentifier : DomReadWriteResourceIdentity
        , IDomResourceIdentifier
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.ResourceIdentifier; } }

        public override string Name
        { get { return "ReadWriteResourceIdentifier"; } }
        #endregion

        #region IDomResourceIdentifier Implementation
        public ResourceIdentifier ApiResourceIdentifier
        { get { return this.GetApiResourceIdentifier(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteResourceIdentifier Create(params Node<DomNodeType>[] domNodes)
        {
            var domReadWriteResourceIdentifier = new DomReadWriteResourceIdentifier(domNodes);
            return domReadWriteResourceIdentifier;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadWriteResourceIdentifier(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private ResourceIdentifier GetApiResourceIdentifier()
        {
            var type = default(string); // required per specification
            var id = default(string);   // required per specification
            var meta = default(Meta);   // optional per specification

            var domResourceIdentifierNodes = this.Nodes();
            foreach (var domResourceIdentifierNode in domResourceIdentifierNodes)
            {
                var domResourceIdentifierNodeType = domResourceIdentifierNode.NodeType;
                switch (domResourceIdentifierNodeType)
                {
                    case DomNodeType.Type:
                        {
                            var domType = (DomType)domResourceIdentifierNode;
                            type = domType.ApiType;
                        }
                        break;

                    case DomNodeType.Id:
                        {
                            var domId = (DomId)domResourceIdentifierNode;
                            id = domId.ApiId;
                        }
                        break;

                    case DomNodeType.Meta:
                        {
                            var domMeta = (IDomMeta)domResourceIdentifierNode;
                            meta = domMeta.Meta;
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(this.NodeType, domResourceIdentifierNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            return new ResourceIdentifier(type, id, meta);
        }
        #endregion
    }
}
