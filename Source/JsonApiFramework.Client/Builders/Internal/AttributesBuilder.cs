// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal class AttributesBuilder<TParentBuilder, TResource> : IAttributesBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributesBuilder<TParentBuilder, TResource> Implementation
        public IAttributesBuilder<TParentBuilder, TResource> AddAttribute<TProperty>(IAttributeProxy<TProperty> attributeProxy)
        {
            Contract.Requires(attributeProxy != null);

            var clrPropertyName = attributeProxy.ClrPropertyName;
            var clrPropertyValue = attributeProxy.ClrPropertyValue;

            this.ValidateClrAttributeDoesNotExist(clrPropertyName);
            this.ClrPropertyNames.Add(clrPropertyName);

            this.ResourceType.MapClrAttributeToDomAttributes(this.DomAttributes, clrPropertyName, clrPropertyValue);
            return this;
        }

        public TParentBuilder AttributesEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributesBuilder(TParentBuilder parentBuilder, IResourceType resourceType, IContainerNode<DomNodeType> domContainerNode)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(resourceType != null);
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.ResourceType = resourceType;

            var domAttributes = domContainerNode.GetOrAddNode(DomNodeType.Attributes, () => DomAttributes.Create());
            this.DomAttributes = domAttributes;

            this.BuildClrAttributeNames();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private IResourceType ResourceType { get; set; }
        private DomAttributes DomAttributes { get; set; }
        private HashSet<string> ClrPropertyNames { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void BuildClrAttributeNames()
        {
            this.ClrPropertyNames = new HashSet<string>();
            foreach (var clrPropertyName in this.DomAttributes.Nodes().Cast<DomAttribute>().Select(domAttribute => domAttribute.ClrPropertyName))
            {
                this.ClrPropertyNames.Add(clrPropertyName);
            }
        }

        private void ValidateClrAttributeDoesNotExist(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            // Validate a DOM attribute node has not already been added to
            // the DOM attributes parent node.
            if (!this.ClrPropertyNames.Contains(clrPropertyName))
                return;

            // The DOM attribute with the given name has already been added
            // to the DOM attributes node.
            var clrResourceType = typeof(TResource).Name;
            var detail = ClientErrorStrings.DocumentBuildExceptionDetailBuildAttributeAlreadyExists
                                           .FormatWith(clrPropertyName, clrResourceType);
            throw new DocumentBuildException(detail);
        }
        #endregion
    }
}
