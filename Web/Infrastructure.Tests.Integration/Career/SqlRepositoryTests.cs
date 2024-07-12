using FluentAssertions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Career.Repositories;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Tests.Integration.Career;

[Collection("Database collection Careers")]
public class SqlCareerRepositoryTests : IClassFixture<SqlCareerRepositoryFixture>
{
    private readonly SqlCareerRepositoryFixture _fixture;
    private readonly SqlCareerRepository _repository;

    public SqlCareerRepositoryTests(SqlCareerRepositoryFixture fixture)
    {
        _fixture = fixture;
        _repository = new SqlCareerRepository(_fixture.ApplicationDbContext);
    }

    [Fact]
    public async Task GetCareersAsync_ShouldReturnAllCareers()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();

        // Act
        var careers = await _repository.GetCareersAsync();

        // Assert
        careers.Should().NotBeEmpty(because: "Test should retrieve elements from the database.");
    }

    [Fact]
    public async Task GetContentAsync_ShouldReturnContentoOfCareer()
    {

        // Arrange
        await _fixture.SeedDatabaseAsync();
        // get acronym from career
        var careers = await _repository.GetCareersAsync();
        var acronym = careers.First().Acronym;

        // Act
        var contents = await _repository.GetContentsAsync(acronym.Value);

        // Assert
        contents.Should().NotBeEmpty(because: "Test should retrieve elements from the database.");
    }

    [Fact]
    public async Task PostCreateCareerAsync_ShouldCreateNewCareer()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();
        var newCareerJson = @"{
            ""Acronym"": ""NEW"",
            ""Name"": ""New Career"",
            ""Description"": ""A new career for testing"",
            ""Duration"": 4,
            ""isSteamRelated"": true,
            ""PercentageFemaleStudents"": 45.5,
            ""isECCI"": false
        }";

        // Act
        var result = await _repository.PostCreateCareerAsync(newCareerJson);

        // Assert
        result.Should().BeTrue(because: "Given correct json, should create a new career");
        var careers = await _repository.GetCareersAsync();
        careers.Should().Contain(c => c.Acronym.Value == "NEW");
    }

    [Fact]
    public async Task PostCreateContentAsync_ShouldCreateNewContent()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();
        var careers = await _repository.GetCareersAsync();
        var careerAcronym = careers.First().Acronym.Value;
        var newContentJson = $@"{{
            ""ContentName"": ""New Content"",
            ""Description"": ""A new content for testing"",
            ""CareerAcronym"": ""{careerAcronym}"",
            ""ContentType"": ""tecnológico""
        }}";

        // Act
        var result = await _repository.PostCreateContentAsync(newContentJson);

        // Assert
        result.Should().BeTrue(because: "Given the correct json file to create a content, should be created.");
        var contents = await _repository.GetContentsAsync(careerAcronym);
        contents.Should().Contain(c => c.ContentName.Value == "New Content");
    }

    [Fact]
    public async Task DeleteContentAsync_ShouldRemoveContent()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();
        var careers = await _repository.GetCareersAsync();
        var careerAcronym = careers.First().Acronym.Value;
        var contents = await _repository.GetContentsAsync(careerAcronym);
        var contentToDelete = contents.First();

        // Act
        var result = await _repository.DeleteContentAsync(contentToDelete.Id);

        // Assert
        result.Should().BeTrue(because: "Should delete the content with the id given.");
        var updatedContents = await _repository.GetContentsAsync(careerAcronym);
        updatedContents.Should().NotContain(c => c.Id == contentToDelete.Id);
    }

    [Fact]
    public async Task GetScholarshipBudgetAsync_ShouldCalculateBudget()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();
        var careers = await _repository.GetCareersAsync();
        var careerAcronym = careers.First().Acronym.Value;

        // Act
        var budget = await _repository.GetScholarshipBudgetAsync(careerAcronym);

        // Assert
        budget.Should().BeGreaterThan(0, because: "the career has valuable content which means it has a budget superior to 0.");
    }

    [Fact]
    public async Task PostModifyContentAsync_ShouldUpdateExistingContent()
    {
        // Arrange
        await _fixture.SeedDatabaseAsync();
        var careers = await _repository.GetCareersAsync();
        var careerAcronym = careers.First().Acronym.Value;
        var contents = await _repository.GetContentsAsync(careerAcronym);
        var contentToModify = contents.First();
        var modifiedContentJson = $@"[{{
            ""Id"": ""{contentToModify.Id}"",
            ""ContentName"": ""Modified Content"",
            ""Description"": ""This content has been modified"",
            ""CareerAcronym"": ""{careerAcronym}"",
            ""ContentType"": ""ambiental""
        }}]";

        // Act
        var result = await _repository.PostModifyContentAsync(modifiedContentJson);

        // Assert
        result.Should().BeTrue(because: "Given a correct json file, the stored contetn should be correctly modified.");
        var updatedContents = await _repository.GetContentsAsync(careerAcronym);
        var modifiedContent = updatedContents.FirstOrDefault(c => c.Id == contentToModify.Id);
        modifiedContent.Should().NotBeNull();
        modifiedContent!.ContentName.Value.Should().Be("Modified Content");
        modifiedContent.ContentType.Value.Should().Be("ambiental");
    }



}