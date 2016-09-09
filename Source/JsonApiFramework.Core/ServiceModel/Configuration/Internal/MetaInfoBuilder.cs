// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class MetaInfoBuilder : IMetaInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public MetaInfoBuilder(Type clrDeclaringType, string clrPropertyName = null)
        {
            Contract.Requires(clrDeclaringType != null);

            var metaInfoFactory = CreateMetaInfoFactory(clrDeclaringType, clrPropertyName);
            this.MetaInfoFactory = metaInfoFactory;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Factory Methods
        internal IMetaInfo CreateMetaInfo()
        {
            var metaInfo = this.MetaInfoFactory();

            if (this.MetaInfoModifierCollection == null)
                return metaInfo;

            foreach (var metaInfoModifier in this.MetaInfoModifierCollection)
            {
                metaInfoModifier(metaInfo);
            }

            return metaInfo;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<MetaInfo> MetaInfoFactory { get; set; }
        private IList<Action<MetaInfo>> MetaInfoModifierCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<MetaInfo> CreateMetaInfoFactory(Type clrDeclaringType, string clrPropertyName)
        {
            Contract.Requires(clrDeclaringType != null);

            Func<MetaInfo> metaInfoFactory = () =>
                {
                    var isNotPartOfResource = String.IsNullOrWhiteSpace(clrPropertyName);
                    if (isNotPartOfResource)
                        return null;

                    var metaInfo = new MetaInfo(clrDeclaringType, clrPropertyName);
                    return metaInfo;
                };
            return metaInfoFactory;
        }
        #endregion
    }
}