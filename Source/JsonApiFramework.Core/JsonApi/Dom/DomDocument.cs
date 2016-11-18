// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.JsonApi.Dom
{
    public class DomDocument : DomNode
    {
        // PUBLIC CONSTRUCTOR ///////////////////////////////////////////////
        #region Constructor
        public DomDocument(IServiceModel serviceModel)
            : base(DomNodeType.Document, "Document")
        {
            Contract.Requires(serviceModel != null);

            this.ServiceModel = serviceModel;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public IServiceModel ServiceModel { get; }
        #endregion
    }
}
