using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;

/// <summary>
/// IListCareerService interface
/// </summary>
public interface IListCareerService
{
    public Task<IEnumerable<Career>> GetCareersAsync();
}
