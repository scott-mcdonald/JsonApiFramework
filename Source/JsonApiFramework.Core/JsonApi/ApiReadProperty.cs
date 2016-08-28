// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable property intended to be read from JSON where the
    /// property value type is not known at compile time and will remain a JToken.
    /// </summary>
    /// <remarks>
    /// The property value will be kept as a JToken until later deserialization
    /// when the target deserializaiton type is known.
    /// </remarks>
    public class ApiReadProperty : ApiProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiReadProperty(string name, JToken value)
            : base(name)
        {
            this.Value = value;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public JToken Value { get; private set; }
        #endregion

        #region Conversion Methods
        public override object ToClrObject(Type clrObjectType)
        {
            var sourceJToken = this.Value;
            if (sourceJToken == null)
                return default(object);

            var clrObject = sourceJToken.ToObject(clrObjectType);
            return clrObject;
        }

        public override TObject ToClrObject<TObject>()
        {
            var sourceJToken = this.Value;
            if (sourceJToken == null)
                return default(TObject);

            var clrObject = sourceJToken.ToObject<TObject>();
            return clrObject;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region ApiProperty Overrides
        protected override string ValueAsString()
        { return this.Value.SafeToString(); }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiProperty Overrides
        internal override object ValueAsObject()
        { return this.Value; }

        internal override void Write(JsonWriter writer, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var name = this.Name;
            var value = this.Value;

            writer.WritePropertyName(name);
            value.WriteTo(writer);
        }
        #endregion
    }
}