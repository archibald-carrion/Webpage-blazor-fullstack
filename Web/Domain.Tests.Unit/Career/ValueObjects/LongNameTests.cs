using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class LongNameTests : IClassFixture<LongNameFixture>
{
    private readonly LongNameFixture _fixture;

    /// <summary>
    /// Constructor for the LongNameTests class.
    /// </summary>
    /// <param name="fixture"></param>
    public LongNameTests(LongNameFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void LongNameConstructor_WithValidValue_ShouldReturnValidLongName()
    {
        // Arrange
        string validLongName = _fixture.ValidLongName;

        // Act
        var longName = new LongName(validLongName);

        // Assert
        longName.Value.Should().Be(validLongName, because:"You should be able to create a LongName with valid parameters.");
    }

    [Fact]
    public void LongNameConstructor_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        string invalidLongName = _fixture.InvalidLongNameEmpty;

        // Act
        Action act = () => LongName.Create(invalidLongName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid long name.", because: "You cannot create a LongName with empty values.");
    }

    [Fact]
    public void TryCreate_WithValidValue_ShouldReturnTrue()
    {
        // Arrange
        string validLongName = _fixture.ValidLongName;

        // Act
        var result = LongName.TryCreate(validLongName, out var longName);

        // Assert
        // TODO: are both asserts necessary?
        result.Should().BeTrue(because: "TryCreate should recognize givne parameters as valid");
        longName.Value.Should().Be(validLongName, because: "TryCreate should recognize givne parameters as valid");
    }

    [Fact]
    public void TryCreate_WithTooLongValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidLongName = _fixture.InvalidLongNameTooLong;

        // Act
        var result = LongName.TryCreate(invalidLongName, out var longName);

        // Assert
        result.Should().BeFalse(because: "TryCreate should recognize that given parameters are too long");
        longName.Should().Be(LongName.Invalid, because: "TryCreate should recognize that given parameters are too long");
    }

    [Fact]
    public void TryCreate_WithIllegalCharacters_ShouldReturnFalse()
    {
        // Arrange
        string invalidLongName = _fixture.InvalidLongNameIllegalChar;

        // Act
        var result = LongName.TryCreate(invalidLongName, out var longName);

        // Assert
        result.Should().BeFalse(because: "TryCreate should recognize given parameters as invalid because there is invalid characters in the given string");
        longName.Should().Be(LongName.Invalid, because: "TryCreate should recognize given parameters as invalid because there is invalid characters in the given string");
    }

    [Fact]
    public void TryCreate_WithEmptyValue_ShouldReturnFalse()
    {
        // Arrange
        string invalidLongName = _fixture.InvalidLongNameEmpty;

        // Act
        var result = LongName.TryCreate(invalidLongName, out var longName);

        // Assert
        result.Should().BeFalse(because: "TryCreate should return false when receiving empty values");
        longName.Should().Be(LongName.Invalid, because: "TryCreate should return false when receiving empty values");
    }

    [Fact]
    public void Create_WithValidValue_ShouldReturnLongName()
    {
        // Arrange
        string validLongName = _fixture.ValidLongName;

        // Act
        var longName = LongName.Create(validLongName);

        // Assert
        longName.Value.Should().Be(validLongName, because: "Create should recognize givne parameters as valid an create longname");
    }

    [Fact]
    public void Create_WithInvalidValue_ShouldThrowException()
    {
        // Arrange
        string invalidLongName = _fixture.InvalidLongNameTooLong;

        // Act
        Action act = () => LongName.Create(invalidLongName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid long name.", because: "Create function does not create longname given a string too long.");
    }
}
