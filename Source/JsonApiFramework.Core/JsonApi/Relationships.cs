// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Collections;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api relationships object.</summary>
    [JsonConverter(typeof(RelationshipsConverter))]
    public class Relationships : JsonObject
        , IEnumerable<KeyValuePair<string, Relationship>>
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
            var content = String.Join(ToStringDelimiter, this.OrderedReadOnlyRelationshipDictionary.Select(x => $"{x.Key}={x.Value.ToString()}"));
            return "{0} [{1}]".FormatWith(TypeName, content);
        }
        #endregion

        #region IEnumerable<KeyValuePair<string, Link>> Implementation
        public IEnumerator<KeyValuePair<string, Relationship>> GetEnumerator()
        { return this.OrderedReadOnlyRelationshipDictionary.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
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

            if (this.TryGetRelationship(rel, out var relationship))
                return relationship;

            throw RelationshipsException.CreateNotFoundException(rel);
        }

        public IEnumerable<Relationship> GetRelationships()
        { return this.OrderedReadOnlyRelationshipDictionary.Values; }

        public IEnumerable<string> GetRels()
        { return this.OrderedReadOnlyRelationshipDictionary.Keys; }

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
        private const string ToStringDelimiter = " ";
        private static readonly string TypeName = typeof(Relationships).Name;
        #endregion
    }
}