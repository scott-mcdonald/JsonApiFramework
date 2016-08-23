// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework
{
    public class DocumentContextOptions<TDocumentContext> : IDocumentContextOptions
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContextOptions()
        {
            this._extensions = new Dictionary<Type, IDocumentContextExtension>();
        }

        public DocumentContextOptions(IEnumerable<IDocumentContextExtension> extensions)
        {
            Contract.Requires(extensions != null);

            this._extensions = extensions.ToDictionary(x => x.GetType());
        }

        public DocumentContextOptions(params IDocumentContextExtension[] extensions)
            : this(extensions.AsEnumerable())
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDocumentContextOptions Implementation
        public Type DocumentContextType
        { get { return typeof(TDocumentContext); } }

        public IEnumerable<IDocumentContextExtension> Extensions
        { get { return this._extensions.Values; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentContextOptions Implementation
        public void AddExtension<TExtension>(TExtension extension)
            where TExtension : class, IDocumentContextExtension
        {
            Contract.Requires(extension != null);

            var key = typeof(TExtension);
            this._extensions.Add(key, extension);
        }

        public TExtension GetExtension<TExtension>()
            where TExtension : class, IDocumentContextExtension
        {
            TExtension extension;
            if (this.TryGetExtension(out extension))
            {
                return extension;
            }

            // Extension does not exist.
            var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailMissingExtension
                                                   .FormatWith(typeof(DocumentContextOptions<TDocumentContext>).Name, typeof(TExtension).Name);
            throw new InternalErrorException(detail);
        }

        public bool TryGetExtension<TExtension>(out TExtension extension)
            where TExtension : class, IDocumentContextExtension
        {
            var key = typeof(TExtension);
            var value = default(IDocumentContextExtension);
            if (this._extensions.TryGetValue(key, out value))
            {
                extension = (TExtension)value;
                return true;
            }

            extension = null;
            return false;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly IDictionary<Type, IDocumentContextExtension> _extensions;
        #endregion
    }
}
