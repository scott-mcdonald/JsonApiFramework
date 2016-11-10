// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Expressions;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ClrObjectInfo<TObject> : JsonObject
        , IClrObjectInfo<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrObjectInfo()
        {
            this.InitializeNewClrObjectLambda();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IClrObjectInfo Implementation
        [JsonProperty] public Type ClrObjectType => typeof(TObject);
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IClrObjectInfo<TObject> Implementation
        public TObject CreateClrObject()
        {
            var clrObject = this.NewClrObjectLambda();
            return clrObject;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<TObject> NewClrObjectLambda { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private void InitializeNewClrObjectLambda()
        {
            var newClrObjectLambdaExpression = ExpressionBuilder.New<TObject>();
            var newClrObjectLambda = newClrObjectLambdaExpression.Compile();
            this.NewClrObjectLambda = newClrObjectLambda;
        }
        #endregion
    }
}