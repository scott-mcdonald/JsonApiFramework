// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Internal
{
    internal class Id<T> : IId<T>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Id(T clrId)
        {
            this.ClrId = clrId;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IId<T> Implementation
        public T ClrId { get; }
        #endregion
    }
}