// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Represents the compiled factory method of the <c>ObjectFactory</c>
    /// for a specific concrete type via a specific constructor signature
    /// based on the number and type of arguments.
    /// </summary>
    /// <typeparam name="T">Type of object to create</typeparam>
    /// <param name="arguments">Arguments to pass to the constructor of the
    /// object to create.</param>
    /// <returns>Created object by calling a specific constructor signature.</returns>
    public delegate T FactoryMethod<out T>(params object[] arguments);

    /// <summary>
    /// Represents an object factory to efficiently create objects with
    /// different constructor signatures using compiled lambda expressions.
    /// </summary>
    public static class ObjectFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static FactoryMethod<T> Create<T>(ConstructorInfo constructorInfo)
        {
            Contract.Requires(constructorInfo != null);

            var constructorParameters = constructorInfo.GetParameters();

            // Create a single parameter of type object[]
            var parameterExpression = Expression.Parameter(typeof(object[]), "arguments");
            var argumentsExpression = new Expression[constructorParameters.Length];

            // Pick each argument from the parameters array and create a typed
            // expression of them.
            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var index = Expression.Constant(i);
                var parameterType = constructorParameters[i].ParameterType;

                var parameterAccessorExpression = Expression.ArrayIndex(parameterExpression, index);

                var parameterCastExpression = Expression.Convert(parameterAccessorExpression, parameterType);

                argumentsExpression[i] = parameterCastExpression;
            }

            // Make a NewExpression that calls the constructor with the
            // arguments we just created
            var newExpression = Expression.New(constructorInfo, argumentsExpression);

            // Create a lambda with the New Expression as body and our parameter
            // object[] as argument.
            var lambda = Expression.Lambda(typeof(FactoryMethod<T>), newExpression, parameterExpression);

            // Compile it
            var compiled = (FactoryMethod<T>)lambda.Compile();
            return compiled;
        }
        #endregion
    }
}
