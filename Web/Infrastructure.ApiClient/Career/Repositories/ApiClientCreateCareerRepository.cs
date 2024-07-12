using Microsoft.Kiota.Abstractions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.CreateCareer;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Repositories;

/// <summary>
/// ApiClientCreateCareerRepository class
/// </summary>
public class ApiClientCreateCareerRepository : ICreateCareerService
{
    private readonly ApiClientKiota _apiClient;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="apiClient"></param>
    public ApiClientCreateCareerRepository(ApiClientKiota apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<double> GetScholarshipBudgetAsync(string careerAcronym)
    {
        try
        {
            // Configure the request with the career acronym
            var requestConfiguration = new Action<RequestConfiguration<Client.GetSchoolarshipBudget.GetSchoolarshipBudgetRequestBuilder.GetSchoolarshipBudgetRequestBuilderGetQueryParameters>>(config =>
            {
                config.QueryParameters.CareerAcronym = careerAcronym;
                //Add("careerAcronym", careerAcronym);
            });

            // Make the API call with the configured request
            var response = await _apiClient.GetSchoolarshipBudget.GetAsync(requestConfiguration);

            Console.WriteLine("budget: " + response.Value);

            // Assuming the API returns a double value for the budget
            if (response != null)
            {
                return response.Value;
            }
            else
            {
                throw new Exception("No scholarship budget data received from the API.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error retrieving scholarship budget: {ex.Message}");
            // Rethrow the exception or return a default value
            throw;
            // or
            // return 0.0;
        }
    }

    /// <summary>
    /// PostCreateCareerAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateCareerAsync(string input)
    {
        try
        {
            // Configurar el requestConfiguration con el input
            var requestConfiguration = new Action<RequestConfiguration<CreateCareerRequestBuilder.CreateCareerRequestBuilderGetQueryParameters>>(config =>
            {
                config.QueryParameters = new CreateCareerRequestBuilder.CreateCareerRequestBuilderGetQueryParameters
                {
                    Input = input
                };
            });

            Console.WriteLine("Linea 59");
            Console.WriteLine(input);

            var output = await _apiClient.CreateCareer.GetAsync(requestConfiguration);
            return true;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción aquí
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
