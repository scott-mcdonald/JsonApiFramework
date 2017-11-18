// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Reflection methods for the .NET Type class.
    /// </summary>
    public static class TypeReflection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Constructor Methods
        public static ConstructorInfo GetConstructor(Type type, params Type[] parameterTypes)
        {
            if (parameterTypes == null || parameterTypes.Length == 0)
                return GetDefaultConstructor(type, DefaultConstructorBindingFlags);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(Type type, ReflectionFlags reflectionFlags, params Type[] parameterTypes)
        {
            if (parameterTypes == null || parameterTypes.Length == 0)
                return GetDefaultConstructor(type, reflectionFlags);

            return GetConstructors(type.GetTypeInfo(), reflectionFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(Type type, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(parameterTypes != null);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetConstructor(Type type, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(parameterTypes != null);

            return GetConstructors(type.GetTypeInfo(), reflectionFlags, parameterTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetDefaultConstructor(Type type)
        {
            Contract.Requires(type != null);
            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, EmptyTypes).SingleOrDefault();
        }

        public static ConstructorInfo GetDefaultConstructor(Type type, ReflectionFlags reflectionFlags)
        {
            Contract.Requires(type != null);
            return GetConstructors(type.GetTypeInfo(), reflectionFlags, EmptyTypes).SingleOrDefault();
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            Contract.Requires(type != null);

            return GetConstructors(type.GetTypeInfo(), DefaultConstructorBindingFlags, null);
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(Type type, ReflectionFlags reflectionFlags)
        {
            Contract.Requires(type != null);

            return GetConstructors(type.GetTypeInfo(), reflectionFlags, null);
        }
        #endregion

        #region Method Methods
        public static MethodInfo GetMethod(Type type, string methodName)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, DefaultMethodBindingFlags, EmptyTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(Type type, string methodName, params Type[] parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, DefaultMethodBindingFlags, parameterTypes ?? EmptyTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(Type type, string methodName, ReflectionFlags reflectionFlags, params Type[] parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, reflectionFlags, parameterTypes ?? EmptyTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(Type type, string methodName, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, DefaultMethodBindingFlags, parameterTypes).SingleOrDefault();
        }

        public static MethodInfo GetMethod(Type type, string methodName, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(methodName) == false);

            return GetMethods(type.GetTypeInfo(), methodName, reflectionFlags, parameterTypes).SingleOrDefault();
        }

        public static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            Contract.Requires(type != null);

            return GetMethods(type.GetTypeInfo(), null, DefaultMethodBindingFlags, null);
        }

        public static IEnumerable<MethodInfo> GetMethods(Type type, ReflectionFlags reflectionFlags)
        {
            Contract.Requires(type != null);

            return GetMethods(type.GetTypeInfo(), null, reflectionFlags, null);
        }
        #endregion

        #region Miscellaneous Methods
        public static Type GetBaseType(Type type)
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
        public static string GetCompactQualifiedName(Type type)
        {
            Contract.Requires(type != null);

            var assemblyQualifiedName = type.AssemblyQualifiedName;
            var compactQualifiedName = RemoveAssemblyDetails(assemblyQualifiedName);
            return compactQualifiedName;
        }
        #endregion

        #region Predicate Methods
        public static bool IsAbstract(Type type)
        {
            Contract.Requires(type != null);
            return type.GetTypeInfo().IsAbstract;
        }

        public static bool IsAssignableFrom(Type type, Type fromType)
        {
            Contract.Requires(type != null);

            return fromType != null && type.GetTypeInfo().IsAssignableFrom(fromType.GetTypeInfo());
        }

        public static bool IsClass(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsClass;
        }

        /// <summary>
        /// A complex type is a type that cannot be converted with default "type converters".
        /// </summary>
        /// <param name="type">This type to query is a complex type or not.</param>
        /// <returns>True is this type cannot be converted with a type converter, false otherwise.</returns>
        public static bool IsComplex(Type type)
        {
            Contract.Requires(type != null);

            return !TypeReflection.IsSimple(type);
        }

        public static bool IsEnum(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsEnumerableOfT(Type type)
        {
            Contract.Requires(type != null);

            return IsEnumerableOfT(type, out var enumerableType);
        }

        public static bool IsEnumerableOfT(Type type, out Type enumerableType)
        {
            Contract.Requires(type != null);

            enumerableType = null;

            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsGenericType)
                return false;

            var enumerableGenericTypeArguments = typeInfo
                .ImplementedInterfaces
                .Where(t => IsGenericType(t) && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(t => t.GenericTypeArguments.First())
                .ToList();

            var enumerableGenericTypeArgumentsCount = enumerableGenericTypeArguments.Count;
            if (enumerableGenericTypeArgumentsCount == 0)
                return false;

            if (enumerableGenericTypeArgumentsCount > 1)
                throw new ArgumentException(String.Format("ClrType [name={0}] implements multiple versions of IEnumerable<T>.", type.Name));

            enumerableType = enumerableGenericTypeArguments[0];
            return true;
        }

        public static bool IsFloatingPoint(Type type)
        {
            Contract.Requires(type != null);

            return FloatingPointTypes.Contains(type);
        }

        public static bool IsGenericTypeDefinition(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsGenericTypeDefinition;
        }

        public static bool IsGenericType(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsImplementationOf(Type type, Type interfaceType)
        {
            Contract.Requires(type != null);

            return interfaceType != null && IsImplementationOf(type.GetTypeInfo(), interfaceType.GetTypeInfo());
        }

        public static bool IsInteger(Type type)
        {
            Contract.Requires(type != null);

            return IntegerTypes.Contains(type);
        }

        public static bool IsNullableType(Type type)
        {
            Contract.Requires(type != null);

            return IsGenericType(type) && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNullableEnum(Type type)
        {
            var isNullableType = IsNullableType(type);
            if (!isNullableType)
                return false;

            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            return IsEnum(nullableUnderlyingType);
        }

        public static bool IsNumber(Type type)
        {
            return IsInteger(type) || IsFloatingPoint(type);
        }

        public static bool IsPrimitive(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsPrimitive || PrimitiveTypes.Contains(type);

        }

        /// <summary>
        /// A simple type is a type that can be converted with default "type converters".
        /// </summary>
        /// <param name="type">This type to query is a simple type or not.</param>
        /// <returns>True is this type can be converted with a type converter, false otherwise.</returns>
        public static bool IsSimple(Type type)
        {
            Contract.Requires(type != null);

            if (TypeReflection.IsPrimitive(type))
                return true;

            if (TypeReflection.IsNullableType(type))
            {
                var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
                return TypeReflection.IsSimple(nullableUnderlyingType);
            }

            // ReSharper disable once InvertIf
            if (TypeReflection.IsEnum(type))
            {
                var enumUnderlyingType = Enum.GetUnderlyingType(type);
                return TypeReflection.IsSimple(enumUnderlyingType);
            }

            return false;
        }

        public static bool IsString(Type type)
        {
            Contract.Requires(type != null);

            return type == typeof(string);
        }

        public static bool IsSubclassOf(Type type, Type baseClass)
        {
            Contract.Requires(type != null);

            return baseClass != null && type.GetTypeInfo().IsSubclassOf(baseClass);
        }

        public static bool IsSubclassOrImplementationOf(Type type, Type baseClassOrInterfaceType)
        {
            Contract.Requires(type != null);

            return baseClassOrInterfaceType != null && IsSubclassOrImplementationOf(type.GetTypeInfo(), baseClassOrInterfaceType.GetTypeInfo());
        }

        public static bool IsValueType(Type type)
        {
            Contract.Requires(type != null);

            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsVoid(Type type)
        {
            Contract.Requires(type != null);

            return type == typeof(void);
        }
        #endregion

        #region Property Methods
        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            return GetProperties(type.GetTypeInfo(), propertyName, DefaultPropertyBindingFlags).SingleOrDefault();
        }

        public static PropertyInfo GetProperty(Type type, string propertyName, ReflectionFlags reflectionFlags)
        {
            Contract.Requires(type != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            return GetProperties(type.GetTypeInfo(), propertyName, reflectionFlags).SingleOrDefault();
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            Contract.Requires(type != null);

            return GetProperties(type.GetTypeInfo(), null, DefaultPropertyBindingFlags);
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type, ReflectionFlags reflectionFlags)
        {
            Contract.Requires(type != null);

            return GetProperties(type.GetTypeInfo(), null, reflectionFlags);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<T> FilterOnName<T>(IEnumerable<T> query, string name, ReflectionFlags reflectionFlags)
            where T : MemberInfo
        {
            if (String.IsNullOrWhiteSpace(name))
                return query;

            var comparisonType = reflectionFlags.HasFlag(ReflectionFlags.IgnoreCase) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
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

        private static IEnumerable<T> FilterOnInstanceAndStatic<T>(IEnumerable<T> query, ReflectionFlags reflectionFlags)
            where T : MethodBase
        {
            var isInstance = reflectionFlags.HasFlag(ReflectionFlags.Instance);
            var isStatic = reflectionFlags.HasFlag(ReflectionFlags.Static);

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

        private static IEnumerable<T> FilterOnPublicAndNonPublic<T>(IEnumerable<T> query, ReflectionFlags reflectionFlags)
            where T : MethodBase
        {
            var isPublic = reflectionFlags.HasFlag(ReflectionFlags.Public);
            var isNonPublic = reflectionFlags.HasFlag(ReflectionFlags.NonPublic);

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

        private static IEnumerable<PropertyInfo> FilterOnInstanceAndStatic(IEnumerable<PropertyInfo> query, ReflectionFlags reflectionFlags)
        {
            var isInstance = reflectionFlags.HasFlag(ReflectionFlags.Instance);
            var isStatic = reflectionFlags.HasFlag(ReflectionFlags.Static);

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

        private static IEnumerable<PropertyInfo> FilterOnPublicAndNonPublic(IEnumerable<PropertyInfo> query, ReflectionFlags reflectionFlags)
        {
            var isPublic = reflectionFlags.HasFlag(ReflectionFlags.Public);
            var isNonPublic = reflectionFlags.HasFlag(ReflectionFlags.NonPublic);

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

        private static IEnumerable<ConstructorInfo> GetConstructors(TypeInfo typeInfo, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes)
        {
            if (typeInfo == null)
                return Enumerable.Empty<ConstructorInfo>();

            var constructors = new List<ConstructorInfo>();

            var constructorsToAdd = typeInfo.DeclaredConstructors;

            constructorsToAdd = FilterOnParameterTypes(constructorsToAdd, parameterTypes);
            constructorsToAdd = FilterOnPublicAndNonPublic(constructorsToAdd, reflectionFlags);

            constructors.AddRange(constructorsToAdd);

            return constructors;
        }

        private static IEnumerable<MethodInfo> GetMethods(TypeInfo typeInfo, string methodName, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes)
        {
            if (typeInfo == null)
                return Enumerable.Empty<MethodInfo>();

            var methods = new List<MethodInfo>();
            while (typeInfo != null)
            {
                var methodsToAdd = typeInfo.DeclaredMethods;

                methodsToAdd = FilterOnName(methodsToAdd, methodName, reflectionFlags);
                // ReSharper disable once PossibleMultipleEnumeration
                methodsToAdd = FilterOnParameterTypes(methodsToAdd, parameterTypes);
                methodsToAdd = FilterOnInstanceAndStatic(methodsToAdd, reflectionFlags);
                methodsToAdd = FilterOnPublicAndNonPublic(methodsToAdd, reflectionFlags);

                methods.AddRange(methodsToAdd);

                if (reflectionFlags.HasFlag(ReflectionFlags.DeclaredOnly))
                    break;

                typeInfo = typeInfo.BaseType != null ? typeInfo.BaseType.GetTypeInfo() : null;
            }
            return methods;
        }

        private static IEnumerable<PropertyInfo> GetProperties(TypeInfo typeInfo, string propertyName, ReflectionFlags reflectionFlags)
        {
            if (typeInfo == null)
                return Enumerable.Empty<PropertyInfo>();

            var properties = new List<PropertyInfo>();
            while (typeInfo != null)
            {
                var propertiesToAdd = typeInfo.DeclaredProperties;

                propertiesToAdd = FilterOnName(propertiesToAdd, propertyName, reflectionFlags);
                propertiesToAdd = FilterOnInstanceAndStatic(propertiesToAdd, reflectionFlags);
                propertiesToAdd = FilterOnPublicAndNonPublic(propertiesToAdd, reflectionFlags);

                properties.AddRange(propertiesToAdd);

                if (reflectionFlags.HasFlag(ReflectionFlags.DeclaredOnly))
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
            if (isInstance || isStatic)
                return;

            var bindingFlagsName = typeof(ReflectionFlags).Name;
            var message = String.Format("{0} must at least specify either {1} or {2}",
                bindingFlagsName,
                ReflectionFlags.Instance,
                ReflectionFlags.Static);

            throw new InvalidOperationException(message);
        }

        private static void ValidatePublicAndNonPublicBindingFlags(bool isPublic, bool isNonPublic)
        {
            if (isPublic || isNonPublic)
                return;

            var bindingFlagsName = typeof(ReflectionFlags).Name;
            var message = String.Format("{0} must at least specify either {1} or {2}",
                bindingFlagsName,
                ReflectionFlags.Public,
                ReflectionFlags.NonPublic);

            throw new InvalidOperationException(message);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private const ReflectionFlags DefaultConstructorBindingFlags =
            ReflectionFlags.Public;

        private const ReflectionFlags DefaultMethodBindingFlags =
            ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static;

        private const ReflectionFlags DefaultPropertyBindingFlags =
            ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static;

        private static readonly Type[] EmptyTypes = new Type[0];

        private static readonly HashSet<Type> FloatingPointTypes = new HashSet<Type>
            {
                typeof(decimal),
                typeof(double),
                typeof(float)
            };

        private static readonly HashSet<Type> IntegerTypes = new HashSet<Type>
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

        private static readonly HashSet<Type> PrimitiveTypes = new HashSet<Type>
            {
                typeof(byte[]),
                typeof(decimal),
                typeof(string),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(Guid),
                typeof(TimeSpan),
                typeof(Type),
                typeof(Uri)
            };
        #endregion
    }
}