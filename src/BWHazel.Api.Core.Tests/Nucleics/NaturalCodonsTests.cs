using System.Linq;
using BWHazel.Api.Core.Nucleics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Nucleics;

/// <summary>
/// Tests for the <see cref="NaturalCodons"/> class.
/// </summary>
public class NaturalCodonsTests
{
    /// <summary>
    /// Tests the <see cref="NaturalCodons.Codons"/> property has the correct amino acid mappings.
    /// </summary>
    /// <param name="nuclideChain">The nuclide chain.</param>
    /// <param name="expectedAminoAcid">The expected amino acid.</param>
    [Theory]
    [InlineData("AAA", "Lysine")]
    [InlineData("AAC", "Asparagine")]
    [InlineData("AAG", "Lysine")]
    [InlineData("AAT", "Asparagine")]
    [InlineData("ACA", "Threonine")]
    [InlineData("ACC", "Threonine")]
    [InlineData("ACG", "Threonine")]
    [InlineData("ACT", "Threonine")]
    [InlineData("AGA", "Arginine")]
    [InlineData("AGC", "Serine")]
    [InlineData("AGG", "Arginine")]
    [InlineData("AGT", "Serine")]
    [InlineData("ATA", "Isoleucine")]
    [InlineData("ATC", "Isoleucine")]
    [InlineData("ATG", "Methionine")]
    [InlineData("ATT", "Isoleucine")]
    [InlineData("CAA", "Glutamine")]
    [InlineData("CAC", "Histidine")]
    [InlineData("CAG", "Glutamine")]
    [InlineData("CAT", "Histidine")]
    [InlineData("CCA", "Proline")]
    [InlineData("CCC", "Proline")]
    [InlineData("CCG", "Proline")]
    [InlineData("CCT", "Proline")]
    [InlineData("CGA", "Arginine")]
    [InlineData("CGC", "Arginine")]
    [InlineData("CGG", "Arginine")]
    [InlineData("CGT", "Arginine")]
    [InlineData("CTA", "Leucine")]
    [InlineData("CTC", "Leucine")]
    [InlineData("CTG", "Leucine")]
    [InlineData("CTT", "Leucine")]
    [InlineData("GAA", "Glutamic Acid")]
    [InlineData("GAC", "Aspartic Acid")]
    [InlineData("GAG", "Glutamic Acid")]
    [InlineData("GAT", "Aspartic Acid")]
    [InlineData("GCA", "Alanine")]
    [InlineData("GCC", "Alanine")]
    [InlineData("GCG", "Alanine")]
    [InlineData("GCT", "Alanine")]
    [InlineData("GGA", "Glycine")]
    [InlineData("GGC", "Glycine")]
    [InlineData("GGG", "Glycine")]
    [InlineData("GGT", "Glycine")]
    [InlineData("GTA", "Valine")]
    [InlineData("GTC", "Valine")]
    [InlineData("GTG", "Valine")]
    [InlineData("GTT", "Valine")]
    [InlineData("TAA", "STOP")]
    [InlineData("TAC", "Tyrosine")]
    [InlineData("TAG", "STOP")]
    [InlineData("TAT", "Tyrosine")]
    [InlineData("TCA", "Serine")]
    [InlineData("TCC", "Serine")]
    [InlineData("TCG", "Serine")]
    [InlineData("TCT", "Serine")]
    [InlineData("TGA", "STOP")]
    [InlineData("TGC", "Cysteine")]
    [InlineData("TGG", "Tryptophan")]
    [InlineData("TGT", "Cysteine")]
    [InlineData("TTA", "Leucine")]
    [InlineData("TTC", "Phenylalanine")]
    [InlineData("TTG", "Leucine")]
    [InlineData("TTT", "Phenylalanine")]
    public void Codons_SholdContainCorrectAminoAcidMappings(string nuclideChain, string expectedAminoAcid)
    {
        Codon? codon = NaturalCodons.Codons.FirstOrDefault(c => c.NuclideChain == nuclideChain);

        codon.ShouldNotBeNull();
        codon.AminoAcid.Name.ShouldBe(expectedAminoAcid);
    }
}