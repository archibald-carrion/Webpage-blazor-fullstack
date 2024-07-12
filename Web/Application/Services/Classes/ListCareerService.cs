using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

/// <summary>
/// ListCareerService class inherits from IListCareerService
/// </summary>
internal class ListCareerService : IListCareerService
{
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="careerRepository"></param>
    public ListCareerService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    /// <summary>
    /// GetCareersAsync method
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Domain.Career.Entities.Career>> GetCareersAsync()
    {
        return _careerRepository.GetCareersAsync();
    }
}
