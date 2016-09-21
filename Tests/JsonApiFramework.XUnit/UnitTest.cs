using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

using JetBrains.Annotations;

namespace JsonApiFramework.XUnit
{
    /// <summary>Base class for an individual unit test for a xUnit test.</summary>
    public abstract class UnitTest : IUnitTest
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region IUnitTest Implementation
        public string Name { get; private set; }
        public Stopwatch Stopwatch { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////
        #region IUnitTest Implementation
        public void Execute(XUnitTest xUnitTest)
        {
            this.XUnitTest = xUnitTest;

            this.WriteLine("Test Name: {0}", this.Name);
            this.WriteLine();

            this.Arrange();
            this.Act();
            this.Assert();
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        { return this.Name; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////
        #region Constructors
        protected UnitTest(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.Stopwatch = new Stopwatch();
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected XUnitTest XUnitTest { get; private set; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////
        #region UnitTest Overrides
        protected virtual void Arrange()
        { }

        protected virtual void Act()
        { }

        protected virtual void Assert()
        { }
        #endregion

        #region Write Methods
        protected void WriteLine()
        { this.XUnitTest.WriteLine(); }

        protected void WriteLine(string message)
        { this.XUnitTest.WriteLine(message); }

        [StringFormatMethod("format")]
        protected void WriteLine(string format, params object[] args)
        { this.XUnitTest.WriteLine(format, args); }

        protected void WriteDashedLine()
        { this.XUnitTest.WriteDashedLine(); }
        #endregion
    }
}