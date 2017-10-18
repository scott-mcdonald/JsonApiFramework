// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class RelationshipConverter : Converter<Relationship>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override Relationship DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrRelationship = ReadRelationshipObject(jObject, serializer);
            return clrRelationship;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, Relationship clrRelationship)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrRelationship != null);

            WriteRelationshipObject(writer, serializer, clrRelationship);
        }
        #endregion
    }
}