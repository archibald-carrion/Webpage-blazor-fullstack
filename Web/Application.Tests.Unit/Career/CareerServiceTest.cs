using Moq;
using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Tests.Unit.Services;

public class CreateCareerServiceTests : IClassFixture<CareerServiceFixture>
{
    private readonly CareerServiceFixture _fixture;
    private readonly Mock<ICareerRepository> _mockRepository;
    private readonly CreateCareerService _service;

    public CreateCareerServiceTests(CareerServiceFixture fixture)
    {
        _fixture = fixture;
        _mockRepository = new Mock<ICareerRepository>();
        _service = new CreateCareerService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetScholarshipBudgetAsync_ShouldReturnCorrectBudget()
    {
        // Arrange
        var careerAcronym = "CS";
        var expectedBudget = 1000.0;
        _mockRepository.Setup(r => r.GetScholarshipBudgetAsync(careerAcronym))
            .ReturnsAsync(expectedBudget);

        // Act
        var result = await _service.GetScholarshipBudgetAsync(careerAcronym);

        // Assert
        result.Should().Be(expectedBudget, because:"The actual budget should be the same as the predicted budget.");
    }

    [Fact]
    public async Task PostCreateCareerAsync_ShouldReturnTrue()
    {
        // Arrange
        var input = "{\"Acronym\":\"ME\",\"Name\":\"Mechanical Engineering\"}";
        _mockRepository.Setup(r => r.PostCreateCareerAsync(input))
            .ReturnsAsync(true);

        // Act
        var result = await _service.PostCreateCareerAsync(input);

        // Assert
        result.Should().BeTrue(because: "Creating a career should return true when given correct json string.");
    }
}

public class CreateContentsServiceTests : IClassFixture<CareerServiceFixture>
{
    private readonly CareerServiceFixture _fixture;
    private readonly Mock<ICareerRepository> _mockRepository;
    private readonly CreateContentsService _service;

    public CreateContentsServiceTests(CareerServiceFixture fixture)
    {
        _fixture = fixture;
        _mockRepository = new Mock<ICareerRepository>();
        _service = new CreateContentsService(_mockRepository.Object);
    }

    [Fact]
    public async Task DeleteContentAsync_ShouldReturnTrue()
    {
        // Arrange
        var contentId = Guid.NewGuid();
        _mockRepository.Setup(r => r.DeleteContentAsync(contentId))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteContentAsync(contentId);

        // Assert
        result.Should().BeTrue(because:"deleting a content given correct id shoudl return true");
    }

    [Fact]
    public async Task PostCreateContentAsync_ShouldReturnTrue()
    {
        // Arrange
        var input = "{\"ContentName\":\"New Content\",\"CareerAcronym\":\"CS\"}";
        _mockRepository.Setup(r => r.PostCreateContentAsync(input))
            .ReturnsAsync(true);

        // Act
        var result = await _service.PostCreateContentAsync(input);

        // Assert
        result.Should().BeTrue(because:"Creating a new content with valid json string should return true.");
    }

    [Fact]
    public async Task PostModifyContentAsync_ShouldReturnTrue()
    {
        // Arrange
        var input = "{\"Id\":\"4325ad8f-c772-4f78-b124-c5a4d6d88eb3\",\"ContentName\":\"Modif Content\"}";
        _mockRepository.Setup(r => r.PostModifyContentAsync(input))
            .ReturnsAsync(true);

        // Act
        var result = await _service.PostModifyContentAsync(input);

        // Assert
        result.Should().BeTrue(because: "modifying an content with a valid json string should return true.");
    }
}

public class DeleteContentServiceTests : IClassFixture<CareerServiceFixture>
{
    private readonly CareerServiceFixture _fixture;
    private readonly Mock<ICareerRepository> _mockRepository;
    private readonly DeleteContentService _service;

    public DeleteContentServiceTests(CareerServiceFixture fixture)
    {
        _fixture = fixture;
        _mockRepository = new Mock<ICareerRepository>();
        _service = new DeleteContentService(_mockRepository.Object);
    }

    [Fact]
    public async Task DeleteContentAsync_ShouldReturnTrue()
    {
        // Arrange
        var contentId = Guid.NewGuid();
        _mockRepository.Setup(r => r.DeleteContentAsync(contentId))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteContentAsync(contentId);

        // Assert
        result.Should().BeTrue(because: "Deleting an element axisting in the db should return true if done correctly.");
    }
}

public class ListCareerServiceTests : IClassFixture<CareerServiceFixture>
{
    private readonly CareerServiceFixture _fixture;
    private readonly Mock<ICareerRepository> _mockRepository;
    private readonly ListCareerService _service;

    public ListCareerServiceTests(CareerServiceFixture fixture)
    {
        _fixture = fixture;
        _mockRepository = new Mock<ICareerRepository>();
        _service = new ListCareerService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetCareersAsync_ShouldReturnAllCareers()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetCareersAsync())
            .ReturnsAsync(_fixture.Careers);

        // Act
        var result = await _service.GetCareersAsync();

        // Assert
        result.Should().BeEquivalentTo(_fixture.Careers, because: "Get careers shold return all careers in the db.");
    }
}

public class ListContentsServiceTests : IClassFixture<CareerServiceFixture>
{
    private readonly CareerServiceFixture _fixture;
    private readonly Mock<ICareerRepository> _mockRepository;
    private readonly ListContentsService _service;

    public ListContentsServiceTests(CareerServiceFixture fixture)
    {
        _fixture = fixture;
        _mockRepository = new Mock<ICareerRepository>();
        _service = new ListContentsService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetContentsAsync_ShouldReturnContentsForCareer()
    {
        // Arrange
        var careerAcronym = "CS";
        // get element to make a comparaison
        var expectedContents = _fixture.Contents.Where(c => c.CareerAcronym.Value == careerAcronym);
        _mockRepository.Setup(r => r.GetContentsAsync(careerAcronym))
            .ReturnsAsync(expectedContents);

        // Act
        var result = await _service.GetContentsAsync(careerAcronym);

        // Assert
        result.Should().BeEquivalentTo(expectedContents, because: "If functional should return the content of carrers");
    }
}