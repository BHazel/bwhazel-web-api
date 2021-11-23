using System;

namespace BWHazel.Api.Core.Numerics;

/// <summary>
/// Converts numbers between different bases.
/// </summary>
public static class BaseConvert
{
    private const int BinaryBase = 2;
    private const int OctalBase = 8;
    private const int HexadecimalBase = 16;

    /// <summary>
    /// Converts a number in a source base to its decimal base form.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <param name="numberBase">The source base to convert from.</param>
    /// <returns>The number in decimal base.</returns>
    public static int ToDecimalBase(string number, int numberBase)
    {
        return Convert.ToInt32(number, numberBase);
    }

    /// <summary>
    /// Converts a number in decimal base to a target base.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <param name="targetBase">The target base to convert into.</param>
    /// <returns>The number as a string in target base form.</returns>
    public static string ToTargetBase(int number, int targetBase)
    {
        return Convert.ToString(number, targetBase);
    }

    /// <summary>
    /// Converts a number in decimal base to binary (base 2).
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number as a string in binary form.</returns>
    public static string ToBinary(int number)
    {
        return ToTargetBase(number, BinaryBase);
    }

    /// <summary>
    /// Converts a binary number to its decimal base form.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number in decimal base.</returns>
    public static int FromBinary(string number)
    {
        return ToDecimalBase(number, BinaryBase);
    }

    /// <summary>
    /// Converts a number in decimal base to octal (base 8).
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number as a string in octal form.</returns>
    public static string ToOctal(int number)
    {
        return ToTargetBase(number, OctalBase);
    }

    /// <summary>
    /// Converts an octal number to its decimal base form.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number in decimal base.</returns>
    public static int FromOctal(string number)
    {
        return ToDecimalBase(number, OctalBase);
    }

    /// <summary>
    /// Converts a number in decimal base to hexadecimal (base 16).
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number as a string in hexadecimal form.</returns>
    public static string ToHexadecimal(int number)
    {
        return ToTargetBase(number, HexadecimalBase);
    }

    /// <summary>
    /// Converts a hexadecimal number to its decimal base form.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number in decimal base.</returns>
    public static int FromHexadecimal(string number)
    {
        return ToDecimalBase(number, HexadecimalBase);
    }
}