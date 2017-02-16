// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Properties;
using JsonApiFramework.Reflection;

namespace JsonApiFramework.Tree
{
    /// <summary>
    /// Abstracts a single attribute of a node attribute collection where the
    /// concept of an attribute represents a named value.
    /// </summary>
    /// <remarks>
    /// Attribute values should never be null and if they are, do not add the
    /// attribute to represent the attribute is not defined. This class assumes
    /// attribute values are never null.
    /// </remarks>
    public abstract class NodeAttribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the name of this attribute.</summary>
        public string Name { get; private set; }

        /// <summary>Gets the previous sibling attribute of this attribute.</summary>
        public NodeAttribute PreviousAttribute { get; internal set; }

        /// <summary>Gets the next sibling attribute of this attribute.</summary>
        public NodeAttribute NextAttribute { get; internal set; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected NodeAttribute(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
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
            var message = CoreErrorStrings.TreeExceptionNodeAttributeAlreadyAddedMessage.FormatWith(this.Name);
            throw new TreeException(message);
        }
        #endregion
    }

    /// <summary>
    /// Represents a single strongly value typed attribute of a node attribute
    /// collection. The value of this attribute is immutable.
    /// </summary>
    /// <typeparam name="TValue">Type of value to store within this node attribute.</typeparam>
    public class NodeAttribute<TValue> : NodeAttribute
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NodeAttribute(string name, TValue value)
            : base(name)
        {
            this.Value = value;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets the value of this attribute.</summary>
        public TValue Value { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            if (IsValueType == false)
            {
                var valueAsObject = (object)this.Value;
                if (valueAsObject == null)
                    return "{0}=null".FormatWith(this.Name);
            }

            var valueString = this.Value.ToString();
            return "{0}=\"{1}\"".FormatWith(this.Name, valueString);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Properties
        private static readonly bool IsValueType = TypeReflection.IsValueType(typeof(TValue));
        #endregion
    }
}
