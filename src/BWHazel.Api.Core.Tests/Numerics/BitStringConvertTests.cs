using System;
using BWHazel.Api.Core.Numerics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Numerics;

/// <summary>
/// Tests the <see cref="BitStringConvert"/> class.
/// </summary>
public class BitStringConvertTests
{
    /// <summary>
    /// Tests the <see cref="BitStringConvert.GetBits(string)"/> method returns the correct value when given a valid binary number string.
    /// </summary>
    /// <param name="binaryNumber">The binary number.</param>
    /// <param name="expectedBits">The expected bits array.</param>
    [Theory]
    [InlineData("0", new[] { false })]
    [InlineData("1", new[] { true })]
    [InlineData("00", new[] { false, false })]
    [InlineData("11", new[] { true, true })]
    [InlineData("1001", new[] { true, false, false, true })]
    public void GetBits_WhenGivenValidBinaryNumber_ReturnsCorrectValue(string binaryNumber, bool[] expectedBits)
    {
        bool[] bits = BitStringConvert.GetBits(binaryNumber);

        bits.Length.ShouldBe(expectedBits.Length);
        bits.ShouldBe(expectedBits);
    }

    /// <summary>
    /// Tests the <see cref="BitStringConvert.GetBits(string)"/> method throw a <see cref="FormatException"/> when given an invalid binary number string.
    /// </summary>
    /// <param name="binaryNumber">The binary number.</param>
    [Theory]
    [InlineData("")]
    [InlineData("0123456789")]
    [InlineData("abc")]
    [InlineData("01?")]
    [InlineData("0 1")]
    public void GetBits_WhenGivenInvalidBinaryNumber_ThrowsFormatException(string binaryNumber)
    {
        FormatException formatException = Should.Throw<FormatException>(() =>
        {
            BitStringConvert.GetBits(binaryNumber);
        });

        formatException.Message.ShouldBe("Number must be a valid binary number.");
    }
}