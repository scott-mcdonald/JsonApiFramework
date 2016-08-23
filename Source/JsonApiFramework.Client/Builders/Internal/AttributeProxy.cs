// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Client.Internal
{
    internal class AttributeProxy<TProperty> : IAttributeProxy<TProperty>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IAttribute<TProperty> Implementation
        public Type ClrPropertyType { get { return typeof(TProperty); } }
        public string ClrPropertyName { get; private set; }
        public TProperty ClrPropertyValue { get; private set; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributeProxy(string clrRropertyName, TProperty clrPropertyValue)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrRropertyName) == false);

            this.ClrPropertyName = clrRropertyName;
            this.ClrPropertyValue = clrPropertyValue;
        }
        #endregion
    }
}