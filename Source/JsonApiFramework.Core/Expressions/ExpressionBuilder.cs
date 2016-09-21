// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using JsonApiFramework.Reflection;

namespace JsonApiFramework.Expressions
{
    /// <summary>
    /// Utility class to build useful expressions that can be compiled into
    /// delegates for execution at runtime.
    /// </summary>
    /// <remarks>
    /// The compiled delegates only need to be compiled once and thus can be
    /// cached for future use, this technique is much faster then reflection
    /// at runtime.
    /// </remarks>
    public static class ExpressionBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Call Methods
        public static LambdaExpression Call(Type type, MethodInfo methodInfo, IEnumerable<Type> genericTypeArguments = null)
        {
            Contract.Requires(type != null);
            Contract.Requires(methodInfo != null);

            var containsGenericParameters = methodInfo.ContainsGenericParameters;
            if (containsGenericParameters)
            {
                CreateClosedGenericTypeAndMethodInfo(genericTypeArguments, ref type, ref methodInfo);
            }

            var arguments = methodInfo.GetParameters();

            var isMethodStatic = methodInfo.IsStatic;
            var isMethodVoid = methodInfo.ReturnType.IsVoid();

            if (!isMethodStatic)
            {
                return !isMethodVoid
                    ? CreateInstanceMethodCallExpression(type, methodInfo, arguments)
                    : CreateInstanceVoidMethodCallExpression(type, methodInfo, arguments);
            }

            return !isMethodVoid
                ? CreateStaticMethodCallExpression(methodInfo, arguments)
                : CreateStaticVoidMethodCallExpression(methodInfo, arguments);
        }

        public static LambdaExpression Call(MethodInfo methodInfo, IEnumerable<Type> genericTypeArguments = null)
        {
            Contract.Requires(methodInfo != null);

            var declaryingType = methodInfo.DeclaringType;
            var lambdaExpression = Call(declaryingType, methodInfo, genericTypeArguments);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TResult>> MethodCall<TObject, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var methodInfo = objectType.GetMethod(methodName);
            var lambdaExpression = (Expression<Func<TObject, TResult>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TArgument, TResult>> MethodCall<TObject, TArgument, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var argumentType = typeof(TArgument);
            var methodInfo = objectType.GetMethod(methodName, argumentType);
            var lambdaExpression = (Expression<Func<TObject, TArgument, TResult>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TArgument1, TArgument2, TResult>> MethodCall<TObject, TArgument1, TArgument2, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);
            var methodInfo = objectType.GetMethod(methodName, argument1Type, argument2Type);
            var lambdaExpression = (Expression<Func<TObject, TArgument1, TArgument2, TResult>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TObject>> VoidMethodCall<TObject>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var methodInfo = objectType.GetMethod(methodName);
            var lambdaExpression = (Expression<Action<TObject>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TObject, TArgument>> VoidMethodCall<TObject, TArgument>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var argumentType = typeof(TArgument);
            var methodInfo = objectType.GetMethod(methodName, argumentType);
            var lambdaExpression = (Expression<Action<TObject, TArgument>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TObject, TArgument1, TArgument2>> VoidMethodCall<TObject, TArgument1, TArgument2>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var objectType = typeof(TObject);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);
            var methodInfo = objectType.GetMethod(methodName, argument1Type, argument2Type);
            var lambdaExpression = (Expression<Action<TObject, TArgument1, TArgument2>>)Call(objectType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TResult>> StaticMethodCall<TClass, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var methodInfo = classType.GetMethod(methodName);
            var lambdaExpression = (Expression<Func<TResult>>)Call(classType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument, TResult>> StaticMethodCall<TClass, TArgument, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var argumentType = typeof(TArgument);
            var methodInfo = classType.GetMethod(methodName, argumentType);
            var lambdaExpression = (Expression<Func<TArgument, TResult>>)Call(classType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TResult>> StaticMethodCall<TClass, TArgument1, TArgument2, TResult>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);
            var methodInfo = classType.GetMethod(methodName, argument1Type, argument2Type);
            var lambdaExpression = (Expression<Func<TArgument1, TArgument2, TResult>>)Call(classType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action> StaticVoidMethodCall<TClass>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var methodInfo = classType.GetMethod(methodName);
            var lambdaExpression = (Expression<Action>)Call(classType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TArgument>> StaticVoidMethodCall<TClass, TArgument>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var argumentType = typeof(TArgument);
            var methodInfo = classType.GetMethod(methodName, argumentType);
            var lambdaExpression = (Expression<Action<TArgument>>)Call(classType, methodInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TArgument1, TArgument2>> StaticVoidMethodCall<TClass, TArgument1, TArgument2>(string methodName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            var classType = typeof(TClass);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);
            var methodInfo = classType.GetMethod(methodName, argument1Type, argument2Type);
            var lambdaExpression = (Expression<Action<TArgument1, TArgument2>>)Call(classType, methodInfo);
            return lambdaExpression;
        }
        #endregion

        #region Cast Methods
        public static LambdaExpression Cast(Type fromType, Type toType)
        {
            Contract.Requires(fromType != null);
            Contract.Requires(toType != null);

            var parameterExpression = Expression.Parameter(fromType, "x");
            var parameterConvertExpression = Expression.Convert(parameterExpression, toType);
            var lambdaExpression = Expression.Lambda(parameterConvertExpression, parameterExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TFrom, TTo>> Cast<TFrom, TTo>()
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);
            var lambdaExpression = (Expression<Func<TFrom, TTo>>)Cast(fromType, toType);
            return lambdaExpression;
        }

        public static LambdaExpression CastAs(Type fromType, Type toType)
        {
            Contract.Requires(fromType != null);
            Contract.Requires(toType != null);

            var parameterExpression = Expression.Parameter(fromType, "x");
            var parameterTypeAsExpression = Expression.TypeAs(parameterExpression, toType);
            var lambdaExpression = Expression.Lambda(parameterTypeAsExpression, parameterExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TFrom, TTo>> CastAs<TFrom, TTo>()
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);
            var lambdaExpression = (Expression<Func<TFrom, TTo>>)CastAs(fromType, toType);
            return lambdaExpression;
        }
        #endregion

        #region Default Methods
        public static LambdaExpression Default(Type objectType)
        {
            Contract.Requires(objectType != null);

            var defaultExpression = Expression.Default(objectType);
            var convertExpression = Expression.Convert(defaultExpression, typeof(object));
            var lambdaExpression = Expression.Lambda(convertExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TObject>> Default<TObject>()
        {
            var objectType = typeof(TObject);
            var defaultExpression = Expression.Default(objectType);
            var lambdaExpression = (Expression<Func<TObject>>)Expression.Lambda(defaultExpression);
            return lambdaExpression;
        }
        #endregion

        #region New Methods
        public static LambdaExpression New(Type objectType)
        {
            Contract.Requires(objectType != null);

            var newExpression = Expression.New(objectType);
            var lambdaExpression = Expression.Lambda(newExpression);
            return lambdaExpression;
        }

        public static LambdaExpression New(Type argumentType, Type objectType)
        {
            Contract.Requires(argumentType != null);
            Contract.Requires(objectType != null);

            var argumentExpression = Expression.Parameter(argumentType, "arg");
            var constructorInfo = objectType.GetConstructor(argumentType);
            var newExpression = Expression.New(constructorInfo, argumentExpression);
            var lambdaExpression = Expression.Lambda(newExpression, argumentExpression);
            return lambdaExpression;
        }

        public static LambdaExpression New(Type argument1Type, Type argument2Type, Type objectType)
        {
            Contract.Requires(argument1Type != null);
            Contract.Requires(argument2Type != null);
            Contract.Requires(objectType != null);

            var argument1Expression = Expression.Parameter(argument1Type, "arg1");
            var argument2Expression = Expression.Parameter(argument2Type, "arg2");
            var constructorInfo = objectType.GetConstructor(argument1Type, argument2Type);
            var newExpression = Expression.New(constructorInfo, argument1Expression, argument2Expression);
            var lambdaExpression = Expression.Lambda(newExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }

        public static LambdaExpression New(ConstructorInfo constructorInfo)
        {
            Contract.Requires(constructorInfo != null);

            var objectType = constructorInfo.DeclaringType;

            var constructorParameters = constructorInfo.GetParameters();
            var constructorParametersLength = constructorParameters.Length;

            // Forward call to the respective New method based on the number of constructor parameters.
            switch (constructorParametersLength)
            {
                case 0:
                {
                    return New(objectType);
                }

                case 1:
                {
                    var argumentType = constructorParameters[0].ParameterType;
                    return New(argumentType, objectType);
                }

                case 2:
                {
                    var argument1Type = constructorParameters[0].ParameterType;
                    var argument2Type = constructorParameters[1].ParameterType;
                    return New(argument1Type, argument2Type, objectType);
                }
            }

            // Currently no support for constructors with 3 or more arguments.
            throw new InvalidOperationException("Currently do not support constructors with 3 or more arguments for this New expression.");
        }

        public static Expression<Func<TObject>> New<TObject>()
        {
            var objectType = typeof(TObject);
            var lambdaExpression = (Expression<Func<TObject>>)New(objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument, TObject>> New<TArgument, TObject>()
        {
            var objectType = typeof(TObject);
            var argumentType = typeof(TArgument);
            var lambdaExpression = (Expression<Func<TArgument, TObject>>)New(argumentType, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TObject>> New<TArgument1, TArgument2, TObject>()
        {
            var objectType = typeof(TObject);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);
            var lambdaExpression = (Expression<Func<TArgument1, TArgument2, TObject>>)New(argument1Type, argument2Type, objectType);
            return lambdaExpression;
        }
        #endregion

        #region Property Methods
        // Instance - Property Getters
        public static LambdaExpression PropertyGetter(Type objectType, PropertyInfo propertyInfo)
        {
            Contract.Requires(objectType != null);
            Contract.Requires(propertyInfo != null);

            var instanceExpression = Expression.Parameter(objectType, "i");
            var propertyExpression = Expression.Property(instanceExpression, propertyInfo);
            var lambdaExpression = Expression.Lambda(propertyExpression, instanceExpression);
            return lambdaExpression;
        }

        public static LambdaExpression PropertyGetter(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var objectType = propertyInfo.DeclaringType;
            var lambdaExpression = ExpressionBuilder.PropertyGetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        public static LambdaExpression PropertyGetter(Type objectType, string propertyName)
        {
            Contract.Requires(objectType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var propertyInfo = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var lambdaExpression = ExpressionBuilder.PropertyGetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TProperty>> PropertyGetter<TObject, TProperty>(string propertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var objectType = typeof(TObject);
            var propertyInfo = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var lambdaExpression = (Expression<Func<TObject, TProperty>>)PropertyGetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        // Instance - Property Setters
        private static LambdaExpression PropertySetter(Type objectType, PropertyInfo propertyInfo)
        {
            Contract.Requires(objectType != null);
            Contract.Requires(propertyInfo != null);

            var propertyType = propertyInfo.PropertyType;
            var instanceExpression = Expression.Parameter(objectType, "i");
            var valueExpression = Expression.Parameter(propertyType, "x");
            var propertyExpression = Expression.Property(instanceExpression, propertyInfo);
            var propertySetExpression = Expression.Assign(propertyExpression, valueExpression);
            var actionDelegateType = CreateActionDelegateType(objectType, propertyType);
            var lambdaExpression = Expression.Lambda(actionDelegateType, propertySetExpression, instanceExpression, valueExpression);
            return lambdaExpression;
        }

        public static LambdaExpression PropertySetter(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var objectType = propertyInfo.DeclaringType;
            var lambdaExpression = ExpressionBuilder.PropertySetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        public static LambdaExpression PropertySetter(Type objectType, string propertyName)
        {
            Contract.Requires(objectType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var propertyInfo = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var lambdaExpression = ExpressionBuilder.PropertySetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TObject, TProperty>> PropertySetter<TObject, TProperty>(string propertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var objectType = typeof(TObject);
            var propertyInfo = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var lambdaExpression = (Expression<Action<TObject, TProperty>>)PropertySetter(objectType, propertyInfo);
            return lambdaExpression;
        }

        // Static - Property Getters
        public static LambdaExpression StaticPropertyGetter(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var propertyExpression = Expression.Property(null, propertyInfo);
            var lambdaExpression = Expression.Lambda(propertyExpression);
            return lambdaExpression;
        }

        public static LambdaExpression StaticPropertyGetter(Type classType, string propertyName)
        {
            Contract.Requires(classType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var propertyInfo = classType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            var lambdaExpression = ExpressionBuilder.StaticPropertyGetter(propertyInfo);
            return lambdaExpression;
        }

        public static Expression<Func<TProperty>> StaticPropertyGetter<TClass, TProperty>(string propertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var classType = typeof(TClass);
            var propertyInfo = classType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            var lambdaExpression = (Expression<Func<TProperty>>)ExpressionBuilder.StaticPropertyGetter(propertyInfo);
            return lambdaExpression;
        }

        // Static - Property Setters
        public static LambdaExpression StaticPropertySetter(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var propertyType = propertyInfo.PropertyType;
            var valueExpression = Expression.Parameter(propertyType, "x");
            var propertyExpression = Expression.Property(null, propertyInfo);
            var propertySetExpression = Expression.Assign(propertyExpression, valueExpression);
            var actionDelegateType = CreateActionDelegateType(propertyType);
            var lambdaExpression = Expression.Lambda(actionDelegateType, propertySetExpression, valueExpression);
            return lambdaExpression;
        }

        public static LambdaExpression StaticPropertySetter(Type classType, string propertyName)
        {
            Contract.Requires(classType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var propertyInfo = classType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            var lambdaExpression = ExpressionBuilder.StaticPropertySetter(propertyInfo);
            return lambdaExpression;
        }

        public static Expression<Action<TProperty>> StaticPropertySetter<TClass, TProperty>(string propertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            var classType = typeof(TClass);
            var propertyInfo = classType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            var lambdaExpression = (Expression<Action<TProperty>>)StaticPropertySetter(propertyInfo);
            return lambdaExpression;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Call Methods
        private static void CreateClosedGenericTypeAndMethodInfo(IEnumerable<Type> genericTypeArguments, ref Type type, ref MethodInfo methodInfo)
        {
            var typeArguments = genericTypeArguments.ToArray();

            var isObjectGenericTypeDefinition = type.IsGenericTypeDefinition();
            if (isObjectGenericTypeDefinition)
            {
                var closedGenericType = type.MakeGenericType(typeArguments);
                type = closedGenericType;

                var methodName = methodInfo.Name;
                var closedParametersTypes = methodInfo
                    .GetParameters()
                    .Select(x =>
                    {
                        var parameterType = x.ParameterType;
                        if (!parameterType.IsGenericParameter)
                            return parameterType;

                        var genericParameterPosition = parameterType.GenericParameterPosition;
                        var genericParameterType = typeArguments[genericParameterPosition];
                        return genericParameterType;
                    })
                    .ToList();

                var closedGenericMethod = closedGenericType
                    .GetMethods()
                    .Single(
                        x =>
                            x.Name == methodName &&
                            x.GetParameters().Select(p => p.ParameterType).SequenceEqual(closedParametersTypes));
                methodInfo = closedGenericMethod;
            }
            else
            {
                var isMethodGenericMethodDefinition = methodInfo.IsGenericMethodDefinition;
                if (isMethodGenericMethodDefinition)
                {
                    methodInfo = methodInfo.MakeGenericMethod(typeArguments);
                }
            }
        }

        private static LambdaExpression CreateInstanceMethodCallExpression(Type type, MethodInfo methodInfo, ParameterInfo[] arguments)
        {
            Contract.Requires(type != null);
            Contract.Requires(methodInfo != null);
            Contract.Requires(arguments != null);

            // Instance Methods
            var argumentsLength = arguments.Length;
            switch (argumentsLength)
            {
                case 0:
                    {
                        var instanceExpression = Expression.Parameter(type, "i");
                        var callExpression = Expression.Call(instanceExpression, methodInfo);
                        var lambdaExpression = Expression.Lambda(callExpression, instanceExpression);
                        return lambdaExpression;
                    }

                case 1:
                    {
                        var argumentType = arguments[0].ParameterType;

                        var instanceExpression = Expression.Parameter(type, "i");
                        var argumentExpression = Expression.Parameter(argumentType, "arg");
                        // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                        var callExpression = Expression.Call(instanceExpression, methodInfo, argumentExpression);
                        var lambdaExpression = Expression.Lambda(callExpression, instanceExpression, argumentExpression);
                        return lambdaExpression;
                    }

                case 2:
                    {
                        var argument1Type = arguments[0].ParameterType;
                        var argument2Type = arguments[1].ParameterType;

                        var instanceExpression = Expression.Parameter(type, "i");
                        var argument1Expression = Expression.Parameter(argument1Type, "arg1");
                        var argument2Expression = Expression.Parameter(argument2Type, "arg2");
                        var callExpression = Expression.Call(instanceExpression, methodInfo, argument1Expression, argument2Expression);
                        var lambdaExpression = Expression.Lambda(callExpression, instanceExpression, argument1Expression, argument2Expression);
                        return lambdaExpression;
                    }
            }

            throw new InvalidOperationException(UnableToCreateCallExpressionMessage);
        }

        private static LambdaExpression CreateInstanceVoidMethodCallExpression(Type type, MethodInfo methodInfo, ParameterInfo[] arguments)
        {
            Contract.Requires(type != null);
            Contract.Requires(methodInfo != null);
            Contract.Requires(arguments != null);

            // Instance Void Methods
            var argumentsLength = arguments.Length;
            switch (argumentsLength)
            {
                case 0:
                    {
                        var instanceExpression = Expression.Parameter(type, "i");
                        var callExpression = Expression.Call(instanceExpression, methodInfo);
                        var actionDelegateType = CreateActionDelegateType(type);
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression, instanceExpression);
                        return lambdaExpression;
                    }

                case 1:
                    {
                        var argumentType = arguments[0].ParameterType;

                        var instanceExpression = Expression.Parameter(type, "i");
                        var argumentExpression = Expression.Parameter(argumentType, "arg");
                        // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                        var callExpression = Expression.Call(instanceExpression, methodInfo, argumentExpression);
                        var actionDelegateType = CreateActionDelegateType(type, argumentType);
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression, instanceExpression, argumentExpression);
                        return lambdaExpression;
                    }

                case 2:
                    {
                        var argument1Type = arguments[0].ParameterType;
                        var argument2Type = arguments[1].ParameterType;

                        var instanceExpression = Expression.Parameter(type, "i");
                        var argument1Expression = Expression.Parameter(argument1Type, "arg1");
                        var argument2Expression = Expression.Parameter(argument2Type, "arg2");
                        var callExpression = Expression.Call(instanceExpression, methodInfo, argument1Expression, argument2Expression);
                        var actionDelegateType = CreateActionDelegateType(type, argument1Type, argument2Type);
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression, instanceExpression, argument1Expression, argument2Expression);
                        return lambdaExpression;
                    }
            }

            throw new InvalidOperationException(UnableToCreateCallExpressionMessage);
        }

        private static LambdaExpression CreateStaticMethodCallExpression(MethodInfo methodInfo, ParameterInfo[] arguments)
        {
            Contract.Requires(methodInfo != null);
            Contract.Requires(arguments != null);

            // Static Methods
            var argumentsLength = arguments.Length;
            switch (argumentsLength)
            {
                case 0:
                    {
                        var callExpression = Expression.Call(methodInfo);
                        var lambdaExpression = Expression.Lambda(callExpression);
                        return lambdaExpression;
                    }

                case 1:
                    {
                        var argumentType = arguments[0].ParameterType;

                        var argumentExpression = Expression.Parameter(argumentType, "arg");
                        var callExpression = Expression.Call(methodInfo, argumentExpression);
                        var lambdaExpression = Expression.Lambda(callExpression, argumentExpression);
                        return lambdaExpression;
                    }

                case 2:
                    {
                        var argument1Type = arguments[0].ParameterType;
                        var argument2Type = arguments[1].ParameterType;

                        var argument1Expression = Expression.Parameter(argument1Type, "arg1");
                        var argument2Expression = Expression.Parameter(argument2Type, "arg2");
                        var callExpression = Expression.Call(methodInfo, argument1Expression, argument2Expression);
                        var lambdaExpression = Expression.Lambda(callExpression, argument1Expression, argument2Expression);
                        return lambdaExpression;
                    }
            }

            throw new InvalidOperationException(UnableToCreateCallExpressionMessage);
        }

        private static LambdaExpression CreateStaticVoidMethodCallExpression(MethodInfo methodInfo, ParameterInfo[] arguments)
        {
            Contract.Requires(methodInfo != null);
            Contract.Requires(arguments != null);

            // Static Void Methods
            var argumentsLength = arguments.Length;
            switch (argumentsLength)
            {
                case 0:
                    {
                        var callExpression = Expression.Call(methodInfo);
                        var actionDelegateType = CreateActionDelegateType();
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression);
                        return lambdaExpression;
                    }

                case 1:
                    {
                        var argumentType = arguments[0].ParameterType;

                        var argumentExpression = Expression.Parameter(argumentType, "arg");
                        var callExpression = Expression.Call(methodInfo, argumentExpression);
                        var actionDelegateType = CreateActionDelegateType(argumentType);
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression, argumentExpression);
                        return lambdaExpression;
                    }

                case 2:
                    {
                        var argument1Type = arguments[0].ParameterType;
                        var argument2Type = arguments[1].ParameterType;

                        var argument1Expression = Expression.Parameter(argument1Type, "arg1");
                        var argument2Expression = Expression.Parameter(argument2Type, "arg2");
                        var callExpression = Expression.Call(methodInfo, argument1Expression, argument2Expression);
                        var actionDelegateType = CreateActionDelegateType(argument1Type, argument2Type);
                        var lambdaExpression = Expression.Lambda(actionDelegateType, callExpression, argument1Expression, argument2Expression);
                        return lambdaExpression;
                    }
            }

            throw new InvalidOperationException(UnableToCreateCallExpressionMessage);
        }
        #endregion

        #region Cast Methods
        private static Type CreateActionDelegateType()
        { return typeof(Action); }

        private static Type CreateActionDelegateType(Type type)
        {
            Contract.Requires(type != null);

            var actionDelegateOpenGenericType = typeof(Action<>);
            var actionDelegateOpenGenericTypeArguments = new[]
                {
                    type
                };
            var actionDelegateClosedGenericType = actionDelegateOpenGenericType.MakeGenericType(actionDelegateOpenGenericTypeArguments);
            return actionDelegateClosedGenericType;
        }

        private static Type CreateActionDelegateType(Type type1, Type type2)
        {
            Contract.Requires(type1 != null);
            Contract.Requires(type2 != null);

            var actionDelegateOpenGenericType = typeof(Action<,>);
            var actionDelegateOpenGenericTypeArguments = new[]
                {
                    type1,
                    type2
                };
            var actionDelegateClosedGenericType = actionDelegateOpenGenericType.MakeGenericType(actionDelegateOpenGenericTypeArguments);
            return actionDelegateClosedGenericType;
        }

        private static Type CreateActionDelegateType(Type type1, Type type2, Type type3)
        {
            Contract.Requires(type1 != null);
            Contract.Requires(type2 != null);
            Contract.Requires(type3 != null);

            var actionDelegateOpenGenericType = typeof(Action<,,>);
            var actionDelegateOpenGenericTypeArguments = new[]
                {
                    type1,
                    type2,
                    type3
                };
            var actionDelegateClosedGenericType = actionDelegateOpenGenericType.MakeGenericType(actionDelegateOpenGenericTypeArguments);
            return actionDelegateClosedGenericType;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string UnableToCreateCallExpressionMessage = "Currently do not support 3 or more arguments for this Call expression.";
        #endregion
    }
}
