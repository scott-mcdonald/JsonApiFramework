// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Abstracts framework-level json:api compliant "links" creation with a
    /// builder progressive fluent interface style.
    /// </summary>
    /// <typeparam name="TBuilder">Type of current builder interface to
    /// return when building the json:api "links" object.</typeparam>
    /// <typeparam name="TParentBuilder">Type of parent builder interface to
    /// return when done building the json:api "links" object.</typeparam>
    public interface ILinksBuilder<out TBuilder, out TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds to the "links" member a user constructed "link" object.
        /// </summary>
        /// <param name="rel">Relation name of the link being added.</param>
        /// <param name="link">Complete user constructed link object.</param>
        TBuilder AddLink(string rel, Link link);

        /// <summary>
        /// Adds to the "links" members user constructed "link" objects.
        /// </summary>
        /// <param name="rel">Relation name of the links being added.</param>
        /// <param name="linkCollection">Collection of complete user constructed link objects.</param>
        TBuilder AddLink(string rel, IEnumerable<Link> linkCollection);

        /// <summary>
        /// Adds to the "links" member framework constructed "link" object(s).
        /// </summary>
        /// <param name="rel">Relation name of the link(s) being added.</param>
        TBuilder AddLink(string rel);

        /// <summary>
        /// Starts the building of framework constructed "link" object(s) that will be added to the "links" member.
        /// </summary>
        /// <param name="rel">Relation name of the link(s) being added.</param>
        ILinkBuilder<TBuilder> Link(string rel);

        /// <summary>Ends the building of the "links" member.</summary>
        TParentBuilder LinksEnd();
        #endregion
    }
}