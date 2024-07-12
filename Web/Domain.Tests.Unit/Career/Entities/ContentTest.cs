using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.Entities;

public class ContentsTest : IClassFixture<ContentsValueObjectsFixture>
{
    private readonly ContentsValueObjectsFixture _fixture;

    /// <summary>
    /// Constructor that receives the fixture to be used in the tests.
    /// </summary>
    /// <param name="fixture"></param>
    public ContentsTest(ContentsValueObjectsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void ContentsConstructor_WithValidParameters_ShouldReturnContent()
    {
        // Arrange
        Guid inputId = Guid.NewGuid();

        // Act
        var content = new Contents(
            inputId,
            _fixture.ContentName,
            _fixture.Description,
            _fixture.CareerAcronym,
            _fixture.ContentType);

        // Assert
        // Probably only need to check the id (?)
        content.Id.Should().Be(inputId, because:"Given correct parameter, should create content.");
        content.ContentName.Should().Be(_fixture.ContentName);
        content.Description.Should().Be(_fixture.Description);
        content.CareerAcronym.Should().Be(_fixture.CareerAcronym);
        content.ContentType.Should().Be(_fixture.ContentType);
    }

    [Fact]
    public void ContentsConstructor_WithEmptyId_ShouldThrowException()
    {
        // Arrange
        Guid inputId = Guid.Empty;

        // Act
        Action act = () => new Contents(
            inputId,
            _fixture.ContentName,
            _fixture.Description,
            _fixture.CareerAcronym,
            _fixture.ContentType);

        // Assert
        act.Should().Throw<ArgumentException>("Id cannot be empty");
    }
}
