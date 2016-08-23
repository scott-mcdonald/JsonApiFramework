// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class AttributesInfoBuilder<TResource>
        where TResource : class, IResource
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributesInfoBuilder()
        {
            var attributesInfoFactory = CreateAttributesInfoFactory();
            this.AttributesInfoFactory = attributesInfoFactory;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IAttributesInfo CreateAttributesInfo(IEnumerable<IAttributeInfo> collection)
        {
            var attributesInfo = this.AttributesInfoFactory(collection);

            if (this.AttributesInfoModifierCollection == null)
                return attributesInfo;

            foreach (var attributesInfoModifier in this.AttributesInfoModifierCollection)
            {
                attributesInfoModifier(attributesInfo);
            }

            return attributesInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IEnumerable<IAttributeInfo>, AttributesInfo> AttributesInfoFactory { get; set; }
        private IList<Action<AttributesInfo>> AttributesInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IEnumerable<IAttributeInfo>, AttributesInfo> CreateAttributesInfoFactory()
        {
            Func<IEnumerable<IAttributeInfo>, AttributesInfo> attributesInfoFactory = (collection) =>
                {
                    var attributesInfo = new AttributesInfo
                        {
                            // AttributesInfo Properties
                            Collection = collection.SafeToList(),
                        };
                    return attributesInfo;
                };
            return attributesInfoFactory;
        }
        #endregion
    }
}