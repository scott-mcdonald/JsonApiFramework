// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    public static class ToOneResourceLinkage
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Properties
        public static IToOneResourceLinkage Create<TResourceId>(TResourceId resourceId)
        {
            var resourceLinkage = new ToOneResourceLinkage<TResourceId>(resourceId);
            return resourceLinkage;
        }

        public static IToOneResourceLinkage CreateNull()
        {
            var resourceLinkage = NullToOneResourceLinkage.Default;
            return resourceLinkage;
        }
        #endregion
    }
}