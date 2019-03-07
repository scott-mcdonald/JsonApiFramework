// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Server
{
    /// <summary>Abstracts framework-level hypermedia path building in a builder progressive fluent interface style.</summary>
    public interface IPathContextBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds path(s) by custom action.</summary>
        IPathContextBuilder AddPath(Action<IPathContextBuilder, object> action);

        /// <summary>Adds a path based on a resource and the relationships to the next resource in the path.</summary>
        IPathContextBuilder AddPath<TPath>(TPath clrResource, string rel, bool includePath = true)
            where TPath : class;

        /// <summary>Adds a path based on a resource identifier and the relationship to the next resource in the path.</summary>
        IPathContextBuilder AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath = true)
            where TPath : class;

        /// <summary>Adds a non-resource based path segment.</summary>
        IPathContextBuilder AddPath(string pathSegment, bool includePath = true);
        #endregion
    }

    /// <summary>Abstracts framework-level "resource-path-context" attribute creation with a builder progressive fluent interface style.</summary>
    /// <typeparam name="TParentBuilder">
    /// Type of parent builder interface to return when done building the
    /// "resource-path-context" attribute.
    /// </typeparam>
    public interface IResourcePathContextBuilder<out TParentBuilder> : IPathContextBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds path(s) by custom action.</summary>
        new IResourcePathContextBuilder<TParentBuilder> AddPath(Action<IPathContextBuilder, object> action);

        /// <summary>Adds a path based on a resource and the relationships to the next resource in the path.</summary>
        new IResourcePathContextBuilder<TParentBuilder> AddPath<TPath>(TPath clrResource, string rel, bool includePath = true)
            where TPath : class;

        /// <summary>Adds a path based on a resource identifier and the relationship to the next resource in the path.</summary>
        new IResourcePathContextBuilder<TParentBuilder> AddPath<TPath, TResourceId>(TResourceId clrResourceId, string rel, bool includePath = true)
            where TPath : class;

        /// <summary>Adds a non-resource based path segment.</summary>
        new IResourcePathContextBuilder<TParentBuilder> AddPath(string pathSegment, bool includePath = true);

        /// <summary>Ends the building of the "resource-path-context" attribute.</summary>
        TParentBuilder PathsEnd();
        #endregion
    }
}