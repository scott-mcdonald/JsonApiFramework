using System.Reflection;
using JsonApiFramework.Attributes.Attributes;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Attributes.Extensions;

public static class ResourceTypeBuilderExtensions
{
    public static IResourceTypeBuilder<TResource> ApplyJsonApiAttributes<TResource>(this IResourceTypeBuilder<TResource> builder)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        ApplyHomeResourceAttribute(builder);

        var properties = typeof(TResource).GetProperties();

        foreach (var property in properties)
        {
            ApplyResourceIdentity(builder, property);
            ApplyAttributes(builder, property);
        }

        return builder;
    }

    private static void ApplyHomeResourceAttribute<TResource>(IResourceTypeBuilder<TResource> builder)
        where TResource : class
    {
        var jsonApiAttribute = typeof(TResource).GetCustomAttribute<JsonApiHomeResourceAttribute>();
        if (jsonApiAttribute is null)
        {
            return;
        }

        // Explicitly set the JSON API type as "api-entry-point".
        // Configure ApiEntryPoint resource as a "singleton" by not specifying any identifier property.
        builder.ResourceIdentity()
            .SetApiType("api-entry-point");
    }

    private static void ApplyResourceIdentity<TResource>(IResourceTypeBuilder<TResource> builder, PropertyInfo property)
        where TResource : class
    {
        var jsonApiAttribute = property.GetCustomAttribute<JsonApiResourceIdentityAttribute>();
        if (jsonApiAttribute is null)
        {
            return;
        }

        var identityBuilder = builder.ResourceIdentity(property.Name, property.PropertyType);

        var jsonResourceAttribute = property.DeclaringType!.GetCustomAttribute<JsonApiResourceTypeAttribute>();
        if (jsonResourceAttribute?.ResourceType is not null)
        {
            identityBuilder.SetApiType(jsonResourceAttribute.ResourceType);
        }
    }

    private static void ApplyAttributes<TResource>(IResourceTypeBuilder<TResource> builder, PropertyInfo property)
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
