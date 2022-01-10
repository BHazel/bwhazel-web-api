using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BWHazel.Api.Web.Tests.Integration;

/// <summary>
/// Configures a web application in memory for testing.
/// </summary>
/// <typeparam name="TStartup">The application startup class.</typeparam>
public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private const string TestEnvironmentName = "Test";

    /// <summary>
    /// Gives a fixture an opportunity to configure the application before it gets built.
    /// </summary>
    /// <param name="builder">The web host builder.</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(TestEnvironmentName);
    }
}