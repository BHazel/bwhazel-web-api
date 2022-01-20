using BWHazel.Api.Core.Nucleics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Nucleics;

/// <summary>
/// Tests for the <see cref="CentralDogma"/> class.
/// </summary>
public class CentralDogmaTests
{
    /// <summary>
    /// Tests the <see cref="CentralDogma.GetComplimentaryDna(string)"/> method returns DNA with the complete mapping when given a strand of known bases.
    /// </summary>
    [Fact]
    public void GetComplimentaryDna_WithStrandOfKnownBases_ReturnsDnaWithCompleteMapping()
    {
        string ssDnaStrand = "ACGT";
        CentralDogma centralDogma = new();

        string complimentaryDna = centralDogma.GetComplimentaryDna(ssDnaStrand);

        complimentaryDna.ShouldBe("TGCA");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetComplimentaryDna(string)"/> method returns DNA containing the default unknown character when given a strand containing an unknown base.
    /// </summary>
    [Fact]
    public void GetComplimentaryDna_WithStrandContainingUnknownBase_ReturnsDnaWithDefaultUnknownCharacter()
    {
        string ssDnaStrand = "ACBT";
        CentralDogma centralDogma = new();

        string complimentaryDna = centralDogma.GetComplimentaryDna(ssDnaStrand);

        complimentaryDna.ShouldBe("TG?A");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetComplimentaryDna(string)"/> method returns an empty string when given an empty strand.
    /// </summary>
    [Fact]
    public void GetComplimentaryDna_WithEmptyStrand_ReturnsEmptyString()
    {
        string ssDnaStrand = string.Empty;
        CentralDogma centralDogma = new();

        string complimentaryDna = centralDogma.GetComplimentaryDna(ssDnaStrand);

        complimentaryDna.ShouldBe(string.Empty);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetMessengerRna(string)"/> method returns RNA with a complete mapping when given a strand of known bases.
    /// </summary>
    [Fact]
    public void GetMessengerRna_WithStrandOfKnownBases_ReturnsRnaWithCompleteMapping()
    {
        string ssDnaStrand = "ACGU";
        CentralDogma centralDogma = new();

        string messengerRna = centralDogma.GetMessengerRna(ssDnaStrand);

        messengerRna.ShouldBe("UGCA");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetMessengerRna(string)"/> method returns RNA with the default unknown character when given a strand containing an unknown base.
    /// </summary>
    [Fact]
    public void GetMessengerRna_WithStrandContainingUnknownBase_ReturnsRnaWithDefaultUnknownCharacter()
    {
        string ssDnaStrand = "ACBU";
        CentralDogma centralDogma = new();

        string messengerRna = centralDogma.GetMessengerRna(ssDnaStrand);

        messengerRna.ShouldBe("UG?A");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetMessengerRna(string)"/> method returns an empty string when given an empty strand.
    /// </summary>
    [Fact]
    public void GetMessengerRna_WithEmptyStrand_ReturnsEmptyString()
    {
        string ssDnaStrand = string.Empty;
        CentralDogma centralDogma = new();

        string messengerRna = centralDogma.GetMessengerRna(ssDnaStrand);

        messengerRna.ShouldBe(string.Empty);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetNucleicAcid(string, char, char, char, char, char)"/> method returns a nucleic acid with complete mapping when given a strand of known bases.
    /// </summary>
    /// <param name="strand">The nucleic acid strand.</param>
    [Theory]
    [InlineData("ACGT")]
    [InlineData("ACGU")]
    public void GetNucleicAcid_WithStrandOfKnownBases_ReturnsNucleicAcidWithCompleteMapping(string strand)
    {
        CentralDogma centralDogma = new();

        string nucleicAcid = centralDogma.GetNucleicAcid(strand, 'W', 'X', 'Y', 'Z');

        nucleicAcid.ShouldBe("WXYZ");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetNucleicAcid(string, char, char, char, char, char)"/> method returns a nucleic acid with the default unknown character when given a strand containing an unknown base and using the default unknown character.
    /// </summary>
    [Fact]
    public void GetNucleicAcid_WithStrandContainingUnknownBaseAndDefaultUnknownCharacter_ReturnsNucleicAcidWithDefaultUnknownCharacter()
    {
        string strand = "ACBT";
        CentralDogma centralDogma = new();

        string nucleicAcid = centralDogma.GetNucleicAcid(strand, 'W', 'X', 'Y', 'Z');

        nucleicAcid.ShouldBe("WX?Z");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetNucleicAcid(string, char, char, char, char, char)"/> method returns a nucleic acid with a custom unknown character when given a strand containing an unknown base and using a custom unknown character.
    /// </summary>
    [Fact]
    public void GetNucleicAcid_WithStrandContainingUnknownBaseAndCustomUnknownCharacter_ReturnsNucleicAcidWithCustomUnknownCharacter()
    {
        string strand = "ACBT";
        CentralDogma centralDogma = new();

        string nucleicAcid = centralDogma.GetNucleicAcid(strand, 'W', 'X', 'Y', 'Z', '!');

        nucleicAcid.ShouldBe("WX!Z");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, string, bool)"/> method returns a peptide with a complete mapping given a nucleic acid strand with length of a multiple of 3.
    /// </summary>
    /// <param name="strand">The nucleic acid strand.</param>
    /// <param name="useTripleCode">A value indicating whether the peptide triple character code should be used.</param>
    /// <param name="expectedPeptide">The expected peptide.</param>
    [Theory]
    [InlineData("AAA", false, "X")]
    [InlineData("BBB", false, "Y")]
    [InlineData("AAABBB", false, "XY")]
    [InlineData("AAABBBAAA", false, "XYX")]
    [InlineData("AAA", true, "Xxx")]
    [InlineData("BBB", true, "Yyy")]
    [InlineData("AAABBB", true, "Xxx Yyy")]
    [InlineData("AAABBBAAA", true, "Xxx Yyy Xxx")]
    public void GetPeptide_WithStrandOfLengthMultipleOf3_ReturnsPeptideWithCompleteMapping(string strand, bool useTripleCode, string expectedPeptide)
    {
        Codon[] codons = new Codon[]
        {
            new("AAA", new AminoAcid("Test Amino Acid X", 'X', "Xxx")),
            new("BBB", new AminoAcid("Test Amino Acid Y", 'Y', "Yyy"))
        };

        CentralDogma centralDogma = new(codons);

        string peptide = centralDogma.GetPeptide(strand, "?", useTripleCode);

        peptide.ShouldBe(expectedPeptide);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, string, bool)"/> method returns a peptide with an unknown final amino acid character when given a nucleic acid strand of length not equal to a multiple of 3.
    /// </summary>
    /// <param name="strand">The nucleic acid strand.</param>
    /// <param name="useTripleCode">A value indicating whether the peptide triple character code should be used.</param>
    /// <param name="expectedPeptide">The expected peptide.</param>
    [Theory]
    [InlineData("A", false, "?")]
    [InlineData("AA", false, "?")]
    [InlineData("AAAA", false, "X?")]
    [InlineData("A", true, "?")]
    [InlineData("AA", true, "?")]
    [InlineData("AAAA", true, "Xxx ?")]
    public void GetPeptide_WithStrandOfLengthNotMultipleOf3_ReturnsPeptideWithUnknownFinalAminoAcid(string strand, bool useTripleCode, string expectedPeptide)
    {
        Codon[] codons = new Codon[]
        {
            new("AAA", new AminoAcid("Test Amino Acid", 'X', "Xxx"))
        };

        CentralDogma centralDogma = new(codons);

        string peptide = centralDogma.GetPeptide(strand, "?", useTripleCode);

        peptide.ShouldBe(expectedPeptide);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, string, bool)"/> method returns a peptide with an unknown amino acid character when given a nucleic acid strand containing unknown codons.
    /// </summary>
    /// <param name="strand">The nucleic acid strand.</param>
    /// <param name="useTripleCode">A value indicating whether the peptide triple character should be used.</param>
    /// <param name="expectedPeptide">The expected peptide.</param>
    [Theory]
    [InlineData("AAB", false, "?")]
    [InlineData("ABAAAA", false, "?X")]
    [InlineData("ABAABA", false, "??")]
    [InlineData("ABAAAAABA", false, "?X?")]
    [InlineData("AAB", true, "?")]
    [InlineData("ABAAAA", true, "? Xxx")]
    [InlineData("ABAABA", true, "? ?")]
    [InlineData("ABAAAAABA", true, "? Xxx ?")]
    public void GetPeptide_WithStrandContainingUnknownCodonNuclideStrand_ReturnsPeptideWithUnknownAminoAcid(string strand, bool useTripleCode, string expectedPeptide)
    {
        Codon[] codons = new Codon[]
        {
            new("AAA", new AminoAcid("Test Amino Acid", 'X', "Xxx"))
        };

        CentralDogma centralDogma = new(codons);

        string peptide = centralDogma.GetPeptide(strand, "?", useTripleCode);

        peptide.ShouldBe(expectedPeptide);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, bool)"/> method returns an empty string when given an empty strand.
    /// </summary>
    /// <param name="useTripleCode">A value indicating whether the peptide triple character code should be used.</param>
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetPeptide_WithEmptyStrand_ReturnsEmptyString(bool useTripleCode)
    {
        string strand = string.Empty;
        CentralDogma centralDogma = new();

        string messengerRna = centralDogma.GetPeptide(strand, useTripleCode);

        messengerRna.ShouldBe(string.Empty);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, bool)"/> returns the correct amino acid when given a valid codon chain.
    /// </summary>
    /// <param name="codon">The codon.</param>
    /// <param name="expectedAminoAcid">The expected amino acid.</param>
    [Theory]
    [InlineData("AAA", "Lys")]
    [InlineData("AAC", "Asn")]
    [InlineData("AAG", "Lys")]
    [InlineData("AAT", "Asn")]
    [InlineData("ACA", "Thr")]
    [InlineData("ACC", "Thr")]
    [InlineData("ACG", "Thr")]
    [InlineData("ACT", "Thr")]
    [InlineData("AGA", "Arg")]
    [InlineData("AGC", "Ser")]
    [InlineData("AGG", "Arg")]
    [InlineData("AGT", "Ser")]
    [InlineData("ATA", "Ile")]
    [InlineData("ATC", "Ile")]
    [InlineData("ATG", "Met")]
    [InlineData("ATT", "Ile")]
    [InlineData("CAA", "Gln")]
    [InlineData("CAC", "His")]
    [InlineData("CAG", "Gln")]
    [InlineData("CAT", "His")]
    [InlineData("CCA", "Pro")]
    [InlineData("CCC", "Pro")]
    [InlineData("CCG", "Pro")]
    [InlineData("CCT", "Pro")]
    [InlineData("CGA", "Arg")]
    [InlineData("CGC", "Arg")]
    [InlineData("CGG", "Arg")]
    [InlineData("CGT", "Arg")]
    [InlineData("CTA", "Leu")]
    [InlineData("CTC", "Leu")]
    [InlineData("CTG", "Leu")]
    [InlineData("CTT", "Leu")]
    [InlineData("GAA", "Glu")]
    [InlineData("GAC", "Asp")]
    [InlineData("GAG", "Glu")]
    [InlineData("GAT", "Asp")]
    [InlineData("GCA", "Ala")]
    [InlineData("GCC", "Ala")]
    [InlineData("GCG", "Ala")]
    [InlineData("GCT", "Ala")]
    [InlineData("GGA", "Gly")]
    [InlineData("GGC", "Gly")]
    [InlineData("GGG", "Gly")]
    [InlineData("GGT", "Gly")]
    [InlineData("GTA", "Val")]
    [InlineData("GTC", "Val")]
    [InlineData("GTG", "Val")]
    [InlineData("GTT", "Val")]
    [InlineData("TAA", "STOP")]
    [InlineData("TAC", "Tyr")]
    [InlineData("TAG", "STOP")]
    [InlineData("TAT", "Tyr")]
    [InlineData("TCA", "Ser")]
    [InlineData("TCC", "Ser")]
    [InlineData("TCG", "Ser")]
    [InlineData("TCT", "Ser")]
    [InlineData("TGA", "STOP")]
    [InlineData("TGC", "Cys")]
    [InlineData("TGG", "Trp")]
    [InlineData("TGT", "Cys")]
    [InlineData("TTA", "Leu")]
    [InlineData("TTC", "Phe")]
    [InlineData("TTG", "Leu")]
    [InlineData("TTT", "Phe")]
    public void GetPeptide_WithValidCodonChain_ReturnsCorrectAminoAcid(string codon, string expectedAminoAcid)
    {
        CentralDogma centralDogma = new();

        string aminoAcid = centralDogma.GetPeptide(codon, true);

        aminoAcid.ShouldBe(expectedAminoAcid);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.GetPeptide(string, bool)"/> method returns a peptide with mappings for thymine when given a strand containing uracil.
    /// </summary>
    [Fact]
    public void GetPeptide_WithStrandContainingUracil_ReturnsPeptideWithMappingsForThymine()
    {
        Codon[] codons = new Codon[]
        {
            new("TTT", new AminoAcid("Test Amino Acid X", 'X', "Xxx")),
            new("TTA", new AminoAcid("Test Amino Acid Y", 'Y', "Yyy"))
        };

        CentralDogma centralDogma = new(codons);
        string peptide = centralDogma.GetPeptide("UUUUUA", false);

        peptide.ShouldBe("XY");
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.IsNucleicAcidValidBases(string)"/> method returns true when given a valid nucleic acid chain.
    /// </summary>
    /// <param name="nuclideChain">The nucleic acid chain.</param>
    /// <param name="expectedValidationResult">The expected validation result.</param>
    [Theory]
    [InlineData("", false)]
    [InlineData("A", true)]
    [InlineData("B", false)]
    [InlineData("C", true)]
    [InlineData("D", false)]
    [InlineData("G", true)]
    [InlineData("T", true)]
    [InlineData("?", false)]
    [InlineData("    ", false)]
    [InlineData("A AA", false)]
    [InlineData("AAAA", true)]
    [InlineData("CCCC", true)]
    [InlineData("GGGG", true)]
    [InlineData("TTTT", true)]
    [InlineData("ACGT", true)]
    [InlineData("A C G T", false)]
    [InlineData("TGCA", true)]
    [InlineData("ACGTTGCA", true)]
    public void IsNucleicAcidValidBases_WithValidNuclideChains_ReturnsTrue(string nuclideChain, bool expectedValidationResult)
    {
        CentralDogma centralDogma = new();

        bool isValid = centralDogma.IsNucleicAcidValidBases(nuclideChain);

        isValid.ShouldBe(expectedValidationResult);
    }

    /// <summary>
    /// Tests the <see cref="CentralDogma.IsNucleicAcidValidLengthForPeptides(string)"/> method returns true when given a nucleic acid with a length of a multiple of 3.
    /// </summary>
    /// <param name="nuclideChain">The nueleic acid chain.</param>
    /// <param name="expectedValidationResult">The expected validation result.</param>
    [Theory]
    [InlineData("", false)]
    [InlineData("A", false)]
    [InlineData("AA", false)]
    [InlineData("AAA", true)]
    [InlineData("AAAA", false)]
    [InlineData("AAAAA", false)]
    [InlineData("AAAAAA", true)]
    public void IsNucleicAcidValidLengthForPeptides_WithNuclideOfLengthMultipleOf3_ReturnsTrue(string nuclideChain, bool expectedValidationResult)
    {
        CentralDogma centralDogma = new();

        bool isValid = centralDogma.IsNucleicAcidValidLengthForPeptides(nuclideChain);

        isValid.ShouldBe(expectedValidationResult);
    }
}
