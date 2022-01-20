using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BWHazel.Api.Web.Tests.Integration.Models;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Web.Tests.Integration.Controllers;

/// <summary>
/// Tests the API routes on the nucleics controller.
/// </summary>
public class NucleicsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
{
    private const string BaseUri = "http://localhost/api/nucleics/";
    private readonly TestWebApplicationFactory<Startup> webApplicationFactory;
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initialises a new instance of the <see cref="AtomicsControllerTests"/> class.
    /// </summary>
    /// <param name="webApplicationFactory">The web application factory.</param>
    public NucleicsControllerTests(TestWebApplicationFactory<Startup> webApplicationFactory)
    {
        webApplicationFactory.ClientOptions.BaseAddress = new Uri(BaseUri);
        this.httpClient = webApplicationFactory.CreateClient();
        this.webApplicationFactory = webApplicationFactory;
    }

    /// <summary>
    /// Tests the centraldogma route returns the correct value when given a valid DNA strand.
    /// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogma_WithValidDnaStrand_ReturnsCorrectValue()
    {
        string ssDna = "ATGCGTTTATGA";
        string routeUri = $"centralDogma/{ssDna}";

        TestCentralDogmaInfo? centralDogmaInfo = await this.httpClient.GetFromJsonAsync<TestCentralDogmaInfo>(routeUri, JsonSerialisationConfiguration.Default);

        string expectedCDna = "TACGCAAATACT";
        string expectedMRna = "UACGCAAAUACU";
        string expectedPeptide = "MRL/";
        centralDogmaInfo!.SsDna.ShouldBe(ssDna);
        centralDogmaInfo!.CDna.ShouldBe(expectedCDna);
        centralDogmaInfo!.MRna.ShouldBe(expectedMRna);
        centralDogmaInfo!.Peptide.ShouldBe(expectedPeptide);
    }

    /// <summary>
    /// Tests the cDna route returns complimentary DNA with complete mappings when given a valid DNA strand.
    /// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaCDna_WithValidDnaStrand_ReturnsComplimentaryDnaWithCompleteMapping()
    {
        string routeUri = $"centralDogma/ACGT/cDna";

        string cDna = await this.httpClient.GetStringAsync(routeUri);

        cDna.ShouldBe("TGCA");
    }

    /// <summary>
    /// Tests the cDna route returns complimentary DNA containing the default unknown character when given a DNA strand with unknown bases.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaCDna_WithDnaStrandContainingUnknownBase_ReturnsComplimentaryDnaWithDefaultUnknownCharacter()
    {
        string routeUri = $"centralDogma/ACBT/cDna";

        string cDna = await this.httpClient.GetStringAsync(routeUri);

        cDna.ShouldBe("TG?A");
    }

    /// <summary>
    /// Tests the mRna route returns messenger RNA with complete mappings when given a valid DNA strand.
    /// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaMRna_WithValidDnaStrand_ReturnsMessengerRnaWithCompleteMapping()
    {
        string routeUri = $"centralDogma/ACGT/mRna";

        string cDna = await this.httpClient.GetStringAsync(routeUri);

        cDna.ShouldBe("UGCA");
    }

    /// <summary>
    /// Tests the mRna route returns messenger RNA containing the default unknown character when given a DNA strand with unknown bases.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaMRna_WithDnaStrandContainingUnknownBase_ReturnsMessengerRnaWithDefaultUnknownCharacter()
    {
        string routeUri = $"centralDogma/ACBT/mRna";

        string cDna = await this.httpClient.GetStringAsync(routeUri);

        cDna.ShouldBe("UG?A");
    }

    /// <summary>
    /// Tests the peptide route returns a peptide with complete mappings when given a valid DNA strand.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaPeptide_WithValidDnaStrand_ReturnsPeptideWithCompleteMapping()
    {
        string routeUri = $"centralDogma/ATGCGTTTATGA/peptide";

        string peptide = await this.httpClient.GetStringAsync(routeUri);

        peptide.ShouldBe("MRL/");
    }

    /// <summary>
    /// Tests the peptide route returns a peptide containing the default unknown character when given a DNA strand containing an unknown codon chain.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaPeptide_WithDnaStrandContainingUnknownBase_ReturnsPeptideWithDefaultUnknownCharacter()
    {
        string routeUri = $"centralDogma/ATGCBTTTATGA/peptide";

        string peptide = await this.httpClient.GetStringAsync(routeUri);

        peptide.ShouldBe("M?L/");
    }

    /// <summary>
    /// Tests the peptide route returns a triple character code peptide with complete mappings when given a valid DNA strand and query string to use the triple code.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaPeptide_WithValidDnaStrandAndQueryToUseTripleCode_ReturnsTripleCodePeptideWithCompleteMapping()
    {
        string routeUri = $"centralDogma/ATGCGTTTATGA/peptide?tripleCode=true";

        string peptide = await this.httpClient.GetStringAsync(routeUri);

        peptide.ShouldBe("Met Arg Leu STOP");
    }

    /// <summary>
    /// Tests the peptide route returns a peptide containing the default unknown character when given a DNA strand containing an unknown codon chain and query string to use the triple code.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmaPeptide_WithDnaStrandContaningUnknownBaseAndQueryToUseTripleCode_ReturnsTripleCodePeptideWithCompleteMapping()
    {
        string routeUri = $"centralDogma/ATGCBTTTATGA/peptide?tripleCode=true";

        string peptide = await this.httpClient.GetStringAsync(routeUri);

        peptide.ShouldBe("Met ? Leu STOP");
    }

    /// <summary>
    /// Tests the peptide route returns a peptide with an unknown final amino acid when given a DNA strand containing a number of bases not equal to a multiple of 3.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CentralDogmanPeptide_WithValidDnaStrandContainingBasesNoMultipleOf3_ReturnsPeptideWithUnknownFinalAminoAcid()
    {
        string routeUri = $"centralDogma/ATGCGTTTATG/peptide?tripleCode=true";

        string peptide = await this.httpClient.GetStringAsync(routeUri);

        peptide.ShouldBe("Met Arg Leu ?");
    }
}