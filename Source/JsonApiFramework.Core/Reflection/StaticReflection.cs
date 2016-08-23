// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Utility class to get property/method names using "static" reflection.
    /// </summary>
    public static class StaticReflection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return StaticReflection.GetMemberName(expression.Body);
        }

        public static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return StaticReflection.GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return StaticReflection.GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return StaticReflection.GetMemberName(expression);
        }

        public static string GetMemberName<T, TProperty>(this T instance, Expression<Func<T, TProperty>> expression)
        {
            return StaticReflection.GetMemberName(expression);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return StaticReflection.GetMemberName(expression);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

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

            throw new ArgumentException(
                "Invalid expression, must be either a MemberExpression, MethodCallExpression, or UnaryExpression.");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression == null)
                throw new ArgumentNullException("unaryExpression");

            var operand = unaryExpression.Operand as MethodCallExpression;
            return operand != null
                ? operand.Method.Name
                : ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
        #endregion
    }
}