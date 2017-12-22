// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class AttributeInfoBuilder<TObject> : IAttributeInfoBuilder<TObject>, IAttributeInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributeInfoBuilder(IClrPropertyBinding clrPropertyBinding)
        {
            Contract.Requires(clrPropertyBinding != null);

            var contextFactory = CreateContextFactory(clrPropertyBinding);
            this.ContextFactory = contextFactory;

            this.ContextModifierCollection = new List<Action<Context>>();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IAttributeInfoBuilder<TObject> Implementation
        public IAttributeInfoBuilder<TObject> SetApiAttributeName(string apiAttributeName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiAttributeName) == false);

            this.ContextModifierCollection.Add(x => { x.ApiAttributeName = apiAttributeName; });
            return this;
        }

        public IAttributeInfoBuilder<TObject> Include()
        {
            this.ContextModifierCollection.Add(x => { x.Excluded = false; });
            return this;
        }

        public IAttributeInfoBuilder<TObject> Exclude()
        {
            this.ContextModifierCollection.Add(x => { x.Excluded = true; });
            return this;
        }
        #endregion

        #region IAttributeInfoFactory Implementation
        public IAttributeInfo Create(IMetadataConventions metadataConventions)
        {
            var context = this.ContextFactory(metadataConventions);

            foreach (var contextModifier in this.ContextModifierCollection)
            {
                contextModifier(context);
            }

            if (context.Excluded)
                return null;

            var apiAttributeName = context.ApiAttributeName;
            var clrPropertyBinding = context.ClrPropertyBinding;

            var attributeInfo = Factory.CreateAttributeInfo(apiAttributeName, clrPropertyBinding);
            return attributeInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IMetadataConventions, Context> ContextFactory { get; }
        private IList<Action<Context>> ContextModifierCollection { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IMetadataConventions, Context> CreateContextFactory(IClrPropertyBinding clrPropertyBinding)
        {
            Contract.Requires(clrPropertyBinding != null);

            Context ContextFactory(IMetadataConventions metadataConventions)
            {
                var apiPropertyName = clrPropertyBinding.ClrPropertyName;

                // TODO: Apply conventions here...
                if (metadataConventions != null)
                { }

                var context = new Context
                {
                    ApiAttributeName = apiPropertyName,
                    ClrPropertyBinding = clrPropertyBinding
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
            public bool Excluded { get; set; }
            public string ApiAttributeName { get; set; }
            public IClrPropertyBinding ClrPropertyBinding { get; set; }
        }
        #endregion
    }
}
