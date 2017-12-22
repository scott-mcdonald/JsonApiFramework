// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ClrPropertyBinding : IClrPropertyBinding
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertyBinding(string clrPropertyName, Type clrPropertyType, IClrPropertyGetter clrPropertyGetter, IClrPropertySetter clrPropertySetter)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(clrPropertyGetter != null);
            Contract.Requires(clrPropertySetter != null);

            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
            this.ClrPropertyGetter = clrPropertyGetter;
            this.ClrPropertySetter = clrPropertySetter;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IClrPropertyBinding Implementation
        public string ClrPropertyName { get; }
        public Type ClrPropertyType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IClrPropertyBinding Implementation
        public TValue GetClrProperty<TObject, TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            if (this.ClrPropertyGetter == null)
                return default(TValue);

            var clrPropertyGetter = (IClrPropertyGetter<TObject>)this.ClrPropertyGetter;
            var clrValue = clrPropertyGetter.GetClrProperty<TValue>(clrObject, typeConverter, typeConverterContext);
            return clrValue;
        }

        public void SetClrProperty<TObject, TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            if (this.ClrPropertySetter == null)
                return;

            var clrPropertySetter = (IClrPropertySetter<TObject>)this.ClrPropertySetter;
            clrPropertySetter.SetClrProperty(clrObject, clrValue, typeConverter, typeConverterContext);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IClrPropertyGetter ClrPropertyGetter { get; }
        private IClrPropertySetter ClrPropertySetter { get; }
        #endregion
    }
}