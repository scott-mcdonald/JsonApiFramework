// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomParseContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomParseContext(IReadOnlyDictionary<PropertyType, JToken> jsonApiJTokens,
                               IReadOnlyDictionary<string, JToken> unknownJTokens)
        {
            this.JsonApiJTokens = jsonApiJTokens;
            this.UnknownJTokens = unknownJTokens;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public IReadOnlyDictionary<PropertyType, JToken> JsonApiJTokens { get; }
        public IReadOnlyDictionary<string, JToken> UnknownJTokens { get; }
        #endregion
    }
}