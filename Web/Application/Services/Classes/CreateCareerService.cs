using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

/// <summary>
/// CreateCareerService class inherits from ICreateCareerService
/// </summary>
internal class CreateCareerService : ICreateCareerService
{
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="careerRepository"></param>
    public CreateCareerService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task<double> GetScholarshipBudgetAsync(string careerAcronym)
    {
        var output = _careerRepository.GetScholarshipBudgetAsync(careerAcronym);
        return await output;
    }


    /// <summary>
    /// PostCreateCareerAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateCareerAsync(string input)
    {
        var output = _careerRepository.PostCreateCareerAsync(input);
        return await output;
    }
}
