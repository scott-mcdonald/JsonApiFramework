// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi2.Dom.Internal;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Represents the integration object needed by the JSON.NET serialization pipeline
    /// for the JSON serialization and deserialization of the DOM.
    /// </summary>
    public class DomContractResolver : DefaultContractResolver
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomContractResolver(DomJsonSerializerSettings domJsonSerializerSettings = null)
        {
            Contract.Requires(domJsonSerializerSettings != null);

            this.DomJsonSerializerSettings = domJsonSerializerSettings ?? DefaultDomJsonSerializerSettings;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected override JsonContract CreateContract(Type objectType)
        {
            Contract.Requires(objectType != null);

            var jsonContract = base.CreateContract(objectType);

            // Initialize contract for DOM object types.
            Action<JsonContract, DomJsonSerializerSettings> contractInitializer;
            if (TypeToJsonContractInitializerDictionary.TryGetValue(objectType, out contractInitializer))
            {
                contractInitializer(jsonContract, this.DomJsonSerializerSettings);
            }
            else
            {
                // Special case of the generic DomValue<TValue> type.
                if (objectType.IsImplementationOf(typeof(IDomValue)))
                    InitializeDomValueContract(jsonContract, this.DomJsonSerializerSettings);
            }

            return jsonContract;
        }
        #endregion

        // PRIVATE PROPERTIES /////////////////////////////////////////////
        #region Properties
        private DomJsonSerializerSettings DomJsonSerializerSettings { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private static void InitializeDomJsonApiVersionContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomJsonApiVersionConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomLinkContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomLinkConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomLinksContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomLinksConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomObjectContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomObjectConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomValueContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomValueConverter(domJsonSerializerSettings);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly DomJsonSerializerSettings DefaultDomJsonSerializerSettings =
            new DomJsonSerializerSettings
            {
                MetaNullValueHandling = NullValueHandling.Ignore
            };

        private static readonly IReadOnlyDictionary<Type, Action<JsonContract, DomJsonSerializerSettings>> TypeToJsonContractInitializerDictionary = new Dictionary<Type, Action<JsonContract, DomJsonSerializerSettings>>
            {
                { typeof(IDomJsonApiVersion), InitializeDomJsonApiVersionContract },
                { typeof(IDomLink), InitializeDomLinkContract },
                { typeof(IDomLinks), InitializeDomLinksContract },
                { typeof(IDomObject), InitializeDomObjectContract },
                { typeof(IDomValue), InitializeDomValueContract },

                { typeof(DomJsonApiVersion), InitializeDomJsonApiVersionContract },
                { typeof(DomLink), InitializeDomLinkContract },
                { typeof(DomLinks), InitializeDomLinksContract },
                { typeof(DomObject), InitializeDomObjectContract },
            };
        #endregion
    }
}