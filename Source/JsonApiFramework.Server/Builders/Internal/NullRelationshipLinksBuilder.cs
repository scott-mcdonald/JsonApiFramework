// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Server.Internal
{
    internal class NullRelationshipLinksBuilder<TParentBuilder> : NullLinksBuilder<IRelationshipLinksBuilder<TParentBuilder>, TParentBuilder>, IRelationshipLinksBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal NullRelationshipLinksBuilder(TParentBuilder parentBuilder)
            : base(parentBuilder)
        {
            this.Builder = this;
        }
        #endregion
    }
}
