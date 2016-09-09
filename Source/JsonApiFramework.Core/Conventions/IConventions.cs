using System.Collections.Generic;

namespace JsonApiFramework.Conventions
{
    public interface IConventions
    {
        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        IEnumerable<INamingConvention> ApiAttributeNamingConventions { get; }
        IEnumerable<INamingConvention> ApiTypeNamingConventions { get; }
        IEnumerable<IResourceTypeConvention> ResourceTypeConventions { get; }
        #endregion
    }
}