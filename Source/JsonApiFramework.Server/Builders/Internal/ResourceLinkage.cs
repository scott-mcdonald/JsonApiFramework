// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class ResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ResourceLinkageType Type { get; private set; }

        public ResourceIdentifier ToOneResourceLinkage { get; private set; }
        public IEnumerable<ResourceIdentifier> ToManyResourceLinkage { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var resourceLinkageType = this.Type;
            var resourceLinkageTypeAsString = resourceLinkageType.ToString();
            switch (resourceLinkageType)
            {
                case ResourceLinkageType.ToOneResourceLinkage:
                    {
                        var toOneResourceLinkageAsString = this.ToOneResourceLinkage != null
                            ? this.ToOneResourceLinkage.ToString()
                            : "null";
                        return String.Format("{0} [type={1} data={2}]", TypeName, resourceLinkageTypeAsString, toOneResourceLinkageAsString);
                    }

                case ResourceLinkageType.ToManyResourceLinkage:
                    {
                        var toManyResourceLinkageAsString = String.Format("[{0}]", this.ToManyResourceLinkage
                            .Select(x => x.ToString())
                            .Aggregate((current, next) => current + ", " + next));
                        return String.Format("{0} [type={1} data={2}]", TypeName, resourceLinkageTypeAsString, toManyResourceLinkageAsString);
                    }

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(ResourceLinkageType).Name, resourceLinkageType);
                        throw new InternalErrorException(detail);
                    }
            }
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ResourceLinkage(ResourceIdentifier toOneResourceLinkage)
        {
            this.Type = ResourceLinkageType.ToOneResourceLinkage;
            this.ToOneResourceLinkage = toOneResourceLinkage;
        }

        internal ResourceLinkage(IEnumerable<ResourceIdentifier> toManyResourceLinkage)
        {
            this.Type = ResourceLinkageType.ToManyResourceLinkage;
            this.ToManyResourceLinkage = EnumerableExtensions.EmptyIfNull(toManyResourceLinkage);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceLinkage).Name;
        #endregion
    }
}