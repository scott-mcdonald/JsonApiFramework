// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class ApiResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ApiResourceLinkageType Type { get; private set; }

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
                case ApiResourceLinkageType.ToOneResourceLinkage:
                    {
                        var toOneResourceLinkageAsString = this.ToOneResourceLinkage != null
                            ? this.ToOneResourceLinkage.ToString()
                            : "null";
                        return String.Format("{0} [type={1} data={2}]", TypeName, resourceLinkageTypeAsString, toOneResourceLinkageAsString);
                    }

                case ApiResourceLinkageType.ToManyResourceLinkage:
                    {
                        var toManyResourceLinkageAsString = String.Format("[{0}]", this.ToManyResourceLinkage
                            .Select(x => x.ToString())
                            .Aggregate((current, next) => current + ", " + next));
                        return String.Format("{0} [type={1} data={2}]", TypeName, resourceLinkageTypeAsString, toManyResourceLinkageAsString);
                    }

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(ApiResourceLinkageType).Name, resourceLinkageType);
                        throw new InternalErrorException(detail);
                    }
            }
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiResourceLinkage(ResourceIdentifier toOneResourceLinkage)
        {
            this.Type = ApiResourceLinkageType.ToOneResourceLinkage;
            this.ToOneResourceLinkage = toOneResourceLinkage;
        }

        internal ApiResourceLinkage(IEnumerable<ResourceIdentifier> toManyResourceLinkage)
        {
            this.Type = ApiResourceLinkageType.ToManyResourceLinkage;
            this.ToManyResourceLinkage = toManyResourceLinkage.EmptyIfNull();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ApiResourceLinkage).Name;
        #endregion
    }
}