// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.Internal.Dom
{
    internal class DomAttribute : Node<DomNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Attribute; } }

        public override string Name
        { get { return "Attribute ({0})".FormatWith(this.ApiPropertyName); } }
        #endregion

        #region Properties
        public JToken ApiAttribute { get; private set; }
        public string ApiPropertyName { get; private set; }

        public object ClrAttribute { get; private set; }
        public string ClrPropertyName { get; private set; }
        public Type ClrPropertyType { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomAttribute CreateFromApiResource(IAttributeInfo attribute, IGetAttributes apiGetAttributes)
        {
            Contract.Requires(attribute != null);
            Contract.Requires(apiGetAttributes != null);

            var apiAttribute = attribute.GetApiAttribute(apiGetAttributes);
            var apiPropertyName = attribute.ApiPropertyName;

            var clrPropertyType = attribute.ClrPropertyType;
            var clrPropertyName = attribute.ClrPropertyName;
            var clrAttribute = apiAttribute.ToObject(clrPropertyType);

            var domAttribute = new DomAttribute(apiAttribute, apiPropertyName, clrAttribute, clrPropertyName, clrPropertyType);
            return domAttribute;
        }

        public static DomAttribute CreateFromClrResource(IAttributeInfo attribute, object clrResource)
        {
            Contract.Requires(attribute != null);
            Contract.Requires(clrResource != null);

            var apiPropertyName = attribute.ApiPropertyName;
            var clrPropertyName = attribute.ClrPropertyName;
            var clrPropertyType = attribute.ClrPropertyType;

            var clrAttribute = attribute.GetClrProperty(clrResource);
            var apiAttribute = JToken.FromObject(clrAttribute);

            var domAttribute = new DomAttribute(apiAttribute, apiPropertyName, clrAttribute, clrPropertyName, clrPropertyType);
            return domAttribute;
        }

        public static DomAttribute CreateFromClrAttribute(IAttributeInfo attribute, object clrAttribute)
        {
            Contract.Requires(attribute != null);

            var apiPropertyName = attribute.ApiPropertyName;
            var clrPropertyName = attribute.ClrPropertyName;
            var clrPropertyType = attribute.ClrPropertyType;

            var apiAttribute = JToken.FromObject(clrAttribute);

            var domAttribute = new DomAttribute(apiAttribute, apiPropertyName, clrAttribute, clrPropertyName, clrPropertyType);
            return domAttribute;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private DomAttribute(JToken apiAttribute, string apiPropertyName, object clrAttribute, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(apiAttribute != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);
            Contract.Requires(clrAttribute != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);
            Contract.Requires(clrPropertyType != null);

            this.ApiAttribute = apiAttribute;
            this.ApiPropertyName = apiPropertyName;

            this.ClrAttribute = clrAttribute;
            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
        }
        #endregion
    }
}
