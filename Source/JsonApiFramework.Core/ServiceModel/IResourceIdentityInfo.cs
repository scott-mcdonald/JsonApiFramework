// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.ServiceModel
{
    public interface IResourceIdentityInfo : IInfoObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ApiType { get; }
        IPropertyInfo Id { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        string GetApiId(object clrResource);
        object GetClrId(object clrResource);

        bool IsClrIdNull(object clrId);

        void SetClrId(object clrResource, object clrId);

        string ToApiId(object clrId);
        object ToClrId(object clrId);
        #endregion
    }
}