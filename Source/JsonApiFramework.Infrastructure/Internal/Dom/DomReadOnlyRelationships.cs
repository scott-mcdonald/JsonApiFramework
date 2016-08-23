// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read-only relationships node in the DOM tree.
    /// </summary>
    internal class DomReadOnlyRelationships : Node<DomNodeType>, IDomRelationships
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Relationships; } }

        public override string Name
        {
            get
            {
                if (this.Relationships == null || this.Relationships.Any() == false)
                {
                    return "ReadOnlyRelationships";
                }

                var rels = this.Relationships
                               .Select(x => x.Key)
                               .Aggregate((current, next) => "{0} {1}".FormatWith(current, next));

                return "ReadOnlyRelationships [{0}]".FormatWith(rels);
            }
        }
        #endregion

        #region IGetReadOnly Implementation
        public bool IsReadOnly
        { get { return true; } }
        #endregion

        #region IDomRelationships Implementation
        public Relationships Relationships
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadOnlyRelationships Create(Relationships relationships)
        {
            Contract.Requires(relationships != null);

            var domReadOnlyRelationships = new DomReadOnlyRelationships(relationships);
            return domReadOnlyRelationships;
        }

        public static DomReadOnlyRelationships Create(IGetRelationships getRelationships)
        {
            Contract.Requires(getRelationships != null && getRelationships.Relationships != null);

            var relationships = getRelationships.Relationships;

            var domReadOnlyRelationships = new DomReadOnlyRelationships(relationships);
            return domReadOnlyRelationships;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomReadOnlyRelationships(Relationships relationships)
        {
            Contract.Requires(relationships != null);

            this.Relationships = relationships;
        }
        #endregion
    }
}
