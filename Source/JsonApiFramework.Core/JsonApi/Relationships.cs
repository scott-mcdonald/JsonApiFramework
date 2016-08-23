// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    [JsonDictionary]
    public class Relationships : JsonDictionary<Relationship>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Relationships()
        { }

        public Relationships(IDictionary<string, Relationship> dictionary)
            : base(dictionary)
        { }

        public Relationships(int capacity)
            : base(capacity)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            if (!this.Any())
                return String.Format("{0} []", TypeName);

            var content = this.Select(x => String.Format("{0}={1}", x.Key, x.Value.ToString()))
                              .Aggregate((current, next) => String.Format("{0} {1}", current, next));

            return String.Format("{0} [{1}]", TypeName, content);
        }
        #endregion

        #region Contains Methods
        public bool ContainsRelationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.ContainsKey(rel);
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
            return !String.IsNullOrWhiteSpace(rel) && this.TryGetValue(rel, out relationship);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Relationships).Name;
        #endregion
    }
}