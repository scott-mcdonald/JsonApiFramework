// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;
using JsonApiFramework.Properties;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomValue<TValue> : DomNode
        , IDomValue
        , IDomWriteable
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomValue(TValue clrValue)
            : base(DomNodeType.Value, SafeToString(clrValue))
        {
            this.ClrUnderlyingValue = clrValue;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomValue Implementation
        public Type ClrUnderlyingValueType => typeof(TValue);
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomValue Implementation
        public TTarget ClrValue<TTarget>(ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            typeConverter = typeConverter ?? DefaultTypeConverter;
            var clrValue = typeConverter.Convert<TValue, TTarget>(this.ClrUnderlyingValue, typeConverterContext);
            return clrValue;
        }
        #endregion

        #region DomNode Overrides
        public override void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            var clrValue = this.ClrUnderlyingValue;

            if (IsValueType && !IsNullableType)
            {
                this.WriteValue(jsonWriter, jsonSerializer);
                return;
            }

            var clrValueAsObject = (object)clrValue;
            if (clrValueAsObject != null)
            {
                this.WriteValue(jsonWriter, jsonSerializer);
                return;
            }

            WriteValueNull(jsonWriter);
        }
        #endregion

        // STATIC CONSTRUCTORS //////////////////////////////////////////////
        #region Static Constructors
        static DomValue()
        {
            var valueType = typeof(TValue);
            IsNullableType = TypeReflection.IsNullableType(valueType);
            IsValueType = TypeReflection.IsValueType(valueType);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TValue ClrUnderlyingValue { get; }

        // ReSharper disable StaticMemberInGenericType
        private static bool IsNullableType { get; }
        private static bool IsValueType { get; }
        // ReSharper restore StaticMemberInGenericType
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string SafeToString(TValue clrValue)
        {
            if (IsValueType && !IsNullableType)
            {
                return clrValue.ToString();
            }

            var clrValueAsObject = (object)clrValue;
            return clrValueAsObject != null ? clrValueAsObject.ToString() : CoreStrings.NullText;
        }

        private void WriteValue(JsonWriter jsonWriter, JsonSerializer jsonSerializer)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);

            var valueAsJToken = JToken.FromObject(this.ClrUnderlyingValue, jsonSerializer);
            valueAsJToken.WriteTo(jsonWriter);
        }

        private static void WriteValueNull(JsonWriter jsonWriter)
        {
            Contract.Requires(jsonWriter != null);

            jsonWriter.WriteToken(JsonToken.Null);
        }
        #endregion
    }
}
