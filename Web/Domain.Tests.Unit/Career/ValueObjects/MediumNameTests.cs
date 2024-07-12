using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class MediumNameTest : IClassFixture<MediumNameFixture>
{
    private readonly MediumNameFixture _fixture;

    /// <summary>
    /// Constructor for the test class
    /// </summary>
    /// <param name="fixture"></param>
    public MediumNameTest(MediumNameFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TryCreate_WithValidValue_ShouldReturnTrue()
    {
        // Arrange
        string validMediumName = _fixture.ValidMediumName;

        // Act
        var result = MediumName.TryCreate(validMediumName, out var mediumName);

        // Assert
        result.Should().BeTrue();
        mediumName.Value.Should().Be(validMediumName, because: "Valid values should allow user to create mediumname");
    }

    [Fact]
    public void Create_WithValidValue_ShouldReturnActualMediumName()
    {
        // Arrange
        string validMediumName = _fixture.ValidMediumName;

        // Act
        var mediumName = MediumName.Create(validMediumName);

        // Assert
        mediumName.Value.Should().Be(validMediumName, because: "Valid values should allow user to create mediumname");
    }

    /// <summary>
    /// diffenrence between this one and previous is that this one call constructor directly
    /// </summary>

    [Fact]
    public void MediumNameConstructor_WithValidValue_ShouldReturnActualMeduimName()
    {
        // Arrange
        string validMediumName = _fixture.ValidMediumName;

        // Act
        var mediumName = new MediumName(validMediumName);

        // Assert
        mediumName.Value.Should().Be(validMediumName, because: "Valid values should allow user to create mediumname");
    }

    [Fact]
    public void MediumNameConstructor_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        string invalidMediumName = _fixture.InvalidMediumNameEmpty;

        // Act
        Action act = () => MediumName.Create(invalidMediumName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid medium name.", because: "You cannot create a Medium name with empty values");
    }

    /// <summary>
    /// Note: there could be a Create method test for each invalid case, but it would be the same as the TryCreate method, so it is not necessary
    /// </summary>

    [Fact]
    public void TryCreate_WithTooLongValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidMediumName = _fixture.InvalidMediumNameTooLong;

        // Act
        var result = MediumName.TryCreate(invalidMediumName, out var mediumName);

        // Assert
        result.Should().BeFalse(because: "There is a maximum of character to the medium name");
        mediumName.Should().Be(MediumName.Invalid, because: "There is a maximum of character to the medium name");
    }

    [Fact]
    public void TryCreate_WithIllegalCharacters_ShouldReturnFalse()
    {
        // Arrange
        string invalidMediumName = _fixture.InvalidMediumNameIllegalChar;

        // Act
        var result = MediumName.TryCreate(invalidMediumName, out var mediumName);

        // Assert
        result.Should().BeFalse(because: "There cannot be illegal characters in the medium name");
        mediumName.Should().Be(MediumName.Invalid, because: "There cannot be illegal characters in the medium name");
    }

    [Fact]
    public void TryCreate_WithEmptyValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidMediumName = _fixture.InvalidMediumNameEmpty;

        // Act
        var result = MediumName.TryCreate(invalidMediumName, out var mediumName);

        // Assert
        result.Should().BeFalse(because: "Medium name cannot be created with empty values");
        mediumName.Should().Be(MediumName.Invalid, because: "Medium name cannot be created with empty values");
    }


    [Fact]
    public void Create_WithInvalidValue_ShouldThrowException()
    {
        // Arrange
        string invalidMediumName = _fixture.InvalidMediumNameTooLong;

        // Act
        Action act = () => MediumName.Create(invalidMediumName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid medium name.", because: "The medium name cannot be created with illegal characters.");
    }
}
