// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Metadata.Internal
{
    internal class RelationshipsInfo : IRelationshipsInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsInfo(IClrPropertyBinding clrRelationshipsPropertyBinding, IEnumerable<IRelationshipInfo> relationshipInfoCollection)
        {
            this.ClrRelationshipsPropertyBinding = clrRelationshipsPropertyBinding;
            this.RelationshipInfoCollection = relationshipInfoCollection.SafeToReadOnlyCollection();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IRelationshipsInfo Implementation
        public IClrPropertyBinding ClrRelationshipsPropertyBinding { get; }

        public IReadOnlyCollection<IRelationshipInfo> RelationshipInfoCollection { get; }
        #endregion
    }
}