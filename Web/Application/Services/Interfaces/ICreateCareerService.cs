namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;

/// <summary>
/// ICreateCareerService interface
/// </summary>
public interface ICreateCareerService
{
    public Task<bool> PostCreateCareerAsync(string input);


    public Task<Double> GetScholarshipBudgetAsync(string careerAcronym);
}
