using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BWHazel.Api.Core.Numerics
{
    /// <summary>
    /// Converts bit strings and boolean collections.
    /// </summary>
    public static class BitStringConvert
    {
        private const string BinaryNumberPattern = "^[01]+$";

        /// <summary>
        /// Converts a binary number in string representation into a collection of booleans.
        /// </summary>
        /// <param name="binaryNumber">The binary number.</param>
        /// <returns>A collection of booleans.</returns>
        public static IEnumerable<bool> GetBits(string binaryNumber)
        {
            if (!Regex.IsMatch(binaryNumber, BinaryNumberPattern))
            {
                throw new FormatException("Number must be a valid binary number.");
            }

            return binaryNumber
                .Select(bit => bit == '1' ? true : false);
        }
    }
}
