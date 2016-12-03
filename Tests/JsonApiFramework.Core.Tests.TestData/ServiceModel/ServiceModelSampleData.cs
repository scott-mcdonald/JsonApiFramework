// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Tests.ServiceModel
{
    public static class ServiceModelSampleData
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Service Models
        /// <summary>
        /// Blog and related resources service model.
        /// </summary>
        /// <remarks>
        /// This service model has CLR resources that have Relationships, Links, and Meta CLR properties defined.
        /// </remarks>
        public static readonly IServiceModel ServiceModelWithBlogResourceTypes = default(IServiceModel);
        #endregion
    }
}
