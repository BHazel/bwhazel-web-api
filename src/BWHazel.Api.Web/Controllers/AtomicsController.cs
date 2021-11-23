using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Atomics;

namespace BWHazel.Api.Web.Controllers;

/// <summary>
/// Actions for atomic operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AtomicsController : Controller
{
    /// <summary>
    /// Calculates the double-bond equivalents for a compound.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <param name="hydrogenAtoms">The number of hydrogen atoms.</param>
    /// <returns>The double-bond equivalents.</returns>
    /// <response code="200">Returns the number of double-bond equivalents.</response>
    [HttpGet]
    [Route("dbe/c/{carbonAtoms}/h/{hydrogenAtoms}")]
    public ActionResult<uint> GetDoubleBondEquivalents(uint carbonAtoms, uint hydrogenAtoms)
    {
        Hydrocarbon hydrocarbon = new(carbonAtoms, hydrogenAtoms);
        uint doubleBondEquivalents = hydrocarbon.GetDoubleBondEquivalents();
        return this.Ok(doubleBondEquivalents);
    }

    /// <summary>
    /// Calculates the number of hyodrgen atoms in a saturated compound.
    /// </summary>
    /// <param name="carbonAtoms">The number of carbon atoms.</param>
    /// <returns>The number of hydrogen atoms.</returns>
    /// <response code="200">Returns the number of hydrogen atoms.</response>
    [HttpGet]
    [Route("dbe/c/{carbonAtoms}/saturated")]
    public ActionResult<uint> GetHydrogenAtomsInSaturatedCompound(uint carbonAtoms)
    {
        Hydrocarbon hydrocarbon = new(carbonAtoms, 0);
        uint hydrogenAtomsInSaturatedCompound = hydrocarbon.GetHydrogenAtomsInSaturatedCompound();
        return this.Ok(hydrogenAtomsInSaturatedCompound);
    }
}