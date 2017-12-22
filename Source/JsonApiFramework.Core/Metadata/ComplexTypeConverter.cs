// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.Metadata.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents a JSON.NET converter for the complex type.</summary>
    public class ComplexTypeConverter : JsonConverter<IComplexType>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected override IComplexType DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var complexType = jObject.ReadComplexTypeObject(serializer);
            return complexType;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, IComplexType serviceModel)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(serviceModel != null);

            writer.WriteComplexTypeObject(serializer, serviceModel);
        }
        #endregion
    }
}