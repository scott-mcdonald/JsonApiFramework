// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class HypermediaAssemblerRegistryExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IHypermediaAssembler SafeGetAssembler(this IHypermediaAssemblerRegistry hypermediaAssemblerRegistry, IEnumerable<IHypermediaPath> hypermediaPaths)
        {
            if (hypermediaAssemblerRegistry == null || hypermediaPaths == null)
            {
                var hypermediaAssembler = HypermediaAssemblerRegistry.GetDefaultAssembler();
                return hypermediaAssembler;
            }

            var clrResourceTypes = hypermediaPaths.GetClrResourceTypes()
                                                  .SafeToReadOnlyList();
            var clrResourceTypesCount = clrResourceTypes.Count;
            switch (clrResourceTypesCount)
            {
                case 1:
                {
                    var clrResourceType = clrResourceTypes[0];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrResourceType);
                    return hypermediaAssembler;
                }

                case 2:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrResourceType = clrResourceTypes[1];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrResourceType);
                    return hypermediaAssembler;
                }

                case 3:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrPath2Type = clrResourceTypes[1];
                    var clrResourceType = clrResourceTypes[2];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrPath2Type, clrResourceType);
                    return hypermediaAssembler;
                }

                case 4:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrPath2Type = clrResourceTypes[1];
                    var clrPath3Type = clrResourceTypes[2];
                    var clrResourceType = clrResourceTypes[3];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrPath2Type, clrPath3Type, clrResourceType);
                    return hypermediaAssembler;
                }

                case 5:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrPath2Type = clrResourceTypes[1];
                    var clrPath3Type = clrResourceTypes[2];
                    var clrPath4Type = clrResourceTypes[3];
                    var clrResourceType = clrResourceTypes[4];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrResourceType);
                    return hypermediaAssembler;
                }

                case 6:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrPath2Type = clrResourceTypes[1];
                    var clrPath3Type = clrResourceTypes[2];
                    var clrPath4Type = clrResourceTypes[3];
                    var clrPath5Type = clrResourceTypes[4];
                    var clrResourceType = clrResourceTypes[5];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrPath5Type, clrResourceType);
                    return hypermediaAssembler;
                }

                case 7:
                {
                    var clrPath1Type = clrResourceTypes[0];
                    var clrPath2Type = clrResourceTypes[1];
                    var clrPath3Type = clrResourceTypes[2];
                    var clrPath4Type = clrResourceTypes[3];
                    var clrPath5Type = clrResourceTypes[4];
                    var clrPath6Type = clrResourceTypes[5];
                    var clrResourceType = clrResourceTypes[6];
                    var hypermediaAssembler = hypermediaAssemblerRegistry.GetAssembler(clrPath1Type, clrPath2Type, clrPath3Type, clrPath4Type, clrPath5Type, clrPath6Type, clrResourceType);
                    return hypermediaAssembler;
                }
            }

            var internalHypermediaPathSizeLimit = clrResourceTypesCount + 1;
            var detail = ServerErrorStrings.InternalErrorExceptionDetailExceededHypermediaPathSizeLimit
                                           .FormatWith(internalHypermediaPathSizeLimit);
            throw new InternalErrorException(detail);
        }
        #endregion
    }
}