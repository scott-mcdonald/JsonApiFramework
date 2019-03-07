// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Extension;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ComplexType : ClrTypeInfo, IComplexType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ComplexType(Type            clrComplexType,
                           IAttributesInfo attributesInfo)
            : base(clrComplexType, attributesInfo)
        {
            this.ExtensionDictionary = new ExtensionDictionary<IComplexType>(this);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public IEnumerable<IExtension<IComplexType>> Extensions => this.ExtensionDictionary.Extensions;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public void AddExtension(IExtension<IComplexType> extension)
        {
            Contract.Requires(extension != null);

            this.ExtensionDictionary.AddExtension(extension);
        }

        public void RemoveExtension(Type extensionType)
        {
            Contract.Requires(extensionType != null);

            this.ExtensionDictionary.RemoveExtension(extensionType);
        }

        public bool TryGetExtension(Type extensionType, out IExtension<IComplexType> extension)
        {
            Contract.Requires(extensionType != null);

            return this.ExtensionDictionary.TryGetExtension(extensionType, out extension);
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ComplexType()
        {
            this.ExtensionDictionary = new ExtensionDictionary<IComplexType>(this);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ExtensionDictionary<IComplexType> ExtensionDictionary { get; }
        #endregion
    }
}