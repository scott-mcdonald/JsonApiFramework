// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts the value of a single node attribute (named value) intended
    /// to be within a node attribute collection.
    /// </summary>
    internal interface INodeAttributeValue
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Represents the string of a node attribute value when generating text
        /// describing a single node attribute as key="value" style of text.
        /// </summary>
        string ToNodeAttributeValueString();
        #endregion
    }
}