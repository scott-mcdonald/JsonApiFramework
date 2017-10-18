// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Properties;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Internal
{
    internal class ReadMeta : Meta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ReadMeta(JObject jObject)
        {
            this.JObject = jObject;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var str = SafeToString(this.JObject);
            return str;
        }
        #endregion

        #region Meta Overrides
        public override T GetObject<T>()
        {
            if (this.JObject == null)
                return default(T);

            var clrObject = this.JObject.ToObject<T>();
            return clrObject;
        }
        #endregion

        // PROTECTED/INTERNAL METHODS ///////////////////////////////////////
        #region Meta Overrides
        protected internal override void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (this.JObject == null)
            {
                writer.WriteToken(JsonToken.Null);
                return;
            }

            this.JObject.WriteTo(writer);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private JObject JObject { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string SafeToString(JObject jObject)
        {
            return jObject?.ToString() ?? CoreStrings.NullText;
        }
        #endregion
    }
}