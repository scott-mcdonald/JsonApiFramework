// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class ResourceIdentityInfoBuilder<TResource> : IResourceIdentityInfoBuilder<TResource>, IResourceIdentityInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentityInfoBuilder(IClrPropertyBinding clrIdPropertyBinding)
        {
            Contract.Requires(clrIdPropertyBinding != null);

            var contextFactory = CreateContextFactory(clrIdPropertyBinding);
            this.ContextFactory = contextFactory;

            this.ContextModifierCollection = new List<Action<Context>>();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceIdentityInfoBuilder<TResource> Implementation
        public IResourceIdentityInfoBuilder<TResource> SetApiType(string apiType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiType) == false);

            this.ContextModifierCollection.Add(x => { x.ApiType = apiType; });
            return this;
        }
        #endregion

        #region IResourceIdentityInfoFactory Implementation
        public IResourceIdentityInfo Create(IMetadataConventions metadataConventions)
        {
            var context = this.ContextFactory(metadataConventions);

            foreach (var contextModifier in this.ContextModifierCollection)
            {
                contextModifier(context);
            }

            var apiType = context.ApiType;
            var clrIdPropertyBinding = context.ClrIdPropertyBinding;

            var resourceIdentityInfo = Factory.CreateResourceIdentityInfo(apiType, clrIdPropertyBinding);
            return resourceIdentityInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IMetadataConventions, Context> ContextFactory { get; }
        private IList<Action<Context>> ContextModifierCollection { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IMetadataConventions, Context> CreateContextFactory(IClrPropertyBinding clrIdPropertyBinding)
        {
            Contract.Requires(clrIdPropertyBinding != null);

            Context ContextFactory(IMetadataConventions metadataConventions)
            {
                var apiType = typeof(TResource).Name;

                // TODO: Apply conventions here...
                if (metadataConventions != null)
                { }

                var context = new Context
                {
                    ApiType = apiType,
                    ClrIdPropertyBinding = clrIdPropertyBinding
                };
                return context;
            }

            return ContextFactory;
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class Context
        {
            public string ApiType { get; set; }
            public IClrPropertyBinding ClrIdPropertyBinding { get; set; }
        }
        #endregion
    }
}
