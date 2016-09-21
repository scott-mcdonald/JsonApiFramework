// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Implementation of <c>ITypeConverterDefinition</c> that accepts a
    /// function object as the actual convert implementation.
    /// </summary>
    public class TypeConverterDefinitionFunc<TSource, TTarget> : ITypeConverterDefinition<TSource, TTarget>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverterDefinitionFunc(Func<TSource, TypeConverterContext, TTarget> converter)
        {
            Contract.Requires(converter != null);

            this.Converter = converter;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ITypeConverterDefinition Implementation
        public Type SourceType { get { return typeof(TSource); } }
        public Type TargetType { get { return typeof(TTarget); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ITypeConverterDefinition<TSource, TTarget> Implementation
        public TTarget Convert(TSource source, TypeConverterContext context)
        { return this.Converter(source, context); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<TSource, TypeConverterContext, TTarget> Converter { get; set; }
        #endregion
    }
}
