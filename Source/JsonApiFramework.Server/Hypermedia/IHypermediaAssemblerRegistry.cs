// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Server.Hypermedia
{
    public interface IHypermediaAssemblerRegistry
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IHypermediaAssembler GetAssembler(Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrPath5Type, Type clrResourceType);
        IHypermediaAssembler GetAssembler(Type clrPath1Type, Type clrPath2Type, Type clrPath3Type, Type clrPath4Type, Type clrPath5Type, Type clrPath6Type, Type clrResourceType);
        #endregion
    }
}
