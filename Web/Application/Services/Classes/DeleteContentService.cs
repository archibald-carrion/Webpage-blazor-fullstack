using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;

/// <summary>
/// DeleteContentService class inherits from IDeleteContentService
/// </summary>
internal class DeleteContentService : IDeleteContentService
{
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="careerRepository"></param>
    public DeleteContentService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    /// <summary>
    /// DeleteContentAsync method
    /// </summary>
    /// <param name="contentId"></param>
    /// <returns></returns>


    public async Task<bool> DeleteContentAsync(Guid contentId)
    {
        return await _careerRepository.DeleteContentAsync(contentId);
    }

    //public Task<bool> DeleteContentAsync(Guid contentId)
    //{
    //    throw new NotImplementedException();
    //}
}
