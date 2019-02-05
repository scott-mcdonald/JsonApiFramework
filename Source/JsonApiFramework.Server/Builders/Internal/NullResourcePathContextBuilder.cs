// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class NullResourcePathContextBuilder<TParentBuilder> : IResourcePathContextBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourcePathContextBuilder<TParentBuilder> Implementation
        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath>(TPath clrResource, string rel, bool includePath = true)
            where TPath : class
        { return this; }

        public IResourcePathContextBuilder<TParentBuilder> AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath = true)
            where TPath : class
        { return this; }

        public IResourcePathContextBuilder<TParentBuilder> AddPath(string pathSegment, bool includePath = true)
        { return this; }

        public TParentBuilder PathsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal NullResourcePathContextBuilder(TParentBuilder parentBuilder)
        {
            Contract.Requires(parentBuilder != null);

            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        #endregion
    }
}
