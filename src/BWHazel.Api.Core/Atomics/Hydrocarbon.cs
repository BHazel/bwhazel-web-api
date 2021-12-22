namespace BWHazel.Api.Core.Atomics;

/// <summary>
/// Represents a hydrocarbon compound.
/// </summary>
public class Hydrocarbon
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Hydrocarbon"/> class.
    /// </summary>
    /// <param name="carbonAroms">The number of carbon atoms.</param>
    /// <param name="hydrogenAtoms">The number of hydrogen atoms.</param>
    public Hydrocarbon(uint carbonAroms, uint hydrogenAtoms)
    {
        this.CarbonAtoms = carbonAroms;
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
    public uint GetDoubleBondEquivalents()
    {
        uint hydrogenAtomsInSaturatedCompound = this.GetHydrogenAtomsInSaturatedCompound();
        return (hydrogenAtomsInSaturatedCompound - this.HydrogenAtoms) / 2;
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