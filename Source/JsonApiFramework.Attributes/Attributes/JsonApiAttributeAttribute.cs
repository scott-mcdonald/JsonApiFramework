namespace JsonApiFramework.Attributes.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class JsonApiAttributeAttribute : Attribute
{
    /// <summary>
    /// Mark a property with the JSON:API attribute name, using convention based naming
    /// </summary>
    public JsonApiAttributeAttribute()
    {
    }

    /// <summary>
    /// Mark a property with the JSON:API attribute name, using a custom name
    /// </summary>
    /// <param name="attributeName"></param>
    public JsonApiAttributeAttribute(string attributeName)
    {
        AttributeName = attributeName;
    }

    public string? AttributeName { get; }

    public bool Ignore { get; init; }

    public bool Hide { get; init; }
}
