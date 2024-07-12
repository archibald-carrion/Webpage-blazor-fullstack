using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Classes;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;


namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application;

public static class ApplicationLayerDependencyInjection
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IListCareerService, ListCareerService>();
        services.AddScoped<ICreateCareerService, CreateCareerService>();
        services.AddScoped<IListContentsService, ListContentsService>();
        services.AddScoped<ICreateContentService, CreateContentsService>();
        return services;
    }



}
