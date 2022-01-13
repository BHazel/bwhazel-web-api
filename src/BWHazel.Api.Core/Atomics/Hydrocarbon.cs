using System;

namespace BWHazel.Api.Core.Atomics;

/// <summary>
/// Represents a hydrocarbon compound.
/// </summary>
public class Hydrocarbon
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Hydrocarbon"/> class.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="hydrogenAtoms">The number of hydrogen atoms.</param>
    public Hydrocarbon(uint carbonAtoms, uint hydrogenAtoms)
    {
        this.CarbonAtoms = carbonAtoms;
        this.HydrogenAtoms = hydrogenAtoms;
    }

    /// <summary>
    /// Gets or sets the number of carbon atoms.
    /// </summary>
    public uint CarbonAtoms { get; set; }

    /// <summary>
    /// Gets or sets the number of hydrogen atoms.
    /// </summary>
    public uint HydrogenAtoms { get; set; }

    /// <summary>
    /// Gets the number of double-bond equivalents.
    /// </summary>
    /// <returns>The number of double-bond equivalents.</returns>
    /// <exception cref="ArithmeticException">Exception thrown when the number of hydrogen atoms is larger than in the saturated compound.</exception>
    public double GetDoubleBondEquivalents()
    {
        uint hydrogenAtomsInSaturatedCompound = this.GetHydrogenAtomsInSaturatedCompound();
        if (hydrogenAtomsInSaturatedCompound < this.HydrogenAtoms)
        {
            throw new ArithmeticException("The number of hydrogen atoms is larger than in the saturated compound.");
        }

        return (hydrogenAtomsInSaturatedCompound - this.HydrogenAtoms) / 2.0;
    }

    /// <summary>
    /// Gets the number of hydrogen atoms in a saturated compound.
    /// </summary>
    /// <returns>The number of hydrogen atoms in a saturated compound.</returns>
    public uint GetHydrogenAtomsInSaturatedCompound()
    {
        return (2 * this.CarbonAtoms) + 2;
    }
}