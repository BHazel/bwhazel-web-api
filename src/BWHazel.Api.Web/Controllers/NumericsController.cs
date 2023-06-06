using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Algebraics;
using BWHazel.Api.Core.Numerics;
using BWHazel.Api.Web.Models;

namespace BWHazel.Api.Web.Controllers;

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
            bits = BitStringConvert.GetBits(binaryNumber);
        }
        catch (FormatException)
        {
            return this.BadRequest($"Input '{number}' is invalid.");
        }

        return this.Ok(bits);
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
    /// <response code="400">The number is 0 or negative.</response>
    [HttpGet]
    [Route("recreational/{number}/isHappy")]
    public ActionResult<bool> IsNumberHappy(uint number)
    {
        bool isHappy = default;

        try
        {
            isHappy = RecreationalMathematics.IsHappy(number);
        }
        catch (ArgumentException)
        {
            return this.BadRequest("Number must be greater than 0.");
        }

        return this.Ok(isHappy);
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
    /// Gets the volume of a pizza.
    /// </summary>
    /// <param name="thickness">The pizza thickness.</param>
    /// <param name="radius">The pizza radius.</param>
    /// <returns>The pizza volume.</returns>
    /// <response code="200">Returns the pizza volume.</response>
    /// <response code="400">The thickness or radius are 0 or negative.</response>
    [HttpGet]
    [Route("recreational/pizzaVolume/thickness/{thickness}/radius/{radius}")]
    public ActionResult<double> GetPizzaVolume(double thickness, double radius)
    {
        double pizzaThickness = default;

        try
        {
            pizzaThickness = RecreationalMathematics.GetPizzaVolume(thickness, radius);
        }
        catch (ArgumentException)
        {
            return this.BadRequest("Thickness and radius must be greater than 0.");
        }

        return this.Ok(pizzaThickness);
    }
}