// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class MetaInfoBuilder : IMetaInfoBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public MetaInfoBuilder(string clrPropertyName = null)
        {
            var metaInfoFactory = CreateMetaInfoFactory(clrPropertyName);
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
        private static Func<MetaInfo> CreateMetaInfoFactory(string clrPropertyName)
        {
            Func<MetaInfo> metaInfoFactory = () =>
                {
                    if (String.IsNullOrWhiteSpace(clrPropertyName))
                        return null;

                    var metaInfo = new MetaInfo
                        {
                            // PropertyInfo Properties
                            ClrPropertyName = clrPropertyName,
                            ClrPropertyType = typeof(Meta)
                        };
                    return metaInfo;
                };
            return metaInfoFactory;
        }
        #endregion
    }
}