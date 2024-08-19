using System.Text.Json;

namespace JsonApiFramework;

/// <summary>
/// Extension methods for instances of <see cref="System.Text.Json.JsonElement"/>.
/// </summary>
public static class JsonElementExtensions
{
    public static IEnumerable<JsonElement> DescendantPropertyValues(this JsonElement element, string name, StringComparison comparison = StringComparison.Ordinal)
    {
        if (name == null)
            throw new ArgumentNullException();
        return DescendantPropertyValues(element, n => name.Equals(n, comparison));
    }

    public static IEnumerable<JsonElement> DescendantPropertyValues(this JsonElement element, Predicate<string> match)
    {
        ArgumentNullException.ThrowIfNull(match);
        var query = (Name: (string)null, Value: element).Traverse(
            t => t.Value.ValueKind switch
                {
                    JsonValueKind.Array => t.Value.EnumerateArray().Select(i => ((string)null, i)),
                    JsonValueKind.Object => t.Value.EnumerateObject().Select(p => (p.Name, p.Value)),
                    _ => Enumerable.Empty<(string, JsonElement)>(),
                }, false)
            .Where(t => t.Name != null && match(t.Name))
            .Select(t => t.Value);
        return query;
    }
}
