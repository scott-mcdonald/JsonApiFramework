// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Converters;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ClrPropertyBinding : IClrPropertyBinding
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertyBinding(IPropertyGetter propertyGetter, IPropertySetter propertySetter)
        {
            this.PropertyGetter = propertyGetter;
            this.PropertySetter = propertySetter;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertyInfo Implementation
        public TValue GetClrProperty<TObject, TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            if (this.PropertyGetter == null)
                return default(TValue);

            var propertyGetter = (IPropertyGetter<TObject>)this.PropertyGetter;
            var clrValue = propertyGetter.GetClrProperty<TValue>(clrObject, typeConverter, typeConverterContext);
            return clrValue;
        }

        public void SetClrProperty<TObject, TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            if (this.PropertySetter == null)
                return;

            var propertySetter = (IPropertySetter<TObject>)this.PropertySetter;
            propertySetter.SetClrProperty(clrObject, clrValue, typeConverter, typeConverterContext);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        private IPropertyGetter PropertyGetter { get; }
        private IPropertySetter PropertySetter { get; }
        #endregion
    }
}