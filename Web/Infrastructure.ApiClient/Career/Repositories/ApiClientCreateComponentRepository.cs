using Microsoft.Kiota.Abstractions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.CreateContent;
using static UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.DeleteContent.DeleteContentRequestBuilder;
using static UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.ModifyContent.ModifyContentRequestBuilder;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Repositories;

internal class ApiClientCreateComponentRepository : ICreateContentService
{
    private readonly ApiClientKiota _apiClient;

    /// <summary>
    /// ApiClientCreateCareerRepository class
    /// </summary>
    /// <param name="apiClient"></param>
    public ApiClientCreateComponentRepository(ApiClientKiota apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<bool> DeleteContentAsync(Guid contentId)
    {
        // print statement to say we entered function
        Console.WriteLine("Entered DeleteContentAsync");
        try
        {
            // Configurar el requestConfiguration con el input
            var requestConfiguration = new Action<RequestConfiguration<DeleteContentRequestBuilderDeleteQueryParameters>>(config =>
            {
                config.QueryParameters = new DeleteContentRequestBuilderDeleteQueryParameters
                {
                    ContentId = contentId
                };
            });
            //var requestConfiguration = new Action<RequestConfiguration<CreateContentRequestBuilder.CreateContentRequestBuilderGetQueryParameters>>(config =>
            //{
            //    config.QueryParameters = new CreateContentRequestBuilder.CreateContentRequestBuilderGetQueryParameters
            //    {
            //        Input = contentId.ToString()
            //    };
            //});

            // Make the API call
            await _apiClient.DeleteContent.DeleteAsync(requestConfiguration);

            Console.WriteLine("Delete content seems to work jeje");
            return true;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Error deleting content: {ex.Message}");
            return false;
        }
    }


    /// <summary>
    /// PostCreateContentAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateContentAsync(string input)
    {
        Console.WriteLine(input);
        Console.WriteLine("Linea 46");
        try
        {
            // Configurar el requestConfiguration con el input
            var requestConfiguration = new Action<RequestConfiguration<CreateContentRequestBuilder.CreateContentRequestBuilderGetQueryParameters>>(config =>
            {
                config.QueryParameters = new CreateContentRequestBuilder.CreateContentRequestBuilderGetQueryParameters
                {
                    Input = input
                };
            });

            Console.WriteLine("Linea 59");
            Console.WriteLine(input);

            var output = await _apiClient.CreateContent.GetAsync(requestConfiguration);
            return true;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción aquí
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> PostModifyContentAsync(string input)
    {
        // print statement to say we entered function
        Console.WriteLine("Entered PostModifyContentAsync");
        try
        {
            // Configurar el requestConfiguration con el input
            var requestConfiguration = new Action<RequestConfiguration<ModifyContentRequestBuilderPostQueryParameters>>(config =>
            {
                config.QueryParameters = new ModifyContentRequestBuilderPostQueryParameters
                {
                    Input = input
                };
            });

            // print the input
            Console.WriteLine(input);

            var output = await _apiClient.ModifyContent.PostAsync(requestConfiguration);

            Console.WriteLine("Modify content seems to work jeje");

            return true;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción aquí
            Console.WriteLine($"Error modifying content: {ex.Message}");
            return false;
        }
    }
}
