using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

/// <summary>
/// CreateContentsService class inherits from ICreateContentService
/// </summary>
internal class CreateContentsService : ICreateContentService
{
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="careerRepository"></param>
    public CreateContentsService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task<bool> DeleteContentAsync(Guid contentId)
    {
        var output = _careerRepository.DeleteContentAsync(contentId);
        return await output;
    }

    /// <summary>
    /// PostCreateContentAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateContentAsync(string input)
    {
        var output = _careerRepository.PostCreateContentAsync(input);
        return await output;
    }

    public async Task<bool> PostModifyContentAsync(string input)
    {
        var output = _careerRepository.PostModifyContentAsync(input);
        return await output;
    }
}
