// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.Json;

namespace JsonApiFramework.Metadata.Internal
{
    internal class TypeBase : JsonObject, ITypeBase
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeBase(Type clrType, IAttributesInfo attributesInfo)
        {
            Contract.Requires(clrType != null);
            Contract.Requires(attributesInfo != null);

            this.ClrType = clrType;
            this.AttributesInfo = attributesInfo;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ITypeBase Implementation
        public Type ClrType { get; }
        public IAttributesInfo AttributesInfo { get; }
        #endregion
    }

    internal class TypeBase<TObject> : TypeBase, ITypeBase<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeBase(IAttributesInfo attributesInfo)
            : base(typeof(TObject), attributesInfo)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ITypeBase<T> Implementation
        public TObject CreateClrObject()
        {
            var createdObject = FactoryLambda();
            return createdObject;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        static TypeBase()
        {
            FactoryLambda = CreateFactoryLambda();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static Func<TObject> FactoryLambda { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<TObject> CreateFactoryLambda()
        {
            var factoryLambdaExpression = ExpressionBuilder.New<TObject>();
            var factoryLambda = factoryLambdaExpression.Compile();
            return factoryLambda;
        }
        #endregion
    }
}