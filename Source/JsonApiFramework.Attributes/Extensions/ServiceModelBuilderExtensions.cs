using System.Reflection;
using JsonApiFramework.Attributes.Attributes;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Attributes.Extensions;

public static class ServiceModelBuilderExtensions
{
    /// <summary>
    /// Discover and add all <see cref="IResourceTypeBuilder"/> and <see cref="IComplexTypeBuilder"/> implementations from the calling assembly.
    /// Also registers resources annotated with the <see cref="JsonApiResourceTypeAttribute"/> or <see cref="JsonApiComplexTypeAttribute"/> from the calling assembly.
    /// Also define home resource, when a class is annotated with the <see cref="JsonApiHomeResourceAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Relies heavily on reflection to discover and add the configurations. Use with caution, consider caching the service model.
    /// In the future, this could be improved by using source generation.
    /// </remarks>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static ServiceModelBuilder AddJsonApiConfigurations(this ServiceModelBuilder builder)
    {
        return AddJsonApiConfigurations(builder, Assembly.GetCallingAssembly());
    }

    /// <summary>
    /// Discover and add all <see cref="IResourceTypeBuilder"/> and <see cref="IComplexTypeBuilder"/> implementations from the specified assembly.
    /// Also registers resources annotated with the <see cref="JsonApiResourceTypeAttribute"/> or <see cref="JsonApiComplexTypeAttribute"/> from the specified assembly.
    /// Also define home resource, when a class is annotated with the <see cref="JsonApiHomeResourceAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Relies heavily on reflection to discover and add the configurations. Use with caution, consider caching the service model.
    /// In the future, this could be improved by using source generation.
    /// </remarks>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static ServiceModelBuilder AddJsonApiConfigurations<T>(this ServiceModelBuilder builder)
    {
        return AddJsonApiConfigurations(builder, typeof(T).Assembly);
    }
    
    /// <summary>
    /// Discover and add all <see cref="IResourceTypeBuilder"/> and <see cref="IComplexTypeBuilder"/> implementations from the specified assemblies.
    /// Also registers resources annotated with the <see cref="JsonApiResourceTypeAttribute"/> or <see cref="JsonApiComplexTypeAttribute"/> from the specified assemblies.
    /// Also define home resource, when a class is annotated with the <see cref="JsonApiHomeResourceAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Relies heavily on reflection to discover and add the configurations. Use with caution, consider caching the service model.
    /// In the future, this could be improved by using source generation.
    /// </remarks>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static ServiceModelBuilder AddJsonApiConfigurations(this ServiceModelBuilder builder, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(assemblies);

        AddResourceTypeClassConfigurations(builder, assemblies);
        // TODO add complex type class configurations

        AddResourceTypeAttributes(builder, assemblies);
        AddComplexTypeAttributes(builder, assemblies);

        AddHomeResource(builder, assemblies);

        return builder;
    }

    private static void AddResourceTypeAttributes(ServiceModelBuilder builder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.GetCustomAttributes<JsonApiResourceTypeAttribute>().Any());

        foreach (var type in types)
        {
            ApplyResourceTypeJsonApiAttributes(builder, type);
        }
    }

    private static void ApplyResourceTypeJsonApiAttributes(ServiceModelBuilder builder, Type type)
    {
        // We have to use reflection to call the generic method, since there is no non-generic method variant :(
        var resourceMethod = typeof(ServiceModelBuilder).GetMethod(nameof(ServiceModelBuilder.Resource));
        var genericResourceMethod = resourceMethod!.MakeGenericMethod(type);
        var resourceTypeBuilder = genericResourceMethod.Invoke(builder, null);

        var applyJsonApiAttributesMethod = typeof(ResourceTypeBuilderExtensions).GetMethod(nameof(ResourceTypeBuilderExtensions.ApplyJsonApiAttributes));
        var genericApplyJsonApiAttributesMethod = applyJsonApiAttributesMethod!.MakeGenericMethod(type);
        genericApplyJsonApiAttributesMethod.Invoke(null, [resourceTypeBuilder]);
    }

    private static void AddComplexTypeAttributes(ServiceModelBuilder builder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.GetCustomAttributes<JsonApiComplexTypeAttribute>().Any());

        foreach (var type in types)
        {
            ApplyComplexTypeJsonApiAttributes(builder, type);
        }
    }

    private static void ApplyComplexTypeJsonApiAttributes(ServiceModelBuilder builder, Type type)
    {
        // We have to use reflection to call the generic method, since there is no non-generic method variant :(
        var complexMethod = typeof(ServiceModelBuilder).GetMethod(nameof(ServiceModelBuilder.Complex));
        var genericComplexMethod = complexMethod!.MakeGenericMethod(type);
        var complexTypeBuilder = genericComplexMethod.Invoke(builder, null);

        var applyJsonApiAttributesMethod = typeof(ComplexTypeBuilderExtensions).GetMethod(nameof(ComplexTypeBuilderExtensions.ApplyJsonApiAttributes));
        var genericApplyJsonApiAttributesMethod = applyJsonApiAttributesMethod!.MakeGenericMethod(type);
        genericApplyJsonApiAttributesMethod.Invoke(null, [complexTypeBuilder]);
    }

    private static void AddHomeResource(ServiceModelBuilder builder, params Assembly[] assemblies)
    {
        var type = assemblies.SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(t => t.GetCustomAttributes<JsonApiHomeResourceAttribute>().Any());

        if (type is null)
        {
            // This means you have to manually configure the home resource in the configuration factory
            return;
        }

        // We have to use reflection to call the generic method, since there is no non-generic method variant :(
        var method = typeof(ServiceModelBuilder).GetMethod(nameof(builder.HomeResource));
        var genericMethod = method!.MakeGenericMethod(type);
        genericMethod.Invoke(builder, null);

        // Since the home resource is not annotated as resource type, we should apply the JSON API attributes explicitly
        ApplyResourceTypeJsonApiAttributes(builder, type);
    }

    private static void AddResourceTypeClassConfigurations(ServiceModelBuilder builder, params Assembly[] assemblies)
    {
        var resourceTypeBuilderType = typeof(ResourceTypeBuilder<>);

        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.BaseType is {IsGenericType: true} && t.BaseType.GetGenericTypeDefinition() == resourceTypeBuilderType);

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            builder.Configurations.Add((IResourceTypeBuilder)instance!);
        }
    }
}
