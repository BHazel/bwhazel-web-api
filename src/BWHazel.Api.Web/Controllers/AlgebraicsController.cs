using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Algebraics;
using BWHazel.Api.Web.Models;

namespace BWHazel.Api.Web.Controllers
{
    /// <summary>
    /// Actions for algebraic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlgebraicsController : Controller
    {
        /// <summary>
        /// Gets the determinant, trace and eigenvalues of a 2x2 real matrix.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <returns>The determinant, trace and eigenvalues.</returns>
        /// <response code="200">Returns the determinant, trace and eigenvalues of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}")]
        public ActionResult<RealMatrix2Info> GetRealMatrix2Info(double e11, double e12, double e21, double e22)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(new RealMatrix2Info()
            {
                Elements = new double[] { e11, e12, e21, e22 },
                Determinant = realMatrix2.GetDeterminant(),
                Trace = realMatrix2.GetTrace(),
                Eigenvalues = realMatrix2.GetEigenvalues(),
                PrincipalEigenvalue = realMatrix2.GetPrincipalEigenvalueIndex()
            });
        }

        /// <summary>
        /// Gets the determinant of a 2x2 real matrix.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <returns>The determinant.</returns>
        /// <response code="200">Returns the determinant of a 2x2 real matrix.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/determinant")]
        public ActionResult<double> GetRealMatrix2Determinant(double e11, double e12, double e21, double e22)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(realMatrix2.GetDeterminant());
        }

        /// <summary>
        /// Gets the trace of the 2x2 real matrix.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <returns>The trace.</returns>
        /// <response code="200">Returns the trace of a 2x2 real matrix.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/trace")]
        public ActionResult<double> GetRealMatrix2Trace(double e11, double e12, double e21, double e22)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(realMatrix2.GetTrace());
        }

        /// <summary>
        /// Gets the eigenvalues of a 2x2 real matrix.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <returns>The eigenvalues.</returns>
        /// <response code="200">Returns the eigenvalues of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvalues")]
        public ActionResult<double[]> GetRealMatrix2Eigenvalues(double e11, double e12, double e21, double e22)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(realMatrix2.GetEigenvalues());
        }

        /// <summary>
        /// Gets the principal eigenvalue index of a 2x2 real matrix from the eigenvalues
        /// array returned from the Eigenvalues endpoint.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <returns>The principal eigenvalue index.</returns>
        /// <response code="200">Returns the principal eigenvalue index of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvalues/principal")]
        public ActionResult<int> GetRealMatrix2PrincipalEigenvalueIndex(double e11, double e12, double e21, double e22)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(realMatrix2.GetPrincipalEigenvalueIndex());
        }

        /// <summary>
        /// Calculates the value for an eigenvector.
        /// </summary>
        /// <param name="e11">The element in row 1, column 1.</param>
        /// <param name="e12">The element in row 1, column 2.</param>
        /// <param name="e21">The element in row 2, column 1.</param>
        /// <param name="e22">The element in row 2, column 2.</param>
        /// <param name="eigenvalue">The eigenvalue.</param>
        /// <param name="value">The substituted value.</param>
        /// <returns>The value for an eigenvector for a specified eigenvalue.</returns>
        /// <response code="200">Returns the eigenvector value for a specified eigenvelue of a 2x2 real matrix.</response>
        [HttpGet]
        [Route("linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvector/{eigenvalue}/{value}")]
        public ActionResult<double> GetRealMatrix2Eigenvector(double e11, double e12, double e21, double e22, double eigenvalue, double value)
        {
            RealMatrix2 realMatrix2 = new(e11, e12, e21, e22);
            return this.Ok(realMatrix2.CalculateEigenvector(eigenvalue, value));
        }
    }
}
