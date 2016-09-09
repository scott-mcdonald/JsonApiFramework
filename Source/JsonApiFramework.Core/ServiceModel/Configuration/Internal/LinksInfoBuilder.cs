// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class LinksInfoBuilder : ILinksInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksInfoBuilder(Type clrDeclaringType, string clrPropertyName = null)
        {
            Contract.Requires(clrDeclaringType != null);

            var linksInfoFactory = CreateLinksInfoFactory(clrDeclaringType, clrPropertyName);
            this.LinksInfoFactory = linksInfoFactory;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal ILinksInfo CreateLinksInfo(IEnumerable<ILinkInfo> collection)
        {
            var linksInfo = this.LinksInfoFactory(collection);

            if (this.LinksInfoModifierCollection == null)
                return linksInfo;

            foreach (var linksInfoModifier in this.LinksInfoModifierCollection)
            {
                linksInfoModifier(linksInfo);
            }

            return linksInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<IEnumerable<ILinkInfo>, LinksInfo> LinksInfoFactory { get; set; }
        private IList<Action<LinksInfo>> LinksInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<IEnumerable<ILinkInfo>, LinksInfo> CreateLinksInfoFactory(Type clrDeclaringType, string clrPropertyName)
        {
            Contract.Requires(clrDeclaringType != null);

            Func<IEnumerable<ILinkInfo>, LinksInfo> linksInfoFactory = (collection) =>
                {
                    var collectionAsList = collection.SafeToList();

                    var isNotPartOfResource = String.IsNullOrWhiteSpace(clrPropertyName);
                    var linksInfo = isNotPartOfResource
                        ? new LinksInfo(collectionAsList)
                        : new LinksInfo(clrDeclaringType, clrPropertyName, collectionAsList);
                    return linksInfo;
                };
            return linksInfoFactory;
        }
        #endregion
    }
}