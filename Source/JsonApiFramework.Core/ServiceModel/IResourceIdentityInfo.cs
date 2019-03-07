// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Extension;

namespace JsonApiFramework.ServiceModel
{
    public interface IResourceIdentityInfo : IExtensibleObject<IResourceIdentityInfo>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ApiType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        string GetApiId(object clrResource);

        object GetClrId(object clrResource);
        string GetClrIdPropertyName();
        Type GetClrIdPropertyType();

        bool IsClrIdNull(object clrId);

        bool IsSingleton();

        void SetApiType(string apiType);

        void SetClrId(object clrResource, object clrId);

        string ToApiId(object clrId);

        object ToClrId(object clrId);
        #endregion
    }
}