// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Converters;
using JsonApiFramework.Tree;
using JsonApiFramework.Tree.Internal;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal abstract class DomNode : Node
        , IDomNode
        , IDomWriteable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomNode Implementation
        public DomNodeType Type { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomNode Implementation
        public virtual IDomDocument DomDocument()
        {
            var rootNode = this.RootNode;

            var domDocument = (IDomDocument)rootNode;
            return domDocument;
        }

        public IEnumerable<IDomNode> DomNodes()
        {
            return this.Nodes()
                       .Cast<IDomNode>();
        }

        public IEnumerable<IDomNode> DescendantDomNodes()
        {
            return this.DescendantNodes()
                       .Cast<IDomNode>();
        }
        #endregion

        #region IDomWriteable Implementation
        public virtual void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        { throw new NotImplementedException(); }
        #endregion

        //#region AddOrUpdate Methods
        //public void AddOrUpdateDomNode(ApiType apiType, DomNode newDomNode)
        //{
        //    Contract.Requires(newDomNode != null);

        //    var oldDomNode = this.GetDomNode(apiType);
        //    if (oldDomNode == null)
        //    {
        //        this.AddNode(newDomNode);
        //        return;
        //    }

        //    this.ReplaceNode(oldDomNode, newDomNode);
        //}
        //#endregion

        //#region Get Methods
        //public DomNode GetDomNode(ApiType apiType)
        //{
        //    return this.Nodes()
        //               .Cast<DomNode>()
        //               .SingleOrDefault(x => x.ApiType() == apiType);
        //}
        //#endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomNode(DomNodeType type, string name)
            : base(name)
        {
            this.Type = type;
        }

        protected DomNode(DomNodeType type, string name, DomNode domNode)
            : base(name, domNode)
        {
            this.Type = type;
        }

        protected DomNode(DomNodeType type, string name, IEnumerable<DomNode> domNodes)
            : base(name, domNodes)
        {
            this.Type = type;
        }
        #endregion

        // PROTECTED FIELDS /////////////////////////////////////////////////
        #region Constants
        protected static readonly ITypeConverter DefaultTypeConverter = new TypeConverter();
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Node Overrides
        internal override TreeStringNodeVisitor CreateTreeStringNodeVisitor()
        {
            var domTreeStringNodeVisitor = new DomTreeStringNodeVisitor();
            return domTreeStringNodeVisitor;
        }
        #endregion
    }
}
