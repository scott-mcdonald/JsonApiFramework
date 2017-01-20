// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Collections;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api relationships object.</summary>
    public class Relationships
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Relationships()
            : this(null)
        { }

        public Relationships(IReadOnlyDictionary<string, Relationship> dictionary)
        {
            this.OrderedReadOnlyRelationshipDictionary = new OrderedReadOnlyDictionary<string, Relationship>(dictionary ?? new Dictionary<string, Relationship>(), StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            if (!this.OrderedReadOnlyRelationshipDictionary.Any())
                return "{0} []".FormatWith(TypeName);

            var content = this.OrderedReadOnlyRelationshipDictionary
                              .Select(x => $"{x.Key}={x.Value.ToString()}")
                              .Aggregate((current, next) => $"{current} {next}");

            return "{0} [{1}]".FormatWith(TypeName, content);
        }
        #endregion

        #region Contains Methods
        public bool ContainsRelationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.OrderedReadOnlyRelationshipDictionary.ContainsKey(rel);
        }
        #endregion

        #region Get Methods
        public Relationship GetRelationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            Relationship relationship;
            if (this.TryGetRelationship(rel, out relationship))
                return relationship;

            throw new RelationshipNotFoundException(rel);
        }

        public bool TryGetRelationship(string rel, out Relationship relationship)
        {
            relationship = null;
            return !String.IsNullOrWhiteSpace(rel) && this.OrderedReadOnlyRelationshipDictionary.TryGetValue(rel, out relationship);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly Relationships Empty = new Relationships();
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private OrderedReadOnlyDictionary<string, Relationship> OrderedReadOnlyRelationshipDictionary { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Relationships).Name;
        #endregion
    }
}