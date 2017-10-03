// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal;

namespace JsonApiFramework
{
    public static class Id
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Properties
        public static IId<T> Create<T>(T clrId)
        {
            var id = new Id<T>(clrId);
            return id;
        }
        #endregion
    }
}