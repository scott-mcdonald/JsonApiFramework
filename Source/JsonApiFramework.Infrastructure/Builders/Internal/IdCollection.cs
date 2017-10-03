// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Internal
{
    internal class IdCollection<T> : IIdCollection<T>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public IdCollection(IEnumerable<T> clrIdCollection)
        {
            this.ClrIdCollection = clrIdCollection;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IIdCollection<T> Implementation
        public IEnumerable<T> ClrIdCollection { get; }
        #endregion
    }
}