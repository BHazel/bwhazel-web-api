using BWHazel.Api.Core.Nucleics;
using Shouldly;
using Xunit;

namespace BWHazel.Api.Core.Tests.Nucleics;

/// <summary>
/// Tests for the <see cref="AminoAcid"/> class.
/// </summary>
public class AminoAcidTests
{
    /// <summary>
    /// Tests that the <see cref="AminoAcid"/> class constructor sets the correct property values.
    /// </summary>
    [Fact]
    public void Constructor_SetsCorrectPropertyValues()
    {
        string name = "Test Amino Acid";
        char code = '!';
        string codeTriple = "???";

        AminoAcid aminoAcid = new(name, code, codeTriple);

        aminoAcid.Name.ShouldBe(name);
        aminoAcid.Code.ShouldBe(code);
        aminoAcid.CodeTriple.ShouldBe(codeTriple);
    }
}
