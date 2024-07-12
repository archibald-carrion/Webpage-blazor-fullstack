using Moq;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Career.Repositories;
using FluentAssertions;
using MockQueryable.Moq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Tests.Unit.Career.Repositories;

public class SqlCareerRepositoryTests : IClassFixture<SqlCareerRepositoryTestsFixture>
{
    private readonly SqlCareerRepositoryTestsFixture _fixture;

    public SqlCareerRepositoryTests(SqlCareerRepositoryTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCareersAsync_WhenThereIsCareeer_ShouldReturnAllCareers()
    {
        // Arrange
        var careerList = _fixture.CareersList;
        var careerDBSetMock = careerList.BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext
            .Setup(dbContext => dbContext.Careers)
            .Returns(careerDBSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var results = await repository.GetCareersAsync();

        // Assert
        results.Should().BeEquivalentTo(careerList, because: "When there is vlaid element in the database the system should return them.");
    }

    [Fact]
    public async Task GetCareersAsync_WhenNoCareer_ShouldReturnEmpty()
    {
        // Arrange
        var careerList = _fixture.EmptyCareerList;
        var careerDBSetMock = careerList.BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext
            .Setup(dbContext => dbContext.Careers)
            .Returns(careerDBSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var results = await repository.GetCareersAsync();

        // Assert
        results.Should().BeEmpty(because: "When there is no career in the databse the system should return empty.");
    }

    [Fact]
    public async Task PostCreateCareerAsync_WithValidInput_ShouldReturnTrue()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var careerDbSetMock = _fixture.CareersList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Careers)
            .Returns(careerDbSetMock.Object);

        var mockDbTransaction = new Mock<IDbContextTransaction>();
        var mockDatabaseFacade = new Mock<DatabaseFacade>(mockDbContext.Object);
        mockDbContext
            .Setup(dbContext => dbContext.Database)
            .Returns(mockDatabaseFacade.Object);
        mockDatabaseFacade
          .Setup(db => db.BeginTransactionAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(mockDbTransaction.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var validInput = JsonSerializer.Serialize(new
        {
            Acronym = "CS",
            Description = "Computer Science",
            Name = "Computer Science",
            Duration = 4,
            isSteamRelated = true,
            PercentageFemaleStudents = 30.5,
            isECCI = true
        });

        // Act
        var result = await repository.PostCreateCareerAsync(validInput);

        // Assert
        result.Should().BeTrue("You should be able to create career with valid current data.");
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task DeleteContentAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.AsQueryable().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        // Mock the FindAsync method
        var contentToDelete = _fixture.ContentsList.First();
        mockDbContext
            .Setup(dbContext => dbContext.Contents.FindAsync(contentToDelete.Id))
            .ReturnsAsync(contentToDelete);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var result = await repository.DeleteContentAsync(contentToDelete.Id);

        // Assert
        result.Should().BeTrue(because: "you should be able to delete given correct content id");
        mockDbContext.Verify(m => m.Contents.Remove(It.Is<Contents>(c => c.Id == contentToDelete.Id)), Times.Once());
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task DeleteContentAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var invalidContentId = Guid.Empty;

        // Act
        var result = await repository.DeleteContentAsync(invalidContentId);

        // Assert
        result.Should().BeFalse("cannot delete content with invalid id.");
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task ModifyContentAsync_WithValidNewCareerAcronym_ShouldReturnTrue()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.AsQueryable().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        var existingContent = _fixture.ContentsList.First();
        mockDbContext
            .Setup(db => db.Contents.FindAsync(existingContent.Id))
            .ReturnsAsync(existingContent);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var validNewCareerAcronym = "EE";
        var validInput = JsonSerializer.Serialize(new[]
        {
            new
            {
                Id = existingContent.Id,
                ContentName = "Programming",
                Description = "Introduction to programming",
                CareerAcronym = validNewCareerAcronym,
                ContentType = "Course"
            }
        });

        // Act
        var result = await repository.PostModifyContentAsync(validInput);

        // Assert
        result.Should().BeTrue(because: "The content and modification string are both correct.");
        mockDbContext.Verify(m => m.Contents.Remove(It.Is<Contents>(c => c.Id == existingContent.Id)), Times.Once());
        mockDbContext.Verify(m => m.Contents.Add(It.Is<Contents>(c => c.CareerAcronym.Value == validNewCareerAcronym)), Times.Once());
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task PostModifyContentAsync_WithValidInput_ShouldReturnTrue()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var existingContent = _fixture.ContentsList.First();
        var modifiedContentInput = JsonSerializer.Serialize(new[]
        {
            new
            {
                Id = existingContent.Id,
                ContentName = "Modified Content",
                Description = "Modified Description",
                CareerAcronym = existingContent.CareerAcronym.Value,
                ContentType = "Modified Type"
            }
        });

        // Mock FindAsync to return the existing content
        mockDbContext
            .Setup(db => db.Contents.FindAsync(existingContent.Id))
            .ReturnsAsync(existingContent);

        // Act
        var result = await repository.PostModifyContentAsync(modifiedContentInput);

        // Assert
        result.Should().BeTrue(because:"Can modify existing content with correct modification string input.");
        mockDbContext.Verify(m => m.Contents.Remove(It.IsAny<Contents>()), Times.Once());
        mockDbContext.Verify(m => m.Contents.Add(It.IsAny<Contents>()), Times.Once());
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task PostModifyContentAsync_WithNonExistentContent_ShouldReturnFalse()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var nonExistentContentInput = JsonSerializer.Serialize(new
        {
            Id = Guid.NewGuid(), // This ID doesn't exist in the fixture
            ContentName = "Non-existent Content",
            Description = "This content doesn't exist",
            CareerAcronym = "CS",
            ContentType = "Test"
        });

        // Act
        var result = await repository.PostModifyContentAsync(nonExistentContentInput);

        // Assert
        result.Should().BeFalse(because:"Cannot modify content that does not exisits.");
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task PostModifyContentAsync_WithInvalidInput_ShouldReturnFalse()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var repository = new SqlCareerRepository(mockDbContext.Object);

        var invalidInput = "This is not valid JSON";

        // Act
        var result = await repository.PostModifyContentAsync(invalidInput);

        // Assert
        result.Should().BeFalse(because: "Invalid json file must return false.");
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task PostModifyContentAsync_WithEmptyInput_ShouldReturnFalse()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var repository = new SqlCareerRepository(mockDbContext.Object);

        var emptyInput = string.Empty;

        // Act
        var result = await repository.PostModifyContentAsync(emptyInput);

        // Assert
        result.Should().BeFalse(because: "cannot modify a content given an empty json file");
        mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task PostModifyContentAsync_WithInvalidInput_ShouldThrowReturnFalse()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var repository = new SqlCareerRepository(mockDbContext.Object);

        var invalidInput = "This is not valid JSON";

        // Act
        var result = await repository.PostModifyContentAsync(invalidInput);

        // Assert
        result.Should().BeFalse(because:"the input string is not a valid json file");
    }

    [Fact]
    public async Task GetScholarshipBudgetAsync_WithValidCareerWithNoContent_ShouldReturn0()
    {
        // Arrange
        var careerAcronym = "CS";
        var contentsDbSetMock = _fixture.ContentsList.AsQueryable().BuildMockDbSet();
        var careerDbSetMock = _fixture.CareersList.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(dbContext => dbContext.Careers).Returns(careerDbSetMock.Object);
        mockDbContext.Setup(dbContext => dbContext.Contents).Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var result = await repository.GetScholarshipBudgetAsync(careerAcronym);

        // Assert
        result.Should().Be(0.0, because: "The career has no content, so no budget."); // The budget is 0.0 because the career has no "valuable" contents
    }

    [Fact]
    public async Task PostCreateContent_WithValidInput_ShouldReturnTrue()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var contentsDbSetMock = _fixture.ContentsList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Contents)
            .Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var validInput = JsonSerializer.Serialize(new
        {
            ContentName = "New Content",
            Description = "New Description",
            CareerAcronym = "CS",
            ContentType = "Course"
        });

        // Act
        var result = await repository.PostCreateContentAsync(validInput);

        // Assert
        result.Should().BeTrue( because: "The input is valied so should create content.");
        mockDbContext.Verify(m => m.Contents.Add(It.IsAny<Contents>()), Times.Once());
    }

    [Fact]
    public async Task GetScholarshipBudgetAsync_WithValidInput_ShouldReturnBudgetOfNot0()
    {
        // Arrange
        var careerAcronym = "EE";
        var contentsDbSetMock = _fixture.ContentsList.AsQueryable().BuildMockDbSet();
        var careerDbSetMock = _fixture.CareersList.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(dbContext => dbContext.Careers).Returns(careerDbSetMock.Object);
        mockDbContext.Setup(dbContext => dbContext.Contents).Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var result = await repository.GetScholarshipBudgetAsync(careerAcronym);

        // Assert
        result.Should().NotBe(0.0, because: "The career has no valuable content, so has budget of 0"); // The budget is 0.0 because the career has no "valuable" contents
    }

    [Fact]
    public async Task GetContentsAsync_WithValidCareerAcronym_ShouldReturnAllContents()
    {
        // Arrange
        var careerAcronym = "CS";
        var contentsDbSetMock = _fixture.ContentsList.AsQueryable().BuildMockDbSet();
        var careerDbSetMock = _fixture.CareersList.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(dbContext => dbContext.Careers).Returns(careerDbSetMock.Object);
        mockDbContext.Setup(dbContext => dbContext.Contents).Returns(contentsDbSetMock.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        // Act
        var result = await repository.GetContentsAsync(careerAcronym);

        // Assert
        result.Should().BeEquivalentTo(_fixture.ContentsList.Where(c => c.CareerAcronym.Value == careerAcronym), because: "Should return correct value.");
    }

    [Fact]
    public async Task PostCreateCareerAsync_WithDatabaseException_ShouldHandleException()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        var careerDbSetMock = _fixture.CareersList.BuildMock().BuildMockDbSet();

        mockDbContext
            .Setup(dbContext => dbContext.Careers)
            .Returns(careerDbSetMock.Object);

        var mockDbTransaction = new Mock<IDbContextTransaction>();
        var mockDatabaseFacade = new Mock<DatabaseFacade>(mockDbContext.Object);
        mockDbContext
            .Setup(dbContext => dbContext.Database)
            .Returns(mockDatabaseFacade.Object);
        mockDatabaseFacade
          .Setup(db => db.BeginTransactionAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(mockDbTransaction.Object);

        var repository = new SqlCareerRepository(mockDbContext.Object);

        var invalidInput = "Not a valid json";

        // Act
        var result = await repository.PostCreateCareerAsync(invalidInput);

        // Assert
        result.Should().BeFalse(because: "Cannot parse non-valid json file.");
    }
}

