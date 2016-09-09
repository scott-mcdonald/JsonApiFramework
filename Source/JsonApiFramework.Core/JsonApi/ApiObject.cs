// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable object composed of <c>ApiProperty</c> objects
    /// intended to be read/written from/to JSON API.
    /// </summary>
    /// <remarks>
    /// Intended to be used as a property for the following json:api compliant
    /// objects:
    /// - Attributes object of a Resource object
    /// - Meta object
    /// - Source object of an Error object
    /// 
    /// Not intended to be inherited from and used as-is.
    /// </remarks>
    [JsonConverter(typeof(ApiObjectConverter))]
    public sealed class ApiObject : JsonObject
        , IEnumerable<ApiProperty>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiObject(IEnumerable<ApiProperty> apiPropertyCollection)
        { _apiPropertyCollection = apiPropertyCollection.SafeToList(); }

        public ApiObject(params ApiProperty[] apiPropertyCollection)
        { _apiPropertyCollection = apiPropertyCollection.SafeToList(); }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var apiObjectString = _apiPropertyCollection
                .Select(x => x.ToString())
                .Aggregate((current, next) =>
                    {
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append(current);
                        stringBuilder.AppendLine();
                        stringBuilder.Append(next);
                        return stringBuilder.ToString();
                    });
            return apiObjectString;
        }
        #endregion

        #region IEnumerable<ApiProperty> Implementation
        public IEnumerator<ApiProperty> GetEnumerator()
        { return _apiPropertyCollection.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return _apiPropertyCollection.GetEnumerator(); }
        #endregion

        #region Methods
        public bool TryGetApiProperty(string name, out ApiProperty apiProperty)
        {
            apiProperty = _apiPropertyCollection.SingleOrDefault(x => name == x.Name);
            return apiProperty != null;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Properties
        private readonly IReadOnlyList<ApiProperty> _apiPropertyCollection;
        #endregion
    }
}