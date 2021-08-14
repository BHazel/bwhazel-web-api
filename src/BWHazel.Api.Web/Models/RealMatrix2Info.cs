namespace BWHazel.Api.Web.Models
{
    /// <summary>
	/// Information about a 2x2 real matrix.
	/// </summary>
    public class RealMatrix2Info
    {
        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <remarks>
        /// The elements are ordered sequentially by row.
        /// </remarks>
        public double[] Elements { get; internal set; }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        public double Determinant { get; internal set; }

        /// <summary>
        /// Gets the trace.
        /// </summary>
        public double Trace { get; internal set; }

        /// <summary>
        /// Gets the eigenvalues.
        /// </summary>
        public double[] Eigenvalues { get; internal set; }

        /// <summary>
        /// Gets the principal eigenvalue.
        /// </summary>
        public double PrincipalEigenvalue { get; internal set; }
    }
}
