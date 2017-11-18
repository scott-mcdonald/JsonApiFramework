// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Json;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ServiceModel : JsonObject, IServiceModel
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModel(string name, IEnumerable<IComplexType> complexTypes, IEnumerable<IResourceType> resourceTypes)
        {
            this.Name = name ?? nameof(ServiceModel);
            this.ComplexTypes = complexTypes.SafeToReadOnlyCollection();
            this.ResourceTypes = resourceTypes.SafeToReadOnlyCollection();

            // ReSharper disable PossibleMultipleEnumeration
            this.ClrTypeToComplexTypeDictionary = this.ComplexTypes
                                                      .ToDictionary(x => x.ClrType);

            this.ApiTypeToResourceTypeDictionary = this.ResourceTypes
                                                       .ToDictionary(x => x.ResourceIdentityInfo.ApiType);

            this.ClrTypeToResourceTypeDictionary = this.ResourceTypes
                                                       .ToDictionary(x => x.ClrType);

            var complexTypesAsTypeBase = this.ComplexTypes.Cast<ITypeBase>();
            var resourceTypesAsTypeBase = this.ResourceTypes.Cast<ITypeBase>();
            this.ClrTypeToTypeBaseDictionary = complexTypesAsTypeBase.Union(resourceTypesAsTypeBase).ToDictionary(x => x.ClrType);

            // ReSharper restore PossibleMultipleEnumeration
        }

        public ServiceModel(string name, IEnumerable<IResourceType> resourceTypes)
            : this(name, null, resourceTypes)
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IServiceModel Implementation
        public string Name { get; }
        public IReadOnlyCollection<IComplexType> ComplexTypes { get; }
        public IReadOnlyCollection<IResourceType> ResourceTypes { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IServiceModel Implementation
        public bool TryGetComplexType(Type clrComplexType, out IComplexType complexType)
        {
            if (clrComplexType != null)
                return this.ClrTypeToComplexTypeDictionary.TryGetValue(clrComplexType, out complexType);

            complexType = null;
            return false;
        }

        public bool TryGetResourceType(string apiResourceType, out IResourceType resourceType)
        {
            if (!String.IsNullOrWhiteSpace(apiResourceType))
                return this.ApiTypeToResourceTypeDictionary.TryGetValue(apiResourceType, out resourceType);

            resourceType = null;
            return false;
        }

        public bool TryGetResourceType(Type clrResourceType, out IResourceType resourceType)
        {
            if (clrResourceType != null)
                return this.ClrTypeToResourceTypeDictionary.TryGetValue(clrResourceType, out resourceType);

            resourceType = null;
            return false;
        }

        public TObject CreateClrObject<TObject>()
        {
            var clrObjectType = typeof(TObject);
            if (!this.TryGetTypeBase(clrObjectType, out var typeBase))
                return default(TObject);

            var typeBaseStronglyTyped = (ITypeBase<TObject>)typeBase;
            var clrObject = typeBaseStronglyTyped.CreateClrObject();
            return clrObject;
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        {
            var complexTypes = this.ComplexTypes.Any()
                ? this.ComplexTypes
                      .Select(x => x.ClrType.Name)
                      .Aggregate((current, next) => current + " " + next)
                : String.Empty;

            var resourceTypes = this.ResourceTypes.Any()
                ? this.ResourceTypes
                      .Select(x => x.ClrType.Name)
                      .Aggregate((current, next) => current + " " + next)
                : String.Empty;

            return $"{TypeName} [complexTypes=[{complexTypes}] resourceTypes=[{resourceTypes}]]";
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyDictionary<Type, IComplexType> ClrTypeToComplexTypeDictionary { get; }
        private IReadOnlyDictionary<string, IResourceType> ApiTypeToResourceTypeDictionary { get; }
        private IReadOnlyDictionary<Type, IResourceType> ClrTypeToResourceTypeDictionary { get; }
        private IReadOnlyDictionary<Type, ITypeBase> ClrTypeToTypeBaseDictionary { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        public bool TryGetTypeBase(Type clrObjectType, out ITypeBase typeBase)
        {
            if (clrObjectType != null)
                return this.ClrTypeToTypeBaseDictionary.TryGetValue(clrObjectType, out typeBase);

            typeBase = null;
            return false;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ServiceModel).Name;
        #endregion
    }
}