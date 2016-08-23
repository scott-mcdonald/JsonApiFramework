// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class HypermediaPathExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IEnumerable<Type> GetClrResourceTypes(this IEnumerable<IHypermediaPath> hypermediaPaths)
        {
            if (hypermediaPaths == null)
                return Enumerable.Empty<Type>();

            var clrResourceTypes = hypermediaPaths.Where(x => x.HasClrResourceType())
                                                  .Select(x => x.GetClrResourceType())
                                                  .ToList();
            return clrResourceTypes;
        }

        public static bool HasClrResourceType(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType != HypermediaPathType.NonResourcePath;
        }

        public static bool HasClrResourceTypes(this IEnumerable<IHypermediaPath> hypermediaPaths)
        {
            if (hypermediaPaths == null)
                return false;

            var hasClrResourceTypes = hypermediaPaths.Any(x => x.HasClrResourceType());
            return hasClrResourceTypes;
        }

        public static bool IsNonResourcePath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.NonResourcePath;
        }

        public static bool IsResourceCollectionPath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.ResourceCollectionPath;
        }

        public static bool IsResourcePath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.ResourcePath;
        }

        public static bool IsToOneResourcePath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.ToOneResourcePath;
        }

        public static bool IsToManyResourceCollectionPath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.ToManyResourceCollectionPath;
        }

        public static bool IsToManyResourcePath(this IHypermediaPath hypermediaPath)
        {
            Contract.Requires(hypermediaPath != null);

            return hypermediaPath.HypermediaPathType == HypermediaPathType.ToManyResourcePath;
        }
        #endregion
    }
}
