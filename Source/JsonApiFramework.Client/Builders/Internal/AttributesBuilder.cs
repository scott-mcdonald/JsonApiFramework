// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Client.Properties;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal class AttributesBuilder<TParentBuilder> : IAttributesBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributesBuilder<TParentBuilder> Implementation
        public IAttributesBuilder<TParentBuilder> AddAttribute<TProperty>(IAttributeProxy<TProperty> attributeProxy)
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
        private TParentBuilder ParentBuilder { get; }
        private IResourceType ResourceType { get; }
        private DomAttributes DomAttributes { get; }
        private HashSet<string> ClrPropertyNames { get; set; }
        #endregion

        #region Calculated Properties
        private Type ClrResourceType => this.ResourceType.ClrType;
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
            var clrResourceType = this.ClrResourceType.Name;
            var detail = ClientErrorStrings.DocumentBuildExceptionDetailBuildAttributeAlreadyExists
                                           .FormatWith(clrPropertyName, clrResourceType);
            throw new DocumentBuildException(detail);
        }
        #endregion
    }

    internal class AttributesBuilder<TParentBuilder, TResource> : AttributesBuilder<TParentBuilder>, IAttributesBuilder<TParentBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributesBuilder<TParentBuilder, TResource> Implementation
        public new IAttributesBuilder<TParentBuilder, TResource> AddAttribute<TProperty>(IAttributeProxy<TProperty> attributeProxy)
        {
            Contract.Requires(attributeProxy != null);

            base.AddAttribute(attributeProxy);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributesBuilder(TParentBuilder parentBuilder, IResourceType resourceType, IContainerNode<DomNodeType> domContainerNode)
            : base(parentBuilder, resourceType, domContainerNode)
        {
        }
        #endregion
    }
}
