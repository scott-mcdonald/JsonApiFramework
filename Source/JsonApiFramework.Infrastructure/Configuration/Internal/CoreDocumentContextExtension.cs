// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Conventions;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    /// <summary>Core document context extension. Internal use only</summary>
    internal class CoreDocumentContextExtension : IDocumentContextExtension
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Core conventions.</summary>
        public IConventions Conventions { get; set; }

        /// <summary>Core service model.</summary>
        public IServiceModel ServiceModel { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void ValidateConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            this.ValidateConventionsConfiguration(configurationErrorMessages);
            this.ValidateServiceModelConfiguration(configurationErrorMessages);
           
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Validate Methods
        private void ValidateConventionsConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            // Conventions are optional.
        }

        private void ValidateServiceModelConfiguration(ICollection<string> configurationErrorMessages)
        {
            Contract.Requires(configurationErrorMessages != null);

            // ServiceModel is required and can not be null.
            if (this.ServiceModel != null)
                return;

            var configurationErrorMessage = InfrastructureErrorStrings
                .DocumentContextExtensionValidationConfigurationError
                .FormatWith(this.GetMemberName(x => x.ServiceModel));
            configurationErrorMessages.Add(configurationErrorMessage);
        }
        #endregion
    }
}