using System;
using BWHazel.Api.Core.Numerics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Numerics;

/// <summary>
/// Tests the <see cref="BaseConvert"/> class.
/// </summary>
public class BaseConvertTests
{
    /// <summary>
    /// Tests the <see cref="BaseConvert.ToDecimalBase(string, int)"/> method returns the correct value when given a valid number in base 2.
    /// </summary>
    /// <param name="binaryNumber">The binary number.</param>
    /// <param name="expectedDecimalNumber">The expected decimal number.</param>
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("10", 2)]
    [InlineData("1001001", 73)]
    public void ToDecimalBase_WhenGivenValidNumberInBase2_ReturnsCorrectValue(string binaryNumber, int expectedDecimalNumber)
    {
        int decimalNumber = BaseConvert.ToDecimalBase(binaryNumber, 2);

        decimalNumber.ShouldBe(expectedDecimalNumber);
    }

    /// <summary>
    /// Tests the <see cref="BaseConvert.ToDecimalBase(string, int)"/> method returns the correct value when given a valid number in base 8.
    /// </summary>
    /// <param name="octalNumber">The octal number.</param>
    /// <param name="expectedDecimalNumber">The expected decimal number.</param>
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("10", 8)]
    public void ToDecimalBase_WhenGivenValidNumberInBase8_ReturnsCorrectValue(string octalNumber, int expectedDecimalNumber)
    {
        int decimalNumber = BaseConvert.ToDecimalBase(octalNumber, 2);

        decimalNumber.ShouldBe(expectedDecimalNumber);
    }
}