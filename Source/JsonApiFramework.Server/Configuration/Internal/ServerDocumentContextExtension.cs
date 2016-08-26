// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Http;
using JsonApiFramework.Reflection;
using JsonApiFramework.Server.Hypermedia;

namespace JsonApiFramework.Server.Internal
{
    /// <summary>Server document context extension. Internal use only</summary>
    internal class ServerDocumentContextExtension : IDocumentContextExtension
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Server URL builder configuration.</summary>
        public IUrlBuilderConfiguration UrlBuilderConfiguration { get; set; }

        /// <summary>Server hypermedia assembler registry for custom hypermedia assemblers.</summary>
        public IHypermediaAssemblerRegistry HypermediaAssemblerRegistry { get; set; }

        /// <summary>Server hypermedia context for building framework-level hypermedia.</summary>
        public IHypermediaContext HypermediaContext { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void ValidateConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            this.ValidateUrlBuilderConfigurationConfiguration(configurationErrorMessages);
            this.ValidateHypermediaAssemblerRegistryConfiguration(configurationErrorMessages);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Validate Methods
        private void ValidateUrlBuilderConfigurationConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            // UrlBuilderConfiguration is required and can not be null.
            if (this.UrlBuilderConfiguration != null)
                return;

            var configurationErrorMessage = InfrastructureErrorStrings
                .DocumentContextExtensionValidationConfigurationError
                .FormatWith(this.GetMemberName(x => x.UrlBuilderConfiguration));
            configurationErrorMessages.Add(configurationErrorMessage);
        }

        private void ValidateHypermediaAssemblerRegistryConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            // HypermediaAssemblerRegistry is optional.
        }
        #endregion
    }
}