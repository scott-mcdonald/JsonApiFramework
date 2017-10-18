// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Xunit.Abstractions;

namespace JsonApiFramework.XUnit
{
    public abstract class XUnitSerializable : IXunitSerializable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Name { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IXunitSerializable Implementation
        public virtual void Deserialize(IXunitSerializationInfo info)
        {
            this.Name = info.GetValue<string>(nameof(this.Name));
        }

        public virtual void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(this.Name), this.Name, typeof(string));
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        { return this.Name; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected XUnitSerializable(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
        }
        #endregion
    }
}