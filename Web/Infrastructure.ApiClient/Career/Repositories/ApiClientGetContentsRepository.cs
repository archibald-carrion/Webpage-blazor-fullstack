using Microsoft.Kiota.Abstractions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Dtos;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client;
using static UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.ListContents.ListContentsRequestBuilder;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Repositories;

/// <summary>
/// ApiClientGetContentsRepository class inherits from IListContentsService
/// </summary>
public class ApiClientGetContentsRepository : IListContentsService
{
    private readonly ApiClientKiota _apiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClientLearningSpaceRepository"/> class with the specified API client.
    /// </summary>
    /// <param name="apiClient">The API client used to communicate with the backend.</param>
    public ApiClientGetContentsRepository(ApiClientKiota apiClient)
    {
        _apiClient = apiClient;
    }


    /// <summary>
    /// Gets the contents asynchronously.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IEnumerable<Domain.Career.Entities.Contents>> GetContentsAsync(string input)
    {
        var requestConfiguration = new Action<RequestConfiguration<ListContentsRequestBuilderGetQueryParameters>>(config =>
        {
            config.QueryParameters = new ListContentsRequestBuilderGetQueryParameters
            {
                Input = input
            };
        });
        Console.WriteLine(requestConfiguration.ToString());
        var getContentsDtos = await _apiClient.ListContents.GetAsync(requestConfiguration);
        var contentsEntitites = getContentsDtos?.Select(ContentsDtoMapperz.ToEntity)
            ?? throw new NullReferenceException();
        return contentsEntitites;
    }
}
