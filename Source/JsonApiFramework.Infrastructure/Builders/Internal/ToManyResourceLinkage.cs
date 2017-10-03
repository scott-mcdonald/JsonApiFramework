// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal class ToManyResourceLinkage<TResourceId> : IToManyResourceLinkage<TResourceId>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyResourceLinkage()
        {
            this.HasValueCollection = false;
            this.ValueCollection = Enumerable.Empty<TResourceId>();
        }

        public ToManyResourceLinkage(IEnumerable<TResourceId> valueCollection)
        {
            this.HasValueCollection = true;
            this.ValueCollection = valueCollection.EmptyIfNull();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToManyResourceLinkage Implementation
        public bool HasValueCollection { get; }
        #endregion

        #region IToManyResourceLinkage<TResourceId> Implementation
        public IEnumerable<TResourceId> ValueCollection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public IEnumerable<ResourceIdentifier> CreateApiResourceIdentifierCollection(IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            return this.ValueCollection.Select(resourceType.CreateApiResourceIdentifier);
        }
        #endregion
    }
}