using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

/// <summary>
/// ListContentsService class inherits from IListContentsService
/// </summary>
internal class ListContentsService : IListContentsService
{
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="careerRepository"></param>
    public ListContentsService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    /// <summary>
    /// GetContentsAsync method
    /// </summary>
    /// <param name="careerAcronym"></param>
    /// <returns></returns>
    public Task<IEnumerable<Domain.Career.Entities.Contents>> GetContentsAsync(string careerAcronym)
    {
        return _careerRepository.GetContentsAsync(careerAcronym);
    }
}
