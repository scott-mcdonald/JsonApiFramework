// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.Json;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    public abstract class DomXUnitTest : XUnitTest
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomXUnitTest(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal void OutputDomTree(DomNode domNode)
        {
            var treeString = domNode.ToTreeString();

            this.Output.WriteLine("DOM Tree");
            this.Output.WriteLine(String.Empty);
            this.Output.WriteLine(treeString);
        }

        internal void OutputEmptyLine()
        {
            this.Output.WriteLine(String.Empty);
        }

        internal void OutputJson(IJsonObject jsonObject)
        {
            var json = jsonObject.ToJson();

            this.Output.WriteLine("JSON");
            this.Output.WriteLine(String.Empty);
            this.Output.WriteLine(json);
        }
        #endregion
    }
}
