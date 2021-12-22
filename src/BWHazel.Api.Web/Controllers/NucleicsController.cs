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
    /// Gets the complimentary DNA, messenger RNA and peptide for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <param name="useTriple">A value indicating whether triple-character peptide codes should be used.</param>
    /// <remarks>
    /// This method is now obsolete and shall be removed on 1st January 2022.
    /// Please use "/api/nucleics/centralDogma/{ssDna}?tripleCode={tripleCode}" instead.
    /// </remarks>
    /// <returns>The complimentary DNA, messenger RNA and peptide.</returns>
    /// <response code="200">Returns the complimentary DNA, messenger RNA and peptide.</response>
    [Obsolete]
    [HttpGet]
    [Route("{ssDna}/triple/{useTriple}")]
    public ActionResult<CentralDogmaInfo> GetCentralDogmaInfoLegacy(string ssDna, bool useTriple)
    {
        return this.GetCentralDogmaInfo(ssDna, useTriple);
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
    /// Gets the complimentary DNA for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <remarks>
    /// This method is now obsolete and shall be removed on 1st January 2022.
    /// Please use "/api/nucleics/centralDogma/{ssDna}/cDna" instead.
    /// </remarks>
    /// <returns>The complimentary DNA.</returns>
    /// <response code="200">Returns the complimentary DNA.</response>
    [Obsolete]
    [HttpGet]
    [Route("{ssDna}/cdna")]
    public ActionResult<string> GetComplimentaryDnaLegacy(string ssDna)
    {
        return this.GetComplimentaryDna(ssDna);
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
    /// Gets the messenger RNA for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <remarks>
    /// This method is now obsolete and shall be removed on 1st January 2022.
    /// Please use "/api/nucleics/centralDogma/{ssDna}/mRna" instead.
    /// </remarks>
    /// <returns>The messenger RNA.</returns>
    /// <response code="200">Returns the messenger RNA.</response>
    [Obsolete]
    [HttpGet]
    [Route("{ssDna}/mrna")]
    public ActionResult<string> GetMessengerRnaLegacy(string ssDna)
    {
        return this.GetMessengerRna(ssDna);
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

    /// <summary>
    /// Gets the peptide for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <remarks>
    /// This method is now obsolete and shall be removed on 1st January 2022.
    /// Please use "/api/nucleics/centralDogma/{ssDna}/peptide" instead.
    /// </remarks>
    /// <returns>The peptide.</returns>
    /// <response code="200">Returns the peptide.</response>
    [Obsolete]
    [HttpGet]
    [Route("{ssDna}/protein")]
    public ActionResult<string> GetPeptideLegacy(string ssDna)
    {
        return this.GetPeptide(ssDna, false);
    }

    /// <summary>
    /// Gets the peptide for a single-stranded DNA strand.
    /// </summary>
    /// <param name="ssDna">The single-stranded DNA strand.</param>
    /// <param name="acidCode">The number of characters for the amino acid codes.</param>
    /// <remarks>
    /// This method is now obsolete and shall be removed on 1st January 2022.  Please note this endpoint is not being replaced.
    /// Please use the "/api/nucleics/centralDogma/{ssDna}/peptide?tripleCode={tripleCode}"
    /// endpoint to get the peptide for a single-stranded DNA strand.
    /// </remarks>
    /// <returns>The peptide.</returns>
    /// <response code="200">Returns the peptide.</response>
    /// <response code="400">The acid code value is not 1 or 3.</response>
    [Obsolete]
    [HttpGet]
    [Route("{ssDna}/protein/{acidCode}")]
    public ActionResult<string> GetPeptideLegacy(string ssDna, int acidCode)
    {
        bool useTriple = false;
        if (acidCode == 1)
        {
            useTriple = false;
        }
        else if (acidCode == 3)
        {
            useTriple = true;
        }
        else
        {
            return this.BadRequest("Acid code must be 1 or 3.");
        }

        return this.GetPeptide(ssDna, useTriple);
    }
}