// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.ServiceModel.Conventions.Internal
{
    internal class NamingConventionsBuilder : INamingConventionsBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NamingConventionsBuilder()
        { this.NamingConventions = new List<INamingConvention>(); }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region INamingConventionsBuilder Implementation
        public INamingConventionsBuilder AddLowerCaseNamingConvention()
        {
            this.NamingConventions.Add(new LowerCaseNamingConvention());
            return this;
        }

        public INamingConventionsBuilder AddPluralNamingConvention()
        {
            this.NamingConventions.Add(new PluralNamingConvention());
            return this;
        }

        public INamingConventionsBuilder AddSingularNamingConvention()
        {
            this.NamingConventions.Add(new SingularNamingConvention());
            return this;
        }

        public INamingConventionsBuilder AddStandardMemberNamingConvention()
        {
            this.NamingConventions.Add(new StandardMemberNamingConvention());
            return this;
        }

        public INamingConventionsBuilder AddUpperCaseNamingConvention()
        {
            this.NamingConventions.Add(new UpperCaseNamingConvention());
            return this;
        }
        #endregion

        #region Factory Methods
        public IEnumerable<INamingConvention> Build()
        { return this.NamingConventions; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IList<INamingConvention> NamingConventions { get; set; }
        #endregion
    }
}