using System.Diagnostics;

namespace JsonApiFramework.XUnit
{
    /// <summary>Abstracts an individual unit test for a xUnit test.</summary>
    public interface IUnitTest
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region Properties
        string Name { get; }
        Stopwatch Stopwatch { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////
        #region Methods
        void Execute(XUnitTest xUnitTest);
        #endregion
    }
}