// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel
{
    public interface IRelationshipInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Rel { get; }
        string ApiRelPathSegment { get; }
        Type ToClrType { get; }
        RelationshipCardinality ToCardinality { get; }
        RelationshipCanonicalRelPathMode ToCanonicalRelPathMode { get; }
        #endregion
    }
}