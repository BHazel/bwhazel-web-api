using BWHazel.Api.Core.Nucleics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Nucleics;

/// <summary>
/// Tests for the <see cref="Codon"/> class.
/// </summary>
public class CodonTests
{
    /// <summary>
    /// Tests that the <see cref="Codon"/> class constructor sets the correct property values.
    /// </summary>
    [Fact]
    public void Constructor_SetsCorrectPropertyValues()
    {
        string nuclideChain = "XYZ";
        AminoAcid aminoAcid = new("Test Amino Acid", '!', "???");

        Codon codon = new(nuclideChain, aminoAcid);

        codon.NuclideChain.ShouldBe(nuclideChain);
        codon.AminoAcid.ShouldBe(aminoAcid);
    }
}

