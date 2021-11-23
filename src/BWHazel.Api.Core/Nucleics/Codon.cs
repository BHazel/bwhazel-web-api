namespace BWHazel.Api.Core.Nucleics;

/// <summary>
/// Represents a codon.
/// </summary>
public class Codon
{
    /// <summary>
    /// Gets or sets the nuclide chain.
    /// </summary>
    /// <remarks>
    /// This takes the single-character codes for the nuclides.
    /// </remarks>
    public string NuclideChain { get; set; }

    /// <summary>
    /// Gets or sets the amino acid.
    /// </summary>
    public AminoAcid AminoAcid { get; set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="Codon"/> class.
    /// </summary>
    /// <param name="nuclideChain">The nuclide chain.</param>
    /// <param name="aminoAcid">The amino acid.</param>
    public Codon(string nuclideChain, AminoAcid aminoAcid)
    {
        this.NuclideChain = nuclideChain;
        this.AminoAcid = aminoAcid;
    }
}