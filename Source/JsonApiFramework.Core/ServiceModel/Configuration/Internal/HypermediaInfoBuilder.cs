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
    internal class HypermediaInfoBuilder : IHypermediaInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaInfoBuilder(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var hypermediaInfoFactory = CreateHypermediaInfoFactory(clrResourceType);
            this.HypermediaInfoFactory = hypermediaInfoFactory;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaInfoBuilder Implementation
        public IHypermediaInfoBuilder SetApiCollectionPathSegment(string apiCollectionPathSegment)
        {
            this.HypermediaInfoModifierCollection = this.HypermediaInfoModifierCollection ?? new List<Action<HypermediaInfo>>();
            this.HypermediaInfoModifierCollection.Add(x => { x.ApiCollectionPathSegment = apiCollectionPathSegment; });
            return this;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IHypermediaInfo CreateHypermediaInfo(IConventions conventions)
        {
            var hypermediaInfo = this.HypermediaInfoFactory(conventions);

            if (this.HypermediaInfoModifierCollection == null)
                return hypermediaInfo;

            foreach (var hypermediaInfoModifier in this.HypermediaInfoModifierCollection)
            {
                hypermediaInfoModifier(hypermediaInfo);
            }

            return hypermediaInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IConventions, HypermediaInfo> HypermediaInfoFactory { get; set; }
        private IList<Action<HypermediaInfo>> HypermediaInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IConventions, HypermediaInfo> CreateHypermediaInfoFactory(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            Func<IConventions, HypermediaInfo> hypermediaInfoFactory = (conventions) =>
                {
                    var apiCollectionPathSegment = clrResourceType.Name;
                    if (conventions != null && conventions.ApiTypeNamingConventions != null)
                    {
                        apiCollectionPathSegment = conventions.ApiTypeNamingConventions.Aggregate(apiCollectionPathSegment, (current, namingConvention) => namingConvention.Apply(current));
                    }

                    var hypermediaInfo = new HypermediaInfo(apiCollectionPathSegment);
                    return hypermediaInfo;
                };
            return hypermediaInfoFactory;
        }
        #endregion
    }
}