// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>IGetJsonApi</c> interface.</summary>
    public static class GetJsonApiVersionExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Getter Extension Methods
        public static bool HasJsonApi(this IGetJsonApiVersion getJsonApi)
        {
            Contract.Requires(getJsonApi != null);

            return getJsonApi.JsonApiVersion != null;
        }
        #endregion
    }
}
