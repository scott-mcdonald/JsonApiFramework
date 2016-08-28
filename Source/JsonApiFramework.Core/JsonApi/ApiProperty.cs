// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts an individual property of an <c>ApiObject</c> intended to
    /// be read/written from/to JSON API.
    /// </summary>
    public abstract class ApiProperty : JsonObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Name { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var name = this.Name.SafeToString();
            var value = this.ValueAsString();
            return String.Format("{0}: {1}", name, value);
        }
        #endregion

        #region Conversion Methods
        public abstract object ToClrObject(Type clrObjectType);
        public abstract TObject ToClrObject<TObject>();
        #endregion

        #region Factory Methods
        public static ApiProperty Create<TValue>(string name, TValue value)
        { return new ApiWriteProperty<TValue>(name, value); }

        public static ApiProperty Create(string name, Type valueType, object valueObject)
        { return new ApiWriteProperty(name, valueType, valueObject); }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiProperty(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region ApiProperty Overrides
        protected abstract string ValueAsString();
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiProperty Overrides
        internal abstract object ValueAsObject();
        internal abstract void Write(JsonWriter writer, JsonSerializer serializer);
        #endregion
    }
}