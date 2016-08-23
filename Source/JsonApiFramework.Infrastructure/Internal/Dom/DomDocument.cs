// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents the root document node of the DOM tree.
    /// </summary>
    internal class DomDocument : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Node Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Document; } }

        public override string Name
        { get { return "Document"; } }
        #endregion

        #region Properties
        public IServiceModel ServiceModel { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Read Methods
        public IDomErrors DomErrors()
        {
            var domErrors = (IDomErrors)this.GetNode(DomNodeType.Errors);
            return domErrors;
        }

        public IEnumerable<IDomResource> DomResources(bool enumerateIncludedResources = true)
        { return this.DomResourceIdentityBasedEnumeration<IDomResource>(domNodeType => domNodeType == DomNodeType.Resource, enumerateIncludedResources); }

        public IEnumerable<IDomResourceIdentity> DomResourceIdentities(bool enumerateIncludedResources = true)
        { return this.DomResourceIdentityBasedEnumeration<IDomResourceIdentity>(domNodeType => domNodeType == DomNodeType.Resource || domNodeType == DomNodeType.ResourceIdentifier, enumerateIncludedResources); }

        public IEnumerable<IDomResourceIdentifier> DomResourceIdentitifiers()
        { return this.DomResourceIdentityBasedEnumeration<IDomResourceIdentifier>(domNodeType => domNodeType == DomNodeType.ResourceIdentifier, false); }

        public static DomDocument Parse(Document apiDocument, IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            var domDocument = NodesContainer<DomNodeType>.CreateRoot(() => new DomDocument(serviceModel));

            ParseDocumentType(domDocument, apiDocument);
            ParseDocumentJsonApi(domDocument, apiDocument);
            ParseDocumentMeta(domDocument, apiDocument);
            ParseDocumentLinks(domDocument, apiDocument);
            ParseDocumentData(domDocument, apiDocument);
            ParseDocumentIncluded(domDocument, apiDocument);
            ParseDocumentErrors(domDocument, apiDocument);

            return domDocument;
        }
        #endregion

        #region Builder Methods
        public static DomDocument Create(IServiceModel serviceModel, params Node<DomNodeType>[] domNodes)
        {
            Contract.Requires(serviceModel != null);

            var domDocument = NodesContainer<DomNodeType>.CreateRoot(() => new DomDocument(serviceModel), domNodes);
            return domDocument;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Builder Methods
        internal DocumentType GetDocumentType()
        {
            // If the document type has already been analyzed and set as an
            // attribute, use the attribute.
            DocumentType documentType;
            if (this.TryAndGetSingleAttribute(DocumentTypeNodeAttributeName, out documentType))
            {
                return documentType;
            }

            documentType = this.AnalyzeDocumentType();
            this.SetSingleAttribute(DocumentTypeNodeAttributeName, documentType);
            return documentType;
        }

        internal DomReadOnlyJsonApiVersion SetDomReadOnlyJsonApiVersion(JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(jsonApiVersion != null);

            // Validation
            this.ValidateJsonApiVersionDoesNotExist();

            // Add the one allowed DOM jsonapi version node to the DOM document node.
            var domReadOnlyJsonApiVersion = this.CreateAndAddNode(() => DomReadOnlyJsonApiVersion.Create(jsonApiVersion));
            return domReadOnlyJsonApiVersion;
        }

        internal DomReadOnlyMeta SetDomReadOnlyMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            // Validation
            this.ValidateMetaDoesNotExist();

            // Add the one allowed DOM meta node to the DOM document node.
            var domReadOnlyMeta = this.CreateAndAddNode(() => DomReadOnlyMeta.Create(meta));
            return domReadOnlyMeta;
        }

        internal DomData AddData()
        {
            // Validation
            this.ValidateDataDoesNotExist();
            this.ValidateDataAndErrorsWillNotCoexist();

            // Add the one allowed DOM data node to the DOM document node.
            var domData = this.CreateAndAddNode(() => DomData.Create());
            return domData;
        }

        internal DomDataCollection AddDataCollection()
        {
            // Validation
            this.ValidateDataCollectionDoesNotExist();
            this.ValidateDataAndErrorsWillNotCoexist();

            // Add the one allowed DOM data collection node to the DOM document node.
            var domDataCollection = this.CreateAndAddNode(() => DomDataCollection.Create());
            return domDataCollection;
        }

        internal DomReadWriteErrors GetOrAddErrors()
        {
            // Validation
            this.ValidateErrorsAndDataWillNotCoexist();

            // Get the existing DOM errors node or add a new DOM errors node.
            var domReadWriteErrors = this.GetOrAddNode(DomNodeType.Errors, () => DomReadWriteErrors.Create());
            return domReadWriteErrors;
        }

        internal DomIncluded GetOrAddIncluded()
        {
            // Validation
            this.ValidateIncludedAndErrorsWillNotCoexist();

            // Get the existing DOM included node or add a new DOM included node.
            var domIncluded = this.GetOrAddNode(DomNodeType.Included, () => DomIncluded.Create());
            return domIncluded;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomDocument(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            this.ServiceModel = serviceModel;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        private DocumentType AnalyzeDocumentType()
        {
            // Analyze "data" and "errors" nodes to determine the concrete
            // document type.
            //
            // 1. If "data" is present, then
            //    1.1 If "data" is an object, then
            //        1.1.1 If "data" is a null, then "NullDocument".
            //        1.1.2 If "data" is a resource, then "ResourceDocument".
            //        1.1.3 If "data" is a resource identifier, then "ResourceIdentifierDocument".
            //    1.2 If "data" is an array, then
            //        1.2.1 If "data" is an empty array, then "EmptyDocument".
            //        1.2.2 If "data" is an array of resources, then "ResourceCollectionDocument".
            //        1.2.3 If "data" is an array of resource identifiers, then "ResourceIdentifierCollectionDocument".
            // 2. If "errors" is present, then "ErrorsDocument".
            // 3. Else "Document".
            var domDataNode = this.GetNode<DomNodeType, DomData>(DomNodeType.Data);
            if (domDataNode != null)
            {
                var domDataChildNode = domDataNode.Node;
                if (domDataChildNode == null)
                {
                    return DocumentType.NullDocument;
                }

                // Determine if the child node represents a resource or resource identifier.
                var domDataChildNodeType = domDataChildNode.NodeType;
                switch (domDataChildNodeType)
                {
                    case DomNodeType.Resource:
                        {
                            return DocumentType.ResourceDocument;
                        }

                    case DomNodeType.ResourceIdentifier:
                        {
                            return DocumentType.ResourceIdentifierDocument;
                        }

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(DomNodeType.Data, domDataChildNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            var domDataCollectionNode = this.GetNode<DomNodeType, DomDataCollection>(DomNodeType.DataCollection);
            if (domDataCollectionNode != null)
            {
                var domDataFirstChildNode = domDataCollectionNode.FirstNode;
                if (domDataFirstChildNode == null)
                {
                    return DocumentType.EmptyDocument;
                }

                // Determine if the first child node represents a resource or resource identifier.
                var domDataFirstChildNodeType = domDataFirstChildNode.NodeType;
                switch (domDataFirstChildNodeType)
                {
                    case DomNodeType.Resource:
                        {
                            return DocumentType.ResourceCollectionDocument;
                        }

                    case DomNodeType.ResourceIdentifier:
                        {
                            return DocumentType.ResourceIdentifierCollectionDocument;
                        }

                    default:
                        {
                            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasUnexpectedChildNode
                                                                   .FormatWith(DomNodeType.DataCollection, domDataFirstChildNodeType);
                            throw new DomException(detail);
                        }
                }
            }

            var containsDomErrorsNode = this.ContainsNode(DomNodeType.Errors);
            return containsDomErrorsNode
                ? DocumentType.ErrorsDocument
                : DocumentType.Document;
        }
        #endregion

        #region Parse Methods
        private static void ParseDocumentType(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            var documentType = apiDocument.GetDocumentType();
            domDocument.SetSingleAttribute(DocumentTypeNodeAttributeName, documentType);
        }

        private static void ParseDocumentJsonApi(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            if (apiDocument.HasJsonApi() == false)
                return;

            domDocument.CreateAndAddNode(() => DomReadOnlyJsonApiVersion.Create(apiDocument));
        }

        private static void ParseDocumentMeta(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            if (apiDocument.HasMeta() == false)
                return;

            domDocument.CreateAndAddNode(() => DomReadOnlyMeta.Create(apiDocument));
        }

        private static void ParseDocumentLinks(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            if (apiDocument.HasLinks() == false)
                return;

            domDocument.CreateAndAddNode(() => DomReadOnlyLinks.Create(apiDocument));
        }

        private static void ParseDocumentData(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            var documentType = apiDocument.GetDocumentType();
            switch (documentType)
            {
                case DocumentType.Document:
                case DocumentType.ErrorsDocument:
                    {
                        // NOOP
                    }
                    break;

                case DocumentType.EmptyDocument:
                    {
                        AddDomDataCollection(domDocument);
                    }
                    break;

                case DocumentType.NullDocument:
                    {
                        AddDomData(domDocument);
                    }
                    break;

                case DocumentType.ResourceDocument:
                    {
                        var apiResource = apiDocument.GetResource();
                        ParsePrimaryData(domDocument, apiResource);
                    }
                    break;

                case DocumentType.ResourceCollectionDocument:
                    {
                        var apiResourceCollection = apiDocument.GetResourceCollection();
                        ParsePrimaryData(domDocument, apiResourceCollection);
                    }
                    break;

                case DocumentType.ResourceIdentifierDocument:
                    {
                        var apiResourceIdentifier = apiDocument.GetResourceIdentifier();
                        ParsePrimaryData(domDocument, apiResourceIdentifier);
                    }
                    break;

                case DocumentType.ResourceIdentifierCollectionDocument:
                    {
                        var apiResourceIdentifierCollection = apiDocument.GetResourceIdentifierCollection();
                        ParsePrimaryData(domDocument, apiResourceIdentifierCollection);
                    }
                    break;

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(DocumentType).Name, documentType);
                        throw new InternalErrorException(detail);
                    }
            }
        }

        private static void ParseDocumentIncluded(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            if (apiDocument.IsIncludedNullOrEmpty())
                return;

            // Add included node.
            var domIncluded = domDocument.CreateAndAddNode(() => DomIncluded.Create());

            var includedResources = apiDocument.GetIncludedResources();
            foreach (var includedResource in includedResources)
            {
                var apiResource = includedResource;
                AddDomReadOnlyResource(domDocument, domIncluded, apiResource);
            }
        }

        private static void ParseDocumentErrors(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            if (apiDocument.IsErrorsDocument())
            {
                ParseErrors(domDocument, apiDocument);
            }
        }

        private static void ParsePrimaryData(DomDocument domDocument, Resource apiResource)
        {
            Contract.Requires(domDocument != null);

            // Add data node.
            var domData = AddDomData(domDocument);

            // Add the primary document data if there is any.
            if (apiResource == null)
                return;

            AddDomReadOnlyResource(domDocument, domData, apiResource);
        }

        private static void ParsePrimaryData(DomDocument domDocument, ResourceIdentifier apiResourceIdentifier)
        {
            Contract.Requires(domDocument != null);

            // Add data node.
            var domData = AddDomData(domDocument);

            // Add the primary document data if there is any.
            if (apiResourceIdentifier == null)
                return;

            AddDomReadOnlyResourceIdentifier(domDocument, domData, apiResourceIdentifier);
        }

        private static void ParsePrimaryData(DomDocument domDocument, IEnumerable<Resource> apiResourceCollection)
        {
            Contract.Requires(domDocument != null);

            // Add data collection node.
            var domDataCollection = AddDomDataCollection(domDocument);

            // Parse the primary document data if there is any.
            if (apiResourceCollection == null)
                return;

            foreach (var apiResource in apiResourceCollection)
            {
                AddDomReadOnlyResource(domDocument, domDataCollection, apiResource);
            }
        }

        private static void ParsePrimaryData(DomDocument domDocument, IEnumerable<ResourceIdentifier> apiResourceIdentifierCollection)
        {
            Contract.Requires(domDocument != null);

            // Add data collection node.
            var domDataCollection = AddDomDataCollection(domDocument);

            // Parse the primary document data if there is any.
            if (apiResourceIdentifierCollection == null)
                return;

            foreach (var apiResourceIdentifier in apiResourceIdentifierCollection)
            {
                AddDomReadOnlyResourceIdentifier(domDocument, domDataCollection, apiResourceIdentifier);
            }
        }

        private static void ParseErrors(DomDocument domDocument, Document apiDocument)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(apiDocument != null);

            var errors = apiDocument.GetErrors();

            domDocument.CreateAndAddNode(() => DomReadOnlyErrors.Create(errors));
        }

        private static DomData AddDomData(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            // Add data node.
            var domData = domDocument.CreateAndAddNode(() => DomData.Create());
            return domData;
        }

        private static DomDataCollection AddDomDataCollection(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            // Add data collection node.
            var domDataCollection = domDocument.CreateAndAddNode(() => DomDataCollection.Create());
            return domDataCollection;
        }

        private static DomReadOnlyResource AddDomReadOnlyResource(DomDocument domDocument, IContainerNode<DomNodeType> domParentNode, Resource apiResource)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(domParentNode != null);
            Contract.Requires(apiResource != null);

            // Find metadata based on the JsonApi resource.
            var apiResourceType = apiResource.Type;
            var resourceType = domDocument.ServiceModel.GetResourceType(apiResourceType);

            // Create/Add DOM read-only resource node to the parent node.
            var clrResource = resourceType.CreateClrResource();

            var domReadOnlyResource = domParentNode.CreateAndAddNode(() => DomReadOnlyResource.Create(apiResource, clrResource));

            // Map the incoming JsonApi resource to the created CLR resource.
            resourceType.MapApiMetaToClrResource(clrResource, apiResource);
            resourceType.MapApiIdToClrResource(clrResource, apiResource);
            resourceType.MapApiAttributesToClrResource(clrResource, apiResource);
            resourceType.MapApiRelationshipsToClrResource(clrResource, apiResource);
            resourceType.MapApiLinksToClrResource(clrResource, apiResource);

            return domReadOnlyResource;
        }

        private static DomReadOnlyResourceIdentifier AddDomReadOnlyResourceIdentifier(DomDocument domDocument, IContainerNode<DomNodeType> domParentNode, ResourceIdentifier apiResourceIdentifier)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(domParentNode != null);
            Contract.Requires(apiResourceIdentifier != null);

            // Find metadata based on the JsonApi resource.
            var apiResourceType = apiResourceIdentifier.Type;
            var resourceType = domDocument.ServiceModel.GetResourceType(apiResourceType);
            var clrResourceType = resourceType.ClrResourceType;

            // Create/Add DOM read-only resource identifier node to the parent node.
            var domReadOnlyResourceIdentifier = domParentNode.CreateAndAddNode(() => DomReadOnlyResourceIdentifier.Create(apiResourceIdentifier, clrResourceType));
            return domReadOnlyResourceIdentifier;
        }
        #endregion

        #region Enumeration Methods
        private IEnumerable<T> DomResourceIdentityBasedEnumeration<T>(Func<DomNodeType, bool> resourceIdentityPredicate, bool enumerateIncludedResources)
            where T : IDomResourceIdentity
        {
            Contract.Requires(resourceIdentityPredicate != null);

            var domDataNode = this.GetNode<DomNodeType, DomData>(DomNodeType.Data);
            if (domDataNode != null)
            {
                var domDataNodeChild = domDataNode.Node;

                if (domDataNodeChild == null || !resourceIdentityPredicate(domDataNodeChild.NodeType))
                    yield break;

                var domDataNodeChildType = domDataNodeChild.NodeType;
                switch (domDataNodeChildType)
                {
                    case DomNodeType.Resource:
                        {
                            var domPrimaryResourceNode = (IDomResource)domDataNodeChild;
                            var domPrimaryResourceIdentityNode = (T)domPrimaryResourceNode;
                            yield return domPrimaryResourceIdentityNode;
                        }
                        break;

                    case DomNodeType.ResourceIdentifier:
                        {
                            var domPrimaryResourceIdentifierNode = (IDomResourceIdentifier)domDataNodeChild;
                            var domPrimaryResourceIdentityNode = (T)domPrimaryResourceIdentifierNode;
                            yield return domPrimaryResourceIdentityNode;
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                                   .FormatWith(typeof(DomNodeType).Name, domDataNodeChildType);
                            throw new InternalErrorException(detail);
                        }
                }

                if (!enumerateIncludedResources)
                    yield break;

                var domIncludedNode = this.GetNode<DomNodeType, DomIncluded>(DomNodeType.Included);
                if (domIncludedNode == null)
                    yield break;

                var domIncludedNodeChild = domIncludedNode.FirstNode;
                while (domIncludedNodeChild != null)
                {
                    var domIncludedResourceNode = (IDomResource)domIncludedNodeChild;
                    var domIncludedResourceIdentityNode = (T)domIncludedResourceNode;
                    yield return domIncludedResourceIdentityNode;

                    domIncludedNodeChild = domIncludedNodeChild.NextNode;
                }
            }
            else
            {
                var domDataCollectionNode = this.GetNode<DomNodeType, DomDataCollection>(DomNodeType.DataCollection);
                if (domDataCollectionNode == null)
                    yield break;

                var domDataCollectionNodeChild = domDataCollectionNode.FirstNode;
                if (domDataCollectionNodeChild == null)
                    yield break;

                while (domDataCollectionNodeChild != null)
                {
                    if (resourceIdentityPredicate(domDataCollectionNodeChild.NodeType))
                    {
                        var domDataCollectionNodeChildType = domDataCollectionNodeChild.NodeType;
                        switch (domDataCollectionNodeChildType)
                        {
                            case DomNodeType.Resource:
                                {
                                    var domPrimaryResourceNode = (IDomResource)domDataCollectionNodeChild;
                                    var domPrimaryResourceIdentityNode = (T)domPrimaryResourceNode;
                                    yield return domPrimaryResourceIdentityNode;
                                }
                                break;

                            case DomNodeType.ResourceIdentifier:
                                {
                                    var domPrimaryResourceIdentifierNode = (IDomResourceIdentifier)domDataCollectionNodeChild;
                                    var domPrimaryResourceIdentityNode = (T)domPrimaryResourceIdentifierNode;
                                    yield return domPrimaryResourceIdentityNode;
                                }
                                break;

                            default:
                                {
                                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                                           .FormatWith(typeof(DomNodeType).Name, domDataCollectionNodeChildType);
                                    throw new InternalErrorException(detail);
                                }
                        }
                    }

                    domDataCollectionNodeChild = domDataCollectionNodeChild.NextNode;
                }

                if (!enumerateIncludedResources)
                    yield break;

                var domIncludedNode = this.GetNode<DomNodeType, DomIncluded>(DomNodeType.Included);
                if (domIncludedNode == null)
                    yield break;

                var domIncludedNodeChild = domIncludedNode.FirstNode;
                while (domIncludedNodeChild != null)
                {
                    var domIncludedResourceNode = (IDomResource)domIncludedNodeChild;
                    var domIncludedResourceIdentityNode = (T)domIncludedResourceNode;
                    yield return domIncludedResourceIdentityNode;

                    domIncludedNodeChild = domIncludedNodeChild.NextNode;
                }
            }
        }
        #endregion

        #region Validation Methods
        private void ValidateDataDoesNotExist()
        {
            // Validate a DOM data node has not already been added to the DOM document.
            var containsDataNode = this.ContainsNode(DomNodeType.Data);
            if (!containsDataNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Data);
            throw new DomException(detail);
        }

        private void ValidateDataCollectionDoesNotExist()
        {
            // Validate a DOM data collection node has not already been added to the DOM document.
            var containsDataCollectionNode = this.ContainsNode(DomNodeType.DataCollection);
            if (!containsDataCollectionNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Data);
            throw new DomException(detail);
        }

        private void ValidateDataAndErrorsWillNotCoexist()
        {
            // Validate a DOM errors node has not already been added to the DOM document before adding a DOM data node.
            var containsErrorsNode = this.ContainsNode(DomNodeType.Errors);
            if (!containsErrorsNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasChildNodesThatMustNotCoexist
                                                   .FormatWith(this.NodeType, Keywords.Errors, Keywords.Data);
            throw new DomException(detail);
        }

        private void ValidateErrorsAndDataWillNotCoexist()
        {
            // Validate a DOM data node has not already been added to the DOM document before adding a DOM errors node.
            var containsDataNode = this.ContainsNode(DomNodeType.Data);
            var containsDataCollectionNode = this.ContainsNode(DomNodeType.DataCollection);
            if (!containsDataNode && !containsDataCollectionNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasChildNodesThatMustNotCoexist
                                                   .FormatWith(this.NodeType, Keywords.Data, Keywords.Errors);
            throw new DomException(detail);
        }

        private void ValidateIncludedAndErrorsWillNotCoexist()
        {
            // Validate a DOM errors node has not already been added to the DOM document before adding a DOM data node.
            var containsErrorsNode = this.ContainsNode(DomNodeType.Errors);
            if (!containsErrorsNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeHasChildNodesThatMustNotCoexist
                                                   .FormatWith(this.NodeType, Keywords.Errors, Keywords.Included);
            throw new DomException(detail);
        }

        private void ValidateJsonApiVersionDoesNotExist()
        {
            // Validate a DOM jsonapi version node has not already been added to the DOM document.
            var containsJsonApiVersionNode = this.ContainsNode(DomNodeType.JsonApiVersion);
            if (!containsJsonApiVersionNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.JsonApi);
            throw new DomException(detail);
        }

        private void ValidateMetaDoesNotExist()
        {
            // Validate a DOM meta node has not already been added to the DOM document.
            var containsMetaNode = this.ContainsNode(DomNodeType.Meta);
            if (!containsMetaNode)
                return;

            var detail = InfrastructureErrorStrings.DomExceptionDetailNodeAlreadyContainsChildNode
                                                   .FormatWith(this.NodeType, Keywords.Meta);
            throw new DomException(detail);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string DocumentTypeNodeAttributeName = "document-type";
        #endregion
    }
}
