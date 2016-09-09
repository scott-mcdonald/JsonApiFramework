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
    internal class DomAttribute : NodesContainer<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Attribute; } }

        public override string Name
        { get { return "Attribute ({0})".FormatWith(this.ApiPropertyName); } }
        #endregion

        #region Properties
        public ApiProperty ApiAttribute { get; private set; }
        public string ApiPropertyName { get; private set; }

        public object ClrAttribute { get; private set; }
        public string ClrPropertyName { get; private set; }
        public Type ClrPropertyType { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomAttribute CreateFromApiResource(IAttributeInfo attributeInfo, IGetAttributes apiGetAttributes)
        {
            Contract.Requires(attributeInfo != null);
            Contract.Requires(apiGetAttributes != null);

            var apiAttribute = attributeInfo.GetApiAttribute(apiGetAttributes);
            var apiPropertyName = attributeInfo.ApiPropertyName;

            var clrPropertyType = attributeInfo.ClrPropertyType;
            var clrPropertyName = attributeInfo.ClrPropertyName;
            var clrAttribute = apiAttribute.ToClrObject(clrPropertyType);

            var domAttribute = new DomAttribute(apiAttribute, apiPropertyName, clrAttribute, clrPropertyName, clrPropertyType);
            return domAttribute;
        }

        public static DomAttribute CreateFromClrResource(IServiceModel serviceModel, IAttributeInfo attributeInfo, object clrResource)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(attributeInfo != null);
            Contract.Requires(clrResource != null);

            var clrAttribute = attributeInfo.GetClrProperty(clrResource);
            return CreateFromClrAttribute(serviceModel, attributeInfo, clrAttribute);
        }

        public static DomAttribute CreateFromClrAttribute(IServiceModel serviceModel, IAttributeInfo attributeInfo, object clrAttribute)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(attributeInfo != null);

            var domAttribute = CreateFromClrAttribute(attributeInfo, clrAttribute);

            var isComplexType = attributeInfo.IsComplexType;
            if (!isComplexType)
                return domAttribute;

            var isCollection = attributeInfo.IsCollection;
            if (isCollection)
            {
                AddCollectionOfComplexTypeAttributes(serviceModel, attributeInfo, domAttribute);
            }
            else
            {
                AddComplexTypeAttributes(serviceModel, attributeInfo, domAttribute);
            }

            return domAttribute;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomAttribute(ApiProperty apiAttribute, string apiPropertyName, object clrAttribute, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(apiAttribute != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);
            Contract.Requires(clrAttribute != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            this.ApiAttribute = apiAttribute;
            this.ApiPropertyName = apiPropertyName;

            this.ClrAttribute = clrAttribute;
            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static DomAttribute CreateFromClrAttribute(IAttributeInfo attributeInfo, object clrAttribute)
        {
            Contract.Requires(attributeInfo != null);

            var apiPropertyName = attributeInfo.ApiPropertyName;
            var clrPropertyName = attributeInfo.ClrPropertyName;
            var clrPropertyType = attributeInfo.ClrPropertyType;

            var apiAttribute = ApiProperty.Create(apiPropertyName, clrPropertyType, clrAttribute);

            var domAttribute = new DomAttribute(apiAttribute, apiPropertyName, clrAttribute, clrPropertyName, clrPropertyType);
            return domAttribute;
        }

        private static void AddCollectionOfComplexTypeAttributes(IServiceModel serviceModel, IAttributeInfo attributeInfo, DomAttribute domAttribute)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(attributeInfo != null);
            Contract.Requires(domAttribute != null);

            var clrAttribute = domAttribute.ClrAttribute;
            if (clrAttribute == null)
                return;

            var clrCollectionItemType = attributeInfo.ClrCollectionItemType;
            var complexType = serviceModel.GetComplexType(clrCollectionItemType);
            var complexTypeAttributesInfo = complexType.AttributesInfo;
            var complexTypeAttributesInfoCollection = complexTypeAttributesInfo.Collection;

            var index = 0;
            var clrCollection = (IEnumerable<object>)clrAttribute;
            foreach (var clrCollectionItem in clrCollection.EmptyIfNull())
            {
                // Add indexed complex object to index node.
                var localClrObject = clrCollectionItem;
                if (localClrObject == null)
                    continue;

                // Add an index node.
                var domIndex = domAttribute.CreateAndAddNode(() => DomIndex.Create(index++));

                // ReSharper disable once PossibleMultipleEnumeration
                foreach (var complexTypeAttributeInfo in complexTypeAttributesInfoCollection)
                {
                    var localAttributeInfo = complexTypeAttributeInfo;
                    domIndex.CreateAndAddNode(() => DomAttribute.CreateFromClrResource(serviceModel, localAttributeInfo, localClrObject));
                }
            }
        }

        private static void AddComplexTypeAttributes(IServiceModel serviceModel, IPropertyInfo attributeInfo, DomAttribute domAttribute)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(attributeInfo != null);
            Contract.Requires(domAttribute != null);

            var clrPropertyType = attributeInfo.ClrPropertyType;
            var clrAttribute = domAttribute.ClrAttribute;
            if (clrAttribute == null)
                return;

            var complexType = serviceModel.GetComplexType(clrPropertyType);
            var complexTypeAttributesInfo = complexType.AttributesInfo;
            var complexTypeAttributesInfoCollection = complexTypeAttributesInfo.Collection;
            foreach (var complexTypeAttributeInfo in complexTypeAttributesInfoCollection)
            {
                var localAttributeInfo = complexTypeAttributeInfo;
                domAttribute.CreateAndAddNode(() => DomAttribute.CreateFromClrResource(serviceModel, localAttributeInfo, clrAttribute));
            }
        }
        #endregion
    }
}
