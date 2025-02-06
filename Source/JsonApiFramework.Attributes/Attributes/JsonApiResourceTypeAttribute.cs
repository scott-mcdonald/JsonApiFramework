namespace JsonApiFramework.Attributes.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class JsonApiResourceTypeAttribute : Attribute
{
    /// <summary>
    /// Mark an entity class as JSON:API resource type, using convention based naming
    /// </summary>
    public JsonApiResourceTypeAttribute()
    {
    }

    /// <summary>
    /// Mark an entity class as JSON:API resource type, using a custom name
    /// </summary>
    /// <param name="resourceType"></param>
    public JsonApiResourceTypeAttribute(string resourceType)
    {
        ResourceType = resourceType;
    }

    public string? ResourceType { get; }
}
