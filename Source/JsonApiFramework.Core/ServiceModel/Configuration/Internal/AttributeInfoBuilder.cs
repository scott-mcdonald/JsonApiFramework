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
            this.AttributeInfoContextFactory = attributeInfoFactory;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributeInfoBuilder Implementation
        public IAttributeInfoBuilder SetApiPropertyName(string apiPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            this.AttributeInfoContextModifierCollection = this.AttributeInfoContextModifierCollection ?? new List<Action<AttributeInfoContext>>();
            this.AttributeInfoContextModifierCollection.Add(x => { x.AttibuteInfo.ApiPropertyName = apiPropertyName; });
            return this;
        }

        public IAttributeInfoBuilder Ignore()
        {
            this.AttributeInfoContextModifierCollection = this.AttributeInfoContextModifierCollection ?? new List<Action<AttributeInfoContext>>();
            this.AttributeInfoContextModifierCollection.Add(x => { x.Ignored = true; });
            return this;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IAttributeInfo CreateAttributeInfo(IConventions conventions)
        {
            var attributeInfoContext = this.AttributeInfoContextFactory(conventions);

            if (this.AttributeInfoContextModifierCollection == null)
            {
                return attributeInfoContext.Ignored == false
                    ? attributeInfoContext.AttibuteInfo
                    : null;
            }

            foreach (var attributeInfoContextModifier in this.AttributeInfoContextModifierCollection)
            {
                attributeInfoContextModifier(attributeInfoContext);
            }

            return attributeInfoContext.Ignored == false
                ? attributeInfoContext.AttibuteInfo
                : null;
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class AttributeInfoContext
        {
            public bool Ignored { get; set; }
            public AttributeInfo AttibuteInfo { get; set; }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IConventions, AttributeInfoContext> AttributeInfoContextFactory { get; set; }
        private IList<Action<AttributeInfoContext>> AttributeInfoContextModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IConventions, AttributeInfoContext> CreateAttributeInfoFactory(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            Func<IConventions, AttributeInfoContext> attributeInfoFactory = (conventions) =>
                {
                    var apiPropertyName = clrPropertyName;
                    if (conventions != null && conventions.ApiAttributeNamingConventions != null)
                    {
                        apiPropertyName = conventions.ApiAttributeNamingConventions.Aggregate(apiPropertyName, (current, namingConvention) => namingConvention.Apply(current));
                    }

                    var attributeInfo = new AttributeInfo(clrDeclaringType, clrPropertyName, clrPropertyType, apiPropertyName, false);
                    var attributeInfoContext = new AttributeInfoContext
                        {
                            AttibuteInfo = attributeInfo
                        };
                    return attributeInfoContext;
                };
            return attributeInfoFactory;
        }
        #endregion
    }
}