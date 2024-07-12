using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

/// <summary>
/// ICareerRepository interface
/// </summary>
public interface ICareerRepository
{
    public Task<IEnumerable<Domain.Career.Entities.Career>> GetCareersAsync();

    public Task<bool> PostCreateCareerAsync(string input);

    public Task<IEnumerable<Domain.Career.Entities.Contents>> GetContentsAsync(string buildingAcronym);

    public Task<bool> PostCreateContentAsync(string input);

    public Task<bool> DeleteContentAsync(System.Guid contentId);

    public Task<double> GetScholarshipBudgetAsync(string careerAcronym);

    public Task<bool> PostModifyContentAsync(string input);
}
