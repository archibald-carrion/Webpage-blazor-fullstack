using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class AcronymTests : IClassFixture<AcronymFixture>
{
    private readonly AcronymFixture _fixture;

    /// <summary>
    /// Constructor that receives the fixture to be used in the tests.
    /// </summary>
    /// <param name="fixture"></param>
    public AcronymTests(AcronymFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void AcronymConstructor_WithValidValue_ShouldReturnCorrectAcronym()
    {
        // Arrange
        string validAcronym = _fixture.ValidAcronym;

        // Act
        var acronym = new Acronym(validAcronym);

        // Assert
        acronym.Value.Should().Be(validAcronym, because: "Given correct parameters constructor should create correct acronym");
    }

    [Fact]
    public void AcronymConstructor_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        string invalidAcronym = _fixture.InvalidAcronymEmpty;

        // Act
        Action act = () => Acronym.Create(invalidAcronym);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid career acronym.", because: "Should no create acronym with empty values.");
    }

    [Fact]
    public void TryCreate_WithValidValue_ShouldReturnTrue()
    {
        // Arrange
        string validAcronym = _fixture.ValidAcronym;

        // Act
        var result = Acronym.TryCreate(validAcronym, out var acronym);

        // Assert
        result.Should().BeTrue(because: "Trycreate should return true givne correct values");
        acronym.Value.Should().Be(validAcronym, because:"Trycreate should return true givne correct values");
    }

    [Fact]
    public void TryCreate_WithTooLongValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidAcronym = _fixture.InvalidAcronymTooLong;

        // Act
        var result = Acronym.TryCreate(invalidAcronym, out var acronym);

        // Assert
        result.Should().BeFalse(because: "Trycreate should return false givne too long values");
        acronym.Should().Be(Acronym.Invalid, because: "Trycreate should return false givne too long values");
    }

    [Fact]
    public void TryCreate_WithIllegalCharacters_ShouldReturnFalse()
    {
        // Arrange
        string invalidAcronym = _fixture.InvalidAcronymIllegalChar;

        // Act
        var result = Acronym.TryCreate(invalidAcronym, out var acronym);

        // Assert
        result.Should().BeFalse(because: "Trycreate should return false givne illegal chars");
        acronym.Should().Be(Acronym.Invalid, because: "Trycreate should return false givne illegal chars");
    }

    [Fact]
    public void TryCreate_WithEmptyValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidAcronym = _fixture.InvalidAcronymEmpty;

        // Act
        var result = Acronym.TryCreate(invalidAcronym, out var acronym);

        // Assert
        result.Should().BeFalse(because: "Trycreate should return false givne empty values");
        acronym.Should().Be(Acronym.Invalid, because: "Trycreate should return false givne empty values");
    }

    [Fact]
    public void Create_WithValidValue_ShouldReturnAcronym()
    {
        // Arrange
        string validAcronym = _fixture.ValidAcronym;

        // Act
        var acronym = Acronym.Create(validAcronym);

        // Assert
        acronym.Value.Should().Be(validAcronym, because: "Create should return acronym givne correct values");
    }

    [Fact]
    public void Create_WithInvalidValue_ShouldThrowArgumentException()
    {
        // Arrange
        string invalidAcronym = _fixture.InvalidAcronymTooLong;

        // Act
        Action act = () => Acronym.Create(invalidAcronym);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid career acronym.", because: "Create should throw exception given string with too many chars.");
    }
}