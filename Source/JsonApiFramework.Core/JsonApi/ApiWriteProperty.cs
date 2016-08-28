// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable property intended to be written to JSON where
    /// the property value type is known at compile time.
    /// </summary>
    /// <typeparam name="TValue">Type of property value to write to JSON</typeparam>
    public class ApiWriteProperty<TValue> : ApiProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiWriteProperty(string name, TValue value)
            : base(name)
        {
            this.Value = value;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public TValue Value { get; private set; }
        #endregion

        #region Conversion Methods
        public override object ToClrObject(Type clrObjectType)
        {
            var sourceValue = this.Value;
            var clrObject = TypeConverter.Convert(sourceValue, clrObjectType);
            return clrObject;
        }

        public override TObject ToClrObject<TObject>()
        {
            var sourceValue = this.Value;
            var clrObject = TypeConverter.Convert<TObject>(sourceValue);
            return clrObject;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region ApiProperty Overrides
        protected override string ValueAsString()
        { return IsValueType ? this.Value.ToString() : this.Value.SafeToString(); }
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

            if (IsValueType && !IsNullableType)
            {
                WriteValue(writer, serializer, name, value);
                return;
            }

            var valueAsObject = (object)value;
            if (valueAsObject == null)
            {
                switch (serializer.NullValueHandling)
                {
                    case NullValueHandling.Include:
                        WriteValueNull(writer, name);
                        return;

                    case NullValueHandling.Ignore:
                        // Ignore a null attribute.
                        return;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            WriteValue(writer, serializer, name, value);
        }
        #endregion

        // STATIC CONSTRUCTORS //////////////////////////////////////////////
        #region Static Constructors
        static ApiWriteProperty()
        {
            IsNullableType = typeof(TValue).IsNullableType();
            IsValueType = typeof(TValue).IsValueType();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region JSON Properties
        private static bool IsNullableType { get; set; }
        private static bool IsValueType { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void WriteValueNull(JsonWriter writer, string name)
        {
            writer.WritePropertyName(name);

            writer.WriteToken(JsonToken.Null);
        }

        private static void WriteValue(JsonWriter writer, JsonSerializer serializer, string name, TValue value)
        {
            writer.WritePropertyName(name);

            var valueAsJToken = JToken.FromObject(value, serializer);
            valueAsJToken.WriteTo(writer);
        }
        #endregion
    }

    /// <summary>
    /// Represents an immutable property intended to be written to JSON where
    /// the property value type is not known at compile time.
    /// </summary>
    public class ApiWriteProperty : ApiProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiWriteProperty(string name, Type valueType, object valueObject)
            : base(name)
        {
            Contract.Requires(valueType != null);

            this.ValueType = valueType;
            this.ValueObject = valueObject;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public Type ValueType { get; private set; }
        public object ValueObject { get; private set; }
        #endregion

        #region Conversion Methods
        public override object ToClrObject(Type clrObjectType)
        {
            var sourceValue = this.ValueObject;
            var clrObject = TypeConverter.Convert(sourceValue, clrObjectType);
            return clrObject;
        }

        public override TObject ToClrObject<TObject>()
        {
            var sourceValue = this.ValueObject;
            var clrObject = TypeConverter.Convert<TObject>(sourceValue);
            return clrObject;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region ApiProperty Overrides
        protected override string ValueAsString()
        { return this.ValueObject.SafeToString(); }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiProperty Overrides
        internal override object ValueAsObject()
        { return this.ValueObject; }

        internal override void Write(JsonWriter writer, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var name = this.Name;
            var valueObject = this.ValueObject;
            if (valueObject == null)
            {
                switch (serializer.NullValueHandling)
                {
                    case NullValueHandling.Include:
                        WriteValueNull(writer, name);
                        return;

                    case NullValueHandling.Ignore:
                        // Ignore a null attribute.
                        return;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            WriteValue(writer, serializer, name, valueObject);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void WriteValueNull(JsonWriter writer, string name)
        {
            writer.WritePropertyName(name);

            writer.WriteToken(JsonToken.Null);
        }

        private static void WriteValue(JsonWriter writer, JsonSerializer serializer, string name, object value)
        {
            writer.WritePropertyName(name);

            var valueAsJToken = JToken.FromObject(value, serializer);
            valueAsJToken.WriteTo(writer);
        }
        #endregion
    }
}