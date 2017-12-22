// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal class RelationshipInfoBuilder<TResource> : IRelationshipInfoBuilder<TResource>, IRelationshipInfoFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipInfoBuilder(string apiRel, RelationshipCardinality apiCardinality, Type clrRelatedResourceType, IClrPropertyBinding clrRelatedResourcePropertyBinding)
        {
            Contract.Requires(clrRelatedResourceType != null);

            var contextFactory = CreateContextFactory(apiRel, apiCardinality, clrRelatedResourceType, clrRelatedResourcePropertyBinding);
            this.ContextFactory = contextFactory;

            this.ContextModifierCollection = new List<Action<Context>>();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipInfoBuilder<TObject> Implementation
        public IRelationshipInfoBuilder<TResource> SetApiRel(string apiRel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiRel) == false);

            this.ContextModifierCollection.Add(x => { x.ApiRel = apiRel; });
            return this;
        }
        #endregion

        #region IRelationshipInfoFactory Implementation
        public IRelationshipInfo Create(IMetadataConventions metadataConventions)
        {
            var context = this.ContextFactory(metadataConventions);

            foreach (var contextModifier in this.ContextModifierCollection)
            {
                contextModifier(context);
            }

            var apiRel = context.ApiRel;
            var apiCardinality = context.ApiCardinality;
            var clrRelatedResourceType = context.ClrRelatedResourceType;
            var clrRelatedResourcePropertyBinding = context.ClrRelatedResourcePropertyBinding;

            var relationshipInfo = Factory.CreateRelationshipInfo(apiRel, apiCardinality, clrRelatedResourceType, clrRelatedResourcePropertyBinding);
            return relationshipInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IMetadataConventions, Context> ContextFactory { get; }
        private IList<Action<Context>> ContextModifierCollection { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IMetadataConventions, Context> CreateContextFactory(string apiRel, RelationshipCardinality apiCardinality, Type clrRelatedResourceType, IClrPropertyBinding clrRelatedResourcePropertyBinding)
        {
            Contract.Requires(clrRelatedResourceType != null);

            Context ContextFactory(IMetadataConventions metadataConventions)
            {
                apiRel = apiRel ?? clrRelatedResourceType.Name;

                // TODO: Apply conventions here...
                if (metadataConventions != null)
                { }

                var context = new Context
                {
                    ApiRel = apiRel,
                    ApiCardinality = apiCardinality,
                    ClrRelatedResourceType = clrRelatedResourceType,
                    ClrRelatedResourcePropertyBinding = clrRelatedResourcePropertyBinding
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
            public string ApiRel { get; set; }
            public RelationshipCardinality ApiCardinality { get; set; }
            public Type ClrRelatedResourceType { get; set; }
            public IClrPropertyBinding ClrRelatedResourcePropertyBinding { get; set; }
        }
        #endregion
    }
}
