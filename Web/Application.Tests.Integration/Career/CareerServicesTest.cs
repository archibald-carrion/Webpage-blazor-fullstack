using Moq;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Tests.Integration.Career;

public class CreateCareerServiceTests : IClassFixture<CareerServicesFixture>
{
    private readonly CareerServicesFixture _fixture;

    public CreateCareerServiceTests(CareerServicesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetScholarshipBudgetAsync_ReturnsExpectedValue()
    {
        // Arrange
        var careerAcronym = new Acronym("CS");
        const double expectedBudget = 10000.0;
        _fixture.MockCareerRepository.Setup(r => r.GetScholarshipBudgetAsync(careerAcronym.Value))
            .ReturnsAsync(expectedBudget);
        var service = new CreateCareerService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.GetScholarshipBudgetAsync(careerAcronym.Value);

        // Assert
        Assert.Equal(expectedBudget, result);
    }

    [Fact]
    public async Task PostCreateCareerAsync_ReturnsExpectedValue()
    {
        // Arrange
        const string input = "Computer Science,CS";
        const bool expectedResult = true;
        _fixture.MockCareerRepository.Setup(r => r.PostCreateCareerAsync(input))
            .ReturnsAsync(expectedResult);
        var service = new CreateCareerService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.PostCreateCareerAsync(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}

public class CreateContentsServiceTests : IClassFixture<CareerServicesFixture>
{
    private readonly CareerServicesFixture _fixture;

    public CreateContentsServiceTests(CareerServicesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteContentAsync_ReturnsExpectedValue()
    {
        // Arrange
        var contentId = Guid.NewGuid();
        const bool expectedResult = true;
        _fixture.MockCareerRepository.Setup(r => r.DeleteContentAsync(contentId))
            .ReturnsAsync(expectedResult);
        var service = new CreateContentsService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.DeleteContentAsync(contentId);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task PostCreateContentAsync_ReturnsExpectedValue()
    {
        // Arrange
        const string input = "Sample Content";
        const bool expectedResult = true;
        _fixture.MockCareerRepository.Setup(r => r.PostCreateContentAsync(input))
            .ReturnsAsync(expectedResult);
        var service = new CreateContentsService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.PostCreateContentAsync(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task PostModifyContentAsync_ReturnsExpectedValue()
    {
        // Arrange
        const string input = "Modified Content";
        const bool expectedResult = true;
        _fixture.MockCareerRepository.Setup(r => r.PostModifyContentAsync(input))
            .ReturnsAsync(expectedResult);
        var service = new CreateContentsService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.PostModifyContentAsync(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}

public class DeleteContentServiceTests : IClassFixture<CareerServicesFixture>
{
    private readonly CareerServicesFixture _fixture;

    public DeleteContentServiceTests(CareerServicesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteContentAsync_ReturnsExpectedValue()
    {
        // Arrange
        var contentId = Guid.NewGuid();
        const bool expectedResult = true;
        _fixture.MockCareerRepository.Setup(r => r.DeleteContentAsync(contentId))
            .ReturnsAsync(expectedResult);
        var service = new DeleteContentService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.DeleteContentAsync(contentId);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}

public class ListCareerServiceTests : IClassFixture<CareerServicesFixture>
{
    private readonly CareerServicesFixture _fixture;

    public ListCareerServiceTests(CareerServicesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCareersAsync_ReturnsExpectedValue()
    {
        // Arrange
        var expectedCareers = new List<Domain.Career.Entities.Career>
        {
            new Domain.Career.Entities.Career(new Acronym("CS"), new LongName("Computer Science"), new LongName("descript of Computer Science"), 1, true, 0.5, true),
            new Domain.Career.Entities.Career(new Acronym("CC"), new LongName("Computer Ccience but with C"), new LongName("descript of Computer Ccience"), 1, true, 0.5, true)
        };
        _fixture.MockCareerRepository.Setup(r => r.GetCareersAsync())
            .ReturnsAsync(expectedCareers);
        var service = new ListCareerService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.GetCareersAsync();

        // Assert
        Assert.Equal(expectedCareers, result);
    }
}

public class ListContentsServiceTests : IClassFixture<CareerServicesFixture>
{
    private readonly CareerServicesFixture _fixture;

    public ListContentsServiceTests(CareerServicesFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetContentsAsync_ReturnsExpectedValue()
    {
        // Arrange
        var careerAcronym = new Acronym("CS");
        var expectedContents = new List<Contents>
        {
            new Contents(Guid.NewGuid(), new MediumName("Introduction to Programming"), new LongName("Description 1"), careerAcronym, new MediumName("Type1")),
            new Contents(Guid.NewGuid(), new MediumName("Data Structures and Algorithms"), new LongName("Description 2"), careerAcronym, new MediumName("Type2"))
        };
        _fixture.MockCareerRepository.Setup(r => r.GetContentsAsync(careerAcronym.Value))
            .ReturnsAsync(expectedContents);
        var service = new ListContentsService(_fixture.MockCareerRepository.Object);

        // Act
        var result = await service.GetContentsAsync(careerAcronym.Value);

        // Assert
        Assert.Equal(expectedContents, result);
    }
}