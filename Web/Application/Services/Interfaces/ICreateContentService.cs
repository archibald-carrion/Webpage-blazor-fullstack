namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;

/// <summary>
/// ICreateContentService interface
/// </summary>
public interface ICreateContentService
{
    public Task<bool> PostCreateContentAsync(string input);
    public Task<bool> DeleteContentAsync(Guid contentId);

    public Task<bool> PostModifyContentAsync(string input);

}
