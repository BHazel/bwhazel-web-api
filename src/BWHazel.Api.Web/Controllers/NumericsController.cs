using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Algebraics;
using BWHazel.Api.Core.Numerics;
using BWHazel.Api.Web.Models;

namespace BWHazel.Api.Web.Controllers
{
    /// <summary>
    /// Actions for numeric operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NumericsController : Controller
    {
        /// <summary>
        /// The algebraics controller.
        /// </summary>
        /// <remarks>
        /// This is included to support legacy matrix operations which will be
        /// retained until 1st January 2022.
        /// </remarks>
        private readonly AlgebraicsController algebraicsController = new();
        
        /// <summary>
        /// Converts a decimal number into binary.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as binary.</returns>
        /// <response code="200">Returns the binary number.</response>
        /// <response code="400">The input number is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/binary")]
        public ActionResult<string> ToBinary(int number)
        {
            string binaryNumber = default;

            try
            {
                binaryNumber = BaseConvert.ToBinary(number);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }

            return this.Ok(binaryNumber);
        }

        /// <summary>
        /// Converts a decimal number into binary.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/numerics/convert/{number}/binary" instead.
        /// </remarks>
        /// <returns>The number as binary.</returns>
        /// <response code="200">Returns the binary number.</response>
        /// <response code="400">The input number is invalid.</response>
        [Obsolete]
        [HttpGet]
        [Route("{number}/binary")]
        public ActionResult<string> ToBinaryLegacy(int number)
        {
            return this.ToBinary(number);
        }

        /// <summary>
        /// Gets the binary representation of a number as a collection of booleans.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as a boolean collection.</returns>
        /// <response code="200">Returns the binary number as a boolean array.</response>
        /// <response code="400">The input number is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/binary/bits")]
        public ActionResult<bool[]> ToBinaryBits(int number)
        {
            bool[] bits = default;

            try
            {
                string binaryNumber = BaseConvert.ToBinary(number);
                IEnumerable<bool> binaryBits = BitStringConvert.GetBits(binaryNumber);
                bits = binaryBits.ToArray();
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }

            return this.Ok(bits);
        }

        /// <summary>
        /// Gets the binary representation of a number as a collection of booleans.
        /// </summary>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/numerics/convert/{number}/binary/bits" instead.
        /// </remarks>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as a boolean collection.</returns>
        /// <response code="200">Returns the binary number as a boolean array.</response>
        /// <response code="400">The input number is invalid.</response>
        [Obsolete]
        [HttpGet]
        [Route("{number}/binary/bits")]
        public ActionResult<bool[]> ToBinaryBitsLegacy(int number)
        {
            return this.ToBinaryBits(number);
        }

        /// <summary>
        /// Converts a binary number into decimal.
        /// </summary>
        /// <param name="number">The binary number.</param>
        /// <returns>The number as decimal.</returns>
        /// <response code="200">Returns the decimal number.</response>
        /// <response code="400">The input number is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/decimal")]
        public ActionResult<int> FromBinary(string number)
        {
            int decimalNumber = default;

            try
            {
                decimalNumber = BaseConvert.FromBinary(number);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }

            return this.Ok(decimalNumber);
        }

        /// <summary>
        /// Converts a number of a specified base into decimal.
        /// </summary>
        /// <param name="number">The number in a specified base.</param>
        /// <param name="numberBase">The base.</param>
        /// <returns>The number as decimal.</returns>
        /// <response code="200">Returns the decimal number.</response>
        /// <response code="400">The input number or base is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/decimal/{numberBase}")]
        public ActionResult<int> FromBase(string number, int numberBase)
        {
            int decimalNumber = default;

            try
            {
                decimalNumber = BaseConvert.ToDecimalBase(number, numberBase);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }
            catch (ArgumentException)
            {
                return this.BadRequest($"'{numberBase}' is an invalid base.");
            }

            return this.Ok(decimalNumber);
        }

        /// <summary>
        /// Converts a binary number into decimal.
        /// </summary>
        /// <param name="number">The binary number.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/numerics/convert/{number}/decimal" instead.
        /// </remarks>
        /// <returns>The number as decimal.</returns>
        /// <response code="200">Returns the decimal number.</response>
        /// <response code="400">The input number is invalid.</response>
        [Obsolete]
        [HttpGet]
        [Route("{number}/decimal")]
        public ActionResult<int> FromBinaryLegacy(string number)
        {
            return this.Ok(Convert.ToInt32(number, 2));
        }

        /// <summary>
        /// Converts a decimal number into octal.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as octal.</returns>
        /// <response code="200">Returns the octal number.</response>
        /// <response code="400">The input number is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/octal")]
        public ActionResult<string> ToOctal(int number)
        {
            string octalNumber = default;

            try
            {
                octalNumber = BaseConvert.ToOctal(number);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }

            return this.Ok(octalNumber);
        }

        /// <summary>
        /// Converts a decimal number into hexadecimal.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as hexadecimal.</returns>
        /// <response code="200">Returns the hexadecimal number.</response>
        /// <response code="400">The input number is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/hexadecimal")]
        public ActionResult<string> ToHexadecimal(int number)
        {
            string hexadecimalNumber = default;

            try
            {
                hexadecimalNumber = BaseConvert.ToHexadecimal(number);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }

            return this.Ok(hexadecimalNumber);
        }

        /// <summary>
        /// Converts a decimal number into a specified base.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <param name="numberBase">The base.</param>
        /// <returns>The number in a specified base.</returns>
        /// <response code="200">Returns the number in the specified base.</response>
        /// <response code="400">The input number or base is invalid.</response>
        [HttpGet]
        [Route("convert/{number}/base/{numberBase}")]
        public ActionResult<string> ToBase(int number, int numberBase)
        {
            string baseNumber = default;

            try
            {
                baseNumber = BaseConvert.ToTargetBase(number, numberBase);
            }
            catch (FormatException)
            {
                return this.BadRequest($"Input '{number}' is invalid.");
            }
            catch (ArgumentException)
            {
                return this.BadRequest($"'{numberBase}' is an invalid base.");
            }

            return this.Ok(baseNumber);
        }

        /// <summary>
        /// Indicates whether a number is happy.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns><c>true</c> if the number is happy, otherwise <c>false</c>.</returns>
        /// <response code="200">Returns a boolean to indicate whether the number is happy.</response>
        [HttpGet]
        [Route("recreational/{number}/isHappy")]
        public ActionResult<bool> IsNumberHappy(uint number)
        {
            return this.Ok(RecreationalMathematics.IsHappy(number));
        }

        /// <summary>
        /// Indicates whether a number is happy.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/numerics/recreational/{number}/isHappy" instead.
        /// </remarks>
        /// <returns><c>true</c> if the number is happy, otherwise <c>false</c>.</returns>
        /// <response code="200">Returns a boolean to indicate whether the number is happy.</response>
        [Obsolete]
        [HttpGet]
        [Route("{number}/happy")]
        public ActionResult<bool> IsNumberHappyLegacy(uint number)
        {
            return this.IsNumberHappy(number);
        }

        /// <summary>
        /// Gets the numbers indicating an unhappy number.
        /// </summary>
        /// <returns>The unhappy number loop.</returns>
        /// <response code="200">Returns the numbers indicating an unhappy number as an array.</response>
        [HttpGet]
        [Route("recreational/unhappyNumbers")]
        public ActionResult<uint[]> GetUnhappyLoopNumbers()
        {
            return this.Ok(RecreationalMathematics.UnhappyLoopNumbers);
        }

        /// <summary>
        /// Gets the determinant, trace and eigenvalues of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}" instead.
        /// </remarks>
        /// <returns>The determinant, trace and eigenvalues of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the determinant, trace and eigenvalues of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}")]
        public ActionResult<RealMatrix2Info> GetRealMatrix2InfoLegacy(double w, double x, double y, double z)
        {
            return this.algebraicsController.GetRealMatrix2Info(w, x, y, z);
        }

        /// <summary>
        /// Gets the determinant of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/determinant" instead.
        /// </remarks>
        /// <returns>The determinant of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the determinant of a 2x2 real matrix.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/determinant")]
        public ActionResult<double> GetRealMatrix2DeterminantLegacy(double w, double x, double y, double z)
        {
            return this.algebraicsController.GetRealMatrix2Determinant(w, x, y, z);
        }

        /// <summary>
        /// Gets the trace of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/trace" instead.
        /// </remarks>
        /// <returns>The trace of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the trace of a 2x2 real matrix.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/trace")]
        public ActionResult<double> GetRealMatrix2TraceLegacy(double w, double x, double y, double z)
        {
            return this.algebraicsController.GetRealMatrix2Trace(w, x, y, z);
        }

        /// <summary>
        /// Gets the eigenvalues of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvalues" instead.
        /// </remarks>
        /// <returns>The eigenvalues of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the eigenvalues of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/eigenvalues")]
        public ActionResult<double[]> GetRealMatrix2EigenvaluesLegacy(double w, double x, double y, double z)
        {
            return this.algebraicsController.GetRealMatrix2Eigenvalues(w, x, y, z);
        }

        /// <summary>
        /// Gets the principal eigenvalue index of a 2x2 real matrix from the eigenvalues
        /// array returned from the Eigenvalues endpoint.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvalues/principal" instead.
        /// </remarks>
        /// <returns>The principal eigenvalue index of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the principal eigenvalue index of a 2x2 real matrix.</response>
        /// <response code="400">The eigenvalues calculation involves a negative discriminant.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/eigenvalues/principal")]
        public ActionResult<int> GetRealMatrix2PrincipalEigenvalueIndexLegacy(double w, double x, double y, double z)
        {
            return this.algebraicsController.GetRealMatrix2PrincipalEigenvalueIndex(w, x, y, z);
        }

        /// <summary>
        /// Calculates the value for an eigenvector of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <param name="eigenvalue">The eigenvalue.</param>
        /// <param name="value">The substituted value.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvector/{eigenvalue}/{value}" instead.
        /// </remarks>
        /// <returns>The value for an eigenvector for a specified eigenvalue of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the eigenvector value for a specified eigenvelue of a 2x2 real matrix.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/eigenvector/{eigenvalue}/{value}")]
        public ActionResult<double> GetRealMatrix2EigenvectorLegacy(double w, double x, double y, double z, double eigenvalue, double value)
        {
            return this.algebraicsController.GetRealMatrix2Eigenvector(w, x, y, z, eigenvalue, value);
        }

        /// <summary>
        /// Calculate a series of values for an eigenvector of a 2x2 real matrix.
        /// </summary>
        /// <param name="w">The element in row 1, column 1.</param>
        /// <param name="x">The element in row 1, column 2.</param>
        /// <param name="y">The element in row 2, column 1.</param>
        /// <param name="z">The element in row 2, column 2.</param>
        /// <param name="start">The substituted value at the start of the series.</param>
        /// <param name="increment">The increment between substituted values.</param>
        /// <param name="total">The total number of values in the series.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.  Please note this endpoint is not being replaced.
        /// Please use the "api/Algebraics/linear/matrix2/e11/{e11}/e12/{e12}/e21/{e21}/e22/{e22}/eigenvector/{eigenvalue}/{value}"
        /// endpoint to calculate individual values.
        /// </remarks>
        /// <returns>The series of values for an eigenvector of a 2x2 real matrix.</returns>
        /// <response code="200">Returns the eigenvector value series for a specified eigenvelue of a 2x2 real matrix.</response>
        [Obsolete]
        [HttpGet]
        [Route("matrix2/w/{w}/x/{x}/y/{y}/z/{z}/eigenvectors/start/{start}/increment/{increment}/total/{total}")]
        public ActionResult<double[][]> GetRealMatrix2EigenvectorsLegacy(double w, double x, double y, double z, double start, double increment, int total)
        {
            RealMatrix2 realMatrix2 = new(w, x, y, z);
            double[] eigenvalues = realMatrix2.GetEigenvalues();

            List<double[]> eigenvectors = new();
            double currentEigenvector = start;
            for (int i = 0; i < total; i++)
            {
                double eigenvector1 = realMatrix2.CalculateEigenvector(eigenvalues[0], currentEigenvector);
                double eigenvector2 = realMatrix2.CalculateEigenvector(eigenvalues[1], currentEigenvector);

                eigenvectors.Add(new double[] { eigenvector1, eigenvector2 });
                currentEigenvector += increment;
            }

            return this.Ok(eigenvectors.ToArray());
        }
    }
}
