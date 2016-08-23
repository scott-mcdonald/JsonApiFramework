namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts any object that has settable <c>Type</c> and <c>Id</c> properties.
    /// </summary>
    /// <remarks>
    /// Resource identity per specification is any object with "type" and "id" string
    /// members that represent a "compound primary key" for the ecosystem of resources for a particular system.
    /// </remarks>
    public interface ISetResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Type { set; }
        string Id { set; }
        #endregion
    }
}