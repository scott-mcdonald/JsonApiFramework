namespace JsonApiFramework.Attributes.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class JsonApiComplexTypeAttribute : Attribute
{
    /// <summary>
    /// Mark an entity class as JSON:API complex type
    /// </summary>
    public JsonApiComplexTypeAttribute()
    {
    }
}
