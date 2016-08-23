// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Extension methods for the .NET Type class.
    /// </summary>
    public static class TypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Constructor Methods
        public static ConstructorInfo GetConstructor(this Type type, params Type[] parameterTypes)
        {
            if (parameterTypes == null || parameterTypes.Length == 0)
                return type.GetDefaultConstructor(DefaultConstructorBindingFlags);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, params Type[] parameterTypes)
        {
            if (parameterTypes == null || parameterTypes.Length == 0)
                return type.GetDefaultConstructor(bindingFlags);

            return GetConstructors(type.GetTypeInfo(), bindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(this Type type, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(parameterTypes != null);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(parameterTypes != null);

            return GetConstructors(type.GetTypeInfo(), bindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            Contract.Requires(type != null);
            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, EmptyTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetDefaultConstructor(this Type type, BindingFlags bindingFlags)
        {
            Contract.Requires(type != null);
            return GetConstructors(type.GetTypeInfo(), bindingFlags, EmptyTypes).SingleOrDefault();
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
        {
            Contract.Requires(type != null);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, null);
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags)
        {
            Contract.Requires(type != null);

            return GetConstructors(type.GetTypeInfo(), bindingFlags, null);
        }
        #endregion

        #region Method Methods
        public static MethodInfo GetMethod(this Type type, string methodName, params Type[] parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, DefaultMethodBindingFlags, parameterTypes ?? EmptyTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(this Type type, string methodName, BindingFlags bindingFlags, params Type[] parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, bindingFlags, parameterTypes ?? EmptyTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(this Type type, string methodName, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, DefaultMethodBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(this Type type, string methodName, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, bindingFlags, parameterTypes).SingleOrDefault();
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type)
        {
            Contract.Requires(type != null);

            return GetMethods(type.GetTypeInfo(), null, DefaultMethodBindingFlags, null);
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags bindingFlags)
        {
            Contract.Requires(type != null);

            return GetMethods(type.GetTypeInfo(), null, bindingFlags, null);
        }
        #endregion

        #region Miscellaneous Methods
        public static Type BaseType(this Type type)
        {
            Contract.Requires(type != null);
            return type.GetTypeInfo().BaseType;
        }

        /// <summary>
        /// Represents a compact(partial) form of the <c>AssemblyQualifiedName</c>
        /// string property.
        /// </summary>
        /// <remarks>
        /// Can be used by the static method Type.GetType(string) to create
        /// <c>Type</c> objects from strings.
        /// </remarks>
        public static string GetCompactQualifiedName(this Type type)
        {
            Contract.Requires(type != null);

            var assemblyQualifiedName = type.AssemblyQualifiedName;
            var compactQualifiedName = RemoveAssemblyDetails(assemblyQualifiedName);
            return compactQualifiedName;
        }
        #endregion

        #region Predicate Methods
        public static bool IsAbstract(this Type type)
        {
            Contract.Requires(type != null);
            return type.GetTypeInfo().IsAbstract;
        }

        public static bool IsAssignableFrom(this Type type, Type fromType)
        {
            Contract.Requires(type != null);

            return fromType != null && type.GetTypeInfo().IsAssignableFrom(fromType.GetTypeInfo());
        }

        public static bool IsClass(this Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsClass;
        }

        public static bool IsEnum(this Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsGenericTypeDefinition(this Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsGenericTypeDefinition;
        }

        public static bool IsGenericType(this Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsImplementationOf(this Type type, Type interfaceType)
        {
            Contract.Requires(type != null);

            return interfaceType != null && IsImplementationOf(type.GetTypeInfo(), interfaceType.GetTypeInfo());
        }

        public static bool IsIntegralType(this Type type)
        {
            Contract.Requires(type != null);

            return IntegralTypes.Contains(type);
        }

        public static bool IsNullableType(this Type type)
        {
            Contract.Requires(type != null);

            return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsStringType(this Type type)
        {
            Contract.Requires(type != null);

            return type == typeof(string);
        }

        public static bool IsSubclassOf(this Type type, Type baseClass)
        {
            Contract.Requires(type != null);

            return baseClass != null && type.GetTypeInfo().IsSubclassOf(baseClass);
        }

        public static bool IsSubclassOrImplementationOf(this Type type, Type baseClassOrInterfaceType)
        {
            Contract.Requires(type != null);

            return baseClassOrInterfaceType != null && IsSubclassOrImplementationOf(type.GetTypeInfo(), baseClassOrInterfaceType.GetTypeInfo());
        }

        public static bool IsValueType(this Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsVoidType(this Type type)
        {
            Contract.Requires(type != null);

            return type == typeof(void);
        }
        #endregion

        #region Property Methods
        public static PropertyInfo GetProperty(this Type type, string propertyName)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            return GetProperties(type.GetTypeInfo(), propertyName, DefaultPropertyBindingFlags).SingleOrDefault();
        }

        public static PropertyInfo GetProperty(this Type type, string propertyName, BindingFlags bindingFlags)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            return GetProperties(type.GetTypeInfo(), propertyName, bindingFlags).SingleOrDefault();
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            Contract.Requires(type != null);

            return GetProperties(type.GetTypeInfo(), null, DefaultPropertyBindingFlags);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags)
        {
            Contract.Requires(type != null);

            return GetProperties(type.GetTypeInfo(), null, bindingFlags);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<T> FilterOnName<T>(IEnumerable<T> query, string name, BindingFlags bindingFlags)
            where T : MemberInfo
        {
            if (String.IsNullOrWhiteSpace(name))
                return query;

            var comparisonType = bindingFlags.HasFlag(BindingFlags.IgnoreCase) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            query = query.Where(x => String.Equals(x.Name, name, comparisonType));
            return query;
        }

        private static IEnumerable<T> FilterOnParameterTypes<T>(IEnumerable<T> query, IEnumerable<Type> parameterTypes)
            where T : MethodBase
        {
            if (parameterTypes == null)
                return query;

            query = query.Where(x => x.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes));
            return query;
        }

        private static IEnumerable<T> FilterOnInstanceAndStatic<T>(IEnumerable<T> query, BindingFlags bindingFlags)
            where T : MethodBase
        {
            var isInstance = bindingFlags.HasFlag(BindingFlags.Instance);
            var isStatic = bindingFlags.HasFlag(BindingFlags.Static);

            ValidateInstanceAndStaticBindingFlags(isInstance, isStatic);

            if (isInstance && !isStatic)
            {
                query = query.Where(x => !x.IsStatic);
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (!isInstance && isStatic)
            {
                query = query.Where(x => x.IsStatic);
            }

            return query;
        }

        private static IEnumerable<T> FilterOnPublicAndNonPublic<T>(IEnumerable<T> query, BindingFlags bindingFlags)
            where T : MethodBase
        {
            var isPublic = bindingFlags.HasFlag(BindingFlags.Public);
            var isNonPublic = bindingFlags.HasFlag(BindingFlags.NonPublic);

            ValidatePublicAndNonPublicBindingFlags(isPublic, isNonPublic);

            if (isPublic && !isNonPublic)
            {
                query = query.Where(x => x.IsPublic);
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (!isPublic && isNonPublic)
            {
                // IsFamilyOrAssembly == protected internal
                // IsFamily == protected
                // IsAssembly == internal
                // IsPrivate == private
                query = query.Where(x => x.IsFamilyOrAssembly || x.IsFamily || x.IsAssembly || x.IsPrivate);
            }

            return query;
        }

        private static IEnumerable<PropertyInfo> FilterOnInstanceAndStatic(IEnumerable<PropertyInfo> query, BindingFlags bindingFlags)
        {
            var isInstance = bindingFlags.HasFlag(BindingFlags.Instance);
            var isStatic = bindingFlags.HasFlag(BindingFlags.Static);

            ValidateInstanceAndStaticBindingFlags(isInstance, isStatic);

            if (isInstance && !isStatic)
            {
                query = query.Where(x => (x.CanRead && !x.GetMethod.IsStatic) || (x.CanWrite && !x.SetMethod.IsStatic));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (!isInstance && isStatic)
            {
                query = query.Where(x => (x.CanRead && x.GetMethod.IsStatic) || (x.CanWrite && x.SetMethod.IsStatic));
            }

            return query;
        }

        private static IEnumerable<PropertyInfo> FilterOnPublicAndNonPublic(IEnumerable<PropertyInfo> query, BindingFlags bindingFlags)
        {
            var isPublic = bindingFlags.HasFlag(BindingFlags.Public);
            var isNonPublic = bindingFlags.HasFlag(BindingFlags.NonPublic);

            ValidatePublicAndNonPublicBindingFlags(isPublic, isNonPublic);

            if (isPublic && !isNonPublic)
            {
                query = query.Where(x => (x.CanRead && x.GetMethod.IsPublic) || (x.CanWrite && x.SetMethod.IsPublic));
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (!isPublic && isNonPublic)
            {
                // IsFamilyOrAssembly == protected internal
                // IsFamily == protected
                // IsAssembly == internal
                // IsPrivate == private
                query = query.Where(x =>
                    (x.CanRead && (x.GetMethod.IsFamilyOrAssembly || x.GetMethod.IsFamily || x.GetMethod.IsAssembly || x.GetMethod.IsPrivate)) ||
                    (x.CanWrite && (x.SetMethod.IsFamilyOrAssembly || x.SetMethod.IsFamily || x.SetMethod.IsAssembly || x.SetMethod.IsPrivate)));
            }

            return query;
        }

        private static IEnumerable<ConstructorInfo> GetConstructors(TypeInfo typeInfo, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes)
        {
            if (typeInfo == null)
                return Enumerable.Empty<ConstructorInfo>();

            var constructors = new List<ConstructorInfo>();

            var constructorsToAdd = typeInfo.DeclaredConstructors;

            constructorsToAdd = FilterOnParameterTypes(constructorsToAdd, parameterTypes);
            constructorsToAdd = FilterOnPublicAndNonPublic(constructorsToAdd, bindingFlags);

            constructors.AddRange(constructorsToAdd);

            return constructors;
        }

        private static IEnumerable<MethodInfo> GetMethods(TypeInfo typeInfo, string methodName, BindingFlags bindingFlags, IEnumerable<Type> parameterTypes)
        {
            if (typeInfo == null)
                return Enumerable.Empty<MethodInfo>();

            var methods = new List<MethodInfo>();
            while (typeInfo != null)
            {
                var methodsToAdd = typeInfo.DeclaredMethods;

                methodsToAdd = FilterOnName(methodsToAdd, methodName, bindingFlags);
                // ReSharper disable once PossibleMultipleEnumeration
                methodsToAdd = FilterOnParameterTypes(methodsToAdd, parameterTypes);
                methodsToAdd = FilterOnInstanceAndStatic(methodsToAdd, bindingFlags);
                methodsToAdd = FilterOnPublicAndNonPublic(methodsToAdd, bindingFlags);

                methods.AddRange(methodsToAdd);

                if (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                    break;

                typeInfo = typeInfo.BaseType != null ? typeInfo.BaseType.GetTypeInfo() : null;
            }
            return methods;
        }

        private static IEnumerable<PropertyInfo> GetProperties(TypeInfo typeInfo, string propertyName, BindingFlags bindingFlags)
        {
            if (typeInfo == null)
                return Enumerable.Empty<PropertyInfo>();

            var properties = new List<PropertyInfo>();
            while (typeInfo != null)
            {
                var propertiesToAdd = typeInfo.DeclaredProperties;

                propertiesToAdd = FilterOnName(propertiesToAdd, propertyName, bindingFlags);
                propertiesToAdd = FilterOnInstanceAndStatic(propertiesToAdd, bindingFlags);
                propertiesToAdd = FilterOnPublicAndNonPublic(propertiesToAdd, bindingFlags);

                properties.AddRange(propertiesToAdd);

                if (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                    break;

                typeInfo = typeInfo.BaseType != null ? typeInfo.BaseType.GetTypeInfo() : null;
            }

            return properties;
        }

        private static bool IsImplementationOf(TypeInfo instanceTypeInfo, TypeInfo interfaceTypeInfo)
        {
            if (instanceTypeInfo == null || interfaceTypeInfo == null)
                return false;

            return interfaceTypeInfo.IsGenericType
                ? instanceTypeInfo.ImplementedInterfaces
                                  .Select(x => x.GetTypeInfo())
                                  .Where(x => x.IsGenericType)
                                  .Any(x => x.GetGenericTypeDefinition().GetTypeInfo().Equals(interfaceTypeInfo))
                : instanceTypeInfo.ImplementedInterfaces
                                  .Select(x => x.GetTypeInfo())
                                  .Where(x => !x.IsGenericType)
                                  .Any(x => x.Equals(interfaceTypeInfo));
        }

        private static bool IsSubclassOrImplementationOf(TypeInfo instanceTypeInfo, TypeInfo baseClassOrInterfaceTypeInfo)
        {
            if (instanceTypeInfo == null || baseClassOrInterfaceTypeInfo == null)
                return false;

            if (instanceTypeInfo.IsSubclassOf(baseClassOrInterfaceTypeInfo.AsType()))
                return true;

            if (IsImplementationOf(instanceTypeInfo, baseClassOrInterfaceTypeInfo))
                return true;

            if (!baseClassOrInterfaceTypeInfo.IsGenericType)
                return false;

            var baseTypeInfo = instanceTypeInfo.BaseType.GetTypeInfo();
            while (baseTypeInfo != null && baseTypeInfo.Equals(typeof(object).GetTypeInfo()) == false)
            {
                if (baseClassOrInterfaceTypeInfo.Equals(baseTypeInfo))
                    return true;

                if (baseTypeInfo.IsGenericType)
                {
                    var baseGenericTypeDefinitionInfo = baseTypeInfo.GetGenericTypeDefinition().GetTypeInfo();
                    if (baseClassOrInterfaceTypeInfo.Equals(baseGenericTypeDefinitionInfo))
                        return true;
                }

                baseTypeInfo = baseTypeInfo.BaseType.GetTypeInfo();
            }

            return false;
        }

        private static string RemoveAssemblyDetails(string assemblyQualifiedName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(assemblyQualifiedName) == false);

            // Loop through the type name and filter out qualified assembly
            // details from nested type names.
            var builder = new StringBuilder();
            var writingAssemblyName = false;
            var skippingAssemblyDetails = false;
            for (var i = 0; i < assemblyQualifiedName.Length; i++)
            {
                var current = assemblyQualifiedName[i];
                switch (current)
                {
                    case '[':
                        writingAssemblyName = false;
                        skippingAssemblyDetails = false;
                        builder.Append(current);
                        break;
                    case ']':
                        writingAssemblyName = false;
                        skippingAssemblyDetails = false;
                        builder.Append(current);
                        break;
                    case ',':
                        if (!writingAssemblyName)
                        {
                            writingAssemblyName = true;
                            builder.Append(current);
                        }
                        else
                        {
                            skippingAssemblyDetails = true;
                        }
                        break;
                    default:
                        if (!skippingAssemblyDetails)
                            builder.Append(current);
                        break;
                }
            }

            return builder.ToString();
        }

        private static void ValidateInstanceAndStaticBindingFlags(bool isInstance, bool isStatic)
        {
            if (!isInstance && !isStatic)
                throw new InvalidOperationException("{0} must at least specify either {1} or {2}".FormatWith(typeof(BindingFlags).Name, BindingFlags.Instance, BindingFlags.Static));
        }

        private static void ValidatePublicAndNonPublicBindingFlags(bool isPublic, bool isNonPublic)
        {
            if (!isPublic && !isNonPublic)
                throw new InvalidOperationException("{0} must at least specify either {1} or {2}".FormatWith(typeof(BindingFlags).Name, BindingFlags.Public, BindingFlags.NonPublic));
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private const BindingFlags DefaultConstructorBindingFlags =
            BindingFlags.Public;

        private const BindingFlags DefaultMethodBindingFlags =
            BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

        private const BindingFlags DefaultPropertyBindingFlags =
            BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

        private static readonly Type[] EmptyTypes = new Type[0];

        private static readonly HashSet<Type> IntegralTypes = new HashSet<Type>
            {
                typeof(sbyte),
                typeof(byte),
                typeof(char),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong)
            };
        #endregion
    }
}
