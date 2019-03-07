// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Represents a single node attribute (named value) intended to be
    /// within a node attribute collection.
    /// </summary>
    internal class NodeAttribute
    {
        // PUBLIC CONSTRUCTOR ///////////////////////////////////////////////
        #region Constructor
        public NodeAttribute(string name, object value = null)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.Value = value;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Node Overrides
        /// <summary>Gets the name of this attribute.</summary>
        public string Name { get; }

        /// <summary>Gets or sets the value of this attribute.</summary>
        public object Value { get; }
        #endregion

        #region Properties
        /// <summary>Gets the previous sibling attribute of this attribute.</summary>
        public NodeAttribute PreviousAttribute { get; internal set; }

        /// <summary>Gets the next sibling attribute of this attribute.</summary>
        public NodeAttribute NextAttribute { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        /// <summary>
        /// Returns a string representation of this attribute in a "name=value" style.
        /// </summary>
        public override string ToString()
        {
            if (this.Value == null)
                return "{0}=null".FormatWith(this.Name);

            var nodeAttributeValue = this.Value as INodeAttributeValue;
            var valueString = nodeAttributeValue != null
                ? nodeAttributeValue.ToNodeAttributeValueString()
                : this.Value.ToString();
            return "{0}=\"{1}\"".FormatWith(this.Name, valueString);
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal void ValidateHasNotBeenAdded()
        {
            if (this.PreviousAttribute == null && this.NextAttribute == null)
                return;

            this.ThrowExceptionForAlreadyBeenAdded();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ThrowExceptionForAlreadyBeenAdded()
        {
            var message = "{0} has already been added to a node.".FormatWith(this.Name);
            throw new TreeException(message);
        }
        #endregion
    }
}
