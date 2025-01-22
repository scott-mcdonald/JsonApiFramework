using System.Reflection;
using JsonApiFramework.Attributes.Attributes;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Attributes.Extensions;

public static class ComplexTypeBuilderExtensions
{
    public static IComplexTypeBuilder<TResource> ApplyJsonApiAttributes<TResource>(this IComplexTypeBuilder<TResource> builder)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        var properties = typeof(TResource).GetProperties();

        foreach (var property in properties)
        {
            ApplyAttributes(builder, property);
        }

        return builder;
    }

    private static void ApplyAttributes<TResource>(IComplexTypeBuilder<TResource> builder, PropertyInfo property)
        where TResource : class
    {
        var jsonApiAttribute = property.GetCustomAttribute<JsonApiAttributeAttribute>();
        if (jsonApiAttribute is null)
        {
            return;
        }

        var attributeBuilder = builder.Attribute(property.Name, property.PropertyType);
        if (!string.IsNullOrEmpty(jsonApiAttribute.AttributeName))
        {
            attributeBuilder.SetApiPropertyName(jsonApiAttribute.AttributeName);
        }

        if (jsonApiAttribute.Ignore)
        {
            attributeBuilder.Ignore();
        }

        if (jsonApiAttribute.Hide)
        {
            attributeBuilder.Hide();
        }
    }

}
