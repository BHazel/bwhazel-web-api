namespace BWHazel.Api.Web.Tests.Integration.Models;

/// <summary>
/// Information on Central Dogma transformations.
/// </summary>
public class TestCentralDogmaInfo
{
    /// <summary>
    /// Gets the single-stranded DNA strand.
    /// </summary>
    public string SsDna { get; set; }

    /// <summary>
    /// Gets the complimentary DNA strand.
    /// </summary>
    public string CDna { get; set; }

    /// <summary>
    /// Gets the messenger RNA strand.
    /// </summary>
    public string MRna { get; set; }

    /// <summary>
    /// Gets the peptide chain.
    /// </summary>
    public string Peptide { get; set; }
}