namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;

/// <summary>
/// IDeleteContentService interface
/// </summary>
public interface IDeleteContentService
{
    public Task<bool> DeleteContentAsync(Guid contentId);
}
