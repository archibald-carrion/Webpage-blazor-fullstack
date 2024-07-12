using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Dtos;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Repositories
{

    /// <summary>
    /// ApiClientGetCareerRepository class inherits from IListCareerService
    /// </summary>
    internal class ApiClientGetCareerRepository : IListCareerService
    {
        private readonly ApiClientKiota _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientLearningSpaceRepository"/> class with the specified API client.
        /// </summary>
        /// <param name="apiClient">The API client used to communicate with the backend.</param>
        public ApiClientGetCareerRepository(ApiClientKiota apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Gets the careers asynchronously.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<Domain.Career.Entities.Career>> GetCareersAsync()
        {
            var getCareerDtos = await _apiClient.ListCareer.GetAsync();
            var careerEntitites = getCareerDtos?.Select(CareerDtoMapper.ToEntity)
                ?? throw new NullReferenceException();

            return careerEntitites;
        }
    }
}
