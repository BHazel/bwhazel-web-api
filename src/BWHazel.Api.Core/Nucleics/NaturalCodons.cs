namespace BWHazel.Api.Core.Nucleics;

/// <summary>
/// Represents naturally occuring amino acids and representative codons.
/// </summary>
public static class NaturalCodons
{
    private static AminoAcid StopPseudoAcid => new("STOP", '/', "STOP");

    /// <summary>
    /// Gets amino acid Alanine (A, Ala).
    /// </summary>
    public static AminoAcid Alanine => new("Alanine", 'A', "Ala");

    /// <summary>
    /// Gets amino acid Arginine (R, Arg).
    /// </summary>
    public static AminoAcid Arginine => new("Arginine", 'R', "Arg");

    /// <summary>
    /// Gets amino acid Asparagine (N, Asn).
    /// </summary>
    public static AminoAcid Asparagine => new("Asparagine", 'N', "Asn");

    /// <summary>
    /// Gets amino acid Aspartic Acid (D, Asp).
    /// </summary>
    public static AminoAcid AsparticAcid => new("Aspartic Acid", 'D', "Asp");

    /// <summary>
    /// Gets amino acid Cysteine (C, Cys).
    /// </summary>
    public static AminoAcid Cysteine => new("Cysteine", 'C', "Cys");

    /// <summary>
    /// Gets amino acid Glutamic Acid (E, Glu).
    /// </summary>
    public static AminoAcid GlutamicAcid => new("Glutamic Acid", 'E', "Glu");

    /// <summary>
    /// Gets amino acid Glutamine (Q, Gln).
    /// </summary>
    public static AminoAcid Glutamine => new("Glutamine", 'Q', "Gln");

    /// <summary>
    /// Gets amino acid Glycine (G, Gly).
    /// </summary>
    public static AminoAcid Glycine => new("Glycine", 'G', "Gly");

    /// <summary>
    /// Gets amino acid Histidine (H, His).
    /// </summary>
    public static AminoAcid Histidine => new("Histidine", 'H', "His");

    /// <summary>
    /// Gets amino acid Isoleucine (I, Ile).
    /// </summary>
    public static AminoAcid Isoleucine => new("Isoleucine", 'I', "Ile");

    /// <summary>
    /// Gets amino acid Leucine (L, Leu).
    /// </summary>
    public static AminoAcid Leucine => new("Leucine", 'L', "Leu");

    /// <summary>
    /// Gets amino acid Lysine (K, Lys).
    /// </summary>
    public static AminoAcid Lysine => new("Lysine", 'K', "Lys");

    /// <summary>
    /// Gets amino acid Methionine (M, Met).
    /// </summary>
    public static AminoAcid Methionine => new("Methionine", 'M', "Met");

    /// <summary>
    /// Gets amino acid Phenylalanine (F, Phe).
    /// </summary>
    public static AminoAcid Phenylalanine => new("Phenylalanine", 'F', "Phe");

    /// <summary>
    /// Gets amino acid Proline (P, Pro).
    /// </summary>
    public static AminoAcid Proline => new("Proline", 'P', "Pro");

    /// <summary>
    /// Gets amino acid Serine (S, Ser).
    /// </summary>
    public static AminoAcid Serine => new("Serine", 'S', "Ser");

    /// <summary>
    /// Gets amino acid Threonine (T, Thr).
    /// </summary>
    public static AminoAcid Threonine => new("Threonine", 'T', "Thr");

    /// <summary>
    /// Gets amino acid Tryptophan (W, Trp).
    /// </summary>
    public static AminoAcid Tryptophan => new("Tryptophan", 'W', "Trp");

    /// <summary>
    /// Gets amino acid Tyrosine (Y, Tyr).
    /// </summary>
    public static AminoAcid Tyrosine => new("Tyrosine", 'Y', "Tyr");

    /// <summary>
    /// Gets amino acid Valine (V, Val).
    /// </summary>
    public static AminoAcid Valine => new("Valine", 'V', "Val");

    /// <summary>
    /// Gets codons for all nucleic acid chains.
    /// </summary>
    public static Codon[] Codons => new Codon[]
    {
        new("AAA", Lysine),
        new("AAC", Asparagine),
        new("AAG", Lysine),
        new("AAT", Asparagine),
        new("ACA", Threonine),
        new("ACC", Threonine),
        new("ACG", Threonine),
        new("ACT", Threonine),
        new("AGA", Arginine),
        new("AGC", Serine),
        new("AGG", Arginine),
        new("AGT", Serine),
        new("ATA", Isoleucine),
        new("ATC", Isoleucine),
        new("ATG", Methionine),
        new("ATT", Isoleucine),
        new("CAA", Glutamine),
        new("CAC", Histidine),
        new("CAG", Glutamine),
        new("CAT", Histidine),
        new("CCA", Proline),
        new("CCC", Proline),
        new("CCG", Proline),
        new("CCT", Proline),
        new("CGA", Arginine),
        new("CGC", Arginine),
        new("CGG", Arginine),
        new("CGT", Arginine),
        new("CTA", Leucine),
        new("CTC", Leucine),
        new("CTG", Leucine),
        new("CTT", Leucine),
        new("GAA", GlutamicAcid),
        new("GAC", AsparticAcid),
        new("GAG", GlutamicAcid),
        new("GAT", AsparticAcid),
        new("GCA", Alanine),
        new("GCC", Alanine),
        new("GCG", Alanine),
        new("GCT", Alanine),
        new("GGA", Glycine),
        new("GGC", Glycine),
        new("GGG", Glycine),
        new("GGT", Glycine),
        new("GTA", Valine),
        new("GTC", Valine),
        new("GTG", Valine),
        new("GTT", Valine),
        new("TAA", StopPseudoAcid),
        new("TAC", Tyrosine),
        new("TAG", StopPseudoAcid),
        new("TAT", Tyrosine),
        new("TCA", Serine),
        new("TCC", Serine),
        new("TCG", Serine),
        new("TCT", Serine),
        new("TGA", StopPseudoAcid),
        new("TGC", Cysteine),
        new("TGG", Tryptophan),
        new("TGT", Cysteine),
        new("TTA", Leucine),
        new("TTC", Phenylalanine),
        new("TTG", Leucine),
        new("TTT", Phenylalanine)
    };
}