// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;
using JsonApiFramework.Extension;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.ServiceModel.Internal;

internal class AttributeInfo : PropertyInfo, IAttributeInfo
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public AttributeInfo(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType, string apiPropertyName, bool isComplexType)
        : base(clrDeclaringType, clrPropertyName, clrPropertyType)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(apiPropertyName) == false);

        this.ExtensionDictionary = new ExtensionDictionary<IAttributeInfo>(this);

        this.ApiPropertyName = apiPropertyName;
        this.IsComplexType = isComplexType;

        this.InitializeCollectionProperties(clrPropertyType);
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region IAttributeInfo Implementation
    public string ApiPropertyName { get; internal set; }

    public bool IsCollection { get; private set; }
    public bool IsComplexType { get; private set; }

    public Type ClrCollectionItemType { get; private set; }
    #endregion

    #region IExtensibleObject<T> Implementation
    [JsonIgnore] public IEnumerable<IExtension<IAttributeInfo>> Extensions => this.ExtensionDictionary.Extensions;
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    public void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary)
    {
        if (clrTypeToComplexTypeDictionary == null)
            return;

        var clrPropertyType = this.ClrPropertyType;
        if (clrPropertyType.IsPrimitive())
            return;

        if (this.IsCollection)
        {
            var clrCollectionItemType = this.ClrCollectionItemType;
            if (clrTypeToComplexTypeDictionary.ContainsKey(clrCollectionItemType) == false)
                return;
        }
        else
        {
            if (clrTypeToComplexTypeDictionary.ContainsKey(clrPropertyType) == false)
                return;
        }

        this.IsComplexType = true;
    }
    #endregion

    #region IExtensibleObject<T> Implementation
    public void AddExtension(IExtension<IAttributeInfo> extension)
    {
        Contract.Requires(extension != null);

        this.ExtensionDictionary.AddExtension(extension);
    }

    public void RemoveExtension(Type extensionType)
    {
        Contract.Requires(extensionType != null);

        this.ExtensionDictionary.RemoveExtension(extensionType);
    }

    public bool TryGetExtension(Type extensionType, out IExtension<IAttributeInfo> extension)
    {
        Contract.Requires(extensionType != null);

        return this.ExtensionDictionary.TryGetExtension(extensionType, out extension);
    }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal AttributeInfo()
    {
        this.ExtensionDictionary = new ExtensionDictionary<IAttributeInfo>(this);
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private void InitializeCollectionProperties(Type clrPropertyType)
    {
        if (clrPropertyType.IsPrimitive())
            return;

        Type clrCollectionItemType;
        if (!clrPropertyType.IsEnumerableOfT(out clrCollectionItemType))
            return;

        this.IsCollection = true;
        this.ClrCollectionItemType = clrCollectionItemType;
    }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private ExtensionDictionary<IAttributeInfo> ExtensionDictionary { get; }
    #endregion
}