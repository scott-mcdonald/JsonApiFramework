// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Reflection;

namespace JsonApiFramework.Server.Hypermedia
{
    public class HypermediaAssemblerRegistry : IHypermediaAssemblerRegistry
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaAssemblerRegistry()
        {
            this.Initialize(Enumerable.Empty<IHypermediaAssembler>());
        }

        public HypermediaAssemblerRegistry(IEnumerable<IHypermediaAssembler> hypermediaAssemblers)
        {
            Contract.Requires(hypermediaAssemblers != null);

            this.Initialize(hypermediaAssemblers);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaAssemblerRegistry Implementation
        public IHypermediaAssembler GetAssembler(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = clrResourceType;
            if (this.HypermediaAssemblerDictionary.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type>(clrPath1Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary1.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrPath2Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type, Type>(clrPath1Type, clrPath2Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary2.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrPath2Type != null);
            Contract.Requires(clrPath3Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type, Type, Type>(clrPath1Type, clrPath2Type, clrPath3Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary3.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrPath2Type != null);
            Contract.Requires(clrPath3Type != null);
            Contract.Requires(clrPath4Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type, Type, Type, Type>(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary4.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrPath5Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrPath2Type != null);
            Contract.Requires(clrPath3Type != null);
            Contract.Requires(clrPath4Type != null);
            Contract.Requires(clrPath5Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type, Type, Type, Type, Type>(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrPath5Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary5.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }

        public IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrPath5Type, Type clrPath6Type, Type clrResourceType)
        {
            Contract.Requires(clrPath1Type != null);
            Contract.Requires(clrPath2Type != null);
            Contract.Requires(clrPath3Type != null);
            Contract.Requires(clrPath4Type != null);
            Contract.Requires(clrPath5Type != null);
            Contract.Requires(clrPath6Type != null);
            Contract.Requires(clrResourceType != null);

            IHypermediaAssembler hypermediaAssembler;
            var key = new Tuple<Type, Type, Type, Type, Type, Type, Type>(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrPath5Type, clrPath6Type, clrResourceType);
            if (this.HypermediaAssemblerDictionary6.TryGetValue(key, out hypermediaAssembler))
            {
                return hypermediaAssembler;
            }

            var defaultAssembler = GetDefaultAssembler();
            return defaultAssembler;
        }
        #endregion

        #region Methods
        public static IHypermediaAssembler GetDefaultAssembler()
        { return DefaultHypermediaAssembler; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<Type, IHypermediaAssembler> HypermediaAssemblerDictionary { get; set; }
        private IDictionary<Tuple<Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary1 { get; set; }
        private IDictionary<Tuple<Type, Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary2 { get; set; }
        private IDictionary<Tuple<Type, Type, Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary3 { get; set; }
        private IDictionary<Tuple<Type, Type, Type, Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary4 { get; set; }
        private IDictionary<Tuple<Type, Type, Type, Type, Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary5 { get; set; }
        private IDictionary<Tuple<Type, Type, Type, Type, Type, Type, Type>, IHypermediaAssembler> HypermediaAssemblerDictionary6 { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void Initialize(IEnumerable<IHypermediaAssembler> hypermediaAssemblers)
        {
            Contract.Requires(hypermediaAssemblers != null);

            this.HypermediaAssemblerDictionary = new Dictionary<Type, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary1 = new Dictionary<Tuple<Type, Type>, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary2 = new Dictionary<Tuple<Type, Type, Type>, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary3 = new Dictionary<Tuple<Type, Type, Type, Type>, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary4 = new Dictionary<Tuple<Type, Type, Type, Type, Type>, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary5 = new Dictionary<Tuple<Type, Type, Type, Type, Type, Type>, IHypermediaAssembler>();
            this.HypermediaAssemblerDictionary6 = new Dictionary<Tuple<Type, Type, Type, Type, Type, Type, Type>, IHypermediaAssembler>();

            foreach (var hypermediaAssembler in hypermediaAssemblers)
            {
                var hypermediaAssemblerType = hypermediaAssembler.GetType();

                if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,,,>)))
                {
                    var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType5 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath5TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType6 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath6TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type, Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrPathType5, clrPathType6, clrResourceType);
                    this.HypermediaAssemblerDictionary6.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,,>)))
                {
                    var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType5 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath5TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrPathType5, clrResourceType);
                    this.HypermediaAssemblerDictionary5.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,,,,>)))
                {
                    var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType4 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath4TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrPathType4, clrResourceType);
                    this.HypermediaAssemblerDictionary4.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,,,>)))
                {
                    var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType3 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath3TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type, Type, Type>(clrPathType1, clrPathType2, clrPathType3, clrResourceType);
                    this.HypermediaAssemblerDictionary3.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,,>)))
                {
                    var clrPathType1 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrPathType2 = (Type)(hypermediaAssemblerType.GetProperty(ClrPath2TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type, Type>(clrPathType1, clrPathType2, clrResourceType);
                    this.HypermediaAssemblerDictionary2.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<,>)))
                {
                    var clrPath1Type = (Type)(hypermediaAssemblerType.GetProperty(ClrPath1TypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = new Tuple<Type, Type>(clrPath1Type, clrResourceType);
                    this.HypermediaAssemblerDictionary1.Add(key, hypermediaAssembler);
                }
                else if (hypermediaAssemblerType.IsImplementationOf(typeof(IHypermediaAssembler<>)))
                {
                    var clrResourceType = (Type)(hypermediaAssemblerType.GetProperty(ClrResourceTypePropertyName, GetPropertyBindingFlags).GetValue(hypermediaAssembler, null));

                    var key = clrResourceType;
                    this.HypermediaAssemblerDictionary.Add(key, hypermediaAssembler);
                }
                else
                {
                    var detail = ServerErrorStrings.InternalErrorExceptionDetailUnknownHypermediaAssembler
                                                   .FormatWith(hypermediaAssembler.Name);
                    throw new InternalErrorException(detail);
                }
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Properties
        private const BindingFlags GetPropertyBindingFlags = BindingFlags.Public | BindingFlags.Instance;

        private static readonly IHypermediaAssembler DefaultHypermediaAssembler = new HypermediaAssembler();

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
