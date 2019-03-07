// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class ResourcePathContextCollectionBuilder<TParentBuilder> : IResourcePathContextBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPathContextBuilder Implementation
        IPathContextBuilder IPathContextBuilder.AddPath(Action<IPathContextBuilder, object> action)
        {
            return this.AddPath(action);
        }

        IPathContextBuilder IPathContextBuilder.AddPath<TPath>(TPath clrResource, string rel, bool includePath)
        {
            return this.AddPath(clrResource, rel, includePath);
        }

        IPathContextBuilder IPathContextBuilder.AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath)
        {
            return this.AddPath<TPath, TResourceId>(clrResourceId, rel, includePath);
        }

        IPathContextBuilder IPathContextBuilder.AddPath(string pathSegment, bool includePath)
        {
            return this.AddPath(pathSegment, includePath);
        }
        #endregion

        #region IResourcePathContextBuilder<TParentBuilder> Implementation
        public IResourcePathContextBuilder<TParentBuilder> AddPath(Action<IPathContextBuilder, object> action)
        {
            foreach (var resourcePathContextBuilder in this.ResourcePathContextBuilders)
            {
                resourcePathContextBuilder.AddPath(action);
            }

            return this;
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath>(TPath clrResource, string rel, bool includePath)
            where TPath : class
        {
            foreach (var resourcePathContextBuilder in this.ResourcePathContextBuilders)
            {
                resourcePathContextBuilder.AddPath(clrResource, rel, includePath);
            }

            return this;
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath)
            where TPath : class
        {
            foreach (var resourcePathContextBuilder in this.ResourcePathContextBuilders)
            {
                resourcePathContextBuilder.AddPath<TPath, TResourceId>(clrResourceId, rel, includePath);
            }

            return this;
        }

        public IResourcePathContextBuilder<TParentBuilder> AddPath(string pathSegment, bool includePath)
        {
            foreach (var resourcePathContextBuilder in this.ResourcePathContextBuilders)
            {
                resourcePathContextBuilder.AddPath(pathSegment, includePath);
            }

            return this;
        }

        public TParentBuilder PathsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ResourcePathContextCollectionBuilder(TParentBuilder parentBuilder, IEnumerable<ResourcePathContextBuilder<TParentBuilder>> resourcePathContextBuilders)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(resourcePathContextBuilders != null);

            this.ParentBuilder = parentBuilder;
            this.ResourcePathContextBuilders = resourcePathContextBuilders.SafeToReadOnlyList();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; }

        private IReadOnlyList<ResourcePathContextBuilder<TParentBuilder>> ResourcePathContextBuilders { get; }
        #endregion
    }
}
