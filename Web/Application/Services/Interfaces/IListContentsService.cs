using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;

/// <summary>
/// IListContentsService interface
/// </summary>
public interface IListContentsService
{
    public Task<IEnumerable<Contents>> GetContentsAsync(string careerAcronym);
}
