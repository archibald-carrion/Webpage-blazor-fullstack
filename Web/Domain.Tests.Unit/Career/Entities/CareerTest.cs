using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.Entities;

public class CareerTest : IClassFixture<CareerValueObjectsFixture>
{
    private readonly CareerValueObjectsFixture _fixture;

    /// <summary>
    /// Constructor that receives the fixture to be used in the tests.
    /// </summary>
    /// <param name="fixture"></param>
    public CareerTest(CareerValueObjectsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CareerConstructor_WithValidParameters_ShouldReturnCareer()
    {
        // Arrange
        const int inputDuration = 4;
        const bool inputIsSteamRelated = true;
        const double inputPercentageFemaleStudents = 25.5;
        const bool inputIsECCI = true;

        // Act
        var career = new Domain.Career.Entities.Career(
            _fixture.Acronym,
            _fixture.Name,
            _fixture.Description,
            inputDuration,
            inputIsSteamRelated,
            inputPercentageFemaleStudents,
            inputIsECCI);

        // Assert
        // Only need to check the acronym ?
        career.Acronym.Should().Be(_fixture.Acronym, because: "Should create valied career given valid elements");
        career.Name.Should().Be(_fixture.Name);
        career.Description.Should().Be(_fixture.Description);
        career.Duration.Should().Be(inputDuration);
        career.IsSteamRelated.Should().Be(inputIsSteamRelated);
        career.PercentageFemaleStudents.Should().Be(inputPercentageFemaleStudents);
        career.IsECCI.Should().Be(inputIsECCI);
    }

    [Fact]
    public void CareerConstructor_WithNegativeDuration_ShouldThrowException()
    {
        // Arrange
        const int inputDuration = -1;
        const bool inputIsSteamRelated = true;
        const double inputPercentageFemaleStudents = 25.5;
        const bool inputIsECCI = true;

        // Act
        Action act = () => new Domain.Career.Entities.Career(
            _fixture.Acronym,
            _fixture.Name,
            _fixture.Description,
            inputDuration,
            inputIsSteamRelated,
            inputPercentageFemaleStudents,
            inputIsECCI);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Duration of career cannot be less than 0", because: "the duration of a career cannot be negative");
    }

    [Fact]
    public void CareerConstructor_WithNegativePercentage_ShouldThrowException()
    {
        // Arrange
        const int inputDuration = 4;
        const bool inputIsSteamRelated = true;
        const double inputPercentageFemaleStudents = -1;
        const bool inputIsECCI = true;

        // Act
        Action act = () => new Domain.Career.Entities.Career(
            _fixture.Acronym,
            _fixture.Name,
            _fixture.Description,
            inputDuration,
            inputIsSteamRelated,
            inputPercentageFemaleStudents,
            inputIsECCI);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Percentage cannot be negative", because: "The percentage cannot be negative.");
    }
}
