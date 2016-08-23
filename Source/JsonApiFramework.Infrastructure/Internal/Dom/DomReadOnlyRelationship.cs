// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only relationship node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyRelationship : Node<DomNodeType>, IDomRelationship
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Relationship; } }

        public override string Name
        { get { return "ReadOnlyRelationship ({0})".FormatWith(this.Rel); } }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomRelationship Implementation
        public string Rel
        { get; private set; }

        public Relationship Relationship
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyRelationship Create(string rel, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            var domReadOnlyRelationship = new DomReadOnlyRelationship(rel, relationship);
            return domReadOnlyRelationship;
        }

        public static DomReadOnlyRelationship Create(string rel, IGetRelationships getRelationships)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(getRelationships != null && getRelationships.Relationships != null);

            var relationship = getRelationships.GetRelationship(rel);

            var domReadOnlyRelationship = new DomReadOnlyRelationship(rel, relationship);
            return domReadOnlyRelationship;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyRelationship(string rel, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            this.Rel = rel;
            this.Relationship = relationship;
        }
        #endregion
    }
}
