// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;
using JsonApiFramework.Properties;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class WriteMeta<TObject> : Meta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public WriteMeta(TObject clrObject)
        {
            this.ClrUnderlyingObject = clrObject;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public Type ClrUnderlyingObjectType => typeof(TObject);
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var str = SafeToString(this.ClrUnderlyingObject);
            return str;
        }
        #endregion

        #region Meta Overrides
        public override T GetObject<T>()
        {
            var typeConverter = TypeConverter.Default;
            var clrValue = typeConverter.Convert<TObject, T>(this.ClrUnderlyingObject);
            return clrValue;
        }
        #endregion

        // PROTECTED/INTERNAL METHODS ///////////////////////////////////////
        #region Meta Overrides
        protected internal override void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (IsValueType && !IsNullableType)
            {
                WriteJson(writer, serializer, this.ClrUnderlyingObject);
                return;
            }

            var clrObjectAsObject = (object)this.ClrUnderlyingObject;
            if (clrObjectAsObject == null)
            {
                writer.WriteToken(JsonToken.Null);
                return;
            }

            WriteJson(writer, serializer, this.ClrUnderlyingObject);
        }
        #endregion

        // STATIC CONSTRUCTORS //////////////////////////////////////////////
        #region Static Constructors
        static WriteMeta()
        {
            var objectType = typeof(TObject);
            IsNullableType = TypeReflection.IsNullableType(objectType);
            IsValueType = TypeReflection.IsValueType(objectType);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TObject ClrUnderlyingObject { get; }

        // ReSharper disable StaticMemberInGenericType
        private static bool IsNullableType { get; }
        private static bool IsValueType { get; }
        // ReSharper restore StaticMemberInGenericType
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string SafeToString(TObject clrObject)
        {
            if (IsValueType && !IsNullableType)
            {
                return clrObject.ToString();
            }

            var clrObjectAsObject = (object)clrObject;
            return clrObjectAsObject?.ToString() ?? CoreStrings.NullText;
        }

        private static void WriteJson(JsonWriter writer, JsonSerializer serializer, TObject clrObject)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var jToken = JToken.FromObject(clrObject, serializer);
            var jObject = (JObject)jToken;
            jObject.WriteTo(writer);
        }
        #endregion
    }
}