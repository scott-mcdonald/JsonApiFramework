// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class AttributesInfo : InfoObject
        , IAttributesInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributesInfo(IEnumerable<IAttributeInfo> collection)
        {
            Contract.Requires(collection != null);

            this.Collection = collection.SafeToList();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IAttributesInfo Implementation
        [JsonProperty] public IEnumerable<IAttributeInfo> Collection { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region InfoObject Overrides
        public override void Initialize(IServiceModel serviceModel, IResourceType resourceType)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(resourceType != null);

            base.Initialize(serviceModel, resourceType);

            foreach (var attribute in this.Collection)
            {
                attribute.Initialize(serviceModel, resourceType);
            }
        }
        #endregion

        #region IAttributesInfo Implementation
        public IAttributeInfo GetApiAttribute(string apiPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            IAttributeInfo attribute;
            if (this.TryGetApiAttribute(apiPropertyName, out attribute))
                return attribute;

            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), this.ResourceType.ClrResourceType.Name);
            var attributeInfoDescription = "{0} [apiPropertyName={1}]".FormatWith(typeof(AttributeInfo).Name, apiPropertyName);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(resourceTypeDescription, attributeInfoDescription);
            throw new ServiceModelException(detail);
        }

        public IAttributeInfo GetClrAttribute(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            IAttributeInfo attribute;
            if (this.TryGetClrAttribute(clrPropertyName, out attribute))
                return attribute;

            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), this.ResourceType.ClrResourceType.Name);
            var attributeInfoDescription = "{0} [clrPropertyName={1}]".FormatWith(typeof(AttributeInfo).Name, clrPropertyName);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(resourceTypeDescription, attributeInfoDescription);
            throw new ServiceModelException(detail);
        }

        public bool TryGetApiAttribute(string apiPropertyName, out IAttributeInfo attribute)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            attribute = this.Collection.SingleOrDefault(x => x.ApiPropertyName == apiPropertyName);
            return attribute != null;
        }

        public bool TryGetClrAttribute(string clrPropertyName, out IAttributeInfo attribute)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            attribute = this.Collection.SingleOrDefault(x => x.ClrPropertyName == clrPropertyName);
            return attribute != null;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributesInfo()
        { }
        #endregion
    }
}