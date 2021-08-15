namespace BWHazel.Api.Core.Nucleics
{
    /// <summary>
    /// Represents an amino acid.
    /// </summary>
    public class AminoAcid
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the single-character code.
        /// </summary>
        public char Code { get; set; }

        /// <summary>
        /// Gets or sets the triple-character code.
        /// </summary>
        public string CodeTriple { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="AminoAcid"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="codeTriple"></param>
        public AminoAcid(string name, char code, string codeTriple)
        {
            this.Name = name;
            this.Code = code;
            this.CodeTriple = codeTriple;
        }
    }
}
