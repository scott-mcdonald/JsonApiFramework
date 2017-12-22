// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomItem : DomNode
        , IDomItem
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomItem(int index)
            : base(DomNodeType.Item, "item")
        {
            this.Index = index;
        }

        public DomItem(int index, DomNode domItemValue)
            : base(DomNodeType.Item, "item", domItemValue)
        {
            this.Index = index;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomItem Implementation
        public int Index
        {
            get => this.GetAttributeValue<int>(IndexAttributeName);
            private set => this.SetAttributeValue(IndexAttributeName, value);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomProperty Implementation
        public IDomNode DomItemValue()
        {
            return this.Nodes()
                       .Cast<IDomNode>()
                       .SingleOrDefault();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string IndexAttributeName = "index";
        #endregion
    }
}
