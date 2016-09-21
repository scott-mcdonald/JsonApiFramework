// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace JsonApiFramework.Reflection
{
    /// <summary>Utility class to get property or method names using "static" reflection.</summary>
    public static class StaticReflection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            Contract.Requires(expression != null);

            return StaticReflection.GetMemberName(expression.Body);
        }

        //public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> expression)
        //{
        //    Contract.Requires(expression != null);

        //    return StaticReflection.GetMemberName(expression.Body);
        //}

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            Contract.Requires(expression != null);

            return StaticReflection.GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            Contract.Requires(expression != null);

            return StaticReflection.GetMemberName(expression.Body);
        }

        //public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        //{
        //    Contract.Requires(expression != null);

        //    return StaticReflection.GetMemberName(expression.Body);
        //}

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            Contract.Requires(expression != null);

            return StaticReflection.GetMemberName(expression.Body);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string GetMemberName(Expression expression)
        {
            Contract.Requires(expression != null);

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                // Reference type property or field
                return memberExpression.Member.Name;
            }

            var methodCallExpression = expression as MethodCallExpression;
            if (methodCallExpression != null)
            {
                // Reference type method
                return methodCallExpression.Method.Name;
            }

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                // Property, field of method returning value type
                return StaticReflection.GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression, must be either a MemberExpression, MethodCallExpression, or UnaryExpression.");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            Contract.Requires(unaryExpression != null);

            var operand = unaryExpression.Operand as MethodCallExpression;
            return operand != null
                ? operand.Method.Name
                : ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
        #endregion
    }
}