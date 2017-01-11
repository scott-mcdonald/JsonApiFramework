using System.Diagnostics;

namespace JsonApiFramework.XUnit
{
    /// <summary>
    /// Abstracts an individual unit test executed inside an xUnitTests object.
    /// </summary>
    public interface IUnitTest
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Name { get; }
        Stopwatch Stopwatch { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Execute(XUnitTests xUnitTest);
        #endregion
    }
}