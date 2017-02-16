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
        public void Deserialize(IXunitSerializationInfo info)
        {
            this.Name = info.GetValue<string>(nameof(this.Name));
        }

        public void Serialize(IXunitSerializationInfo info)
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