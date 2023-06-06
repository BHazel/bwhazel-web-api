using System;
using System.Linq;

namespace BWHazel.Api.Core.Numerics;

/// <summary>
/// Operations 
/// </summary>
public static class RecreationalMathematics
{
    /// <summary>
    /// Gets the loop values indicating an unhappy number.
    /// </summary>
    public static uint[] UnhappyLoopNumbers =>
        new uint[] { 4, 16, 37, 58, 89, 145, 42, 20 };

    /// <summary>
    /// Indicates if a number is happy.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns><c>true</c> if the number is happy, otherwise <c>false</c>.</returns>
    public static bool IsHappy(uint number)
    {
        if (number == 0)
        {
            throw new ArgumentException("Number must be greater than 0.");
        }

        bool isHappinessDetermined = false;
        uint currentNumber = number;

        while (!isHappinessDetermined)
        {
            currentNumber = SumDigitSquares(currentNumber);
            if (currentNumber == 1 || UnhappyLoopNumbers.Contains(currentNumber))
            {
                isHappinessDetermined = true;
            }
        }

        return currentNumber == 1;
    }

    /// <summary>
    /// Calculates the volume of a pizza, based on the Pizza Theorem.
    /// </summary>
    /// <param name="thickness">The pizza thickness.</param>
    /// <param name="radius">The pizza radius.</param>
    /// <remarks>
    /// Weisstein, Eric W. "Pizza Theorem." From MathWorld--A Wolfram Web Resource. http://mathworld.wolfram.com/PizzaTheorem.html.
    /// </remarks>
    /// <returns>The pizza volume.</returns>
    public static double GetPizzaVolume(double thickness, double radius)
    {
        if (thickness <= 0 || radius <= 0)
        {
            throw new ArgumentException("Thickness and radius must be greater than 0.");
        }

        return Math.PI * Math.Pow(radius, 2) * thickness;
    }

    /// <summary>
    /// Sums the squares of the digits of a number.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>The sum of the squares of the digits.</returns>
    private static uint SumDigitSquares(uint number)
    {
        string numberString = number.ToString();
        uint digitsSquaredTotal = 0;

        for (int i = 0; i < numberString.Length; i++)
        {
            uint digitSquared =
                (uint)Math.Pow(
                    uint.Parse(numberString[i].ToString()),
                    2
                );

            digitsSquaredTotal += digitSquared;
        }

        return digitsSquaredTotal;
    }
}