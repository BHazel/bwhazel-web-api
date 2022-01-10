using System;
using Microsoft.AspNetCore.Mvc;
using BWHazel.Api.Core.Nucleics;
using BWHazel.Api.Web.Models;

namespace BWHazel.Api.Web.Controllers;

/// <summary>
/// Actions for nucleic operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NucleicsController : Controller
{
    private readonly CentralDogma centralDogma = new();

    /// <summary>
    /// Gets the complimentary DNA, messenger RNA and peptide for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <param name="tripleCode">A value indicating whether triple-character peptide codes should be used.</param>
    /// <returns>The complimentary DNA, messenger RNA and peptide.</returns>
    /// <response code="200">Returns the complimentary DNA, messenger RNA and peptide.</response>
    [HttpGet]
    [Route("centralDogma/{ssDna}")]
    public ActionResult<CentralDogmaInfo> GetCentralDogmaInfo(string ssDna, bool tripleCode)
    {
        return this.Ok(new CentralDogmaInfo()
        {
            SsDna = ssDna,
            CDna = this.centralDogma.GetComplimentaryDna(ssDna),
            MRna = this.centralDogma.GetMessengerRna(ssDna),
            Peptide = this.centralDogma.GetPeptide(ssDna, tripleCode)
        }) ;
    }

    /// <summary>
    /// Gets the complimentary DNA for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <returns>The complimentary DNA.</returns>
    /// <response code="200">Returns the complimentary DNA.</response>
    [HttpGet]
    [Route("centralDogma/{ssDna}/cDna")]
    public ActionResult<string> GetComplimentaryDna(string ssDna)
    {
        return this.Ok(this.centralDogma.GetComplimentaryDna(ssDna));
    }

    /// <summary>
    /// Gets the messenger RNA for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <returns>The messenger RNA.</returns>
    /// <response code="200">Returns the messenger RNA.</response>
    [HttpGet]
    [Route("centralDogma/{ssDna}/mRna")]
    public ActionResult<string> GetMessengerRna(string ssDna)
    {
        return this.Ok(this.centralDogma.GetMessengerRna(ssDna));
    }

    /// <summary>
    /// Gets the peptide for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <param name="tripleCode">A value indicating whether triple-character peptide codes should be used.</param>
    /// <returns>The peptide.</returns>
    /// <response code="200">Returns the peptide.</response>
    [HttpGet]
    [Route("centralDogma/{ssDna}/peptide")]
    public ActionResult<string> GetPeptide(string ssDna, bool tripleCode)
    {
        return this.Ok(this.centralDogma.GetPeptide(ssDna, tripleCode));
    }
}