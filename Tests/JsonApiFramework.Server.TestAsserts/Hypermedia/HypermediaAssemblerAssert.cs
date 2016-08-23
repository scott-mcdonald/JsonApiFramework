// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Reflection;
using JsonApiFramework.Server.Hypermedia;

using Xunit;

namespace JsonApiFramework.Server.TestAsserts.Hypermedia
{
    public static class HypermediaAssemblerAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IHypermediaAssembler expected, IHypermediaAssembler actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedType = expected.GetType();
            Assert.IsType(expectedType, actual);

            if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,,,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes7(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes7(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
                Assert.Equal(expectedTypes.Item3, actualTypes.Item3);
                Assert.Equal(expectedTypes.Item4, actualTypes.Item4);
                Assert.Equal(expectedTypes.Item5, actualTypes.Item5);
                Assert.Equal(expectedTypes.Item6, actualTypes.Item6);
                Assert.Equal(expectedTypes.Item7, actualTypes.Item7);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes6(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes6(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
                Assert.Equal(expectedTypes.Item3, actualTypes.Item3);
                Assert.Equal(expectedTypes.Item4, actualTypes.Item4);
                Assert.Equal(expectedTypes.Item5, actualTypes.Item5);
                Assert.Equal(expectedTypes.Item6, actualTypes.Item6);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes5(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes5(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
                Assert.Equal(expectedTypes.Item3, actualTypes.Item3);
                Assert.Equal(expectedTypes.Item4, actualTypes.Item4);
                Assert.Equal(expectedTypes.Item5, actualTypes.Item5);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,,,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes4(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes4(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
                Assert.Equal(expectedTypes.Item3, actualTypes.Item3);
                Assert.Equal(expectedTypes.Item4, actualTypes.Item4);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes3(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes3(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
                Assert.Equal(expectedTypes.Item3, actualTypes.Item3);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<,>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes2(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes2(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
                Assert.Equal(expectedTypes.Item2, actualTypes.Item2);
            }
            else if (expectedType.IsImplementationOf(typeof(IHypermediaAssembler<>)))
            {
                var expectedTypes = GetHypermediaAssemblerTypes1(expectedType, expected);
                var actualTypes = GetHypermediaAssemblerTypes1(expectedType, expected);

                Assert.Equal(expectedTypes.Item1, actualTypes.Item1);
            }
            else if (expectedType == DefaultHypermediaAssemblerType)
            {
                // NOOP
            } 
            else
            {
                Assert.True(false, "Unknown hypermedia assembler types.");
            }
        }

        public static void Equal(IEnumerable<IHypermediaAssembler> expected, IEnumerable<IHypermediaAssembler> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToList();
            var actualCollection = actual.SafeToList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedItem = expectedCollection[index];
                var actualItem = actualCollection[index];

                HypermediaAssemblerAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Tuple<Type, Type, Type, Type, Type, Type, Type> GetHypermediaAssemblerTypes7(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType5 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath5TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType6 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath6TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type, Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrPathType5, clrPathType6, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type, Type, Type, Type, Type, Type> GetHypermediaAssemblerTypes6(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType5 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath5TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrPathType5, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type, Type, Type, Type, Type> GetHypermediaAssemblerTypes5(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type, Type, Type, Type> GetHypermediaAssemblerTypes4(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type, Type, Type> GetHypermediaAssemblerTypes3(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type, Type>(clrPathType1, clrPathType2, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type, Type> GetHypermediaAssemblerTypes2(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type, Type>(clrPathType1, clrResourceType);
            return assemblerTypes;
        }

        private static Tuple<Type> GetHypermediaAssemblerTypes1(Type hypermediaAssemblerType, object hypermediaAssembler)
        {
            Assert.NotNull(hypermediaAssemblerType);
            Assert.NotNull(hypermediaAssembler);

            var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

            var assemblerTypes = new Tuple<Type>(clrResourceType);
            return assemblerTypes;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Properties
        private const BindingFlags GetPropertyBindingFlags = BindingFlags.Public | BindingFlags.Instance;

        private static readonly Type DefaultHypermediaAssemblerType = typeof(HypermediaAssembler);

        private static readonly string ClrResourceTypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object>>(x => x.ClrResourceType);
        private static readonly string ClrPath1TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object>>(x => x.ClrPath1Type);
        private static readonly string ClrPath2TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object, object>>(x => x.ClrPath2Type);
        private static readonly string ClrPath3TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object, object, object>>(x => x.ClrPath3Type);
        private static readonly string ClrPath4TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object, object, object, object>>(x => x.ClrPath4Type);
        private static readonly string ClrPath5TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object, object, object, object, object>>(x => x.ClrPath5Type);
        private static readonly string ClrPath6TypePropertyName = StaticReflection.GetMemberName<IHypermediaAssembler<object, object, object, object, object, object, object>>(x => x.ClrPath6Type);
        #endregion
    }
}