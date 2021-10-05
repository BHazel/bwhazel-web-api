using System;
namespace BWHazel.Api.Web.Models
{
    /// <summary>
    /// Information on Central Dogma transformations.
    /// </summary>
    public class CentralDogmaInfo
    {
        /// <summary>
        /// Gets the single-stranded DNA strand.
        /// </summary>
        public string SsDna { get; internal set; }

        /// <summary>
        /// Gets the complimentary DNA strand.
        /// </summary>
        public string CDna { get; internal set; }

        /// <summary>
        /// Gets the messenger RNA strand.
        /// </summary>
        public string MRna { get; internal set; }

        /// <summary>
        /// Gets the peptide chain.
        /// </summary>
        public string Peptide { get; internal set; }

        /// <summary>
        /// Gets the protein.
        /// </summary>
        /// <remarks>
        /// This property is now obsolete and shall be removed on 1st January 2022.
        /// Please use "Peptide" instead.
        /// </remarks>
        [Obsolete]
        public string Protein => this.Peptide;
    }
}
