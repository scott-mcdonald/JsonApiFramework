// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomLinks : DomObject
        , IDomLinks
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinks(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomLinks(IEnumerable<DomProperty> domProperties)
            : base("links object", domProperties)
        { }
        #endregion
    }
}
