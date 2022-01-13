using BWHazel.Api.Core.Atomics;
using Shouldly;
using System;
using Xunit;

namespace BWHazel.Api.Core.Tests.Atomics;

/// <summary>
/// Tests for the <see cref="Hydrocarbon"/> class.
/// </summary>
public class HydrocarbonTests
{
    /// <summary>
    /// Tests that the <see cref="Hydrocarbon"/> class constructor sets the correct atom values.
    /// </summary>
    [Fact]
    public void Constructor_SetsCorrectAtomtValues()
    {
        uint carbonAtoms = 2;
        uint hydrogenAtoms = 6;

        Hydrocarbon hydrocarbon = new(carbonAtoms, hydrogenAtoms);

        hydrocarbon.CarbonAtoms.ShouldBe(carbonAtoms);
        hydrocarbon.HydrogenAtoms.ShouldBe(hydrogenAtoms);
    }

    /// <summary>
    /// Tests that the <see cref="Hydrocarbon.GetDoubleBondEquivalents"/> method returns the correct value when given positive numbers of carbon and hydrogen atoms.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="hydrogenAtoms">The number of hydrogen atoms.</param>
    /// <param name="expectedDbe">The expected double-bond equivalents.</param>
    [Theory]
    [InlineData(1, 4, 0)]
    [InlineData(2, 6, 0)]
    [InlineData(2, 4, 1)]
    [InlineData(2, 2, 2)]
    [InlineData(2, 3, 1.5)]
    [InlineData(4, 5, 2.5)]
    public void GetDoubleBondEquivalents_WithPositiveCarbonAndHydrogenAtoms_ReturnsCorrectValue(uint carbonAtoms, uint hydrogenAtoms, double expectedDbe)
    {
        Hydrocarbon hydrocarbon = new(carbonAtoms, hydrogenAtoms);

        double dbe = hydrocarbon.GetDoubleBondEquivalents();

        dbe.ShouldBe(expectedDbe);
    }

    /// <summary>
    /// Tests that the <see cref="Hydrocarbon.GetDoubleBondEquivalents"/> method throws an <see cref="ArithmeticException"/> if the number of hydrogen atoms is greater than in the saturated compound.
    /// </summary>
    [Fact]
    public void GetDoubleBondEquivalents_WithHydrogenAtomsGreaterThanInSaturatedCompound_ThrowsArithmeticException()
    {
        Hydrocarbon hydrocarbon = new(2, 10);

        ArithmeticException exception = Should.Throw<ArithmeticException>(() =>
        {
            hydrocarbon.GetDoubleBondEquivalents();
        });

        exception.Message.ShouldBe("The number of hydrogen atoms is larger than in the saturated compound.");
    }

    /// <summary>
    /// Tests the <see cref="Hydrocarbon.GetHydrogenAtomsInSaturatedCompound"/> method returns the correct value when given a positive number of carbon atoms.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="expectedHydrogenAtoms">The expected number of hydrogen atoms.</param>
    [Theory]
    [InlineData(1, 4)]
    [InlineData(2, 6)]
    [InlineData(3, 8)]
    [InlineData(4, 10)]
    [InlineData(10, 22)]
    [InlineData(20, 42)]
    public void GetHydrogenAtomsInSaturatedCompound_WithPositiveCarbonAtoms_ReturnsCorrectValue(uint carbonAtoms, uint expectedHydrogenAtoms)
    {
        Hydrocarbon hydrocarbon = new(carbonAtoms, 1);

        uint hydrogenAtoms = hydrocarbon.GetHydrogenAtomsInSaturatedCompound();

        hydrogenAtoms.ShouldBe(expectedHydrogenAtoms);
    }

    /// <summary>
    /// Tests the <see cref="Hydrocarbon.GetHydrogenAtomsInSaturatedCompound"/> method returns 2 hydrogen atoms when given 0 carbon atoms.
    /// </summary>
    [Fact]
    public void GetHydrogenAtomsInSaturatedCompound_WithZeroCarbonAtoms_Returns2HydrogenAtoms()
    {
        Hydrocarbon hydrocarbon = new(0, 0);

        uint hydrogenAtoms = hydrocarbon.GetHydrogenAtomsInSaturatedCompound();

        hydrogenAtoms.ShouldBe(2u);
    }
}
