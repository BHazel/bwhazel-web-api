namespace BWHazel.Api.Web.Tests.Integration.Models;

/// <summary>
/// Information about a 2x2 real matrix.
/// </summary>
/// <remarks>
/// This model should be used for testing only.
/// </remarks>
public class TestRealMatrix2Info
{
    /// <summary>
    /// Gets the elements.
    /// </summary>
    /// <remarks>
    /// The elements are ordered sequentially by row.
    /// </remarks>
    public double[] Elements { get; set; }

    /// <summary>
    /// Gets the determinant.
    /// </summary>
    public double Determinant { get; set; }

    /// <summary>
    /// Gets the trace.
    /// </summary>
    public double Trace { get; set; }

    /// <summary>
    /// Gets the eigenvalues.
    /// </summary>
    public double[] Eigenvalues { get; set; }

    /// <summary>
    /// Gets the principal eigenvalue.
    /// </summary>
    public double PrincipalEigenvalue { get; set; }
}