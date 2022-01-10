using System;
using System.Linq;
using BWHazel.Api.Core.Algebraics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Algebraics;

/// <summary>
/// Tests for the <see cref="RealMatrix2"/> class.
/// </summary>
public class RealMatrix2Tests
{
    /// <summary>
    /// Tests that the <see cref="RealMatrix2.GetDeterminant"/> method returns the correct value.
    /// </summary>
    /// <param name="e11">The element in row 1, column 1.</param>
    /// <param name="e12">The element in row 1, column 2.</param>
    /// <param name="e21">The element in row 2, column 1.</param>
    /// <param name="e22">The element in row 2, column 2.</param>
    /// <param name="expectedDeterminant">The expected determinant.</param>
    [Theory]
    [InlineData(1, 0, 0, 1, 1)]
    [InlineData(1, 0, 0, -1, -1)]
    [InlineData(1, 2, 3, 4, -2)]
    [InlineData(-1, 2, -3, 4, 2)]
    [InlineData(1.5, 3.7, 4.6, 8.2, -4.72)]
    [InlineData(1.5, -3.7, 4.6, -8.2, 4.72)]
    public void GetDeterminant_ReturnsCorrectValue(double e11, double e12, double e21, double e22, double expectedDeterminant)
    {
        RealMatrix2 matrix = new(e11, e12, e21, e22);

        double determinant = matrix.GetDeterminant();

        determinant.ShouldBe(expectedDeterminant, 0.01);
    }

    /// <summary>
    /// Tests that the <see cref="RealMatrix2.GetTrace"/> method returns the correct value.
    /// </summary>
    /// <param name="e11">The element in row 1, column 1.</param>
    /// <param name="e12">The element in row 1, column 2.</param>
    /// <param name="e21">The element in row 2, column 1.</param>
    /// <param name="e22">The element in row 2, column 2.</param>
    /// <param name="expectedTrace">The expected trace.</param>
    [Theory]
    [InlineData(1, 0, 0, 1, 2)]
    [InlineData(1, 0, 0, -1, 0)]
    [InlineData(1, 2, 3, 4, 5)]
    [InlineData(-1, 2, -3, 4, 3)]
    [InlineData(1.5, 3.7, 4.6, 8.2, 9.7)]
    [InlineData(1.5, -3.7, 4.6, -8.2, -6.7)]
    public void GetTrace_ReturnsCorrectValue(double e11, double e12, double e21, double e22, double expectedTrace)
    {
        RealMatrix2 matrix = new(e11, e12, e21, e22);

        double trace = matrix.GetTrace();

        trace.ShouldBe(expectedTrace, 0.01);
    }

    /// <summary>
    /// Tests that the <see cref="RealMatrix2.GetEigenvalues"/> method returns the correct values.
    /// </summary>
    /// <param name="e11">The element in row 1, column 1.</param>
    /// <param name="e12">The element in row 1, column 2.</param>
    /// <param name="e21">The element in row 2, column 1.</param>
    /// <param name="e22">The element in row 2, column 2.</param>
    /// <param name="expectedEiganvalue1">The expected value for the first eigenvalue.</param>
    /// <param name="expectedEiganvalue2">The expected value for the second eigenvalue.</param>
    [Theory]
    [InlineData(1, 0, 0, 1, 1, 1)]
    [InlineData(1, 0, 0, -1, 1, -1)]
    [InlineData(1, 2, 3, 4, 5.37, -0.37)]
    [InlineData(-1, 2, -3, 4, 2, 1)]
    [InlineData(1.5, 3.7, 4.6, 8.2, 10.16, -0.46)]
    [InlineData(1.5, -3.7, 4.6, -8.2, -0.8, -5.9)]
    public void GetEigenvalues_ReturnsCorrectValues(double e11, double e12, double e21, double e22, double expectedEiganvalue1, double expectedEiganvalue2)
    {
        RealMatrix2 matrix = new(e11, e12, e21, e22);

        double[] eigenvalues = matrix.GetEigenvalues();

        eigenvalues[0].ShouldBe(expectedEiganvalue1, 0.01);
        eigenvalues[1].ShouldBe(expectedEiganvalue2, 0.01);
    }

    /// <summary>
    /// Tests that the <see cref="RealMatrix2.GetEigenvalues"/> method throws an <see cref="ArithmeticException"/> if the eigenvalues are complex.
    /// </summary>
    [Fact]
    public void GetEigenvalues_ForNonRealEigenvalues_ThrowsException()
    {
        RealMatrix2 matrix = new(1, 2, -2, 1);

        ArithmeticException exception = Should.Throw<ArithmeticException>(() =>
        {
            matrix.GetEigenvalues();
        });

        exception.Message.ShouldBe("Calculation has negative discriminant; eigenvalues are complex numbers.");
    }

    [Theory]
    [InlineData(1, 2, 3, 4, new[] { 1.45, 2.91, 4.37 }, new[] { -0.45, -0.91, -1.37 })]
    [InlineData(-1, 2, -3, 4, new[] { -1.0, -2.0, -3.0 }, new[] { -0.67, -1.33, -2.0 })]
    [InlineData(1.5, 3.7, 4.6, 8.2, new[] { 1.88, 3.77, 5.65 }, new[] { -0.42, -0.85, -1.28 })]
    [InlineData(1.5, -3.7, 4.6, -8.2, new[] { -0.5, -1.0, -1.5 }, new[] { -1.61, -3.22, -4.83 })]
    public void CalculateEigenvector_ForEigenvalue_ReturnsCorrectValue(double e11, double e12, double e21, double e22, double[] expectedEigenvectors1, double[] expectedEigenvectors2)
    {
        RealMatrix2 matrix = new(e11, e12, e21, e22);
        double[] eigenvalues = matrix.GetEigenvalues();

        double[] eigenvectors1 = Enumerable.Range(1, 3)
            .Select(value => matrix.CalculateEigenvector(eigenvalues[0], value))
            .ToArray();

        double[] eigenvectors2 = Enumerable.Range(1, 3)
            .Select(value => matrix.CalculateEigenvector(eigenvalues[1], value))
            .ToArray();

        eigenvectors1.ShouldBe(expectedEigenvectors1, 0.01);
        eigenvectors2.ShouldBe(expectedEigenvectors2, 0.01);
    }
}