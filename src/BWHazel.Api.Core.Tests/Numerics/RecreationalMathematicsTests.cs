using System;
using BWHazel.Api.Core.Numerics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Numerics;

/// <summary>
/// Tests the <see cref="RecreationalMathematics"/> class.
/// </summary>
public class RecreationalMathematicsTests
{
    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.IsHappy(uint)"/> method returns true if given a happy number.
    /// </summary>
    /// <param name="number">The number.</param>
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(28)]
    [InlineData(236)]
    [InlineData(1000)]
    public void IsHappy_WhenGivenHappyNumber_ReturnsTrue(uint number)
    {
        bool isHappy = RecreationalMathematics.IsHappy(number);

        isHappy.ShouldBeTrue();
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.IsHappy(uint)"/> method returns false if given an unhappy number.
    /// </summary>
    /// <param name="number">The number.</param>
    [Theory]
    [InlineData(9)]
    [InlineData(25)]
    [InlineData(400)]
    [InlineData(1234)]
    public void IsHappy_WhenGivenUnhappyNumber_ReturnsFalse(uint number)
    {
        bool isHappy = RecreationalMathematics.IsHappy(number);

        isHappy.ShouldBeFalse();
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.IsHappy(uint)"/> method returns false if given a number in the unhappy numbers loop.
    /// </summary>
    /// <param name="number">The number.</param>
    [Theory]
    [InlineData(4)]
    [InlineData(16)]
    [InlineData(20)]
    [InlineData(37)]
    [InlineData(42)]
    [InlineData(58)]
    [InlineData(89)]
    [InlineData(145)]
    public void IsHappy_WhenGivenUnhappyLoopNumber_ReturnsFalse(uint number)
    {
        bool isHappy = RecreationalMathematics.IsHappy(number);

        isHappy.ShouldBeFalse();
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.IsHappy(uint)"/> method throws an <see cref="ArgumentException"/> when given 0.
    /// </summary>
    [Fact]
    public void IsHappy_WhenGivenZero_ThrowsArgumentException()
    {
        ArgumentException exception = Should.Throw<ArgumentException>(() =>
        {
            RecreationalMathematics.IsHappy(0);
        });

        exception.Message.ShouldBe("Number must be greater than 0.");
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.GetPizzaVolume(double, double)"/> method returns the correct value when given positive numbers for thickness and radius.
    /// </summary>
    /// <param name="thickness">The pizza thickness.</param>
    /// <param name="radius">The pizza radius,</param>
    /// <param name="expectedVolume">The expected pizza volume.</param>
    [Theory]
    [InlineData(1.0, 1.0, 3.14)]
    [InlineData(10.0, 1.0, 31.42)]
    [InlineData(10.0, 10.0, 3141.59)]
    [InlineData(5.0, 10.0, 1570.80)]
    [InlineData(5.0, 2.5, 98.17)]
    [InlineData(3.75, 2.5, 73.63)]
    public void GetPizzaVolume_WhenGivenPositiveNumbers_ReturnsCorrectValue(double thickness, double radius, double expectedVolume)
    {
        double pizzaVolume = RecreationalMathematics.GetPizzaVolume(thickness, radius);

        pizzaVolume.ShouldBe(expectedVolume, 0.01);
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.GetPizzaVolume(double, double)"/> method throws a <see cref="ArgumentException"/> when given 0 for either thickness or radius.
    /// </summary>
    /// <param name="thicknes">The pizza thickness.</param>
    /// <param name="radius">The pizza radius.</param>
    [Theory]
    [InlineData(2.75, 0)]
    [InlineData(0, 2.75)]
    public void GetPizzaVolume_WhenGivenZeroO_ThrowsArgumentException(double thicknes, double radius)
    {
        ArgumentException exception = Should.Throw<ArgumentException>(() =>
        {
            RecreationalMathematics.GetPizzaVolume(thicknes, radius);
        });

        exception.Message.ShouldBe("Thickness and radius must be greater than 0.");
    }

    /// <summary>
    /// Tests the <see cref="RecreationalMathematics.GetPizzaVolume(double, double)"/> method throws a <see cref="ArgumentException"/> when given a negative number for either thickness or radius.
    /// </summary>
    /// <param name="thicknes">The pizza thickness.</param>
    /// <param name="radius">The pizza radius.</param>
    [Theory]
    [InlineData(2.75, -1.25)]
    [InlineData(-1.25, 2.75)]
    public void GetPizzaVolume_WhenGivenNegativeNumbers_ThrowsArgumentException(double thicknes, double radius)
    {
        ArgumentException exception = Should.Throw<ArgumentException>(() =>
        {
            RecreationalMathematics.GetPizzaVolume(thicknes, radius);
        });

        exception.Message.ShouldBe("Thickness and radius must be greater than 0.");
    }
}
