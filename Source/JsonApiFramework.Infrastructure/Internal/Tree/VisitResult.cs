// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Internal.Tree
{
    internal enum VisitResult
    {
        /// <summary>
        /// Visiting continues for both contained and sibling nodes (if any) in document order.
        /// </summary>
        Continue,

        /// <summary>
        /// Visiting continues for sibling nodes only (if any) in document order.
        /// </summary>
        ContinueWithSiblingNodesOnly,

        /// <summary>
        /// Visiting is done and should stop immediately.
        /// </summary>
        Done
    };
}