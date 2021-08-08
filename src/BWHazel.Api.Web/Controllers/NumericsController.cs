using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Numerics;

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
        /// Converts a decimal number into binary.
        /// </summary>
        /// <param name="number">The decimal number.</param>
        /// <returns>The number as binary.</returns>
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
        /// <returns>The number as </returns>
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
        /// <returns>The number as </returns>
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
        /// Converts a binary number into decimal.
        /// </summary>
        /// <param name="number">The binary number.</param>
        /// <remarks>
        /// This method is now obsolete and shall be removed on 1st January 2022.
        /// Please use "/api/numerics/convert/{number}/decimal" instead.
        /// </remarks>
        /// <returns>The number as decimal.</returns>
        [Obsolete]
        [HttpGet]
        [Route("{number}/decimal")]
        public ActionResult<int> FromBinaryLegacy(string number)
        {
            return this.Ok(Convert.ToInt32(number, 2));
        }
    }
}
