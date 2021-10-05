using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BWHazel.Api.Core.Nucleics
{
    /// <summary>
    /// Generates nucleic and peptide acid chains according to the Central Dogma of
    /// Molecular Biology.
    /// </summary>
    public class CentralDogma
    {
        private const string DnaStrandPattern = "^[ACGT]*$";
        private const string RnaStrandPattern = "^[ACGU]*$";
        private const char DefaultUnknownSingleCharacter = '?';

        /// <summary>
        /// Gets or sets the codon mappings.
        /// </summary>
        public Codon[] Codons { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="CentralDogma"/> class.
        /// </summary>
        /// <remarks>
        /// The natually-occuring codons defined in the <see cref="NaturalAminoAcids"/> class
        /// are used for the codon mappings.
        /// </remarks>
        public CentralDogma()
            : this(NaturalAminoAcids.Codons)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CentralDogma"/> class.
        /// </summary>
        /// <param name="codons">The codons.</param>
        public CentralDogma(Codon[] codons)
        {
            this.Codons = codons;
        }

        /// <summary>
        /// Gets the complimentary DNA strand for the single-stranded DNA strand.
        /// </summary>
        /// <param name="ssDnaStrand">The single-stranded DNA strand.</param>
        /// <returns>The complimentary DNA strand.</returns>
        public string GetComplimentaryDna(string ssDnaStrand)
        {
            return this.GetNucleicAcid(ssDnaStrand, 'T', 'G', 'C', 'A');
        }

        /// <summary>
        /// Gets the messenger RNA strand for the single-stranded DNA strand.
        /// </summary>
        /// <param name="ssDnaStrand">The single-stranded DNA strand.</param>
        /// <returns>The messenger RNA strand.</returns>
        public string GetMessengerRna(string ssDnaStrand)
        {
            return this.GetNucleicAcid(ssDnaStrand, 'U', 'G', 'C', 'A');
        }

        /// <summary>
        /// Gets the peptide chain for the single-stranded DNA strand.
        /// </summary>
        /// <param name="ssDnaStrand">The single-stranded DNA strand.</param>
        /// <param name="tripleCode"></param>
        /// <returns></returns>
        public string GetPeptide(string ssDnaStrand, bool tripleCode)
        {
            return this.GetPeptide(ssDnaStrand, DefaultUnknownSingleCharacter.ToString(), tripleCode);
        }

        /// <summary>
        /// Gets the complimentary nucleic acid strand for the specified nucleic acid strand.
        /// </summary>
        /// <param name="strand">The nucleic acid strand.</param>
        /// <param name="a">The complimentary base for ATP.</param>
        /// <param name="c">The complimentary base for CTP.</param>
        /// <param name="g">The complimentary base for GTP.</param>
        /// <param name="tu">The complimentary base for TTP or UTP.</param>
        /// <param name="unknown">The character for an unknown base in the strand.</param>
        /// <returns>The complimentary nucleic acid strand.</returns>
        public string GetNucleicAcid(string strand, char a, char c, char g, char tu, char unknown = DefaultUnknownSingleCharacter)
        {
            StringBuilder nucleicAcidBuilder = new();
            foreach (char acidBase in strand)
            {
                char complimentaryBase = acidBase switch
                {
                    'A' => a,
                    'C' => c,
                    'G' => g,
                    'T' or 'U' => tu,
                    _ => unknown
                };

                nucleicAcidBuilder.Append(complimentaryBase);
            }

            return nucleicAcidBuilder.ToString();
        }

        /// <summary>
        /// Gets the peptide chain for a nucleic acid strand.
        /// </summary>
        /// <param name="strand">The nucleic acid strand.</param>
        /// <param name="unknown">The character for an unknown codon in the strand.</param>
        /// <param name="tripleCode">A value indicating whether amino acid triple-character codes should be used.</param>
        /// <returns>The peptide chain.</returns>
        public string GetPeptide(string strand, string unknown, bool tripleCode = false)
        {
            StringBuilder peptideChainBuilder = new();
            string currentPeptideCode = default;
            for (int i = 0; i < strand.Length; i += 3)
            {
                if (i + 3 <= strand.Length)
                {
                    string codonString =
                        strand
                            .Substring(i, 3)
                            .Replace('U', 'T');

                    Codon codon =
                        this.Codons
                            .FirstOrDefault(c => c.NuclideChain == codonString);

                    if (codon != null)
                    {
                        currentPeptideCode = tripleCode ?
                            $"{codon.AminoAcid.CodeTriple} " : codon.AminoAcid.Code.ToString();
                    }
                    else
                    {
                        currentPeptideCode = unknown;
                    }
                }
                else
                {
                    currentPeptideCode = unknown;
                }

                peptideChainBuilder.Append(currentPeptideCode);
            }

            return peptideChainBuilder.ToString();
        }

        /// <summary>
        /// Determines whether a nucleic acid strand contains valid bases.
        /// </summary>
        /// <param name="strand">The nucleic acid strand.</param>
        /// <returns><c>true</c> if the strand is valid, otherwise <c>false</c>.</returns>
        public bool IsNucleicAcidValidBases(string strand)
        {
            return Regex.IsMatch(strand, DnaStrandPattern) ^ Regex.IsMatch(strand, RnaStrandPattern);
        }

        /// <summary>
        /// Determines whether a nucleic acid strand has a valid length for peptide transformation.
        /// </summary>
        /// <param name="strand">The nucleic acid strand.</param>
        /// <returns><c>true</c> if the strand is valid, otherwise <c>false</c>.</returns>
        public bool IsNucleicAcidValidLengthForPeptides(string strand)
        {
            return strand.Length % 3 == 0;
        }
    }
}
