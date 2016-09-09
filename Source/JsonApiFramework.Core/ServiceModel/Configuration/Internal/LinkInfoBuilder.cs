// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class LinkInfoBuilder : ILinkInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinkInfoBuilder(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var linkInfoFactory = CreateLinkInfoFactory(rel);
            this.LinkInfoFactory = linkInfoFactory;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal ILinkInfo CreateLinkInfo()
        {
            var linkInfo = this.LinkInfoFactory();

            if (this.LinkInfoModifierCollection == null)
                return linkInfo;

            foreach (var linkInfoModifier in this.LinkInfoModifierCollection)
            {
                linkInfoModifier(linkInfo);
            }

            return linkInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<LinkInfo> LinkInfoFactory { get; set; }
        private IList<Action<LinkInfo>> LinkInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<LinkInfo> CreateLinkInfoFactory(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return () => new LinkInfo(rel);
        }
        #endregion
    }
}