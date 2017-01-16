// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Tree;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomArray : DomNode
        , IDomArray
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomArray(params DomItem[] domItems)
            : this(domItems.AsEnumerable())
        { }

        public DomArray(IEnumerable<DomItem> domItems)
            : base(DomNodeType.Array, "array", domItems)
        {
            var count = this.DomNodes()
                            .Count();
            this.Count = count;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomArray Implementation
        public int Count
        {
            get { return this.GetAttributeValue<int>(CountAttributeName); }
            private set { this.SetAttributeValue(CountAttributeName, value); }
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomArray Implementation
        public IEnumerable<IDomItem> DomItems()
        {
            return this.Nodes()
                       .Cast<IDomItem>();
        }
        #endregion

        #region DomNode Overrides
        public override void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonWriter.WriteStartArray();

            var domItems = this.DomItems();
            WriteDomItems(jsonWriter, jsonSerializer, domJsonSerializerSettings, domItems);

            jsonWriter.WriteEndArray();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Write Methods
        private static void WriteDomItems(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings, IEnumerable<IDomItem> domItems)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            if (domItems == null)
                return;

            foreach (var domItem in domItems)
            {
                WriteDomItem(jsonWriter, jsonSerializer, domJsonSerializerSettings, domItem);
            }
        }

        private static void WriteDomItem(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings, IDomItem domItem)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);
            Contract.Requires(domItem != null);

            var domItemValue = domItem.DomItemValue();
            if (domItemValue == null)
            {
                jsonWriter.WriteToken(JsonToken.Null);
                return;
            }

            var domWriteable = (IDomWriteable)domItemValue;
            domWriteable.WriteJson(jsonWriter, jsonSerializer, domJsonSerializerSettings);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CountAttributeName = "count";
        #endregion
    }
}
