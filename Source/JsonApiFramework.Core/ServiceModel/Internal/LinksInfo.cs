// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class LinksInfo : PropertyInfo
        , ILinksInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksInfo(IEnumerable<ILinkInfo> collection)
            : base(null, null)
        {
            Contract.Requires(collection != null);

            this.Collection = collection.SafeToList();
        }

        public LinksInfo(string clrLinksPropertyName, IEnumerable<ILinkInfo> collection)
            : base(clrLinksPropertyName, typeof(Links))
        {
            Contract.Requires(collection != null);

            this.Collection = collection.SafeToList();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ILinksInfo Implementation
        [JsonProperty] public IEnumerable<ILinkInfo> Collection { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region InfoObject Overrides
        public override void Initialize(IServiceModel serviceModel, IResourceType resourceType)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(resourceType != null);

            base.Initialize(serviceModel, resourceType);

            foreach (var link in this.Collection)
            {
                link.Initialize(serviceModel, resourceType);
            }
        }
        #endregion

        #region ILinksInfo Implementation
        public ILinkInfo GetLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            ILinkInfo link;
            if (this.TryGetLink(rel, out link))
                return link;

            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType), this.ResourceType.ClrResourceType.Name);
            var linkDescription = "{0} [rel={1}]".FormatWith(typeof(Link).Name, rel);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(resourceTypeDescription, linkDescription);
            throw new ServiceModelException(detail);
        }

        public bool TryGetLink(string rel, out ILinkInfo link)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            link = this.Collection.SingleOrDefault(x => x.Rel == rel);
            return link != null;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal LinksInfo()
        { }
        #endregion
    }
}