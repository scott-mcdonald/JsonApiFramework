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
    internal class ResourceIdentityInfoBuilder : IResourceIdentityInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentityInfoBuilder(Type clrResourceType, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            var resourceIdentityInfoFactory = CreateResourceIdentityInfoFactory(clrResourceType, clrPropertyName, clrPropertyType);
            this.ResourceIdentityInfoFactory = resourceIdentityInfoFactory;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceIdentityInfoBuilder Implementation
        public IResourceIdentityInfoBuilder SetApiType(string apiType)
        {
            this.ResourceIdentityInfoModifierCollection = this.ResourceIdentityInfoModifierCollection ?? new List<Action<ResourceIdentityInfo>>();
            this.ResourceIdentityInfoModifierCollection.Add(x => { x.ApiType = apiType; });
            return this;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IResourceIdentityInfo CreateResourceIdentityInfo(IConventions conventions)
        {
            var resourceIdentityInfo = this.ResourceIdentityInfoFactory(conventions);

            if (this.ResourceIdentityInfoModifierCollection == null)
                return resourceIdentityInfo;

            foreach (var resourceIdentityInfoModifier in this.ResourceIdentityInfoModifierCollection)
            {
                resourceIdentityInfoModifier(resourceIdentityInfo);
            }

            return resourceIdentityInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IConventions, ResourceIdentityInfo> ResourceIdentityInfoFactory { get; set; }
        private IList<Action<ResourceIdentityInfo>> ResourceIdentityInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IConventions, ResourceIdentityInfo> CreateResourceIdentityInfoFactory(Type clrResourceType, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            Func<IConventions, ResourceIdentityInfo> resourceIdentityInfoFactory = (conventions) =>
                {
                    var apiType = clrResourceType.Name;
                    if (conventions != null && conventions.ApiTypeNamingConventions != null)
                    {
                        apiType = conventions.ApiTypeNamingConventions.Aggregate(apiType, (current, namingConvention) => namingConvention.Apply(current));
                    }

                    var resourceIdentityInfo = new ResourceIdentityInfo
                        {
                            // ResourceIdentityInfo Properties
                            ApiType = apiType,
                            Id = new PropertyInfo
                                {
                                    // PropertyInfo Properties
                                    ClrPropertyName = clrPropertyName,
                                    ClrPropertyType = clrPropertyType
                                }
                        };
                    return resourceIdentityInfo;
                };
            return resourceIdentityInfoFactory;
        }
        #endregion
    }
}