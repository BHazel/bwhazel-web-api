using System.Text.Json;

namespace BWHazel.Api.Web.Tests.Integration;

/// <summary>
/// Pre-configured options for JSON serialisation.
/// </summary>
public static class JsonSerialisationConfiguration
{
    /// <summary>
    /// Gets the default configuration where property names are case-insensitive.
    /// </summary>
    public static JsonSerializerOptions Default => new()
    {
        PropertyNameCaseInsensitive = true
    };
}
