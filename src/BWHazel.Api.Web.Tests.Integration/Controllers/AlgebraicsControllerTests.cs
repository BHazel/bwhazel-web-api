using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BWHazel.Api.Web.Tests.Integration.Models;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Web.Tests.Integration.Controllers;

/// <summary>
/// Tests for the API routes on the algebraics controller.
/// </summary>
public class AlgebraicsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
{
	private const string BaseUri = "http://localhost/api/algebraics/";
	private readonly TestWebApplicationFactory<Startup> webApplicationFactory;
	private readonly HttpClient httpClient;

	/// <summary>
	/// Intiailises a new instance of the <see cref="AlgebraicsControllerTests"/> class.
	/// </summary>
	/// <param name="webApplicationFactory">The web application factory.</param>
	public AlgebraicsControllerTests(TestWebApplicationFactory<Startup> webApplicationFactory)
	{
		webApplicationFactory.ClientOptions.BaseAddress = new Uri(BaseUri);
		this.httpClient = webApplicationFactory.CreateClient();
		this.webApplicationFactory = webApplicationFactory;
	}

	/// <summary>
	/// Tests the matrix2 route returns the correct value when given valid elements.
	/// </summary>
	/// <param name="e11">The element in row 1, column 1.</param>
	/// <param name="e12">The element in row 1, column 2.</param>
	/// <param name="e21">The element in row 2, column 1.</param>
	/// <param name="e22">The element in row 2, column 2.</param>
	/// <param name="expectedRealMatrix2Info">The expected result represented as a <see cref="TestRealMatrix2Info"/> instance.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[MemberData(nameof(GetRealMatrix2InfoResultsForValidElements))]
	public async Task RealMatrix2_WithValidElements_ReturnsCorrectValue(string e11, string e12, string e21, string e22, TestRealMatrix2Info expectedRealMatrix2Info)
    {
		string routeUri = $"linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}";
		TestRealMatrix2Info? realMatrix2Info = await this.httpClient.GetFromJsonAsync<TestRealMatrix2Info>(routeUri, JsonSerialisationConfiguration.Default);

		realMatrix2Info!.Elements.ShouldBe(expectedRealMatrix2Info.Elements, 0.01);
		realMatrix2Info!.Determinant.ShouldBe(expectedRealMatrix2Info.Determinant, 0.01);
		realMatrix2Info!.Trace.ShouldBe(expectedRealMatrix2Info.Trace, 0.01);
		realMatrix2Info!.Eigenvalues.ShouldBe(expectedRealMatrix2Info.Eigenvalues, 0.01);
	}

	/// <summary>
	/// Tests the determinant route returns the correct value when given valid elements.
	/// </summary>
	/// <param name="e11">The element in row 1, column 1.</param>
	/// <param name="e12">The element in row 1, column 2.</param>
	/// <param name="e21">The element in row 2, column 1.</param>
	/// <param name="e22">The element in row 2, column 2.</param>
	/// <param name="expectedDeterminant">The expected determinant.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("1", "0", "0", "1", 1)]
	[InlineData("1", "0", "0", "-1", -1)]
	[InlineData("1", "2", "3", "4", -2)]
	[InlineData("-1", "2", "-3", "4", 2)]
	[InlineData("1.5", "3.7", "4.6", "8.2", -4.72)]
	[InlineData("1.5", "-3.7", "4.6", "-8.2", 4.72)]
	public async Task LinearMatrix2Determinant_WithValidElements_ReturnsCorrectValue(string e11, string e12, string e21, string e22, double expectedDeterminant)
	{
		string routeUri = $"linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/determinant";
		double determinant = await this.httpClient.GetFromJsonAsync<double>(routeUri);

		determinant.ShouldBe(expectedDeterminant, 0.01);
	}

	/// <summary>
	/// Tests the trace route returns the correct value when given valid elements.
	/// </summary>
	/// <param name="e11">The element in row 1, column 1.</param>
	/// <param name="e12">The element in row 1, column 2.</param>
	/// <param name="e21">The element in row 2, column 1.</param>
	/// <param name="e22">The element in row 2, column 2.</param>
	/// <param name="expectedTrace">The expected trace.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("1", "0", "0", "1", 2)]
	[InlineData("1", "0", "0", "-1", 0)]
	[InlineData("1", "2", "3", "4", 5)]
	[InlineData("-1", "2", "-3", "4", 3)]
	[InlineData("1.5", "3.7", "4.6", "8.2", 9.7)]
	[InlineData("1.5", "-3.7", "4.6", "-8.2", -6.7)]
	public async Task LinearMatrix2Trace_WithValidElements_ReturnsCorrectValue(string e11, string e12, string e21, string e22, double expectedTrace)
	{
		string routeUri = $"linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/trace";
		double trace = await this.httpClient.GetFromJsonAsync<double>(routeUri);

		trace.ShouldBe(expectedTrace, 0.01);
	}

	/// <summary>
	/// Tests the eigenvalues route returns the correct values when given valid elements.
	/// </summary>
	/// <param name="e11">The element in row 1, column 1.</param>
	/// <param name="e12">The element in row 1, column 2.</param>
	/// <param name="e21">The element in row 2, column 1.</param>
	/// <param name="e22">The element in row 2, column 2.</param>
	/// <param name="expectedEigenvalue1">The expected value for the first eigenvalue.</param>
	/// <param name="expectedEigenvalue2">The expected value for the second eigenvalue.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("1", "0", "0", "1", 1, 1)]
	[InlineData("1", "0", "0", "-1", 1, -1)]
	[InlineData("1", "2", "3", "4", 5.37, -0.37)]
	[InlineData("-1", "2", "-3", "4", 2, 1)]
	[InlineData("1.5", "3.7", "4.6", "8.2", 10.16, -0.46)]
	[InlineData("1.5", "-3.7", "4.6", "-8.2", -0.8, -5.9)]
	public async Task LinearMatrix2Eigenvalues_WithValidElements_ReturnsCorrectValues(string e11, string e12, string e21, string e22, double expectedEigenvalue1, double expectedEigenvalue2)
    {
		string routeUri = $"linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvalues";
		double[]? eigenvalues = await this.httpClient.GetFromJsonAsync<double[]>(routeUri);

		eigenvalues![0].ShouldBe(expectedEigenvalue1, 0.01);
		eigenvalues![1].ShouldBe(expectedEigenvalue2, 0.01);
	}

	/// <summary>
	/// Tests the eigenvector route returns the correct value when given valid elements.
	/// </summary>
	/// <param name="e11">The element in row 1, column 1.</param>
	/// <param name="e12">The element in row 1, column 2.</param>
	/// <param name="e21">The element in row 2, column 1.</param>
	/// <param name="e22">The element in row 2, column 2.</param>
	/// <param name="expectedEigenvectors1">The expected eigenvectors for the first eigenvalue.</param>
	/// <param name="expectedEigenvectors2">The expected eigenvectors for the second eigenvalue.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("1", "2", "3", "4", new[] { 1.45, 2.91, 4.37 }, new[] { -0.45, -0.91, -1.37 })]
	[InlineData("-1", "2", "-3", "4", new[] { -1.0, -2.0, -3.0 }, new[] { -0.67, -1.33, -2.0 })]
	[InlineData("1.5", "3.7", "4.6", "8.2", new[] { 1.88, 3.77, 5.65 }, new[] { -0.42, -0.85, -1.28 })]
	[InlineData("1.5", "-3.7", "4.6", "-8.2", new[] { -0.5, -1.0, -1.5 }, new[] { -1.61, -3.22, -4.83 })]
	public async Task LinearMatrix2Eigenvector_WithValidElements_ReturnsCorrectValue(string e11, string e12, string e21, string e22, double[] expectedEigenvectors1, double[] expectedEigenvectors2)
    {
		string matrix2BaseRouteUri = $"linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}";
		double[]? eigenvalues = await this.httpClient.GetFromJsonAsync<double[]>($"{matrix2BaseRouteUri}/eigenvalues");

		double[] eigenvectors1 = Enumerable.Range(1, 3)
			.Select(async value => await this.httpClient.GetFromJsonAsync<double>($"{matrix2BaseRouteUri}/eigenvector/{eigenvalues![0]}/{value}"))
			.Select(task => task.Result)
			.ToArray();

		double[] eigenvectors2 = Enumerable.Range(1, 3)
			.Select(async value => await this.httpClient.GetFromJsonAsync<double>($"{matrix2BaseRouteUri}/eigenvector/{eigenvalues![1]}/{value}"))
			.Select(task => task.Result)
			.ToArray();

		eigenvectors1.ShouldBe(expectedEigenvectors1, 0.01);
		eigenvectors2.ShouldBe(expectedEigenvectors2, 0.01);
	}

	/// <summary>
	/// Tests an API route returns a bad request and problem details if given an invalid parameter.
	/// </summary>
	/// <param name="route">The API route.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("/")]
	[InlineData("/determinant")]
	[InlineData("/trace")]
	[InlineData("/eigenvalues")]
	[InlineData("/eigenvector/1/1")]
	public async Task Route_WithInvalidParameter_ShouldReturnBadRequestAndProblemDetails(string route)
    {
		string matrix2BaseRouteUri = $"linear/matrix2/e11/1/e12/2/e21/x/e22/4";

		HttpResponseMessage response = await this.httpClient.GetAsync($"{matrix2BaseRouteUri}{route}");
		ValidationProblemDetails? validationProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

		string invalidParameter = "e21";
		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		response.Content.Headers.ContentType!.MediaType.ShouldBe("application/problem+json");
		validationProblemDetails!.Errors.ShouldNotBeEmpty();
		validationProblemDetails!.Errors.ShouldContainKey(invalidParameter);
		validationProblemDetails!.Errors[invalidParameter].ShouldContain("The value 'x' is not valid.");
	}

	/// <summary>
	/// Tests an API route returns a bad request and problem details if given an invalid parameter.
	/// </summary>
	/// <param name="route">The API route.</param>
	/// <param name="expectedInvalidParameters">The expected invalid parameters.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	[Theory]
	[InlineData("/", new[] { "e21" })]
	[InlineData("/determinant", new[] { "e21" })]
	[InlineData("/trace", new[] { "e21" })]
	[InlineData("/eigenvalues", new[] { "e21" })]
	[InlineData("/eigenvector/x/1", new[] { "e21", "eigenvalue" })]
	public async Task Route_WithMultipleInvalidElements_ReturnsBadRequestAndProblemDetails(string route, string[] expectedInvalidParameters)
	{
		string matrix2BaseRouteUri = "linear/matrix2/e11/x/e12/2/e21/x/e22/4";

		HttpResponseMessage response = await this.httpClient.GetAsync($"{matrix2BaseRouteUri}{route}");
		ValidationProblemDetails? validationProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

		string expectedValidationMessage = "The value 'x' is not valid.";
		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		response.Content.Headers.ContentType!.MediaType.ShouldBe("application/problem+json");
		validationProblemDetails!.Errors.ShouldNotBeEmpty();

		foreach (string invalidParameter in expectedInvalidParameters)
        {
			validationProblemDetails!.Errors.ShouldContainKey(invalidParameter);
			validationProblemDetails!.Errors[invalidParameter].ShouldContain(expectedValidationMessage);
        }
	}

	/// <summary>
	/// Creates a collection of test case input and output data for the
	/// <see cref="RealMatrix2_WithValidElements_ReturnsCorrectValue(string, string, string, string, TestRealMatrix2Info)"/> test.
	/// </summary>
	/// <returns>A collection of test case input and output data.</returns>
	private static IEnumerable<object[]> GetRealMatrix2InfoResultsForValidElements()
    {
		List<object[]> results = new()
		{
			new object[]
            {
				"1",
				"2",
				"3",
				"4",
				new TestRealMatrix2Info()
                {
					Elements = new[] { 1.0, 2.0, 3.0, 4.0 },
					Determinant = -2,
					Trace = 5,
					Eigenvalues = new[] { 5.37, -0.37 }
                }
            },
			new object[]
			{
				"-1",
				"2",
				"-3",
				"4",
				new TestRealMatrix2Info()
				{
					Elements = new[] { -1.0, 2.0, -3.0, 4.0 },
					Determinant = 2,
					Trace = 3,
					Eigenvalues = new[] { 2.0, 1.0 }
				}
			},
			new object[]
			{
				"1",
				"0",
				"0",
				"1",
				new TestRealMatrix2Info()
				{
					Elements = new[] { 1.0, 0.0, 0.0, 1.0 },
					Determinant = 1,
					Trace = 2,
					Eigenvalues = new[] { 1.0, 1.0 }
				}
			},
			new object[]
			{
				"1",
				"0",
				"0",
				"-1",
				new TestRealMatrix2Info()
				{
					Elements = new[] { 1.0, 0.0, 0.0, -1.0 },
					Determinant = -1,
					Trace = 0,
					Eigenvalues = new[] { 1.0, -1.0 }
				}
			},
			new object[]
			{
				"1.5",
				"3.7",
				"4.6",
				"8.2",
				new TestRealMatrix2Info()
				{
					Elements = new[] { 1.5, 3.7, 4.6, 8.2 },
					Determinant = -4.72,
					Trace = 9.7,
					Eigenvalues = new[] { 10.16, -0.46 }
				}
			},
			new object[]
			{
				"1.5",
				"-3.7",
				"4.6",
				"-8.2",
				new TestRealMatrix2Info()
				{
					Elements = new[] { 1.5, -3.7, 4.6, -8.2 },
					Determinant = 4.72,
					Trace = -6.7,
					Eigenvalues = new[] { -0.8, -5.9 }
				}
			}
		};

		return results;
    }
}