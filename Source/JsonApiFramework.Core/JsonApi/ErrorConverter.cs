// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>Error</c> objects.
    /// </summary>
    public class ErrorConverter : Converter<Error>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Error ReadTypedObject(JObject errorJObject, JsonSerializer serializer)
        {
            Contract.Requires(errorJObject != null);
            Contract.Requires(serializer != null);

            var error = new Error();

            ReadId(errorJObject, serializer, error);
            ReadStatus(errorJObject, serializer, error);
            ReadCode(errorJObject, serializer, error);
            ReadTitle(errorJObject, serializer, error);
            ReadDetail(errorJObject, serializer, error);
            ReadSource(errorJObject, serializer, error);
            ReadLinks(errorJObject, serializer, error);
            ReadMeta(errorJObject, serializer, error);

            return error;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            writer.WriteStartObject();

            WriteId(writer, serializer, error);
            WriteStatus(writer, serializer, error);
            WriteCode(writer, serializer, error);
            WriteTitle(writer, serializer, error);
            WriteDetail(writer, serializer, error);
            WriteSource(writer, serializer, error);
            WriteLinks(writer, serializer, error);
            WriteMeta(writer, serializer, error);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        // ReSharper disable once UnusedParameter.Local
        private static void ReadId(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var id = ReadString(errorJToken, Keywords.Id);
            error.Id = id;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ReadStatus(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var status = ReadString(errorJToken, Keywords.Status);
            error.Status = status;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ReadCode(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var code = ReadString(errorJToken, Keywords.Code);
            error.Code = code;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ReadTitle(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var title = ReadString(errorJToken, Keywords.Title);
            error.Title = title;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ReadDetail(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var detail = ReadString(errorJToken, Keywords.Detail);
            error.Detail = detail;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ReadSource(JToken errorJToken, JsonSerializer serializer, Error error)
        {
            Contract.Requires(errorJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            var sourceJToken = errorJToken.SelectToken(Keywords.Source);
            if (sourceJToken == null)
                return;

            var sourceJObject = (JObject)sourceJToken;
            error.Source = sourceJObject;
        }
        #endregion

        #region Write Methods
        // ReSharper disable once UnusedParameter.Local
        private static void WriteId(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            WriteString(writer, serializer, Keywords.Id, error.Id);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteStatus(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            WriteString(writer, serializer, Keywords.Status, error.Status);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteCode(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            WriteString(writer, serializer, Keywords.Code, error.Code);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteTitle(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            WriteString(writer, serializer, Keywords.Title, error.Title);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteDetail(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            WriteString(writer, serializer, Keywords.Detail, error.Detail);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void WriteSource(JsonWriter writer, JsonSerializer serializer, Error error)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(error != null);

            if (error.Source == null)
                return;

            writer.WritePropertyName(Keywords.Source);
            error.Source.WriteTo(writer);
        }
        #endregion
    }
}