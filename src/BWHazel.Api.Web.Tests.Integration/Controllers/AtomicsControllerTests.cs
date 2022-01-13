using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Web.Tests.Integration.Controllers;

/// <summary>
/// Tests for the API routes on the atomics controller.
/// </summary>
public class AtomicsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
{
    private const string BaseUri = "http://localhost/api/atomics/";
    private readonly TestWebApplicationFactory<Startup> webApplicationFactory;
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initialises a new instance of the <see cref="AtomicsControllerTests"/> class.
    /// </summary>
    /// <param name="webApplicationFactory">The web application factory.</param>
    public AtomicsControllerTests(TestWebApplicationFactory<Startup> webApplicationFactory)
    {
        webApplicationFactory.ClientOptions.BaseAddress = new Uri(BaseUri);
        this.httpClient = webApplicationFactory.CreateClient();
        this.webApplicationFactory = webApplicationFactory;
    }

    /// <summary>
    /// Tests the dbe route returns the correct value when given valid numbers of carbon and hydrogen atoms.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="hydrogenAtoms">The number of hydrogen atoms.</param>
    /// <param name="expectedDbe">The expected double-bond equivalents.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Theory]
    [InlineData("1", "4", 0)]
    [InlineData("2", "6", 0)]
    [InlineData("2", "4", 1)]
    [InlineData("2", "2", 2)]
    [InlineData("2", "3", 1.5)]
    [InlineData("4", "5", 2.5)]
    public async Task DbeCH_WithValidCarbonAndHydrogenAtoms_ReturnsCorrectValue(string carbonAtoms, string hydrogenAtoms, double expectedDbe)
    {
        string routeUri = $"dbe/c/{carbonAtoms}/h/{hydrogenAtoms}";
        double dbe = await this.httpClient.GetFromJsonAsync<double>(routeUri);

        dbe.ShouldBe(expectedDbe);
    }

    /// <summary>
    /// Tests the dbe route returns a bad request when given a number of hydrogen atoms greater than in a saturated compound.
    /// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task DbeCH_WithHydrogenAtomsGreaterThanInSaturatedCompound_ReturnsBadRequest()
    {
        string routeUri = $"dbe/c/2/h/10";

        HttpResponseMessage response = await this.httpClient.GetAsync(routeUri);
        string responseMessage = await response.Content.ReadAsStringAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        responseMessage.ShouldBe("The number of hydrogen atoms is larger than in the saturated compound.");
    }

    /// <summary>
    /// Tests the saturated route returns the correct value when given a positive number of carbon atoms.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="expectedHydrogenAtoms">The expected number of hydrogen atoms.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Theory]
    [InlineData("1", 4)]
    [InlineData("2", 6)]
    [InlineData("3", 8)]
    [InlineData("4", 10)]
    [InlineData("10", 22)]
    [InlineData("20", 42)]
    public async Task DbeCSaturated_WithPositiveCarbonAtoms_ReturnsCorrectValue(string carbonAtoms, uint expectedHydrogenAtoms)
    {
        string routeUri = $"dbe/c/{carbonAtoms}/saturated";

        uint hydrogenAtoms = await this.httpClient.GetFromJsonAsync<uint>(routeUri);

        hydrogenAtoms.ShouldBe(expectedHydrogenAtoms);
    }

    /// <summary>
    /// Tests the saturated route returns 2 hydrogen atoms when given 0 carbon atoms.
    /// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task DbeCSaturated_WithZeroCarbonAtoms_Returns2HydrogenAtoms()
    {
        string routeUri = $"dbe/c/0/saturated";

        uint hydrogenAtoms = await this.httpClient.GetFromJsonAsync<uint>(routeUri);

        hydrogenAtoms.ShouldBe(2u);
    }

    /// <summary>
    /// Tests an API route returns a bad request and problem details if given invalid atom values.
    /// </summary>
    /// <param name="route">The API route.</param>
    /// <param name="expectedInvalidAtoms">The expected invalid atom parameters.</param>
    /// <param name="expectedInvalidValues">The expected invalid atom values.</param>
    /// <returns></returns>
    [Theory]
    [InlineData("/c/x/h/6", new[] { "carbonAtoms" }, new[] { "x" })]
    [InlineData("/c/2/h/-6", new[] { "hydrogenAtoms" }, new[] { "-6" })]
    [InlineData("/c/2.5/h/x", new[] { "carbonAtoms", "hydrogenAtoms" }, new[] { "2.5", "x" })]
    [InlineData("/c/x/saturated", new[] { "carbonAtoms" }, new[] { "x" })]
    [InlineData("/c/-2/saturated", new[] { "carbonAtoms" }, new[] { "-2" })]
    [InlineData("/c/2.5/saturated", new[] { "carbonAtoms" }, new[] { "2.5" })]
    public async Task DbeRoute_WithInvalidAtoms_ShouldReturnBadRequestAndProblemDetails(string route, string[] expectedInvalidAtoms, string[] expectedInvalidValues)
    {
        string dbeBaseRouteUri = $"dbe";

        HttpResponseMessage response = await this.httpClient.GetAsync($"{dbeBaseRouteUri}{route}");
        ValidationProblemDetails? validationProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        response.Content.Headers.ContentType!.MediaType.ShouldBe("application/problem+json");
        validationProblemDetails!.Errors.ShouldNotBeEmpty();
        
        for (int i = 0; i < expectedInvalidAtoms.Length; i++)
        {
            string invalidKey = expectedInvalidAtoms[i];
            string invalidValue = expectedInvalidValues[i];

            validationProblemDetails!.Errors.ShouldContainKey(invalidKey);
            validationProblemDetails!.Errors[invalidKey].ShouldContain($"The value '{invalidValue}' is not valid.");
        }
    }
}