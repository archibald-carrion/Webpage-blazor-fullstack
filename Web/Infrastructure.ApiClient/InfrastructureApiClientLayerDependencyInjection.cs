using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Repositories;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client;


namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient;

/// <summary>
/// InfrastructureApiClientLayerDependencyInjection class
/// </summary>
public static class InfrastructureApiClientLayerDependencyInjection
{

    /// <summary>
    /// AddInfrastructureApiClientLayerServices method
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureApiClientLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Step 1: Configure authentication for the ApiClient.
        services.AddScoped<IAuthenticationProvider, AnonymousAuthenticationProvider>();
        // Step 2: Configure the request adapter.
        services.AddScoped<IRequestAdapter, HttpClientRequestAdapter>(serviceProvider =>
        {
            var adapter = new HttpClientRequestAdapter(
                serviceProvider.GetRequiredService<IAuthenticationProvider>(),
                httpClient: serviceProvider.GetRequiredService<HttpClient>());

            // Step 3: Define the base URL.
            adapter.BaseUrl = configuration["DownstreamApi:BaseUrl"];

            return adapter;
        });
        // Step 4: Register the ApiClient.
        services.AddScoped<ApiClientKiota>();
        services.AddScoped<IListCareerService, ApiClientGetCareerRepository>();
        services.AddScoped<IListContentsService, ApiClientGetContentsRepository>();
        services.AddScoped<ICreateCareerService, ApiClientCreateCareerRepository>();
        services.AddScoped<ICreateContentService, ApiClientCreateComponentRepository>();


        return services;
    }
}