using System;
using MathNet.Numerics.LinearAlgebra;

namespace BWHazel.Api.Core.Algebraics
{
    /// <summary>
	/// Represents a 2x2 matrix with real values.
	/// </summary>
    public class RealMatrix2
    {
        private Matrix<double> matrix;

        /// <summary>
        /// Intialises a new instance of the <see cref="RealMatrix2"/> class.
        /// </summary>
        /// <remarks>
        /// This configures the matrix as a 2x2 identity matrix.
        /// </remarks>
        public RealMatrix2()
            : this(1, 0, 0, 1)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="RealMatrix2"/> class.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        public RealMatrix2(double e11, double e12, double e21, double e22)
        {
            this.matrix = Matrix<double>.Build.Dense(
                2,
                2,
                new double[]
                {
                    e11, e12,
                    e21, e22
                });
        }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <returns>The determinant.</returns>
        public double GetDeterminant()
        {
            return this.matrix.Determinant();
        }

        /// <summary>
        /// Gets the trace.
        /// </summary>
        /// <returns>The trace.</returns>
        public double GetTrace()
        {
            return this.matrix.Trace();
        }

        /// <summary>
        /// Gets the eigenvalues.
        /// </summary>
        /// <returns>The eigenvalues as an array.</returns>
        public double[] GetEigenvalues()
        {
            double a = 1.0;
            double b = (-1 * (this.matrix[0, 0] - this.matrix[1, 1]));
            double c = ((-1 * this.matrix[0, 1]) * this.matrix[1, 0])
                + (this.matrix[0, 0] * this.matrix[1, 1]);

            double discriminant = Math.Pow(b, 2) - (4 * a * c);
            if (discriminant < 0)
            {
                throw new ArithmeticException("Calculation has negative discriminant; eigenvalues are complex numbers.");
            }

            double eigenvalue1 = ((-1 * b) + Math.Sqrt(discriminant)) / (2 * a);
            double eigenvalue2 = ((-1 * b) - Math.Sqrt(discriminant)) / (2 * a);

            return new double[]
            {
                eigenvalue1, eigenvalue2
            };
        }

        /// <summary>
        /// Gets the index of the principal eigenvalue from the eigenvalues
        /// array returned from <see cref="GetEigenvalues"/>.
        /// </summary>
        /// <returns>The index of the principal eigenvalue.</returns>
        public int GetPrincipalEigenvalueIndex()
        {
            double[] eigenvalues = this.GetEigenvalues();
            double trace = this.GetTrace();

            if ((eigenvalues[0] / trace) > (eigenvalues[1] / trace))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Calculates the value for an eigenvector.
        /// </summary>
        /// <param name="eigenvalue">The eigenvalue.</param>
        /// <param name="value">The substituted value.</param>
        /// <returns>The value for an eigenvector for a specified eigenvalue.</returns>
        public double CalculateEigenvector(double eigenvalue, double value)
        {
            return (-1 * (this.matrix[0, 0] - eigenvalue) * value) / this.matrix[0, 1];
        }
    }
}
