// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Internal;

namespace JsonApiFramework
{
    public static class ToManyResourceLinkage
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Properties
        public static IToManyResourceLinkage<TResourceId> Create<TResourceId>(IEnumerable<TResourceId> resourceIdCollection)
        {
            var resourceLinkage = new ToManyResourceLinkage<TResourceId>(resourceIdCollection);
            return resourceLinkage;
        }

        public static IToManyResourceLinkage CreateEmpty()
        {
            var resourceLinkage = NullToManyResourceLinkage.Default;
            return resourceLinkage;
        }
        #endregion
    }
}