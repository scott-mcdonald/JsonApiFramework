// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class AttributeInfoBuilder : IAttributeInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributeInfoBuilder(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            var attributeInfoFactory = CreateAttributeInfoFactory(clrDeclaringType, clrPropertyName, clrPropertyType);
            this.AttributeInfoFactory = attributeInfoFactory;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributeInfoBuilder Implementation
        public IAttributeInfoBuilder SetApiPropertyName(string apiPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            this.AttributeInfoModifierCollection = this.AttributeInfoModifierCollection ?? new List<Action<AttributeInfo>>();
            this.AttributeInfoModifierCollection.Add(x => { x.ApiPropertyName = apiPropertyName; });
            return this;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IAttributeInfo CreateAttributeInfo(IConventions conventions)
        {
            var attributeInfo = this.AttributeInfoFactory(conventions);

            if (this.AttributeInfoModifierCollection == null)
                return attributeInfo;

            foreach (var attributeInfoModifier in this.AttributeInfoModifierCollection)
            {
                attributeInfoModifier(attributeInfo);
            }

            return attributeInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IConventions, AttributeInfo> AttributeInfoFactory { get; set; }
        private IList<Action<AttributeInfo>> AttributeInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IConventions, AttributeInfo> CreateAttributeInfoFactory(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            Func<IConventions, AttributeInfo> attributeInfoFactory = (conventions) =>
                {
                    var apiPropertyName = clrPropertyName;
                    if (conventions != null && conventions.ApiAttributeNamingConventions != null)
                    {
                        apiPropertyName = conventions.ApiAttributeNamingConventions.Aggregate(apiPropertyName, (current, namingConvention) => namingConvention.Apply(current));
                    }

                    var attributeInfo = new AttributeInfo(clrDeclaringType, clrPropertyName, clrPropertyType, apiPropertyName, false);
                    return attributeInfo;
                };
            return attributeInfoFactory;
        }
        #endregion
    }
}